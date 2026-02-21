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
    /// 发票
    /// </summary>
    public partial interface IContInvoiceService
    {
        /// <summary>
        /// 发票大列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>返回列表</returns>
        LayPageInfo<ContInvoiceListViewDTO> GetMainList<s>(PageInfo<ContInvoice> pageInfo, Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 实际资金核销发票列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
         LayPageInfo<ContInvoiceActViewDTO> GetActInvoiceList<s>(PageInfo<ContInvoice> pageInfo, Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc, int actId);
        /// <summary>
        /// 合同查看里发票列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<ContractInvoice> GetList<s>(PageInfo<ContInvoice> pageInfo, Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 保存发票
        /// </summary>
        /// <param name="contInvoice">发票对象</param>
        /// <returns>返回当前对象</returns>
        ContInvoice AddSave(ContInvoice contInvoice);
        /// <summary>
        /// 修改发票明细等
        /// </summary>
        /// <param name="contInvoice">发票对象</param>
        void UpdateItems(ContInvoice contInvoice);
        /// <summary>
        /// 清除垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户</param>
        /// <returns></returns>
        int ClearJunkItemData(int currUserId);

        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>


        int Xiug(int ContId);
        int Delete(string Ids, int usid);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ContInvoiceViewDTO ShowView(int Id);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段对象</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);


    }
}
