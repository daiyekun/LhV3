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
    /// 组用户列表
    /// </summary>
    public partial  interface IGroupUserService
    {
        /// <summary>
        /// 查询用户信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<UserInforDTO> GetListUser(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda);

        /// <summary>
        /// 用户用户组中间表数据
        /// </summary>
        /// <param name="userIds">用户ID</param>
        /// <param name="groupId">组ID</param>
        /// <returns></returns>
        int Delete(string userIds, int groupId);
    }
}
