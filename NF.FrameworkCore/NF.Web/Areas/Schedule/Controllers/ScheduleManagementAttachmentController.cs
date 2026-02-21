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
    /// 进度管理信息
    /// </summary>
    [Area("Schedule")]
    [Route("Schedule/[controller]/[action]")]
    public class ScheduleManagementAttachmentController : NfBaseController
    {
        public IMapper _IMapper;
        public IScheduleManagementAttachmentService _IScheduleManagementAttachmentService;
        public ScheduleManagementAttachmentController(IMapper IMapper, IScheduleManagementAttachmentService IScheduleManagementAttachmentService)
        {
            _IMapper = IMapper;
            _IScheduleManagementAttachmentService = IScheduleManagementAttachmentService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<ScheduleManagementAttachment>();
            var predicateAnd = PredicateBuilder.True<ScheduleManagementAttachment>();
            var predicateOr = PredicateBuilder.False<ScheduleManagementAttachment>();
            predicateOr = predicateOr.Or(a => a.SchedulemId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.SchedulemId == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IScheduleManagementAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        [NfCustomActionFilter("新建进度管理附件", OptionLogTypeEnum.Add, "新建进度管理附件", true)]
        public IActionResult Save(ScheduleManagementAttachmentDTO ScheduleManagementAttachmentDTO)
        {

            var saveInfo = _IMapper.Map<ScheduleManagementAttachment>(ScheduleManagementAttachmentDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + ScheduleManagementAttachmentDTO.FolderName + "/" + ScheduleManagementAttachmentDTO.GuidFileName;
            saveInfo.SchedulemId = (ScheduleManagementAttachmentDTO.SchedulemId ?? 0) <= 0 ? -this.SessionCurrUserId : ScheduleManagementAttachmentDTO.SchedulemId;
            saveInfo.FolderName = ScheduleManagementAttachmentDTO.FolderName;
            _IScheduleManagementAttachmentService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改进度管理附件", OptionLogTypeEnum.Update, "修改进度管理附件", true)]
        public IActionResult UpdateSave(ScheduleManagementAttachmentDTO ScheduleManagementAttachmentDTO)
        {
            if (ScheduleManagementAttachmentDTO.Id > 0)
            {
                var updateinfo = _IScheduleManagementAttachmentService.Find(ScheduleManagementAttachmentDTO.Id);
                var updatedata = _IMapper.Map(ScheduleManagementAttachmentDTO, updateinfo);
                updateinfo.Path = "Uploads/" + ScheduleManagementAttachmentDTO.FolderName + "/" + ScheduleManagementAttachmentDTO.GuidFileName;
                updateinfo.FolderName = ScheduleManagementAttachmentDTO.FolderName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IScheduleManagementAttachmentService.Update(updatedata);
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
            _IScheduleManagementAttachmentService.Delete(Ids);
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
                Data = _IScheduleManagementAttachmentService.ShowView(Id)


            });
        }
    }
}
