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

namespace NF.Web.Areas.Contract.Controllers
{
    /// <summary>
    /// 合同说明
    /// </summary>
    [Area("Contract")]
    [Route("Contract/[controller]/[action]")]
    public class ContDescriptionController : NfBaseController
    {

        private IContDescriptionService _IContDescriptionService;
        private IMapper _IMapper;
        public ContDescriptionController(IContDescriptionService IContDescriptionService, IMapper IMapper)
        {
            _IContDescriptionService = IContDescriptionService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<ContDescription>();
            var predicateAnd = PredicateBuilder.True<ContDescription>();
            var predicateOr = PredicateBuilder.False<ContDescription>();
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ContId == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IContDescriptionService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(ContDescriptionDTO ContDescriptionDTO)
        {

            var saveInfo = _IMapper.Map<ContDescription>(ContDescriptionDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.ContId = (ContDescriptionDTO.ContId ?? 0) <= 0 ? -this.SessionCurrUserId : ContDescriptionDTO.ContId;
            _IContDescriptionService.Add(saveInfo);

            return GetResult();

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateSave(ContDescriptionDTO ContDescriptionDTO)
        {
            if (ContDescriptionDTO.Id > 0)
            {
                var updateinfo = _IContDescriptionService.Find(ContDescriptionDTO.Id);
                var updatedata = _IMapper.Map(ContDescriptionDTO, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IContDescriptionService.Update(updatedata);
            }

            return GetResult();

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IContDescriptionService.Delete(Ids);
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
                Data = _IContDescriptionService.ShowView(Id)


            });
        }
    }
}