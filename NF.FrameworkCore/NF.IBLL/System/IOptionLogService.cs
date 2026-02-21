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
    /// 操作日志-审计日志
    /// </summary>
    public partial interface IOptionLogService
    {
        /// <summary>
        /// 查询操作日志分页
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<OptionLogDTO> GetList(PageInfo<OptionLog> pageInfo, Expression<Func<OptionLog, bool>> whereLambda);
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="Ids">修改数据Ids</param>
        /// <returns>受影响行数</returns>
        int UpdateState(string Ids);
    }
}
