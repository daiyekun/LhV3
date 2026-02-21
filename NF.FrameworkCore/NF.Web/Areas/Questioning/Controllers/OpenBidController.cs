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

namespace NF.Web.Areas
{
    [Area("Questioning")]
    [Route("Questioning/[controller]/[action]")]
    public class OpenBidController : NfBaseController
    {
        public IMapper _IMapper;
        //询价开标
        public IOpenBidService _IOpenBidService;
        public OpenBidController(IMapper IMapper, IOpenBidService IOpenBidService)
        {
            _IMapper = IMapper;
            _IOpenBidService = IOpenBidService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<OpenBid>();
            var predicateAnd = PredicateBuilder.True<OpenBid>();
            var predicateOr = PredicateBuilder.False<OpenBid>();
            predicateOr = predicateOr.Or(a => a.QuesId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.QuesId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IOpenBidService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立开标情况", OptionLogTypeEnum.Add, "建立开标情况", true)]
        public IActionResult SaveKbqk(int contId, OpenBidDTO OpenBid)
        {
                var saveInfo = _IMapper.Map<OpenBid>(OpenBid);
                saveInfo.Name = "新建开标情况";
                saveInfo.Unit = 0;
                saveInfo.TotalPrices = 0;
                saveInfo.UnitPrice = 0;
                saveInfo.Personnel = this.SessionCurrUserId;
                saveInfo.IsDelete = 0;
                saveInfo.QuesId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
                var dic = _IOpenBidService.Add(saveInfo);

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
        public IActionResult SaveData(int contId, IList<OpenBidDTO> subs)
        {

            _IOpenBidService.AddSave(subs, contId);
            return GetResult();
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _IOpenBidService.Delete(Ids);
            return GetResult();
        }
    }
}