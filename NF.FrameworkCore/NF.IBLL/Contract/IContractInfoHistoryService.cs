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
    /// 合同历史
    /// </summary>
   public partial interface IContractInfoHistoryService
    {
        /// <summary>
        /// 合同变更记录
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<ContChangeInfo> GetChangeList<s>(PageInfo<ContractInfoHistory> pageInfo, Expression<Func<ContractInfoHistory, bool>> whereLambda, Expression<Func<ContractInfoHistory, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 选择的时候查看合同明细
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ContractInfoHistoryViewDTO SelChangeView(int Id);

        

    }
}
