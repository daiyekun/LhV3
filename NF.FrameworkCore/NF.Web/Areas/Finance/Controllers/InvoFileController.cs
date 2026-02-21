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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.Finance.Controllers
{
    /// <summary>
    /// 发票附件
    /// </summary>
    [Area("Finance")]
    [Route("Finance/[controller]/[action]")]
    public class InvoFileController : NfBaseController
    {
        private IInvoFileService _IInvoFileService;
        private IMapper _IMapper;

        public InvoFileController(IInvoFileService IInvoFileService, IMapper IMapper)
        {
            _IInvoFileService = IInvoFileService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="projectId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int finceId)
        {
            var pageInfo = new NoPageInfo<Model.Models.InvoFile>();
            var predicateAnd = PredicateBuilder.True<Model.Models.InvoFile>();
            var predicateOr = PredicateBuilder.False<Model.Models.InvoFile>();
            predicateOr = predicateOr.Or(a => a.InvoId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.InvoId == finceId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IInvoFileService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(InvoFileDTO InvoFileDTO)
        {

            var saveInfo = _IMapper.Map<InvoFile>(InvoFileDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + InvoFileDTO.FolderName + "/" + InvoFileDTO.GuidFileName;
            saveInfo.InvoId = (InvoFileDTO.InvoId ?? 0) <= 0 ? -this.SessionCurrUserId : InvoFileDTO.InvoId;

            _IInvoFileService.Add(saveInfo);

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
        public IActionResult UpdateSave(InvoFileDTO InvoFileDTO)
        {
            if (InvoFileDTO.Id > 0)
            {
                var updateinfo = _IInvoFileService.Find(InvoFileDTO.Id);
                var updatedata = _IMapper.Map(InvoFileDTO, updateinfo);
                updateinfo.Path = "Uploads/" + InvoFileDTO.FolderName + "/" + InvoFileDTO.GuidFileName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IInvoFileService.Update(updatedata);
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
            _IInvoFileService.Delete(Ids);
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
                Data = _IInvoFileService.ShowView(Id)


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
