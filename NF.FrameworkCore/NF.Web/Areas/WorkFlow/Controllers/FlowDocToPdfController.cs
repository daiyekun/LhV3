using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NF.IBLL;
using NF.ViewModel.Models;
using NF.Web.Controllers;
using Rotativa.AspNetCore;

namespace NF.Web.Areas.WorkFlow.Controllers
{
    /// <summary>
    /// 打印审批单
    /// </summary>
    [Area("WorkFlow")]
    [Route("WorkFlow/[controller]/[action]")]
    public class FlowDocToPdfController : NfBaseController
    {
        private IAppInstService _IAppInstService;
        private IFlowPdfService _IFlowPdfService;
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger _logger;
        public FlowDocToPdfController(IAppInstService IAppInstService
            , IFlowPdfService IFlowPdfService
            , ILogger<FlowDocToPdfController> logger)
        {
            _IAppInstService = IAppInstService;
            _IFlowPdfService = IFlowPdfService;
            _logger = logger;
        }
        /// <summary>
        /// 生成PDF
        /// </summary>
        /// <param name="WfInceId">审批实例ID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FlowinceToPdf(int WfInceId)
        {
           
            ViewAsPdf demoViewPortrait = null;  
            var wfinfo= _IAppInstService.Find(WfInceId);
           
            switch (wfinfo.ObjType)
            {
                case (int)FlowObjEnums.Customer://客户
                    {
                        CompanyInfo info = _IFlowPdfService.GetCompFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("CustomerPDF", info);
                        demoViewPortrait.FileName = "customer.pdf";
                    }
                    break;
                case (int)FlowObjEnums.Supplier://供应商
                    {
                        CompanyInfo info = _IFlowPdfService.GetCompFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("SupplierPDF", info);
                        demoViewPortrait.FileName = "supplier.pdf";
                    }
                    break;
                case (int)FlowObjEnums.Other://其他对方
                    {
                        CompanyInfo info = _IFlowPdfService.GetCompFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("OtherPDF", info);
                        demoViewPortrait.FileName = "other.pdf";
                    }
                    break;

                case (int)FlowObjEnums.project://项目{
                    {
                        ProjectInfo info = _IFlowPdfService.GetProjectFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("ProjectPDF", info);
                        demoViewPortrait.FileName = "project.pdf";
                    }

                    break;
                case (int)FlowObjEnums.Contract://合同
                    {
                        ContractPdfInfo info = _IFlowPdfService.GetContractFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("ContractPDF", info);
                        demoViewPortrait.FileName = "Contract.pdf";
                    }
                    break;
                case (int)FlowObjEnums.payment://付款
                    {
                        PaymentPdfInfo info = _IFlowPdfService.GetPaymentFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("PaymentPDF", info);
                        demoViewPortrait.FileName = "payment.pdf";
                    }
                    break;
                case (int)FlowObjEnums.InvoiceIn://收票
                    {
                        InvoiceInPdfInfo info = _IFlowPdfService.GetInvoiceInFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("InvoiceInPDF", info);
                        demoViewPortrait.FileName = "invoiceIn.pdf";
                    }
                    break;
                case (int)FlowObjEnums.InvoiceOut://开票
                    {
                        InvoiceOutPdfInfo info = _IFlowPdfService.GetInvoiceOutFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("InvoiceOutPDF", info);
                        demoViewPortrait.FileName = "invoiceOut.pdf";
                    }
                    break;
                case (int)FlowObjEnums.Inquiry://询价
                    {
                        InquiryInfo info = _IFlowPdfService.GetInquFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("InquiryPDF", info);
                        demoViewPortrait.FileName = "InquiryOut.pdf";
                    }
                    break;
                case (int)FlowObjEnums.Questioning://约谈
                    {
                        QuestioningInfo info = _IFlowPdfService.GetQuesFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("QuestioningPDF", info);
                        demoViewPortrait.FileName = "QuestioningOut.pdf";
                    }
                    break;
                case (int)FlowObjEnums.Tender://招标
                    {
                        TeInfo info = _IFlowPdfService.GetTendFlowPdfData(wfinfo);
                        demoViewPortrait = new ViewAsPdf("TeInfoPDF", info);
                        demoViewPortrait.FileName = "TeInfoOut.pdf";
                    }
                    break;


            }
         
            //纵向、横向
            demoViewPortrait.PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait;
            //页面大小
            demoViewPortrait.PageSize = Rotativa.AspNetCore.Options.Size.A4;
            
            return demoViewPortrait;
            
        } 

       
    }
}