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
    /// 计划资金
    /// </summary>
   public partial  interface IContPlanFinanceService
    {
        /// <summary>
        /// 保存计划资金
        /// </summary>
        /// <param name="planFinance">计划资金</param>
        /// <param name="planFinanceHistory">计划资金历史</param>
        /// <returns>Id:->Hid:</returns>
        Dictionary<string, int> AddSave(ContPlanFinance planFinance,ContPlanFinanceHistory planFinanceHistory);
        /// <summary>
        /// 计划资金保存
        /// </summary>
        /// <param name="planFinance">计划资金实体</param>
        /// <param name="IsFrameWorkCont">是否是框架合同，框架合同表示是执行建立资金</param>
        /// <returns></returns>
        ContPlanFinance AddSave(ContPlanFinance planFinance,bool IsFrameWorkCont=false);
        /// <summary>
        /// 修改计划资金
        /// </summary>
        /// <param name="planFinance">修改计划资金对象</param>
        /// <param name="planFinanceHistory">修改计划资金拷贝对象（历史）</param>
        /// <returns>Id:\Hid:字典</returns>
        Dictionary<string, int> UpdateSave(ContPlanFinance planFinance, ContPlanFinanceHistory planFinanceHistory);
        /// <summary>
        /// 修改计划资金
        /// </summary>
        /// <param name="planFinance">计划资金实体</param>
        /// <param name="IsFrameWorkCont">是否是框架合同，如果是框架合同表示执行添加计划资金需修改合同金额</param>
        /// <returns>当前对象</returns>
        ContPlanFinance UpdateSave(ContPlanFinance planFinance, bool IsFrameWorkCont = false);
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ContPlanFinanceViewDTO> GetList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 查询列表大列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ContPlanFinanceListViewDTO> GetMainList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc);
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
        ContPlanFinanceViewDTO ShowView(int Id);
        /// <summary>
        /// 更新合同金额
        /// </summary>
        /// <param name="ContId">合同金额</param>
        void UpdateContAmount(int ContId);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID</param>
        /// <param name="IsFrameWorkCont">是否是框架合同，如果是框架合同需要更新合同金额</param>
        /// <returns></returns>
        int Delete(string Ids, bool IsFrameWorkCont = false);
        /// <summary>
        /// 查询相关列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="ActId">实际资金ID</param>
        /// <returns></returns>
        LayPageInfo<ContPlanFinanceViewSecoundDTO> GetListSecod<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc, int ActId);

        /// <summary>
        /// 计划资金核销表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="ActId">实际资金ID</param>
        /// <returns></returns>
        LayPageInfo<ContPlanFinanceViewSecoundDTO> GetPlanCheckList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc, int ActId);
        /// <summary>
        /// 千分位转数字
        /// </summary>
        /// <param name="strnum">传入值</param>
        /// <returns></returns>
        decimal? ParseThousandthString(string strnum);
        /// <summary>
        /// 根据名字查询部门id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int DepartmentID(string name);
    }
}
