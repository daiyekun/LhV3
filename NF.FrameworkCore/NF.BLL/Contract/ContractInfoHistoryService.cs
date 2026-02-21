using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel;
using NF.Common.Extend;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 合同历史
    /// </summary>
   public partial  class ContractInfoHistoryService
    {
       public  LayPageInfo<ContChangeInfo> GetChangeList<s>(PageInfo<ContractInfoHistory> pageInfo, Expression<Func<ContractInfoHistory, bool>> whereLambda, Expression<Func<ContractInfoHistory, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id=a.Id,
                            Name=a.Name,
                            ModificationRemark= a.ModificationRemark,
                            ModificationTimes=a.ModificationTimes,
                            ModifyUserId=a.ModifyUserId,
                            ModifyDateTime= a.ModifyDateTime,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContChangeInfo
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ChageDesc = a.ModificationRemark,
                            ChangeVersions= (a.ModificationTimes??0)==0?"原始合同":$"第{a.ModificationTimes}次变更",
                            ChangeDate = a.ModifyDateTime,
                            ChangePerson = this.Db.GetRedisUserFieldValue(a.ModifyUserId)
                            //RedisValueUtility.GetUserShowName(a.ModifyUserId),
                        };
            return new LayPageInfo<ContChangeInfo>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }
        /// <summary>
        /// 选择时查看合同明细
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContractInfoHistoryViewDTO SelChangeView(int Id)
        {
           
                var query = from a in _ContractInfoHistorySet.AsNoTracking()
                            where a.Id == Id
                            select new
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Code = a.Code,
                                OtherCode = a.OtherCode,
                                ContSourceId = a.ContSourceId,
                                ContTypeId = a.ContTypeId,
                                IsFramework = a.IsFramework,
                                ContDivision = a.ContDivision,
                                CompId = a.CompId,
                                CompName = a.Comp.Name,
                                ProjName = a.Project.Name,
                                AmountMoney = a.AmountMoney,
                                CurrencyId = a.CurrencyId,
                                CurrencyRate = a.CurrencyRate,
                                EstimateAmount = a.EstimateAmount,
                                AdvanceAmount = a.AdvanceAmount,
                                StampTax = a.StampTax,
                                CreateUserId = a.CreateUserId,
                                CreateDateTime = a.CreateDateTime,
                                SngnDateTime = a.SngnDateTime,
                                EffectiveDateTime = a.EffectiveDateTime,
                                PlanCompleteDateTime = a.PlanCompleteDateTime,
                                ActualCompleteDateTime = a.ActualCompleteDateTime,
                                DeptId = a.DeptId,
                                ProjectId = a.ProjectId,
                                ContState = a.ContState,
                                MainDeptId = a.MainDeptId,
                                Reserve1 = a.Reserve1,
                                Reserve2 = a.Reserve2,
                                Comp3Name = a.CompId3Navigation.Name,
                                Comp4Name = a.CompId4Navigation.Name,
                                CompId3 = a.CompId3,
                                CompId4 = a.CompId4,
                                PrincipalUserId = a.PrincipalUserId,
                                FinanceTerms = a.FinanceTerms,
                                PerformanceDateTime = a.PerformanceDateTime,
                                ModificationRemark=a.ModificationRemark,


                            };
                var local = from a in query.AsEnumerable()
                            select new ContractInfoHistoryViewDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Code = a.Code,
                                OtherCode = a.OtherCode,
                                ContSourceId = a.ContSourceId,
                                ContTypeId = a.ContTypeId,
                                IsFramework = a.IsFramework,
                                ContDivision = (a.ContDivision ?? 0).ToString(),
                                CompId = a.CompId,
                                AmountMoney = a.AmountMoney,
                                CurrencyId = a.CurrencyId,
                                CurrencyRate = a.CurrencyRate,
                                EstimateAmount = a.EstimateAmount,
                                AdvanceAmount = a.AdvanceAmount,
                                StampTax = a.StampTax,
                                CreateUserId = a.CreateUserId,
                                CreateDateTime = a.CreateDateTime,
                                SngnDateTime = a.SngnDateTime,
                                EffectiveDateTime = a.EffectiveDateTime,
                                PlanCompleteDateTime = a.PlanCompleteDateTime,
                                ActualCompleteDateTime = a.ActualCompleteDateTime,
                                DeptId = a.DeptId,
                                ProjectId = a.ProjectId,
                                ContState = a.ContState,
                                MainDeptId = a.MainDeptId,
                                Reserve1 = a.Reserve1,
                                Reserve2 = a.Reserve2,
                                //合同类别
                                ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                                //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别                                                                                        //合同来源
                                ContSName = this.Db.GetRedisDataDictionaryValue(a.ContSourceId ?? 0, DataDictionaryEnum.contSource),
                                //DataDicUtility.GetDicValueToRedis(a.ContSourceId, DataDictionaryEnum.contSource),
                                CompName = a.CompName,//合同对方
                                ProjName = a.ProjName,//项目名称
                                ContPro = EmunUtility.GetDesc(typeof(ContractProperty), a.IsFramework ?? 0),//合同属性
                                ContSum = a.ContDivision > 0 ? "是" : "否",
                                ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                                CurrencyName = RedisValueUtility.GetCurrencyName(a.CurrencyId), ///币种
                                Rate = a.CurrencyRate ?? 1,//汇率
                                EsAmountThod = a.EstimateAmount.ThousandsSeparator(),//预估金额
                                AdvAmountThod = a.AdvanceAmount.ThousandsSeparator(),//预收预付
                                StampTaxThod = a.StampTax.ThousandsSeparator(),//千分位
                                CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                                //RedisValueUtility.GetUserShowName(a.CreateUserId),//创建人
                                PrincipalUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                                //RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? 0),//负责人
                                DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
// RedisValueUtility.GetDeptName(a.DeptId ?? -2),
                                MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),
                                //RedisValueUtility.GetDeptName(a.MainDeptId ?? -2),
                                StateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                                Comp3Name = a.Comp3Name,
                                Comp4Name = a.Comp4Name,
                                CompId3 = a.CompId3,
                                CompId4 = a.CompId4,
                                FinanceTerms = a.FinanceTerms,//资金条款
                                PerformanceDateTime = a.PerformanceDateTime,
                                ChangeDesc=a.ModificationRemark,//变更说明

                            };

                return local.FirstOrDefault();



            

        }

        
    }
}
