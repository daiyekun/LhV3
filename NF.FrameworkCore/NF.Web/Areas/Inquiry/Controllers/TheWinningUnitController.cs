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

namespace NF.Web.Areas
{
    /// <summary>
    /// 招标信息
    /// </summary>
    [Area("Inquiry")]
    [Route("Inquiry/[controller]/[action]")]
    public class TheWinningUnitController : NfBaseController
    {
        public IMapper _IMapper;
        public ITheWinningUnitService _ITheWinningUnitService;
        public IOpenTenderConditionService _IOpenTenderConditionService;
        public IInquiryService _IInquiryService;
        public TheWinningUnitController(IMapper IMapper
            , ITheWinningUnitService ITheWinningUnitService
            , IOpenTenderConditionService IOpenTenderConditionService,
            IInquiryService IInquiryService)
        {
            _IMapper = IMapper;
            _ITheWinningUnitService = ITheWinningUnitService;
            _IOpenTenderConditionService = IOpenTenderConditionService;
            _IInquiryService = IInquiryService;
        }

        /// <summary>
        /// 新建中标人
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立中标人", OptionLogTypeEnum.Add, "建立中标人", true)]
        public IActionResult SaveKbr(int contId, TheWinningUnitDTO TheWinningUnit)
        {
            var saveInfo = _IMapper.Map<TheWinningUnit>(TheWinningUnit);
            saveInfo.Name = "名称";
            saveInfo.WinningUnit = 0;
            saveInfo.BidPrices = 0;
            saveInfo.BidPrice = 0;
            saveInfo.BidUser = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.LnquiryId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
            _ITheWinningUnitService.Add(saveInfo);
            return GetResult();

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<TheWinningUnit>();
            var predicateAnd = PredicateBuilder.True<TheWinningUnit>();
            var predicateOr = PredicateBuilder.False<TheWinningUnit>();
            predicateOr = predicateOr.Or(a => a.LnquiryId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.LnquiryId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _ITheWinningUnitService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _ITheWinningUnitService.Delete(Ids);
            return GetResult();
        }
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetZbList(int contId, int? limit, int? page, string keyWord)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<OpenTenderCondition>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<OpenTenderCondition>();
            var predicateOr = PredicateBuilder.False<OpenTenderCondition>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1 && (a.LnquiryId == contId || a.LnquiryId == -this.SessionCurrUserId));//表示没有删除的数据
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _ITheWinningUnitService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 条件判断
        /// </summary>
        /// <param name="saveInfo"></param>
        /// <returns></returns>
        //private RequstResult CheckContSave(IList<TheWinningUnitDTO> saveInfo, int contId, int UserId)
        //{
        //    var requstResult = new RequstResult()
        //    {
        //        Msg = "操作成功",
        //        Code = 0,
        //        RetValue = 0,
        //    };
        //    if (saveInfo.Count > 1)
        //    {
        //        int UnId = 0;
        //        for (int i = 0; i < saveInfo.Count; i++)
        //        {
        //            foreach (var item in saveInfo)
        //            {
        //                if (item.SuccessUntiId != 0)
        //                {
        //                    if (i == 0)
        //                    { UnId = item.SuccessUntiId; }
        //                    else if (i > 0)
        //                    {
        //                        if (item.SuccessUntiId != UnId)
        //                        {
        //                            requstResult.RetValue = 1;
        //                            requstResult.Msg = "开标单位必须相同！";
        //                            UnId = item.SuccessUntiId;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    requstResult.RetValue = 2;
        //                    requstResult.Msg = "不可提交无效数据！";
        //                    return requstResult;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        var queryplance = _ITheWinningUnitService.GetQueryable(a => a.LnquiryId == -this.SessionCurrUserId || a.LnquiryId == contId && a.IsDelete == 0);
        //        var succesid = queryplance.FirstOrDefault();
        //        if (succesid != null)
        //        {
        //            if (saveInfo.FirstOrDefault().SuccessUntiId != succesid.WinningUnit && succesid.WinningUnit != 1)
        //            {
        //                requstResult.RetValue = 1;
        //                requstResult.Msg = "开标单位必须相同！";
        //            }
        //            ;
        //        }

        //    }
        //    return requstResult;
        //}
        /// <summary>
        /// 保存中标单位
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(int contId, IList<TheWinningUnitDTO> subs)
        {
            //var chkres = 0;//CheckContSave(subs, contId, -this.SessionCurrUserId);
            //if (Convert.ToInt32(chkres.RetValue) > 0)
            //{
            //    return GetResult(chkres);
            //}
            //else
            //{
                _ITheWinningUnitService.AddSave(subs, contId);
                return GetResult();
            //}

        }
        /// <summary>
        /// 保存标的
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveBZData(int contId)
        {
            var updateinfo = _IOpenTenderConditionService.Find(contId);
            TheWinningUnitDTO info = new TheWinningUnitDTO();
            #region 将开标情况 单位 总价添加到招标表
            var InquiryDB = _IInquiryService.Find(updateinfo.LnquiryId??0);
            if (InquiryDB != null)
            {
                InquiryDB.Zbdw = updateinfo.Unit == 0 ? 0 : updateinfo.Unit;
                InquiryDB.Zje = updateinfo.TotalPrices;
                _IInquiryService.Update(InquiryDB);
            }
            #endregion
            //查询单位是否有值
            int Count = _ITheWinningUnitService.GetQueryable(p => p.LnquiryId == updateinfo.LnquiryId && p.IsDelete == 0).Count();
            if (Count == 1)
            {
                var Lable = _ITheWinningUnitService.GetQueryable(p => p.LnquiryId == updateinfo.LnquiryId && p.IsDelete == 0).FirstOrDefault();
                var updateinfoss = _ITheWinningUnitService.Find(Lable.Id);
                updateinfoss.Name = updateinfo.Name;
                updateinfoss.Zbdwid = updateinfo.Unit;
                updateinfoss.WinningUnit = updateinfo.Unit == 0 ? 0 : updateinfo.Id;
                updateinfoss.BidPrices = updateinfo.TotalPrices;//总价
                updateinfoss.BidPrice = updateinfo.UnitPrice;//单价
                updateinfoss.BidUser = updateinfo.Personnel;//人员
                updateinfoss.LnquiryId = updateinfo.LnquiryId;//开标id
                updateinfoss.Lxr = updateinfo.Lxr;
                updateinfoss.Lxfs = updateinfo.Lxfs;
                updateinfoss.IsDelete = 0;
                _ITheWinningUnitService.Update(updateinfoss);
            }
            else
            {
                info.Name = updateinfo.Name;
                info.Zbdwid = updateinfo.Unit;
                info.WinningUnit = updateinfo.Unit == 0 ? 0 : updateinfo.Id;
                info.BidPrices = updateinfo.TotalPrices;//总价
                info.BidPrice = updateinfo.UnitPrice;//单价
                info.BidUser = updateinfo.Personnel;//人员
                info.LnquiryId = updateinfo.LnquiryId;//开标id
                info.Lxr = updateinfo.Lxr;
                info.Lxfs = updateinfo.Lxfs;
                info.IsDelete = 0;
                _ITheWinningUnitService.SaveBZData(info);
            }

            return GetResult();
        }

    }
}