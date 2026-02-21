using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IAPPInvoice
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
        APPLayPageInfo<APPContInvoice> APPGetMainList<s>(Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc,int start, int limit);

        /// <summary>
        /// 根据合同ID查询合同基本信息
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        APPContInvoice APPfinanceInvoiceDert(int Id);

        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ContractInfoViewDTO APPfinanceInvoiceCONT(int Id);
    }
}
