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
    public class AjglSsBqFileController : NfBaseController
    {
        private IAjglSsBqAttachmentService _IAjglSsBqAttachmentService;
        private IMapper _IMapper;

        public AjglSsBqFileController(IAjglSsBqAttachmentService IAjglSsBqAttachmentService, IMapper IMapper)
        {
            _IAjglSsBqAttachmentService = IAjglSsBqAttachmentService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<AjglSsBqAttachment>();
            var predicateAnd = PredicateBuilder.True<AjglSsBqAttachment>();
            var predicateOr = PredicateBuilder.False<AjglSsBqAttachment>();
            predicateOr = predicateOr.Or(a => a.AjglSsbqwjid == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.AjglSsbqwjid == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IAjglSsBqAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(AjglSsbqDTO Ssbq)
        {

            var saveInfo = _IMapper.Map<AjglSsBqAttachment>(Ssbq);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            if (Ssbq.AjglSsbqwjid == null)
            {
                saveInfo.AjglSsbqwjid = 0;
            }
            saveInfo.Path = "Uploads/" + Ssbq.FolderName + "/" + Ssbq.GuidFileName;
            saveInfo.AjglSsbqwjid = (Ssbq.AjglSsbqwjid ?? 0) <= 0 ? -this.SessionCurrUserId : Ssbq.AjglSsbqwjid??0;
            saveInfo.FolderName = Ssbq.FolderName;
            _IAjglSsBqAttachmentService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改合同附件", OptionLogTypeEnum.Update, "修改合同附件", true)]
        public IActionResult UpdateSave(AjglSsbqDTO Ssbq)
        {
            if (Ssbq.Id > 0)
            {
                var updateinfo = _IAjglSsBqAttachmentService.Find(Ssbq.Id);
                var updatedata = _IMapper.Map(Ssbq, updateinfo);
                updateinfo.Path = "Uploads/" + Ssbq.FolderName + "/" + Ssbq.GuidFileName;
                updateinfo.FolderName = Ssbq.FolderName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IAjglSsBqAttachmentService.Update(updatedata);
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
            _IAjglSsBqAttachmentService.Delete(Ids);
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
                Data = _IAjglSsBqAttachmentService.ShowView(Id)


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
            var contText = _IAjglSsBqAttachmentService.Find(Id);
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
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 20),
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
            var contText = _IAjglSsBqAttachmentService.Find(Id);
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
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 20),
                            wordname);
            var pdfpath = Path.Combine(
                             Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 20),
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
