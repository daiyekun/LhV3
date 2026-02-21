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
    /// 实际资金附件
    /// </summary>
    [Area("Finance")]
    [Route("Finance/[controller]/[action]")]
    public class ActFinceFileController : NfBaseController
    {
        private IActFinceFileService _IActFinceFileService;
        private IMapper _IMapper;

        public ActFinceFileController(IActFinceFileService IActFinceFileService, IMapper IMapper)
        {
            _IActFinceFileService = IActFinceFileService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="finceId">资金ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int finceId)
        {
            var pageInfo = new NoPageInfo<Model.Models.ActFinceFile>();
            var predicateAnd = PredicateBuilder.True<Model.Models.ActFinceFile>();
            var predicateOr = PredicateBuilder.False<Model.Models.ActFinceFile>();
            predicateOr = predicateOr.Or(a => a.ActId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.ActId == finceId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IActFinceFileService.GetList(pageInfo, predicateAnd, a => a.Id, false);
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
        public IActionResult Save(ActFinceFileDTO ActFinceFileDTO)
        {

            var saveInfo = _IMapper.Map<ActFinceFile>(ActFinceFileDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Path = "Uploads/" + ActFinceFileDTO.FolderName + "/" + ActFinceFileDTO.GuidFileName;
            saveInfo.ActId = (ActFinceFileDTO.ActId ?? 0) <= 0 ? -this.SessionCurrUserId : ActFinceFileDTO.ActId;

            _IActFinceFileService.Add(saveInfo);

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
        public IActionResult UpdateSave(ActFinceFileDTO ActFinceFileDTO)
        {
            if (ActFinceFileDTO.Id > 0)
            {
                var updateinfo = _IActFinceFileService.Find(ActFinceFileDTO.Id);
                var updatedata = _IMapper.Map(ActFinceFileDTO, updateinfo);
                updateinfo.Path = "Uploads/" + ActFinceFileDTO.FolderName + "/" + ActFinceFileDTO.GuidFileName;
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IActFinceFileService.Update(updatedata);
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
            _IActFinceFileService.Delete(Ids);
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
                Data = _IActFinceFileService.ShowView(Id)


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
