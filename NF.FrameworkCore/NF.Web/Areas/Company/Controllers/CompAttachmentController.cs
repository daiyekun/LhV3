using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    /// <summary>
    /// 附件
    /// </summary>
    [Area("Company")]
    [Route("Company/[controller]/[action]")]
    public class CompAttachmentController : NfBaseController
    {
        private ICompAttachmentService _ICompAttachmentService;
        private IMapper _IMapper;

        public CompAttachmentController(ICompAttachmentService ICompAttachmentService, IMapper IMapper)
        {
            _IMapper = IMapper;
            _ICompAttachmentService = ICompAttachmentService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int companyId)
        {
            var pageInfo = new PageInfo<Model.Models.CompAttachment>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.CompAttachment>();
            var predicateOr = PredicateBuilder.False<Model.Models.CompAttachment>();
            predicateOr = predicateOr.Or(a => a.CompanyId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.CompanyId == companyId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _ICompAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(CompAttachmentDTO CompAttachmentDTO)
        {

            var saveInfo = _IMapper.Map<CompAttachment>(CompAttachmentDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + CompAttachmentDTO.FolderName + "/" + CompAttachmentDTO.GuidFileName;
            if ((CompAttachmentDTO.CompanyId ?? 0) <= 0)
            {
                saveInfo.CompanyId = -this.SessionCurrUserId;
            }
            else
            {
                saveInfo.CompanyId = CompAttachmentDTO.CompanyId;
            }


            _ICompAttachmentService.Add(saveInfo);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 新建附件
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateSave(CompAttachmentDTO CompAttachmentDTO)
        {
            if (CompAttachmentDTO.Id > 0)
            {
                var updateinfo = _ICompAttachmentService.Find(CompAttachmentDTO.Id);
                var updatedata = _IMapper.Map(CompAttachmentDTO, updateinfo);
                updateinfo.Path = "Uploads/" + CompAttachmentDTO.FolderName + "/" + CompAttachmentDTO.GuidFileName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _ICompAttachmentService.Update(updatedata);
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
            _ICompAttachmentService.Delete(Ids);
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
                Data = _ICompAttachmentService.ShowView(Id)


            });
        }

        //[EnableCors("AllowSpecificOrigin")]
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public IActionResult Upload(IFormCollection formCollection)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "上传成功",
                Code = 0,


            });
        }

    }
}