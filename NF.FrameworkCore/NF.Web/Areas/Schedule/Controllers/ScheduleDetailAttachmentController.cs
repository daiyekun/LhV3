using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.Schedule;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.Schedule.Controllers
{
    /// <summary>
    /// 进度明细信息
    /// </summary>
    [Area("Schedule")]
    [Route("Schedule/[controller]/[action]")]
    public class ScheduleDetailAttachmentController : NfBaseController
    {
        public IMapper _IMapper;
        public IScheduleDetailAttachmentService _IScheduleDetailAttachmentService;
        public ScheduleDetailAttachmentController(IMapper IMapper, IScheduleDetailAttachmentService IScheduleDetailAttachmentService)
        {
            _IMapper = IMapper;
            _IScheduleDetailAttachmentService = IScheduleDetailAttachmentService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<ScheduleDetailAttachment>();
            var predicateAnd = PredicateBuilder.True<ScheduleDetailAttachment>();
            var predicateOr = PredicateBuilder.False<ScheduleDetailAttachment>();
            predicateOr = predicateOr.Or(a => a.ScheduledId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ScheduledId == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IScheduleDetailAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        [NfCustomActionFilter("新建进度明细附件", OptionLogTypeEnum.Add, "新建进度明细附件", true)]
        public IActionResult Save(ScheduleDetailAttachmentDTO ScheduleDetailAttachmentDTO)
        {

            var saveInfo = _IMapper.Map<ScheduleDetailAttachment>(ScheduleDetailAttachmentDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + ScheduleDetailAttachmentDTO.FolderName + "/" + ScheduleDetailAttachmentDTO.GuidFileName;
            saveInfo.ScheduledId = (ScheduleDetailAttachmentDTO.ScheduledId ?? 0) <= 0 ? -this.SessionCurrUserId : ScheduleDetailAttachmentDTO.ScheduledId;
            saveInfo.FolderName = ScheduleDetailAttachmentDTO.FolderName;
            _IScheduleDetailAttachmentService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改进度明细附件", OptionLogTypeEnum.Update, "修改进度明细附件", true)]
        public IActionResult UpdateSave(ScheduleDetailAttachmentDTO ScheduleDetailAttachmentDTO)
        {
            if (ScheduleDetailAttachmentDTO.Id > 0)
            {
                var updateinfo = _IScheduleDetailAttachmentService.Find(ScheduleDetailAttachmentDTO.Id);
                var updatedata = _IMapper.Map(ScheduleDetailAttachmentDTO, updateinfo);
                updateinfo.Path = "Uploads/" + ScheduleDetailAttachmentDTO.FolderName + "/" + ScheduleDetailAttachmentDTO.GuidFileName;
                updateinfo.FolderName = ScheduleDetailAttachmentDTO.FolderName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IScheduleDetailAttachmentService.Update(updatedata);
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
            _IScheduleDetailAttachmentService.Delete(Ids);
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
                Data = _IScheduleDetailAttachmentService.ShowView(Id)


            });
        }
    }
}
