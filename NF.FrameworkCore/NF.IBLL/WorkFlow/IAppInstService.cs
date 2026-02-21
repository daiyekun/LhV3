using AutoMapper;
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
    /// 审批实例
    /// </summary>
    public partial interface IAppInstService
    {
        /// <summary>
        /// 提交审批
        /// </summary>
        /// <returns>审批实例</returns>
        AppInst SubmitWorkFlow(AppInst appInst, IMapper _IMapper);
        /// <summary>
        /// 查询发起列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<AppsponsorListDTO> GetAppSponsorList<s>(PageInfo<AppInst> pageInfo, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 待处理列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<AppPendingListDTO> GetAppPendingList<s>(PageInfo<AppInst> pageInfo,int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 已处理
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<AppProcessedListDTO> GetAppProcessedList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 查询被打回
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<AppsponsorListDTO> GetAppBeBackList<s>(PageInfo<AppInst> pageInfo, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 提交流程时修改审批对象相关信息
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns></returns>
        int SubmitWfUpdateObjWfInfo(AppInst appInst);

        /// <summary>
        /// 审批历史
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<ApproveHistListDTO> GetAppHistList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 获取待处理
        /// </summary>
        /// <param name="currUserId">当前用户</param>
        /// <returns>待处理数</returns>
        ConMesgInfo GetConsoleMsgNumber(int currUserId);
        /// <summary>
        /// 我审批（通过与打回）
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="sessionUserId"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
       /// <param name="wosp">1：已通过，0：被打回</param>
        /// <returns></returns>
        LayPageInfo<AppProcessedListDTO> GetWoShenPiList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc,  int wosp);




    }
}
