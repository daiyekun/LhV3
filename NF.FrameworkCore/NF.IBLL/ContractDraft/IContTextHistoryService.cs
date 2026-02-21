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
    /// 合同文本历史
    /// </summary>
  public partial  interface IContTextHistoryService
    {
        /// <summary>
        /// 查询合同文本列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ContTextHistoryViewDTO> GetList<s>(PageInfo<ContTextHistory> pageInfo, Expression<Func<ContTextHistory, bool>> whereLambda, Expression<Func<ContTextHistory, s>> orderbyLambda, bool isAsc);
    }
}
