using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 角色菜单分配
    /// </summary>
   public partial interface IRoleModuleService
    {
        /// <summary>
        /// 保存角色所有菜单信息
        /// </summary>
        /// <param name="Id">角色ID</param>
        /// <param name="roleModules">角色菜单集合</param>
        /// <returns>添加的对象集合</returns>
        IEnumerable<RoleModule> SaveRoleModels(int Id,IList<RoleModule> roleModules);
        /// <summary>
        /// 根据用户ID查询角色对应的模块ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        IEnumerable<int> GetModelIdsByUserId(int userId);
       

    }
}
