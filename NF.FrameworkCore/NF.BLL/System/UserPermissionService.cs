using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 用户权限分配实现
    /// </summary>
   public partial  class UserPermissionService
    {
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="userPermissions">用户权限列表</param>
        /// <returns></returns>
        public IEnumerable<UserPermission> AddPermission(IEnumerable<UserPermission> userPermissions)
        {
            var firstinfo = userPermissions.FirstOrDefault();

            string sqlstr = "delete UserPermission where ModeId=" + firstinfo.ModeId + " and  UserId=" + firstinfo.UserId;
            ExecuteSqlCommand(sqlstr);
            return Add(userPermissions);
        }
    }
}
