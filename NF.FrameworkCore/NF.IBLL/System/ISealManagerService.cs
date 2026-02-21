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
    /// 印章管理
    /// </summary>
    public partial interface ISealManagerService
    {
        /// <summary>
        /// 查询用户信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>

        LayPageInfo<SealManagerViewDTO> GetList<s>(PageInfo<SealManager> pageInfo, Expression<Func<SealManager, bool>> whereLambda, Expression<Func<SealManager, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        SealManagerViewDTO ShowView(int Id);
        /// <summary>
        /// 删除印章
        /// </summary>
        /// <param name="Ids">删除IDs</param>
        /// <returns></returns>
        int Delete(string Ids);
        /// <summary>
        /// 修改字段值
        /// </summary>
        /// <param name="info">修改字段新</param>
        /// <returns>返回受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);

        LayPageInfo<SealManagerViewDTO> GetSelectList<s>(PageInfo<SealManager> pageInfo, Expression<Func<SealManager, bool>> whereLambda, Expression<Func<SealManager, s>> orderbyLambda, bool isAsc);




    }
}