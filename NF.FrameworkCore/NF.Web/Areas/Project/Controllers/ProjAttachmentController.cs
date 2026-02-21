using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

namespace NF.Web.Areas.Project.Controllers
{
    [Area("Project")]
    [Route("Project/[controller]/[action]")]
    public class ProjAttachmentController : NfBaseController
    {
        private IProjAttachmentService _IProjAttachmentService;
        private IMapper _IMapper;

        public ProjAttachmentController(IProjAttachmentService IProjAttachmentService, IMapper IMapper)
        {
            _IProjAttachmentService = IProjAttachmentService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int projectId)
        {
            var pageInfo = new NoPageInfo<Model.Models.ProjAttachment>();
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjAttachment>();
            var predicateOr = PredicateBuilder.False<Model.Models.ProjAttachment>();
            predicateOr = predicateOr.Or(a => a.ProjectId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.ProjectId == projectId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IProjAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(ProjAttachmentDTO ProjAttachmentDTO)
        {

            var saveInfo = _IMapper.Map<ProjAttachment>(ProjAttachmentDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + ProjAttachmentDTO.FolderName + "/" + ProjAttachmentDTO.GuidFileName;
            saveInfo.ProjectId = (ProjAttachmentDTO.ProjectId ?? 0) <= 0 ? -this.SessionCurrUserId : ProjAttachmentDTO.ProjectId;

            _IProjAttachmentService.Add(saveInfo);

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
        public IActionResult UpdateSave(ProjAttachmentDTO ProjAttachmentDTO)
        {
            if (ProjAttachmentDTO.Id > 0)
            {
                var updateinfo = _IProjAttachmentService.Find(ProjAttachmentDTO.Id);
                var updatedata = _IMapper.Map(ProjAttachmentDTO, updateinfo);
                updateinfo.Path = "Uploads/" + ProjAttachmentDTO.FolderName + "/" + ProjAttachmentDTO.GuidFileName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IProjAttachmentService.Update(updatedata);
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
            _IProjAttachmentService.Delete(Ids);
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
                Data = _IProjAttachmentService.ShowView(Id)


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