using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Tender
{
    /// <summary>
    /// 招标信息
    /// </summary>
    [Area("Tender")]
    [Route("Tender/[controller]/[action]")]
    public class TendererNameLabelController : NfBaseController
    {
        public IMapper _IMapper;
        public ITendererNameLabelService _ITendererNameLabelService;
        public TendererNameLabelController(IMapper IMapper, ITendererNameLabelService ITendererNameLabelService)
        {
            _IMapper = IMapper;
            _ITendererNameLabelService = ITendererNameLabelService;
        }
        public IActionResult Build()
        {
            return View();
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立开标人", OptionLogTypeEnum.Add, "建立开标人", true)]
        public IActionResult SaveKbr(int contId, TendererNameLabelDTO tendererNameLabel)
        {
                var saveInfo = _IMapper.Map<TendererNameLabel>(tendererNameLabel);
                saveInfo.TeNameLabel = "名称";
                saveInfo.Psition = "职位";
                saveInfo.UserId = this.SessionCurrUserId;
                saveInfo.IsDelete = 0;
                saveInfo.TeDartment = 0;
                saveInfo.TenderId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
                _ITendererNameLabelService.Add(saveInfo);
                return GetResult();

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<TendererNameLabel>();
            var predicateAnd = PredicateBuilder.True<TendererNameLabel>();
            var predicateOr = PredicateBuilder.False<TendererNameLabel>();
            predicateOr = predicateOr.Or(a => a.TenderId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.TenderId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _ITendererNameLabelService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _ITendererNameLabelService.Delete(Ids);
            return GetResult();
        }
        /// <summary>
        /// 保存招标人
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(int contId, IList<TendererNameLabelDTO> subs)
        {

            _ITendererNameLabelService.AddSave(subs, contId, _IMapper);
            return GetResult();
        }
    }
}