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
    /// 角色
    /// </summary>
    public partial interface IRoleService
    {
        /// <summary>
        /// 查询用户信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<RoleDTO> GetList(PageInfo<Role> pageInfo, Expression<Func<Role, bool>> whereLambda);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        ///<summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段信息</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 保存系统角色
        /// </summary>
        /// <param name="info">角色信息</param>
        /// <returns>返回主信息</returns>
        Role SaveInfo(Role info);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        RoleDTO ShowView(int Id);
        /// <summary>
        /// 判断输入值是否存在
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="inputValue">输入值</param>
        /// <param name="Id">修改时的ID</param>
        /// <returns></returns>
        bool CheckFieldValExist(string fieldName, string inputValue, int? Id);
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        LayPageInfo<RoleDTO> GetListAll();

    }
}
