using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.StaticFiles;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.NfCommon.Controllers
{
    /// <summary>
    /// 系统文件上传操作公共类
    /// </summary>
    [Area("NfCommon")]
    [Route("NfCommon/[controller]/[action]")]
    [EnableCors("AllowSpecificOrigin")]

    public class NfAttachmentController : NfBaseController
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
        public IAjglAttachmentService _IAjglAttachmentService;
        public IAjglSpwsAttachmentService _IAjglSpwsAttachmentService;
        public IAjglSsBqAttachmentService _IAjglSsBqAttachmentService;
        /// <summary>
        /// 发票附件
        /// </summary>
        public IInvoFileService _IInvoFileService;
        public NfAttachmentController(ICompAttachmentService ICompAttachmentService, IProjAttachmentService IProjAttachmentService,
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
            , IInvoFileService IInvoFileService
             ,IAjglAttachmentService      IAjglAttachmentService 
             ,IAjglSpwsAttachmentService  IAjglSpwsAttachmentService
             ,IAjglSsBqAttachmentService IAjglSsBqAttachmentService
            )
        {
            _IAjglAttachmentService = IAjglAttachmentService;
            _IAjglSpwsAttachmentService = IAjglSpwsAttachmentService;
           _IAjglSsBqAttachmentService = IAjglSsBqAttachmentService;
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
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        //[EnableCors("AllowSpecificOrigin")]
        public IActionResult Upload(IFormCollection formCollection)
        {
            foreach (IFormFile file in formCollection.Files)
            {
                #region 小文件可以
                //StreamReader reader = new StreamReader(file.OpenReadStream());
                // String content = reader.ReadToEnd();
                // String name = file.FileName;
                //String filename = @"D:/Test/" + name;
                var path = Path.Combine(
                      Directory.GetCurrentDirectory(), "Uploads", "CustomerFile",
                      file.FileName);
                //var  dircpath = Path.Combine(
                //       Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "CustomerFile123");
                //if (!Directory.Exists(dircpath)) {
                //    Directory.CreateDirectory(dircpath);
                //}
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
                #endregion








            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "上传成功",
                Code = 0,


            });
        }

        /// <summary>
        /// 上传大文件
        /// </summary>
        /// <param name="downLoadAndUploadRequestInfo">上传文件时请求对象</param>
        /// <returns>返回上传后信息</returns>
        [HttpPost]
        [DisableFormValueModelBinding]
        //[EnableCors("AllowSpecificOrigin")]
        //DownLoadAndUploadRequestInfo downLoadAndUploadRequestInfo
        [NfCustomActionFilter("上传文件", OptionLogTypeEnum.Upload, "上传文件", true)]
        public async Task<IActionResult> UploadAsync(DownLoadAndUploadRequestInfo downLoadAndUploadRequestInfo)
        {


            //var path = Path.Combine(
            //             Directory.GetCurrentDirectory(), "wwwroot", "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoadAndUploadRequestInfo.folderIndex)
            //             );
            var path = Path.Combine(
                         Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoadAndUploadRequestInfo.folderIndex)
                         );
            FormValueProvider formModel;
            UploadFileInfo uploadFileInfo = new UploadFileInfo();
            if (downLoadAndUploadRequestInfo.folderIndex == 11)
            {//水印
                uploadFileInfo.RemGuidName = false;
                uploadFileInfo.SourceFileName = "ContractTextWordWaterMark.dotx";
            }
            if (downLoadAndUploadRequestInfo.folderIndex == 15)
            {
                var se = this.SessionCurrUserId;
                uploadFileInfo.GuidFileName = se + ".jpg";

            }
            //  formModel = await Request.StreamFiles(path, uploadFileInfo);
            formModel = await Request.StreamFiles_Dz(path, uploadFileInfo, downLoadAndUploadRequestInfo.folderIndex, this.SessionCurrUserId);

            var viewModel = new MyViewModel();

            var bindingSuccessful = await TryUpdateModelAsync(viewModel, prefix: "",
                valueProvider: formModel);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            uploadFileInfo.FolderName = EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoadAndUploadRequestInfo.folderIndex);
            if (downLoadAndUploadRequestInfo.folderIndex == 15)
            {
                _IUserInforService.ADDDzqz(this.SessionCurrUserId);


            }
            //return Ok(viewModel);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "上传成功",
                Code = 0,
                RetValue = uploadFileInfo
            });
        }
        /// <summary>
        /// 下载
        /// folderIndex参考枚举--》ViewMode/Extend/UploadAndDownloadFoldersEnum
        /// </summary>
        /// <param name="downLoadAndUploadRequestInfo">下载请求对象</param>
        /// <returns>返回文件对象</returns>
        [EnableCors("AllowSpecificOrigin")]
        [NfCustomActionFilter("下载文件", OptionLogTypeEnum.Download, "下载文件", true)]
        public IActionResult Download(DownLoadAndUploadRequestInfo downLoadAndUploadRequestInfo)
        {
            string guidFileName = string.Empty;
            string txtpath = "";
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
                            txtpath = contText.Path;
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
                        txtpath = contText.Path;
                        if (contText != null)
                        {
                            guidFileName = contText.FileName;
                        }
                    }
                    else
                    {
                        var contText = _IContTextService.Find(downLoadAndUploadRequestInfo.Id);
                        txtpath = contText.Path;
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
                case 18://案件附件
                    guidFileName = _IAjglAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 19://审判文书
                    guidFileName = _IAjglSpwsAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;
                case 20://诉讼保全
                    guidFileName = _IAjglSsBqAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
                    break;

            }
            //string filename = fileinfo.Path;

            if (guidFileName.StartsWith('~'))
            {
                var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

                guidFileName = filearr.LastOrDefault();
            }
            //var pathf = Path.Combine(
            //                 Directory.GetCurrentDirectory(), "wwwroot", "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoadAndUploadRequestInfo.folderIndex),
            //                 guidFileName);
            var pathf = Path.Combine(
                             Directory.GetCurrentDirectory(),  "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoadAndUploadRequestInfo.folderIndex),
                             guidFileName);
            if (!FileUtility.Exists(pathf))
            {
                if (downLoadAndUploadRequestInfo.folderIndex == 6)
                {
                    pathf = Path.Combine(
                           Directory.GetCurrentDirectory(), txtpath.TrimStart('~').TrimStart('/')
                           );
                }
                
            }

            var downInfo = FileStreamingHelper.Download(pathf);
            //var s = ToBase64String(downInfo.NfFileStream);

            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }


        private const int BufferSize = 1024 * 8;
        public static string ToBase64String(Stream s)
        {

            byte[] buff = null;
            StringBuilder rtnvalue = new StringBuilder();

            using (BinaryReader br = new BinaryReader(s))
            {
                do
                {
                    buff = br.ReadBytes(BufferSize);
                    rtnvalue.Append(Convert.ToBase64String(buff));

                } while (buff.Length != 0);

                br.Close();
            }

            return rtnvalue.ToString(); ;
        }



    }


    /// <summary>
    /// 目前没有意义
    /// </summary>
    public class MyViewModel
    {
        public string Username { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
    }

}