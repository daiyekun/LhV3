using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
    public partial class ContPlanFinanceService
    {
        public LayPageInfo<WxContPlanFinance> WxGetPlanCheckList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc, int ActId)
        {
            var plancklist = this.Db.Set<PlanFinnCheck>().AsNoTracking().Where(a => a.ActualFinanceId == ActId && a.IsDelete == 0).ToList();
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            //PlanCompleteDateTime = a.PlanCompleteDateTime,
                            //SettlementModes = a.SettlementModes,//结算方式
                            ConfirmedAmount = a.ConfirmedAmount,//已完成
                        };
            var local = from a in query.AsEnumerable()
                        select new WxContPlanFinance
                        {
                            Id = a.Id,
                            Name = a.Name,
                            AmountMoney = a.AmountMoney,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            ConfirmedAmount = a.ConfirmedAmount,
                            ConfirmedAmountThod = a.ConfirmedAmount.ThousandsSeparator(),
                            SurplusAmount = (a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0),
                            SurplusAmountThod = ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)).ThousandsSeparator(),
                            CheckAmount = plancklist.Where(p => p.PlanFinanceId == a.Id).Any() ? plancklist.Where(p => p.PlanFinanceId == a.Id).FirstOrDefault().AmountMoney : 0,
                            CheckAmountThod = (plancklist.Where(p => p.PlanFinanceId == a.Id).Any() ? plancklist.Where(p => p.PlanFinanceId == a.Id).FirstOrDefault().AmountMoney : 0).ThousandsSeparator(),
                            SyPlanAmountThod = ((a.AmountMoney ?? 0) - (this.Db.Set<PlanFinnCheck>().Where(p => p.PlanFinanceId == a.Id && p.IsDelete != 1).Sum(p => (decimal?)p.AmountMoney) ?? 0)).ThousandsSeparator()
                        };
            return new LayPageInfo<WxContPlanFinance>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }

        public LayPageInfo<WxContPlanFinanceView> GetWxListSecod<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc)
        {
            //var plancklist = this.Db.Set<PlanFinnCheck>().AsNoTracking().Where(a => a.ActualFinanceId == ActId && a.IsDelete == 0).ToList();
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
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
                        select new WxContPlanFinanceView
                        {
                            Id = a.Id,
                            Name = a.Name,
                            AmountMoney = a.AmountMoney,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            SettlModelName = this.Db.GetRedisDataDictionaryValue(a.SettlementModes ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementModes, DataDictionaryEnum.SettlModes),
                            ConfirmedAmountThod = a.ConfirmedAmount.ThousandsSeparator(),
                            SubAmountThod = a.SubAmount.ThousandsSeparator(),
                            CompRate = ((a.ConfirmedAmount ?? 0) / (a.AmountMoney ?? 0)).ConvertToPercent(),
                            BalanceThod = ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)).ThousandsSeparator(),
                            //计划资金-已核销金额（状态1（已确认））
                            //SurplusAmount =(a.AmountMoney??0)-(plancklist.Where(p => p.PlanFinanceId == a.Id&&p.ChkState==1).Sum(p=>(decimal?)p.AmountMoney)??0),
                            //SurplusAmountThod = ((a.AmountMoney??0) - (plancklist.Where(p => p.PlanFinanceId == a.Id && p.ChkState == 1).Sum(p => (decimal?)p.AmountMoney) ?? 0)).ThousandsSeparator(),
                            //CheckAmount= plancklist.Where(p=>p.PlanFinanceId==a.Id).Any()? plancklist.Where(p => p.PlanFinanceId == a.Id).FirstOrDefault().AmountMoney:0,
                            //CheckAmountThod = (plancklist.Where(p => p.PlanFinanceId == a.Id).Any() ? plancklist.Where(p => p.PlanFinanceId == a.Id).FirstOrDefault().AmountMoney : 0).ThousandsSeparator()
                        };
            return new LayPageInfo<WxContPlanFinanceView>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

    }
}
