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
    /// <summary>
    /// 其他联系人
    /// </summary>
    public class CompContactController : NfBaseController
    {
        private ICompContactService _ICompContactService;
        private IMapper _IMapper { get; set; }

        public CompContactController(ICompContactService ICompContactService, IMapper IMapper){
            _IMapper = IMapper;
            _ICompContactService = ICompContactService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam,int companyId)
        {
            var pageInfo = new PageInfo<Model.Models.CompContact>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.CompContact>();
            var predicateOr = PredicateBuilder.False<Model.Models.CompContact>();
            predicateOr = predicateOr.Or(a => a.CompanyId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.CompanyId == companyId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _ICompContactService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(CompContactDTO compContactDTO)
        { 
           
          var saveInfo = _IMapper.Map<CompContact>(compContactDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            if ((compContactDTO.CompanyId ?? 0) <= 0)
            {
                saveInfo.CompanyId = -this.SessionCurrUserId;
            }
            else
            {
                saveInfo.CompanyId = compContactDTO.CompanyId;
            }
            
            
            _ICompContactService.Add(saveInfo);

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
        public IActionResult UpdateSave(CompContactDTO compContactDTO)
        {
            if (compContactDTO.Id > 0)
            {
                var updateinfo = _ICompContactService.Find(compContactDTO.Id);
                var updatedata = _IMapper.Map(compContactDTO, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _ICompContactService.Update(updatedata);
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
            _ICompContactService.Delete(Ids);
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
                Data = _ICompContactService.ShowView(Id)


            });
        }

    }
}