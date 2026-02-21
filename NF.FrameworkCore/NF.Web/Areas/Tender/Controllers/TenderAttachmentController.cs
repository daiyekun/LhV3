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

namespace NF.Web.Areas.Tender
{
    /// <summary>
    /// 招标信息
    /// </summary>
    [Area("Tender")]
    [Route("Tender/[controller]/[action]")]
    public class TenderAttachmentController : NfBaseController
    {
        public IMapper _IMapper;
        public ITenderAttachmentService _ITenderAttachmentService;
        public TenderAttachmentController(IMapper IMapper, ITenderAttachmentService ITenderAttachmentService)
            {
            _IMapper = IMapper;
            _ITenderAttachmentService = ITenderAttachmentService;
            }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<TenderAttachment>();
            var predicateAnd = PredicateBuilder.True<TenderAttachment>();
            var predicateOr = PredicateBuilder.False<TenderAttachment>();
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ContId == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _ITenderAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        [NfCustomActionFilter("新建招标附件", OptionLogTypeEnum.Add, "新建招标附件", true)]
        public IActionResult Save(TenderAttachmentDTO TenderAttachmentDTO)
        {

            var saveInfo = _IMapper.Map<TenderAttachment>(TenderAttachmentDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + TenderAttachmentDTO.FolderName + "/" + TenderAttachmentDTO.GuidFileName;
            saveInfo.ContId = (TenderAttachmentDTO.ContId ?? 0) <= 0 ? -this.SessionCurrUserId : TenderAttachmentDTO.ContId;
            saveInfo.FolderName = TenderAttachmentDTO.FolderName;
            _ITenderAttachmentService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改招标附件", OptionLogTypeEnum.Update, "修改招标附件", true)]
        public IActionResult UpdateSave(TenderAttachmentDTO TenderAttachmentDTO)
        {
            if (TenderAttachmentDTO.Id > 0)
            {
                var updateinfo = _ITenderAttachmentService.Find(TenderAttachmentDTO.Id);
                var updatedata = _IMapper.Map(TenderAttachmentDTO, updateinfo);
                updateinfo.Path = "Uploads/" + TenderAttachmentDTO.FolderName + "/" + TenderAttachmentDTO.GuidFileName;
                updateinfo.FolderName = TenderAttachmentDTO.FolderName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _ITenderAttachmentService.Update(updatedata);
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
            _ITenderAttachmentService.Delete(Ids);
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
                Data = _ITenderAttachmentService.ShowView(Id)


            });
        }
    }
}