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
    public class ProjDescriptionController : NfBaseController
    {
        private IProjDescriptionService _IProjDescriptionService;
        private IMapper _IMapper;
        public ProjDescriptionController(IProjDescriptionService IProjDescriptionService, IMapper IMapper)
        {
            _IProjDescriptionService = IProjDescriptionService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int projectId)
        {
            var pageInfo = new NoPageInfo<Model.Models.ProjDescription>();
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjDescription>();
            var predicateOr = PredicateBuilder.False<Model.Models.ProjDescription>();
            predicateOr = predicateOr.Or(a => a.ProjectId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.ProjectId == projectId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IProjDescriptionService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(ProjDescriptionDTO ProjDescriptionDTO)
        {

            var saveInfo = _IMapper.Map<ProjDescription>(ProjDescriptionDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            if ((ProjDescriptionDTO.ProjectId ?? 0) <= 0)
            {
                saveInfo.ProjectId = -this.SessionCurrUserId;
            }
            else
            {
                saveInfo.ProjectId = ProjDescriptionDTO.ProjectId;
            }
            _IProjDescriptionService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateSave(ProjDescriptionDTO ProjDescriptionDTO)
        {
            if (ProjDescriptionDTO.Id > 0)
            {
                var updateinfo = _IProjDescriptionService.Find(ProjDescriptionDTO.Id);
                var updatedata = _IMapper.Map(ProjDescriptionDTO, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IProjDescriptionService.Update(updatedata);
            }

            return GetResult();

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IProjDescriptionService.Delete(Ids);
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
                Data = _IProjDescriptionService.ShowView(Id)


            });
        }
    }
}