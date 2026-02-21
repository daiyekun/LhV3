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
    /// 登录日志
    /// </summary>
   public partial  interface ILoginLogService
    {
        /// <summary>
        /// 查询登录日志分页
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<LoginLogDTO> GetList(PageInfo<LoginLog> pageInfo, Expression<Func<LoginLog, bool>> whereLambda);
    }
}
