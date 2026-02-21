using Microsoft.EntityFrameworkCore;
using NF.BLL.Common;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Finance.Enums;
using NF.ViewModel.Models.MobileApp;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    public partial class APPFinance_actualList : BaseService<ContActualFinance>, IFinance_actual
    {
        private DbSet<ContractInfoViewDTO> _ContractInfoHistory = null;
        private DbSet<ContActualFinance> _ContActualFinance = null;
        public APPFinance_actualList(DbContext dbContext)
           : base(dbContext)
        {
            _ContActualFinance = base.Db.Set<ContActualFinance>();
            _ContractInfoHistory = base.Db.Set<ContractInfoViewDTO>();
        }
        public APPFinance_actualList() { }
        /// <summary>
        /// 查询计划资金大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public APPLayPageInfo<ContActualFinanceListViewDTO> GetMainList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda,
            Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc, int type, int start, int limit)
        {
            //var tempquery = _ContractInfoSet.Include(a => a.Project).Include(a => a.Comp).AsTracking()
            //    .Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
           // var tempquery = _ContActualFinance.Where<ContActualFinance>(whereLambda.Compile()).AsQueryable();
           var tempquery=_ContActualFinance.Include(a=>a.Cont).ThenInclude(a => a.Comp).AsTracking()
                  .Where<ContActualFinance>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            var query = from a in tempquery
                        where a.FinceType == type
                        select new
                        {
                            Id = a.Id,
                            ContId = a.ContId,//合同id
                            ContName = a.Cont.Name,// 合同名称app
                            ContCode = a.Cont.Code,// 合同编号app
                                                   //   CompName = a.Comp == null ? "" : a.Comp.Name,
                            CompName = a.Cont.Comp.Name==null?"": a.Cont.Comp.Name,//  客户名称app

                            //  CompName = a.Cont.Comp.Name,//  客户名称app
                             //COMP_ID = a.Cont.Comp.Id == null ? 0 : a.Cont.Comp.Id,//客户id
                           COMP_ID = a.Cont.Comp.Id,//客户id
                            AmountMoney = a.AmountMoney,//  金额 app
                            Astate = a.Astate,// 资金状态app
                            ConfirmUserId = a.ConfirmUserId,// 确认人 app
                            SettlementMethod = a.SettlementMethod,// 结算方式 app
                            ActualSettlementDate = a.ActualSettlementDate,//
                        };
            var local = from a in query.AsEnumerable()
                        select new ContActualFinanceListViewDTO
                        {
                            Id = a.Id,
                            ContId = a.ContId,// 合同id
                            CompId = a.COMP_ID,// 客户/供应商id app
                            ContCode = a.ContCode,// 合同编号app
                            ContName = a.ContName,// 合同名称app
                            CompName = a.CompName, //客户名称app
                            ActualSettlementDate = a.ActualSettlementDate,// 结算日期 app
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),// 金额 app
                            AstateDic = EmunUtility.GetDesc(typeof(ActFinanceStateEnum), a.Astate ?? -1), //资金状态app
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人 app
                            SettlementMethodDic = this.Db.GetRedisDataDictionaryValue(a.SettlementMethod ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementMethod, DataDictionaryEnum.SettlModes),//结算方式 app
                        };
            int totalCount = local.Count(); //共有记录数
            int pageNum = start / limit;//共有页数 
            var result = local.Skip(limit * pageNum).Take(limit).ToList();
            return new APPLayPageInfo<ContActualFinanceListViewDTO>()
            {
                totalCount = totalCount,
                items = result,
                //totalCount = totalCount,
                //items = local.ToList(),
            };
        }

        /// <summary>
        /// app根据合同id查询信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ContActualFinanceListViewDTO XJZJ_XQ(int Id)
        {
            var query = from a in _ContActualFinance.AsNoTracking()
                        where a.ContId == Id
                        select new
                        {
                            Id = a.Id, // 实际资金id

                            AmountMoney = a.AmountMoney,//  金额 app
                            VoucherNo = a.VoucherNo,// 票据号码
                            SettlementMethod = a.SettlementMethod,
                            ActualSettlementDate = a.ActualSettlementDate,
                            Bank = a.Bank,// 开户银行
                            Account = a.Account,// 账号
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            Remark = a.Remark,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContActualFinanceListViewDTO
                        {
                            Id = a.Id,// id
                            AmountMoney = a.AmountMoney,//  金额 app
                            VoucherNo = a.VoucherNo,// 票据号码
                            SettlementMethod = a.SettlementMethod,
                            SettlementMethodDic = this.Db.GetRedisDataDictionaryValue(a.SettlementMethod ?? 0, DataDictionaryEnum.SettlModes),

                            //DataDicUtility.GetDicValueToRedis(a.SettlementMethod, DataDictionaryEnum.SettlModes),// 结算方式
                            ActualSettlementDate = a.ActualSettlementDate,// 结算日期
                            Bank = a.Bank,// 开户银行
                            Account = a.Account,//账号
                            Reserve1 = a.Reserve1,//备注1
                            Reserve2 = a.Reserve2,//备注2
                            Remark = a.Remark,//备注
                        };
            return local.FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public APPFinance_actual XJZJ_HT(int Id)
        {
            var query = from a in Db.Set<ContractInfo>().AsNoTracking()
                        where a.Id == Id
                        // join 
                        //b in Db.Set< ContStatistics >().AsNoTracking()
                        // on a.Id equals b.ContId
                        // var query = from a in Db.Set<ContractInfoViewDTO>().AsNoTracking()
                        //where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            code = a.Code,
                            AmountMoney = a.AmountMoney,//合同金额/收票金额/付款金额
                            CompName = a.Comp.Name,
                            //HtWcJeThod = GetHtWcJe(a.Id),//完成金额
                            //FaPiaoThod = b.CompInAm//发票金额
                        };
            var local = from a in query.AsEnumerable()
                        select new APPFinance_actual
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.code,
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额/收票金额/付款金额
                            CompName = a.CompName,
                            HtWcJeThod = GetHtWcJe(a.Id).ToString(),
                            FaPiaoThod = GetFpJe(a.Id).ToString()
                        };
            return local.FirstOrDefault();
        }
        /// <summary>
        /// 合同完成金额
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <returns></returns>
        private decimal GetHtWcJe(int Id)
        {
            var info = Db.Set<ContStatistic>().Where(a => a.ContId == Id).FirstOrDefault();
            if (info != null)
                return info.CompActAm ?? 0;
            return 0;
        }
        /// <summary>
        ///APP 根据合同完成金额
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <returns></returns>
        private decimal GetFpJe(int Id)
        {
            var info = Db.Set<ContStatistic>().Where(a => a.ContId == Id).FirstOrDefault();
            if (info != null)
                return info.CompInAm ?? 0;
            return 0;
        }
        /// <summary>
        /// app根据合同id查询计划资金
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<APPContPlanFinance> XJZJ_JHZJ(int Id)
        {
            var query = from a in Db.Set<ContPlanFinance>().AsNoTracking()
                            // var query = from a in Db.Set<ContractInfoViewDTO>().AsNoTracking()
                            // var ss= a.Id == Id
                        where a.ContId == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            SettlementModes = a.SettlementModes,//结算方式
                            ConfirmedAmount = a.ConfirmedAmount,//已完成
                            SubAmount = a.SubAmount,//已提交
                            SurplusAmount = a.SurplusAmount,//可核销
                            CheckAmount = a.CheckAmount,//本次核销
   



                        };
            var local = from a in query.AsEnumerable()
                        select new APPContPlanFinance
                        {
                            Id = a.Id,
                            Name = a.Name,
                            AmountMoney = a.AmountMoney,
                            PlanCompleteDateTime = a.PlanCompleteDateTime==null? DateTime.Now: a.PlanCompleteDateTime,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            SettlModelName = this.Db.GetRedisDataDictionaryValue(a.SettlementModes ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementModes, DataDictionaryEnum.SettlModes),
                            ConfirmedAmountThod = a.ConfirmedAmount.ThousandsSeparator(),
                            SubAmountThod = a.SubAmount.ThousandsSeparator(),
                            CompRate = ((a.ConfirmedAmount ?? 0) / (a.AmountMoney ?? 0)).ConvertToPercent(),
                            BalanceThod = ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)).ThousandsSeparator(),

                         
                        };
            return local.ToList();



        }
    }
}
