using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 审批单服务
    /// </summary>
    public partial interface IFlowPdfService: IBaseService<AppInst>
    {
        /// <summary>
        /// 获取合同对方审批单相关信息
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>审批单数据对象</returns>
        CompanyInfo GetCompFlowPdfData(AppInst appInst);
        /// <summary>
        /// 项目审批单
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>审批单数据对象</returns>
        ProjectInfo GetProjectFlowPdfData(AppInst appInst);
        /// <summary>
        /// 合同审批单
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>审批单数据对象</returns>
        ContractPdfInfo GetContractFlowPdfData(AppInst appInst);

        /// <summary>
        /// 付款审批单
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>审批单数据对象</returns>
        PaymentPdfInfo GetPaymentFlowPdfData(AppInst appInst);

        /// <summary>
        /// 收票
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>收票单数据对象</returns>
        InvoiceInPdfInfo GetInvoiceInFlowPdfData(AppInst appInst);
        /// <summary>
        /// 开票
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>开票单数据对象</returns>
        InvoiceOutPdfInfo GetInvoiceOutFlowPdfData(AppInst appInst);


        /// <summary>
        /// 获取询价审批单相关信息
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>审批单数据对象</returns>
        InquiryInfo GetInquFlowPdfData(AppInst appInst);

        /// <summary>
        /// 获取约谈审批单相关信息
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>审批单数据对象</returns>
        QuestioningInfo GetQuesFlowPdfData(AppInst appInst);

        /// <summary>
        /// 获取招标审批单相关信息
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>审批单数据对象</returns>
        TeInfo GetTendFlowPdfData(AppInst appInst);



    }
}
