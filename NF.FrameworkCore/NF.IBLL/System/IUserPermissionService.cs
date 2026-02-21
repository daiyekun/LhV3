using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 用户权限分配
    /// </summary>
   public partial interface IUserPermissionService
    {
        IEnumerable<UserPermission> AddPermission(IEnumerable<UserPermission> userPermissions);
       
    }
}
