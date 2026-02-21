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
    /// 合同对方处理接口
    /// </summary>
    public partial interface ICompanyService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        /// <summary>
        /// 查询客户信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<CompanyViewDTO> GetList<s>(PageInfo<Company> pageInfo, Expression<Func<Company, bool>> whereLambda, Expression<Func<Company, s>> orderbyLambda, bool isAsc);
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
        CompanyViewDTO ShowView(int Id);
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
        /// 获取资金统计
        /// </summary>
        /// <param name="compId">合同对方ID</param>
        /// <returns></returns>
        FundCalcul GetFundStatistics(int compId);
        /// <summary>
        /// 查询项目列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<ProjectManagerViewDTO> GetProjList<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
            Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 标的大列表查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<ContSubjectMatterListDTO> GetSubmitList<s>(PageInfo<ContSubjectMatter> pageInfo, Expression<Func<ContSubjectMatter, bool>> whereLambda, Expression<Func<ContSubjectMatter, s>> orderbyLambda, bool isAsc);

         int GetIsHt(string ids);
    }
}
