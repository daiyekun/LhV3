using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.OfficeComm;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.CaseManagement.Controllers
{
    [Area("CaseManagement")]
    [Route("CaseManagement/[controller]/[action]")]
    public class AjfjController : NfBaseController
    {
        private IAjglAttachmentService _IAjglAttachmentService;
        private IMapper _IMapper;

        public AjfjController(IAjglAttachmentService IAjglAttachmentService, IMapper IMapper)
        {
            _IAjglAttachmentService = IAjglAttachmentService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<AjglAttachment>();
            var predicateAnd = PredicateBuilder.True<AjglAttachment>();
            var predicateOr = PredicateBuilder.False<AjglAttachment>();
            predicateOr = predicateOr.Or(a => a.Ajglid == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.Ajglid == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IAjglAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            return View();

        }
        public IActionResult Builds()
        {
            return View();

        }
        /// <summary>
        /// 新建附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新建合同附件", OptionLogTypeEnum.Add, "新建合同附件", true)]
        public IActionResult Save(AjfjDTO AjfjDTO)
        {

            var saveInfo = _IMapper.Map<AjglAttachment>(AjfjDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            if (AjfjDTO.Ajglid==null)
            {
                saveInfo.Ajglid = 0;
            }
            saveInfo.Path = "Uploads/" + AjfjDTO.FolderName + "/" + AjfjDTO.GuidFileName;
            saveInfo.Ajglid = (AjfjDTO.Ajglid ?? 0) <= 0 ? -this.SessionCurrUserId : AjfjDTO.Ajglid;
            saveInfo.FolderName = AjfjDTO.FolderName;
            _IAjglAttachmentService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改合同附件", OptionLogTypeEnum.Update, "修改合同附件", true)]
        public IActionResult UpdateSave(AjfjDTO AjfjDTO)
        {
            if (AjfjDTO.Id > 0)
            {
                var updateinfo = _IAjglAttachmentService.Find(AjfjDTO.Id);
                var updatedata = _IMapper.Map(AjfjDTO, updateinfo);
                updateinfo.Path = "Uploads/" + AjfjDTO.FolderName + "/" + AjfjDTO.GuidFileName;
                updateinfo.FolderName = AjfjDTO.FolderName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IAjglAttachmentService.Update(updatedata);
            }

            return GetResult();

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除合同附件", OptionLogTypeEnum.Del, "删除合同附件", false)]
        public IActionResult Delete(string Ids)
        {
            _IAjglAttachmentService.Delete(Ids);
            return GetResult();
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IAjglAttachmentService.ShowView(Id)


            });
        }

        /// <summary>
        ///合同附件-- pdf预览
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetFileBytes(int Id)
        {
            string guidFileName = string.Empty;
            var contText = _IAjglAttachmentService.Find(Id);
            if (contText != null)
            {
                guidFileName = contText.GuidFileName;
            }
            if (guidFileName.StartsWith('~'))
            {
                var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

                guidFileName = filearr.LastOrDefault();
            }
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 18),
                            guidFileName);

            var downInfo = FileStreamingHelper.Download(pathf);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }

        /// <summary>
        /// Word文件预览
        /// </summary>
        /// <returns></returns>
        public IActionResult WordView(int Id)
        {
            string guidFileName = string.Empty;
            var contText = _IAjglAttachmentService.Find(Id);
            if (contText != null)
            {
                guidFileName = contText.GuidFileName;
            }

            if (guidFileName.StartsWith('~'))
            {
                var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

                guidFileName = filearr.LastOrDefault();
            }



            var wordname = guidFileName;

            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 18),
                            wordname);
            var pdfpath = Path.Combine(
                             Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 18),
                             guidFileName.Replace(".docx", ".pdf"));
            var markpath = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 11),
                            "ContractTextWordWaterMark.dotx");
            //WpsWordToPdfHelper.WordToPdf(pathf, pdfpath);
            MsWordToPdfHelper wpfh = new MsWordToPdfHelper();

            wpfh.ConvertWordToPdf(pathf, pdfpath);

            var downInfo = FileStreamingHelper.Download(pdfpath);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }
    }
}
