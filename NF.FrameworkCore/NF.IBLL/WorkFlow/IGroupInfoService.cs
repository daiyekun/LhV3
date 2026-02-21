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
    /// 组
    /// </summary>
    public partial interface IGroupInfoService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        GroupInfoViewDTO ShowView(int Id);
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns></returns>
        LayPageInfo<GroupInfoDTO> GetList(PageInfo<GroupInfo> pageInfo, Expression<Func<GroupInfo, bool>> whereLambda);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段对象</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 选择组
        /// </summary>
        /// <param name="pageInfo">分页信息</param>
        /// <param name="whereLambda">where条件表达式</param>
        /// <returns></returns>
        LayPageInfo<SelectGroupList> SelectGroupList(PageInfo<GroupInfo> pageInfo, Expression<Func<GroupInfo, bool>> whereLambda);

        /// <summary>
        /// 选择组用户
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        LayPageInfo<SelectGroupusinfoList> SelectGroupUsinfoList(PageInfo<GroupInfo> pageInfo, Expression<Func<GroupInfo, bool>> whereLambda,int Zid);

    }
}
