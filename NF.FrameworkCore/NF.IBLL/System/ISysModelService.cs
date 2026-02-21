using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 菜单管理
    /// </summary>
   public partial interface ISysModelService
    {
        /// <summary>
        /// 查询模块列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<SysModelDTO> GetList(PageInfo<SysModel> pageInfo, Expression<Func<SysModel, bool>> whereLambda);
        /// <summary>
        /// 获取树
        /// </summary>
        /// <returns></returns>
        IList<TreeInfo> GetTree();
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<SysModelDTO> GetListAll();
        /// <summary>
        /// 判断输入值是否存在
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="inputValue">输入值</param>
        /// <param name="Id">修改时的ID</param>
        /// <returns></returns>
        bool CheckFieldValExist(string fieldName, string inputValue, int? Id);
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="info">保存信息实体</param>
        /// <returns>返回主信息</returns>
        SysModel SaveInfo(SysModel info);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        SysModelDTO ShowView(int Id);
        ///<summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段信息</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 获取Xtree树形菜单-用于菜单权限分配
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>Xtree数据结构</returns>
        IList<XTree> GetXtTree(IList<int> Ids);
        /// <summary>
        /// 根据权限获取左侧菜单
        /// </summary>
        /// <param name="mIds">菜单ID集合</param>
        /// <param name="userId">当前登录人ID，用于判断超级管理员</param>
        /// <returns>左侧菜单</returns>
        IList<LeftTree> GetLeftTree(IList<int> mIds,int userId);
        /// <summary>
        /// 返回功能菜单选择树
        /// </summary>
        /// <returns></returns>
        IList<TreeSelectInfo> GetModelTreeSelect();
        /// <summary>
        /// 权限分配树
        /// </summary>
        /// <param name="IsUser">0：用户权限，1角色权限</param>
        /// <param name="userIdorRoleId">用户或者角色ID</param>
        /// <param name="fpQx">是否是权限分配树</param>
        /// <returns></returns>
        IList<TreeInfo> GetTree(int IsUser, int userIdorRoleId, bool fpQx);
    }
}
