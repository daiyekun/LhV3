using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NF.BLL
{
    /// <summary>
    /// 角色数据功能权限分配
    /// </summary>
   public partial class RolePermissionService
    {
        public IEnumerable<RolePermission> AddPermission(IEnumerable<RolePermission> rolePermissions)
        {
           var firstinfo= rolePermissions.FirstOrDefault();
            
            string sqlstr = "delete RolePermission where ModeId=" + firstinfo.ModeId+ " and  RoleId="+firstinfo.RoleId;
            ExecuteSqlCommand(sqlstr);
            return Add(rolePermissions);
        }
    }
}
