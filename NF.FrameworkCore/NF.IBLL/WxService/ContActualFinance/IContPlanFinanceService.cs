using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{
    public partial interface IContPlanFinanceService
    {
        /// <summary>
        /// 计划资金核销表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="ActId">实际资金ID</param>
        /// <returns></returns>
        LayPageInfo<WxContPlanFinance> WxGetPlanCheckList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc, int ActId);
        /// <summary>
        /// 查询相关列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="ActId">实际资金ID</param>
        /// <returns></returns>
        LayPageInfo<WxContPlanFinanceView> GetWxListSecod<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc);

    }
}
