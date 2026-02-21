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
    /// 用户-角色-中间表操作
    /// </summary>
    public partial interface IUserRoleService
    {
        /// <summary>
        /// 查询用户信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<UserInforDTO> GetListUser(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda,int roleId);
        /// <summary>
        /// 用户用户角色中间表数据
        /// </summary>
        /// <param name="userIds">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        int Delete(string userIds, int roleId);

        IList<UserRole> GetListUserRole(Expression<Func<UserRole, bool>> whereLambda);
        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="userRoles">用户角色集合</param>
        /// <param name="uId">当前用户ID</param>
        /// <returns></returns>
        IEnumerable<UserRole> SetUserRole(IEnumerable<UserRole> userRoles,int uId);
    }
}
