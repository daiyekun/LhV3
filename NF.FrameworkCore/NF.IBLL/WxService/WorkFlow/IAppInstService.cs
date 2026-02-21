using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using NF.ViewModel.Models.WeiXinModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{

    /// <summary>
    /// 流程触发微信相关类
    /// </summary>
    public partial interface IAppInstService
    {
        /// <summary>
        /// 获取当前节点消息-将消息写入Reids
        /// </summary>
        /// <param name="appInst">当前审批实例</param>
        void WeiXinFlowNodeMsg(AppInst appInst);
        /// <summary>
        /// 打回或者通过通知消息
        /// </summary>
        /// <param name="appInst">当前审批实例</param>
        /// <param name="MsgType">0:通过，1：打回</param>
        void WxDhOrTgMsg(AppInst appInst, int MsgType);
        /// <summary>
        /// 提醒之前节点审批人员以及发起人员
        /// </summary>
        /// <param name="appInst">当前审批实例</param>
        /// <param name="spres">审批结果 0：同意，1：不同意</param>
        void WxmsgPrvNode(AppInst appInst, int spres,string jdName);
            /// <summary>
            /// 判断当前人员流程权限
            /// </summary>
            /// <param name="flowPerm">审批对象
            ///</param>
            ///<returns>返回对象</returns>
            WorFlowPerssion GetFlowPermission(FlowPerm flowPerm);
        /// <summary>
        /// 查询 审批意见情况
        /// </summary>
        /// <param name="flowPerm">审批对象信息</param>
        IList<WfNodeView> GetFlowOptions(FlowPerm flowPerm);
        /// <summary>
        /// 微信待处理列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<WxAppSp> GetAppWxDclList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 已处理
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<WxAppSp> WxYcl<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 微信我审批（通过与打回）
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="sessionUserId"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="wosp">1：已通过，0：被打回</param>
        /// <returns></returns>
        LayPageInfo<WxAppSp> GetWxWtgList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc, int wosp);
        /// <summary>
        /// 查询发起列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<WxAppSp> GetYfqList<s>(PageInfo<AppInst> pageInfo, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 查询被打回
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<WxAppSp> GetBdhList<s>(PageInfo<AppInst> pageInfo, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 审批历史
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<WxApproveHistList> GetWXAppHistList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 根据审批实例查询流程节点信息-时间轴
        /// </summary>
        /// <param name="InstId">实例ID</param>
        /// <returns></returns>
        IList<WooFlowTime> GetFlowTime(int InstId);

        /// <summary>
        /// 根据审批实例查询流程节点信息-时间轴（目前使用）
        /// </summary>
        /// <param name="flowPerm"></param>
        IList<WooFlowTime> GetFlowTime(FlowPerm flowPerm);

        /// <summary>
        /// 待处理审批超时提醒
        /// 目前2天。代码写死
        /// </summary>
        void SearchAppMsg();

        /// <summary>
        /// 删除待处理消息
        /// </summary>
        /// <returns></returns>
        int ClearMsg(int instId, int nodeId, int userId);
        /// <summary>
        /// 查询当前待审批
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetAppSum();
        /// <summary>
        /// 提醒条数
        /// </summary>
        void PubMsgRowsToList();
        /// <summary>
        /// 查询待处理审批信息
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        DaiShenPiInfo GetDclInfo(int userId);





    }
}
