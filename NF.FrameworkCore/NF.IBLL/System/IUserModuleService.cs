using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 用户菜单
    /// </summary>
   public partial interface IUserModuleService
    {
        /// <summary>
        /// 保存用户所有菜单信息
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <param name="roleModules">用户菜单集合</param>
        /// <returns>添加的对象集合</returns>
        IEnumerable<UserModule> SaveUserModels(int Id, IList<UserModule> userModules);
        /// <summary>
        /// 查询用户模块集合
        /// </summary>
        /// <param name="whereLambda">Where条件</param>
        /// <returns>返回用户模块集合</returns>
        IList<UserModule> GetUserModules(Expression<Func<UserModule, bool>> whereLambda);
    }
}
