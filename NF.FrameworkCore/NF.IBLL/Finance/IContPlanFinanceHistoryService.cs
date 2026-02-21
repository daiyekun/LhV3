using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 计划资金历史
    /// </summary>
    public partial interface IContPlanFinanceHistoryService
    {
        /// <summary>
        /// 计划资金历史列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<ContPlanFinanceHistoryViewDTO> GetList<s>(PageInfo<ContPlanFinanceHistory> pageInfo, Expression<Func<ContPlanFinanceHistory, bool>> whereLambda, Expression<Func<ContPlanFinanceHistory, s>> orderbyLambda, bool isAsc);
    }
}
