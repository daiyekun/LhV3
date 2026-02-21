using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IAPPWOrkList
    {

        /// <summary>
        /// 待处理列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        APPWFLayPageInfo<AppPendingListDTO> GetAppWorkList<s>(int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc, int start, int limit);

        /// <summary>
        /// 查询发起列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        APPWFLayPageInfo<AppPendingListDTO> GetAppSponsorList<s>(Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc,int start, int limit);
        /// <summary>
        /// 已处理
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        APPWFLayPageInfo<AppProcessedListDTO> GetAppProcessedList<s>(int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc, int start, int limit);
        //查询合同基本信息
        APPWFLayPageInfo<ContractInfoHistoryViewDTO> GetIDWorkList(int sessionUserId);
        //查询客户基本信息
        APPWFLayPageInfo<CompanyViewDTO> GetCUIDWorkList(int comid);
        //查询发票基本信息
        APPWFLayPageInfo<ContInvoiceListViewDTO> GetInvIDWorkList(int comid);
        //查询付款基本信息
        APPWFLayPageInfo<ContActualFinanceListViewDTO> GetIFukuanIDWorkList(int comid);
        //查询项目基本信息
        APPWFLayPageInfo<ProjectManagerViewDTO> GetIPRonIDWorkList(int comid);
        List<APPcontractSPD> AppcontractSPDDetail(int Id);
        /// <summary>
        /// 提交审批意见
        /// </summary>
        /// <param name="submitOption">提交审批意见对象</param>
        /// <returns>
        /// -1：标识查找分支节点时没有找到满足条件的节点
        /// 1：标识成功
        /// </returns>
        int SubmintOption(AppWorkcs submitOption);

        /// <summary>
        /// 不同意时提交意见
        /// </summary>
        /// <param name="submitOption">提交审批意见对象</param>
        /// <returns>
        /// </returns>
        bool SubmintDisagreeOption(AppWorkcs submitOption);
    }
}
