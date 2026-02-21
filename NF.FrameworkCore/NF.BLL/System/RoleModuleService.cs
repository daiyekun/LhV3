using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 角色分配菜单实现类
    /// </summary>
  public partial  class RoleModuleService
    {
        /// <summary>
        /// 保存角色所有菜单信息
        /// </summary>
        /// <param name="Id">角色ID</param>
        /// <param name="roleModules">角色菜单集合</param>
        /// <returns></returns>
        public IEnumerable<RoleModule>  SaveRoleModels(int Id, IList<RoleModule> roleModules)
        {
            string sqlstr = "delete RoleModule where RoleId="+ Id;
            ExecuteSqlCommand(sqlstr);
            return Add(roleModules);

        }
        /// <summary>
        /// 根据用户获取所拥有的模块ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>模块ID集合</returns>
        public IEnumerable<int> GetModelIdsByUserId(int userId)
        {

            var query = Db.Set<UserRole>().Where(a => a.UserId == userId).Select(a=>a.RoleId).ToList();
            var Romd = Db.Set<RoleModule>().Where(a => query.Contains(a.RoleId)).Select(a => a.ModuleId);
            return Romd;


            //var query = from a in Db.Set<UserRole>().Where(a => a.UserId == userId)
            //            join b in Db.Set<RoleModule>()
            //            on a.RoleId equals b.RoleId into tempr
            //            from eci in tempr.DefaultIfEmpty()
            //            select new
            //            {
            //                eci.ModuleId
            //            };

            //return query.Select(a=>a.ModuleId).ToList();


          

        }
    }
}
