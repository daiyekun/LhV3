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
    /// 系统功能点
    /// </summary>
   public partial interface ISysFunctionService
    {
        /// <summary>
        /// 查询列表并分页
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<SysFunctionDTO> GetList(PageInfo<SysFunction> pageInfo, Expression<Func<SysFunction, bool>> whereLambda);
    }
}
