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
    public class ScheduleListController : NfBaseController
    {
        public IMapper _IMapper;
        public IScheduleListService _IScheduleListService;
        public ScheduleListController(IMapper IMapper, IScheduleListService IScheduleListService)
        {
            _IMapper = IMapper;
            _IScheduleListService = IScheduleListService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<ScheduleList>();
            var predicateAnd = PredicateBuilder.True<ScheduleList>();
            var predicateOr = PredicateBuilder.False<ScheduleList>();
            predicateOr = predicateOr.Or(a => a.ScheduleId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ScheduleId == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IScheduleListService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(ScheduleListDTO ScheduleListDTO)
        {

            var saveInfo = _IMapper.Map<ScheduleList>(ScheduleListDTO);

            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.IsDelete = 0;
            saveInfo.Mystate = 0;
            saveInfo.ScheduleId= (ScheduleListDTO.ScheduleId ?? 0) <= 0 ? -this.SessionCurrUserId : ScheduleListDTO.ScheduleId;
            _IScheduleListService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改进度管理附件", OptionLogTypeEnum.Update, "修改进度管理附件", true)]
        public IActionResult UpdateSave(ScheduleListDTO ScheduleListDTO)
        {

            if (ScheduleListDTO.Id > 0)
            {
                var updateinfo = _IScheduleListService.Find(ScheduleListDTO.Id);
                var updatedata = _IMapper.Map(ScheduleListDTO, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;

                _IScheduleListService.Update(updatedata);
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
            _IScheduleListService.Delete(Ids);
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
                Data = _IScheduleListService.ShowView(Id)


            });
        }
    }
}
