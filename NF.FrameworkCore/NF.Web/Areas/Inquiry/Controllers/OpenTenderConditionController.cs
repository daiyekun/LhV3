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
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Inquiry
{
    [Area("Inquiry")]
    [Route("Inquiry/[controller]/[action]")]
    public class OpenTenderConditionController  : NfBaseController
    {
        public IMapper _IMapper;
        //询价开标
        public IOpenTenderConditionService _IOpenTenderConditionService;
        public OpenTenderConditionController(IMapper IMapper, IOpenTenderConditionService IOpenTenderConditionService)
        {
            _IMapper = IMapper;
            _IOpenTenderConditionService = IOpenTenderConditionService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<OpenTenderCondition>();
            var predicateAnd = PredicateBuilder.True<OpenTenderCondition>();
            var predicateOr = PredicateBuilder.False<OpenTenderCondition>();
            predicateOr = predicateOr.Or(a => a.LnquiryId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.LnquiryId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IOpenTenderConditionService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立开标情况", OptionLogTypeEnum.Add, "建立开标情况", true)]
        public IActionResult SaveKbqk(int contId, OpenTenderConditionDTO OpenTenderCondition)
        {
            var saveInfo = _IMapper.Map<OpenTenderCondition>(OpenTenderCondition);
            saveInfo.Name = "新建询价情况";
            saveInfo.Lxr = "";
            saveInfo.Lxfs = "";
            saveInfo.Unit = 0;
            saveInfo.TotalPrices = 0;
            saveInfo.UnitPrice = 0;
            saveInfo.Personnel = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.LnquiryId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
            
            var dic = _IOpenTenderConditionService.Add(saveInfo);

            return GetResult(new RequstResult
            {
                Code = 0,
                Msg = "操作成功",
                Data = dic

            });


        }
        /// <summary>
        /// 保存标的
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(int contId, IList<OpenTenderConditionDTO> subs)
        {

            _IOpenTenderConditionService.AddSave(subs, contId);
            return GetResult();
        }
        //保存到中标单位
        public IActionResult SaveZb(int contId, IList<OpenTenderConditionDTO> subs)
        {

            _IOpenTenderConditionService.AddSaves(subs, contId);
            return GetResult();
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _IOpenTenderConditionService.Delete(Ids);
            return GetResult();
        }
    }
}