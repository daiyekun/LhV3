using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 角色功能数据权限分配
    /// </summary>
   public partial  interface IRolePermissionService
    {
        IEnumerable<RolePermission> AddPermission(IEnumerable<RolePermission> rolePermissions);
    }
}
