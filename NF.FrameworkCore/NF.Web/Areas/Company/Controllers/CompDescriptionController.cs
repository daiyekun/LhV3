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

namespace NF.Web.Areas.Company.Controllers
{
    [Area("Company")]
    [Route("Company/[controller]/[action]")]
    public class CompDescriptionController : NfBaseController
    {
        private ICompDescriptionService _ICompDescriptionService;
        private IMapper _IMapper;
        public CompDescriptionController(ICompDescriptionService ICompDescriptionService, IMapper IMapper)
        {
            _ICompDescriptionService = ICompDescriptionService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int companyId)
        {
            var pageInfo = new PageInfo<Model.Models.CompDescription>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.CompDescription>();
            var predicateOr = PredicateBuilder.False<Model.Models.CompDescription>();
            predicateOr = predicateOr.Or(a => a.CompanyId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.CompanyId == companyId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _ICompDescriptionService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        /// 新建联系人
        /// </summary>
        /// <returns></returns>
        public IActionResult Save(CompDescriptionDTO CompDescriptionDTO)
        {

            var saveInfo = _IMapper.Map<CompDescription>(CompDescriptionDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            if ((CompDescriptionDTO.CompanyId ?? 0) <= 0)
            {
                saveInfo.CompanyId = -this.SessionCurrUserId;
            }
            else
            {
                saveInfo.CompanyId = CompDescriptionDTO.CompanyId;
            }
            _ICompDescriptionService.Add(saveInfo);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 新建联系人
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateSave(CompDescriptionDTO CompDescriptionDTO)
        {
            if (CompDescriptionDTO.Id > 0)
            {
                var updateinfo = _ICompDescriptionService.Find(CompDescriptionDTO.Id);
                var updatedata = _IMapper.Map(CompDescriptionDTO, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _ICompDescriptionService.Update(updatedata);
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
            _ICompDescriptionService.Delete(Ids);
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
                Data = _ICompDescriptionService.ShowView(Id)


            });
        }
    }
}