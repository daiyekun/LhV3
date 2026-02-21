using NF.Model.Extend;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 权限接口
    /// </summary>
   public partial interface ISysPermissionModelService
    {
        /// <summary>
        /// 客户列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funCode">功能状态码</param>
        /// <returns>客户权限表达式</returns>
        Expression<Func<T, bool>> GetListPermissionExpression<T>(string funCode, int userId, int deptId = 0) where T : ICreateUser, IPrincipalUser;
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="funcCode">权限标识</param>
        /// <param name="userId">用户ID</param>
        /// <returns>true/False</returns>
       // bool GetAddPermission(string funcCode, int userId);
    }
}
