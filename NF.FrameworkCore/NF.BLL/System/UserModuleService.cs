using Microsoft.EntityFrameworkCore;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 用户菜单实现类
    /// </summary>
   public partial class UserModuleService
    {
        /// <summary>
        /// 保存用户所有菜单信息
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <param name="userModules">用户菜单集合</param>
        /// <returns></returns>
        public IEnumerable<UserModule> SaveUserModels(int Id, IList<UserModule> userModules)
        {
            string sqlstr = "delete UserModule where UserId=" + Id;
            ExecuteSqlCommand(sqlstr);
            return Add(userModules);

        }
        /// <summary>
        /// 查询返回用户模块集合
        /// </summary>
        /// <param name="whereLambda">Where条件</param>
        /// <returns>用户模块集合</returns>
        public IList<UserModule> GetUserModules(Expression<Func<UserModule, bool>> whereLambda)
        {
            return Db.Set<UserModule>().AsNoTracking().Where(whereLambda.Compile()).ToList();
        }
    }
}
