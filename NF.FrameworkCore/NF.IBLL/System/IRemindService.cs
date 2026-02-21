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
    /// 提醒接口
    /// </summary>
    public partial interface IRemindService
    {
        /// <summary>
        /// 提醒列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<RemindDTO> GetList<s>(PageInfo<Remind> pageInfo, Expression<Func<Remind, bool>> whereLambda, Expression<Func<Remind, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="Ids">删除ID字符串</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 获取提醒信息
        /// </summary>
        /// <param name="mesgInfo">提醒对象</param>
        /// <param name="currUserId">当前登录人</param>
        /// <returns>提醒信息实体</returns>
        ConMesgInfo GetConsoleReminder(ConMesgInfo mesgInfo ,int currUserId,int currUserDeptId);
        /// <summary>
        /// 获取计划资金提醒表达式
        /// </summary>
        /// <param name="content">提醒项</param>
        /// <returns>计划资金表达</returns>
        Expression<Func<ContPlanFinance, bool>> GetPlanFinanceExpression(string content);
        /// <summary>
        /// 取得查看实际资金的条件表达式
        /// </summary>
        /// <param name="content">设置项</param>
        /// <returns>实际资金表达式</returns>
        Expression<Func<ContActualFinance, bool>> GetActualFinanceExpression(string content);
        /// <summary>
        /// 发票表达式
        /// </summary>
        /// <param name="content">提醒项</param>
        /// <returns>发票条件表达式</returns>
        Expression<Func<ContInvoice, bool>> GetInvoiceExpression(string content);
        /// <summary>
        /// 合同表达式
        /// </summary>
        /// <param name="content">提醒项</param>
        /// <returns>合同表达式</returns>
        Expression<Func<ContractInfo, bool>> GetContractExpression(string content);
        /// <summary>
        /// 审批实例表达式
        /// </summary>
        /// <param name="content">提醒项</param>
        /// <param name="userId">当前登录人</param>
        /// <returns>审批实例表达式</returns>
        Expression<Func<AppInst, bool>> GetWfIntanceExpression(string content, int userId);
    }
}
