using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Inquiry
{
    [Area("Inquiry")]
    [Route("Inquiry/[controller]/[action]")]
    public class InquiryAttachmentController : NfBaseController
    {
        public IMapper _IMapper;
        public IInquiryAttachmentService _IInquiryAttachmentService;
        public InquiryAttachmentController(IMapper IMapper, IInquiryAttachmentService IInquiryAttachmentService)
        {
            _IMapper = IMapper;
            _IInquiryAttachmentService = IInquiryAttachmentService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<InquiryAttachment>();
            var predicateAnd = PredicateBuilder.True<InquiryAttachment>();
            var predicateOr = PredicateBuilder.False<InquiryAttachment>();
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ContId == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IInquiryAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        /// <summary>
        /// 新建附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新建询价附件", OptionLogTypeEnum.Add, "新建询价附件", true)]
        public IActionResult Save(InquiryAttachmentDTO InquiryAttachmentDTO)
        {

            var saveInfo = _IMapper.Map<InquiryAttachment>(InquiryAttachmentDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + InquiryAttachmentDTO.FolderName + "/" + InquiryAttachmentDTO.GuidFileName;
            saveInfo.ContId = (InquiryAttachmentDTO.ContId ?? 0) <= 0 ? -this.SessionCurrUserId : InquiryAttachmentDTO.ContId;
            saveInfo.FolderName = InquiryAttachmentDTO.FolderName;
            _IInquiryAttachmentService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改招标附件", OptionLogTypeEnum.Update, "修改招标附件", true)]
        public IActionResult UpdateSave(InquiryAttachmentDTO InquiryAttachmentDTO)
        {
            if (InquiryAttachmentDTO.Id > 0)
            {
                var updateinfo = _IInquiryAttachmentService.Find(InquiryAttachmentDTO.Id);
                var updatedata = _IMapper.Map(InquiryAttachmentDTO, updateinfo);
                updateinfo.Path = "Uploads/" + InquiryAttachmentDTO.FolderName + "/" + InquiryAttachmentDTO.GuidFileName;
                updateinfo.FolderName = InquiryAttachmentDTO.FolderName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IInquiryAttachmentService.Update(updatedata);
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
            _IInquiryAttachmentService.Delete(Ids);
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
                Data = _IInquiryAttachmentService.ShowView(Id)


            });
        }
    }
}