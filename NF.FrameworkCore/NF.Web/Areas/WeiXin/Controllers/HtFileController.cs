using Common.Utility;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.WeiXinModels;
using NF.Web.Utility.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.WeiXin.Controllers
{
    /// <summary>
    /// 合同文件相关操作
    /// </summary>
    /// 
    [Area("WeiXin")]
    [Route("WeiXin/[controller]/[action]")]
    public class HtFileController : Controller
    {
        /// <summary>
        /// 合同对方附件
        /// </summary>
        private ICompAttachmentService _ICompAttachmentService;
        /// <summary>
        /// 项目附件
        /// </summary>
        private IProjAttachmentService _IProjAttachmentService;
        /// <summary>
        /// 合同附件
        /// </summary>
        private IContAttachmentService _IContAttachmentService;
        /// <summary>
        /// 合同文本
        /// </summary>
        private IContTextService _IContTextService;
        /// <summary>
        /// 合同文本归档明细
        /// </summary>
        private IContTextArchiveItemService _IContTextArchiveItemService;
        /// <summary>
        /// 单品附件
        /// </summary>
        private IBcAttachmentService _IBcAttachmentService;
        /// <summary>
        /// 标的交付描述
        /// </summary>
        private IContSubDeService _IContSubDeService;
        /// <summary>
        /// 合同文本历史
        /// </summary>
        private IContTextHistoryService _IContTextHistoryService;
        private IWinningInqService _IWinningInqService;
        /// <summary>
        /// 招标
        /// </summary>
        public ITenderAttachmentService _ITenderAttachmentService;
        /// <summary>
        /// 询价附件
        /// </summary>
        public IInquiryAttachmentService _IInquiryAttachmentService;
        /// <summary>
        /// 约谈附件
        /// </summary>
        public IQuestioningAttachmentService _IQuestioningAttachmentService;
        public IUserInforService _IUserInforService;
        /// <summary>
        /// 资金附件
        /// </summary>
        public IActFinceFileService _IActFinceFileService;
        /// <summary>
        /// 发票附件
        /// </summary>
        public IInvoFileService _IInvoFileService;

        public HtFileController(ICompAttachmentService ICompAttachmentService, IProjAttachmentService IProjAttachmentService,
          IContAttachmentService IContAttachmentService, IContTextService IContTextService
          , IContTextArchiveItemService IContTextArchiveItemService
          , IBcAttachmentService IBcAttachmentService
          , IContSubDeService IContSubDeService
          , IWinningInqService IWinningInqService
          , IContTextHistoryService IContTextHistoryService
          , ITenderAttachmentService ITenderAttachmentService
          , IInquiryAttachmentService IInquiryAttachmentService
          , IQuestioningAttachmentService IQuestioningAttachmentService
          , IUserInforService IUserInforService
          , IActFinceFileService IActFinceFileService
          , IInvoFileService IInvoFileService)
        {
            _IWinningInqService = IWinningInqService;
            _ICompAttachmentService = ICompAttachmentService;
            _IProjAttachmentService = IProjAttachmentService;
            _IContAttachmentService = IContAttachmentService;
            _IContTextService = IContTextService;
            _IContTextArchiveItemService = IContTextArchiveItemService;
            _IBcAttachmentService = IBcAttachmentService;
            _IContSubDeService = IContSubDeService;
            _IContTextHistoryService = IContTextHistoryService;
            _ITenderAttachmentService = ITenderAttachmentService;
            _IInquiryAttachmentService = IInquiryAttachmentService;
            _IQuestioningAttachmentService = IQuestioningAttachmentService;
            _IUserInforService = IUserInforService;
            _IActFinceFileService = IActFinceFileService;
            _IInvoFileService = IInvoFileService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 合同文本下载
        /// </summary>
        /// <returns></returns>
        public IActionResult WxFileDownLoad()
        {
            var jsondata = HttpContext.Request.Query["postdata"][0];
            
            //var postdata= Request.Form.
            DownLoadAndUploadRequestInfo downLoadAndUploadRequestInfo = JsonUtility.DeserializeJsonToObject<DownLoadAndUploadRequestInfo>(jsondata); ;
            string guidFileName = string.Empty;
            switch (downLoadAndUploadRequestInfo.folderIndex)
            {
                case 0://客户附件
                case 1://供应商
                case 2://其他对方
                    guidFileName = _ICompAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 4://项目附件
                    guidFileName = _IProjAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 5://合同附件
                    guidFileName = _IContAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 6://合同文本
                    if (downLoadAndUploadRequestInfo.Dtype == 1)
                    {
                        var contText = _IContTextHistoryService.Find(downLoadAndUploadRequestInfo.Id);
                        if (contText != null)
                        {
                            if (!contText.GuidFileName.EndsWith(".docx") && contText.IsFromTemp == (int)SourceTxtEnum.TempDraft)
                            {


                                guidFileName = contText.FileName;

                            }
                            else
                            {
                                guidFileName = contText.GuidFileName;
                            }
                        }
                    }
                    else if (downLoadAndUploadRequestInfo.Dtype == 2)
                    {//下载PDF
                        var contText = _IContTextService.Find(downLoadAndUploadRequestInfo.Id);
                        if (contText != null)
                        {
                            guidFileName = contText.FileName;
                        }
                    }
                    else
                    {
                        var contText = _IContTextService.Find(downLoadAndUploadRequestInfo.Id);
                        if (contText != null)
                        {
                            if (!contText.GuidFileName.EndsWith(".docx") && contText.IsFromTemp == (int)SourceTxtEnum.TempDraft)
                            {
                                if (downLoadAndUploadRequestInfo.DownType == 1)
                                {
                                    guidFileName = contText.WordPath;
                                }
                                else
                                {
                                    guidFileName = contText.FileName;
                                }
                            }
                            else
                            {
                                guidFileName = contText.GuidFileName;
                            }
                        }

                    }


                    break;
                case 7://合同文本归档电子版下载
                    guidFileName = _IContTextArchiveItemService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 8://单品附件
                    guidFileName = _IBcAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 9://标的交付附件
                    guidFileName = _IContSubDeService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 12://招标附件
                    guidFileName = _ITenderAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 13://询价附件
                    guidFileName = _IInquiryAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 14://约谈附件
                    guidFileName = _IQuestioningAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;

                case 15://电子签章
                    guidFileName = _IQuestioningAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 16://资金附件
                    guidFileName = _IActFinceFileService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 17://资金附件
                    guidFileName = _IInvoFileService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;

            }
            //string filename = fileinfo.Path;

            if (guidFileName.StartsWith('~'))
            {
                var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

                guidFileName = filearr.LastOrDefault();
            }

            var pathf = Path.Combine(
                             Directory.GetCurrentDirectory(),  "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoadAndUploadRequestInfo.folderIndex),
                             guidFileName);

            var downInfo = FileStreamingHelper.Download(pathf);
            //WxDownLoadInfo down = new WxDownLoadInfo();
            //down.Filebff = FileBinaryConvertHelper.File2Bytes(pathf);
            //down.Memi = downInfo.Memi;

            // return Content(JsonUtility.SerializeObject( File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName)));
            // return Content(JsonUtility.SerializeObject(downInfo));
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }
    }
}
