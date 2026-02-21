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

namespace NF.Web.Areas.Business.Controllers
{
    /// <summary>
    /// 业务品类附件
    /// </summary>
    [Area("Business")]
    [Route("Business/[controller]/[action]")]
    public class BcAttachmentController : NfBaseController
    {
        private IBcAttachmentService _IBcAttachmentService;
        private IMapper _IMapper;
        public BcAttachmentController(IBcAttachmentService IBcAttachmentService
            , IMapper IMapper)
        {
            _IBcAttachmentService = IBcAttachmentService;
            _IMapper = IMapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Build()
        {
            return View();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="bcId">单品ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int bId)
        {
            var pageInfo = new PageInfo<Model.Models.BcAttachment>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.BcAttachment>();
            var predicateOr = PredicateBuilder.False<Model.Models.BcAttachment>();
            predicateOr = predicateOr.Or(a => a.BcId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.BcId == bId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IBcAttachmentService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建附件
        /// </summary>
        /// <returns></returns>
        public IActionResult Save(BcAttachmentDTO BcAttachmentDTO)
        {

            
            
                var saveInfo = _IMapper.Map<BcAttachment>(BcAttachmentDTO);
                saveInfo.CreateDateTime = DateTime.Now;
                saveInfo.ModifyDateTime = DateTime.Now;
                saveInfo.CreateUserId = this.SessionCurrUserId;
                saveInfo.ModifyUserId = this.SessionCurrUserId;
                saveInfo.IsDelete = 0;
                saveInfo.Path = "Uploads/" + BcAttachmentDTO.FolderName + "/" + BcAttachmentDTO.GuidFileName;
                if ((BcAttachmentDTO.BcId ?? 0) <= 0)
                {
                    saveInfo.BcId = -this.SessionCurrUserId;
                }
                else
                {
                    saveInfo.BcId = BcAttachmentDTO.BcId;
                }


                _IBcAttachmentService.Add(saveInfo);

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
        public IActionResult UpdateSave(BcAttachmentDTO BcAttachmentDTO)
        {
            if (BcAttachmentDTO.Id > 0)
            {
                var updateinfo = _IBcAttachmentService.Find(BcAttachmentDTO.Id);
                var updatedata = _IMapper.Map(BcAttachmentDTO, updateinfo);
                updateinfo.Path = "Uploads/" + BcAttachmentDTO.FolderName + "/" + BcAttachmentDTO.GuidFileName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IBcAttachmentService.Update(updatedata);
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
            _IBcAttachmentService.Delete(Ids);
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
                Data = _IBcAttachmentService.ShowView(Id)


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