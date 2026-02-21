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
    /// 标的历史
    /// </summary>
   public partial interface IContSubjectMatterHistoryService
    {
        /// <summary>
        /// 查询标的历史列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ContSubjectMatterHistoryViewDTO> GetList<s>(PageInfo<ContSubjectMatterHistory> pageInfo, Expression<Func<ContSubjectMatterHistory, bool>> whereLambda, Expression<Func<ContSubjectMatterHistory, s>> orderbyLambda, bool isAsc);
    }
}
