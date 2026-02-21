using Microsoft.EntityFrameworkCore;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NF.BLL
{
    public partial class finance_plan : BaseService<ContPlanFinance>, IAPPfinance_plan
    //  public  class finance_plan: BaseService<ContPlanFinance>, Ifinance_plan
    {
        private DbSet<ContPlanFinance> _ContPlanFinance = null;
        public finance_plan(DbContext dbContext)
           : base(dbContext)
        {
            _ContPlanFinance = Db.Set<ContPlanFinance>();
        }
        public finance_plan() { }
        public APPLayPageInfo<ContPlanFinanceListViewDTO> GetMainList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda,
Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc, int type, int start, int limit)
        {
          //  var tempquery = _ContPlanFinance.AsTracking().Where<ContPlanFinance>(whereLambda.Compile()).AsQueryable();
         //   var tempquery = _ContActualFinance.Include(a => a.Cont).ThenInclude(a => a.Comp).AsTracking()
         //.Where<ContActualFinance>(whereLambda.Compile()).AsQueryable();
            var tempquery = _ContPlanFinance.Include(a=>a.Cont).ThenInclude(a=>a.Comp).AsTracking()
                .Where<ContPlanFinance>(whereLambda.Compile()).AsQueryable();
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
                        where a.Ftype == type
                        select new
                        {
                            Id = a.Id,
                            ContId = a.ContId,
                            //  ContName = a.Cont.Name,      //合同名称
                            ContName = a.Cont.Name==null?"" : a.Cont.Name,
                            //  ContCode = a.Cont.Code,    // 合同编号 
                            ContCode = a.Cont.Code==null? "" :a.Cont.Code,
                            CompName = a.Cont.Comp.Name==null ? "":a.Cont.Comp.Name,
                         //   CompName = a.Cont.Comp.Name,
                            //客户名称
                            Name = a.Name,         // 计划资金名称
                            AmountMoney = a.AmountMoney,   // 金额 
                            PlanCompleteDateTime = a.PlanCompleteDateTime,    // 计划完成日期
                            ConfirmedAmount = a.ConfirmedAmount,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContPlanFinanceListViewDTO
                        {
                            Id = a.Id,
                            ContId = a.ContId,
                            ContName = a.ContName,      //合同名称
                            ContCode = a.ContCode,    // 合同编号 
                            CompName = a.CompName,        //客户名称
                            Name = a.Name,         // 计划资金名称
                            AmountMoney = a.AmountMoney,   // 金额 
                            PlanCompleteDateTime = a.PlanCompleteDateTime==null? DateTime.Now:a.PlanCompleteDateTime,    // 计划完成日期
                            BalanceThod = ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)).ThousandsSeparator(),//余额
                            ContActBl = GetWcBl(a.AmountMoney, a.ConfirmedAmount),//完成比例  // 完成比例
                        };
            int totalCount = local.Count(); //共有记录数
            int pageNum = start / limit;//共有页数 
            var result = local.Skip(limit * pageNum).Take(limit).ToList();
            return new APPLayPageInfo<ContPlanFinanceListViewDTO>()
            {
                totalCount = totalCount,
                items = result,
                //totalCount = totalCount,
                //items = local.ToList(),
            };
        }
        /// <summary>
        /// 计算比例
        /// </summary>
        /// <param name="amount">计划资金金额</param>
        /// <param name="wcje">完成金额</param>
        /// <returns>完成比例</returns>
        private string GetWcBl(decimal? amount, decimal? wcje)
        {
            return (((amount ?? 0) == 0 || (wcje ?? 0) == 0) ? 0 : ((wcje ?? 0) / (amount ?? 0))).ConvertToPercent();
        }
    }
}
