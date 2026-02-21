using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Extend.Enums;
using NF.Common.Extend;
using Microsoft.EntityFrameworkCore;
using NF.IBLL;
using NF.ViewModel;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 发票
    /// </summary>
    /// 
    public partial class APPInvoice : BaseService<ContInvoice>, IAPPInvoice
    {
        private DbSet<ContInvoice> _APPInvoice = null;
        public APPInvoice(DbContext dbContext)
           : base(dbContext)
        {
            _APPInvoice = base.Db.Set<ContInvoice>();
        }

        /// <summary>
        /// 查询发票大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public APPLayPageInfo<APPContInvoice> APPGetMainList<s>(Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc, int start, int limit)
        {
            //var tempquery = Db.Set<ContInvoice>().AsTracking().Where<ContInvoice>(whereLambda.Compile()).AsQueryable();
            var tempquery = Db.Set<ContInvoice>().Include(a => a.Cont).ThenInclude(a => a.Comp).AsTracking()
                  .Where<ContInvoice>(whereLambda.Compile()).AsQueryable();
            //pageInfo.TotalCount = tempquery.Count();
            //var tempquery = Db.Set<ContInvoice>().AsTracking().Where<ContInvoice>(whereLambda.Compile()).AsQueryable();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            InType = a.InType,
                            ContId = a.ContId,
                            ContName = a.Cont.Name,
                            ContCode = a.Cont.Code,
                            CompName = a.Cont.Comp.Name,
                            CompId = a.Cont.CompId,
                            DeptId = a.Cont.DeptId,
                            ContTypeId = a.Cont.ContTypeId,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            InCode = a.InCode,
                            AmountMoney = a.AmountMoney,
                            InState = a.InState,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            Remark = a.Remark,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            FinanceType = a.Cont.FinanceType,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItem = a.MakeOutDateTime,
                            MakeOutDateTime =a.MakeOutDateTime,
                            CreateUserid=a.CreateUserId,
                            //ConfirmDateTIme=a.ConfirmDateTIme
                        };
            var local = from a in query.AsEnumerable()
                        select new APPContInvoice
                        {

                            Id = a.Id,
                            ContractnId=a.ContId,//合同ID
                            ContractnName = a.ContName,//合同名称
                            contractNO = a.ContCode,//合同编号
                            CompayName = a.CompName,//客户名称
                            FapiaoNO = a.InCode==null?"": a.InCode,//发票号
                            AmountMoney = a.AmountMoney,//金额
                            MakeOutDateTime=a.MakeOutDateTime,//开票日期
                            //ConfirmDateTIme=a.ConfirmDateTIme,//确认时间
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                          //  RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            FapiaoState = EmunUtility.GetDesc(typeof(InvoiceStateEnum), a.InState ?? -1),//发票状态
                            FapiaoType = this.Db.GetRedisDataDictionaryValue(a.InType ?? 0, DataDictionaryEnum.InvoiceType),

                            //DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId??0),
                           // RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                        };
            int totalCount = local.Count();
            int pageNum = start / limit;//共有页数 
            var result = local.Skip(limit * pageNum).Take(limit).ToList();
            return new APPLayPageInfo<APPContInvoice>()
            {
                totalCount = totalCount,
                items = result,


            };


        }

        /// <summary>
        /// 根据合同ID查询合同基本信息
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        public APPContInvoice APPfinanceInvoiceDert(int Id)
        {
            var query = from a in _APPInvoice .AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            InType = a.InType,
                            ContId = a.ContId,
                            ContName = a.Cont.Name,
                            ContCode = a.Cont.Code,
                            CompName = a.Cont.Comp.Name,
                            CompId = a.Cont.CompId,
                            DeptId = a.Cont.DeptId,
                            ContTypeId = a.Cont.ContTypeId,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            InCode = a.InCode,
                            AmountMoney = a.AmountMoney,
                            InState = a.InState,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            Remark = a.Remark,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            FinanceType = a.Cont.FinanceType,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItem = a.MakeOutDateTime,
                            MakeOutDateTime = a.MakeOutDateTime,
                            CreateUserid = a.CreateUserId,

                        };
            var local = from a in query.AsEnumerable()
                        select new APPContInvoice
                        {
                            Id = a.Id,
                            ContractnId = a.ContId,//合同ID
                            ContractnName = a.ContName,//合同名称
                            contractNO = a.ContCode,//合同编号
                            CompayName = a.CompName,//客户名称
                            FapiaoNO = a.InCode,//发票号
                            AmountMoney = a.AmountMoney,//金额
                            MakeOutDateTime = a.MakeOutDateTime,//开票日期
                                                                //ConfirmDateTIme=a.ConfirmDateTIme,//确认时间
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //  RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                           // CreateUserName = RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            FapiaoState = EmunUtility.GetDesc(typeof(InvoiceStateEnum), a.InState ?? -1),//发票状态
                            //FapiaoType = DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                            //ConfirmUserName = RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人

                            FapiaoType = this.Db.GetRedisDataDictionaryValue(a.InType ?? 0, DataDictionaryEnum.InvoiceType),

                            //DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? 0),
                            // RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                        };
            return local.FirstOrDefault();
        }

        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContractInfoViewDTO APPfinanceInvoiceCONT(int Id) 
                                                                                                                           
        {
            var CONtractID = Db.Set<ContInvoice>().Where(p => p.Id == Id).FirstOrDefault();

                var query = from a in Db.Set<ContractInfo>().AsNoTracking()
                            where a.Id == CONtractID.ContId
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
                                SumContName = a.SumCont.Name,//总包合同
                                SumContId = a.SumContId,
                                //htwcje = GetHtWcJe(a.Id),//实际资金已确认
                                //fpje=GetFpJe(a.Id),//发票已确认金额


                            };
                var local = from a in query.AsEnumerable()
                            select new ContractInfoViewDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Code = a.Code,
                                OtherCode = a.OtherCode,
                                ContSourceId = a.ContSourceId,
                                ContTypeId = a.ContTypeId,
                                IsFramework = a.IsFramework,
                                //如果不是String.修改就得手动绑定Radio
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
                                //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                                                                                                                                //合同来源
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
                                PrincipalUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                                //RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? 0),//负责人
                                DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),

                              //  RedisValueUtility.GetDeptName(a.DeptId ?? -2),
                                MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),
                                //RedisValueUtility.GetDeptName(a.MainDeptId ?? -2),
                                StateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                                Comp3Name = a.Comp3Name,
                                Comp4Name = a.Comp4Name,
                                CompId3 = a.CompId3,
                                CompId4 = a.CompId4,
                                FinanceTerms = a.FinanceTerms,//资金条款
                                PerformanceDateTime = a.PerformanceDateTime,
                                SumContName = a.SumContName,
                                SumContId = a.SumContId,//总包ID。修改时绑定
                            };
                return local.FirstOrDefault();
             

            
        }



    }
}
