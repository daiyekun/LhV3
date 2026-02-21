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
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.Project.Controllers
{
    [Area("Project")]
    [Route("Project/[controller]/[action]")]
    public class ProjScheduleController : NfBaseController
    {
        private IProjScheduleService _IProjScheduleService;
        private IMapper _IMapper;
        public ProjScheduleController(IProjScheduleService IProjScheduleService, IMapper IMapper)
        {
            _IProjScheduleService = IProjScheduleService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int projectId)
        {
            var pageInfo = new NoPageInfo<Model.Models.ProjSchedule>();
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjSchedule>();
            var predicateOr = PredicateBuilder.False<Model.Models.ProjSchedule>();
            predicateOr = predicateOr.Or(a => a.ProjectId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.ProjectId == projectId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IProjScheduleService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        /// 新建
        /// </summary>
        /// <returns></returns>
        public IActionResult Save(ProjScheduleDTO ProjScheduleDTO)
        {

            var saveInfo = _IMapper.Map<ProjSchedule>(ProjScheduleDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            if ((ProjScheduleDTO.ProjectId ?? 0) <= 0)
            {
                saveInfo.ProjectId = -this.SessionCurrUserId;
            }
            else
            {
                saveInfo.ProjectId = ProjScheduleDTO.ProjectId;
            }
            _IProjScheduleService.Add(saveInfo);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateSave(ProjScheduleDTO ProjScheduleDTO)
        {
            if (ProjScheduleDTO.Id > 0)
            {
                var updateinfo = _IProjScheduleService.Find(ProjScheduleDTO.Id);
                var updatedata = _IMapper.Map(ProjScheduleDTO, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IProjScheduleService.Update(updatedata);
            }

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IProjScheduleService.Delete(Ids);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0,


            });
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
                Data = _IProjScheduleService.ShowView(Id)


            });
        }
    }
}