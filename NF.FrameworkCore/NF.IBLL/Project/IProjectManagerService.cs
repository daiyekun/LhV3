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
    /// 项目管理
    /// </summary>
   public partial interface IProjectManagerService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        /// <summary>
        /// 查询信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ProjectManagerViewDTO> GetList<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda, Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc);
        LayPageInfo<ProjectManagerViewDTO> GetList1<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda, Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc);
        LayPageInfo<ProjectManagerViewDTO> GetList2<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda, Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc);
        LayPageInfo<ProjectManagerViewDTO> GetList3<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda, Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        DELETElist GetIsHt(string Ids);
        DELETElist GetIsHtlist(string Ids);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ProjectManagerViewDTO ShowView(int Id);
        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="currUserId">当前用户ID</param>
        int UpdateItems(int Id, int currUserId);
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        int ClearJunkItemData(int currUserId);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段对象</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="fields">修改字段集合</param>
        /// <returns>受影响行数</returns>
        int UpdateField(IList<UpdateFieldInfo> fields);
        /// <summary>
        /// 首页项目列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>返回分页数据JSON</returns>
        LayPageInfo<ProjectManagerConsoleDTO> GetConsoleProjList<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
            Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc);

        int XMLB(string name);

        int Ren(string name);

        /// <summary>
        /// 获取资金统计
        /// </summary>
        /// <param name="项目ID">合同对方ID</param>
        /// <returns>资金统计对象</returns>
        ProjFundCalcul GetFundStatistics(int projId);
        /// <summary>
        /// 查询相关合同
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<ProjContract> GetContsByProjId<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc);
        
        }
}
