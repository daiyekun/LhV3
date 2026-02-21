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
    /// 实际资金
    /// </summary>
    public  partial interface IContActualFinanceService
    {
        /// <summary>
        /// 实际资金大列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>返回列表</returns>
        LayPageInfo<ContActualFinanceListViewDTO> GetMainList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda, Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 保存实际资金
        /// </summary>
        /// <param name="contActual">实际资金对象</param>
        /// <param name="CheckType">核销类型，0：计划资金、1：发票</param>
        /// <param name="chkData">核销数据</param>
        /// <returns>返回当前对象</returns>
        ContActualFinance AddSave(ContActualFinance contActual, int CheckType, IList<CheckData> chkData);
        /// <summary>
        /// 修改实际资金
        /// </summary>
        /// <param name="contActual">实际资金对象</param>
        /// <param name="CheckType">核销类型，0：计划资金、1：发票</param>
        /// <param name="chkData">核销数据</param>
        /// <returns>返回当前对象</returns>
        ContActualFinance UpdateSave(ContActualFinance contActual, int CheckType, IList<CheckData> chkData);

        int Sjzj(decimal je, int id);
        int UpdSjzj(decimal je, int id,int Zid,int usid);

        /// <summary>
        /// 核销明细
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>返回列表</returns>
        LayPageInfo<ContActualFinanceChkListViewDTO> GetChkList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda, Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ContActualFinanceViewDTO ShowView(int Id);

        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids ,int usid);

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段对象</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);

        /// <summary>
        /// 合同查看页面实际资金列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>返回列表</returns>
        LayPageInfo<ContractActualFinance> GetList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda, Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc);


    }
}
