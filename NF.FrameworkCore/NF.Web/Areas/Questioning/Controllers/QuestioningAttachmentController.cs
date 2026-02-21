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


namespace NF.Web.Areas
{
    [Area("Questioning")]
    [Route("Questioning/[controller]/[action]")]
    public class QuestioningAttachmentController : NfBaseController
    {
        public IMapper _IMapper;
        public IQuestioningAttachmentService _IQuestioningAttachmentService;
        public QuestioningAttachmentController(IMapper IMapper, IQuestioningAttachmentService IQuestioningAttachmentService)
        {
            _IMapper = IMapper;
            _IQuestioningAttachmentService = IQuestioningAttachmentService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<QuestioningAttachment>();
            var predicateAnd = PredicateBuilder.True<QuestioningAttachment>();
            var predicateOr = PredicateBuilder.False<QuestioningAttachment>();
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ContId == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IQuestioningAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(QuestioningAttachmentDTO QuestioningAttachmentDTO)
        {

            var saveInfo = _IMapper.Map<QuestioningAttachment>(QuestioningAttachmentDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + QuestioningAttachmentDTO.FolderName + "/" + QuestioningAttachmentDTO.GuidFileName;
            saveInfo.ContId = (QuestioningAttachmentDTO.ContId ?? 0) <= 0 ? -this.SessionCurrUserId : QuestioningAttachmentDTO.ContId;
            saveInfo.FolderName = QuestioningAttachmentDTO.FolderName;
            _IQuestioningAttachmentService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改招标附件", OptionLogTypeEnum.Update, "修改招标附件", true)]
        public IActionResult UpdateSave(QuestioningAttachmentDTO QuestioningAttachmentDTO)
        {
            if (QuestioningAttachmentDTO.Id > 0)
            {
                var updateinfo = _IQuestioningAttachmentService.Find(QuestioningAttachmentDTO.Id);
                var updatedata = _IMapper.Map(QuestioningAttachmentDTO, updateinfo);
                updateinfo.Path = "Uploads/" + QuestioningAttachmentDTO.FolderName + "/" + QuestioningAttachmentDTO.GuidFileName;
                updateinfo.FolderName = QuestioningAttachmentDTO.FolderName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IQuestioningAttachmentService.Update(updatedata);
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
            _IQuestioningAttachmentService.Delete(Ids);
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
                Data = _IQuestioningAttachmentService.ShowView(Id)


            });
        }
    }
}