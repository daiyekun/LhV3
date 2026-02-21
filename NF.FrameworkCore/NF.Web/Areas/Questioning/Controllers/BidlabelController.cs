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
    [Area("Questioning")]
    [Route("Questioning/[controller]/[action]")]
    public class BidlabelController : NfBaseController
    {
        public IMapper _IMapper;
        public IBidlabelService _IBidlabelService;
        public IOpenBidService _IOpenBidService;
        public IQuestioningService _IQuestioningService;
        public BidlabelController(IMapper IMapper, IBidlabelService IBidlabelService, IOpenBidService IOpenBidService, IQuestioningService IQuestioningService)
        {
            _IMapper = IMapper;
            _IBidlabelService = IBidlabelService;
            _IOpenBidService = IOpenBidService;
            _IQuestioningService = IQuestioningService;
        }
        /// <summary>
        /// 新建中标人
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立中标人", OptionLogTypeEnum.Add, "建立中标人", true)]
        public IActionResult SaveKbr(int contId, BidlabelDTO Bidlabel)
        {
            var saveInfo = _IMapper.Map<Bidlabel>(Bidlabel);
            saveInfo.Name = "名称";
            saveInfo.WinningUnit = 0;
            saveInfo.BidPrices = 0;
            saveInfo.BidPrice = 0;
            saveInfo.BidUser = this.SessionCurrUserId;
            saveInfo.Bidlabel1 = 0;
            saveInfo.QuesId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
            _IBidlabelService.Add(saveInfo);
            return GetResult();

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<Bidlabel>();
            var predicateAnd = PredicateBuilder.True<Bidlabel>();
            var predicateOr = PredicateBuilder.False<Bidlabel>();
            predicateOr = predicateOr.Or(a => a.QuesId == -this.SessionCurrUserId && a.Bidlabel1 == 0);
            predicateOr = predicateOr.Or(a => a.QuesId == contId && a.Bidlabel1 == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IBidlabelService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _IBidlabelService.Delete(Ids);
            return GetResult();
        }
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetZbList(int contId, int? limit, int? page, string keyWord)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<OpenBid>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<OpenBid>();
            var predicateOr = PredicateBuilder.False<OpenBid>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1 && (a.QuesId == contId || a.QuesId == -this.SessionCurrUserId));//表示没有删除的数据
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IBidlabelService.GetList(pageInfo, predicateAnd);
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
        public IActionResult SaveData(int contId, IList<BidlabelDTO> subs)
        {
            //var chkres = 0;//CheckContSave(subs, contId, -this.SessionCurrUserId);
            //if (Convert.ToInt32(chkres.RetValue) > 0)
            //{
            //    return GetResult(chkres);
            //}
            //else
            //{
            _IBidlabelService.AddSave(subs, contId);
            return GetResult();
            //}

        }
        /// <summary>
        /// 洽谈情况确定到洽谈单位
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveBZData(int contId)
        {
            var updateinfo = _IOpenBidService.Find(contId);
            BidlabelDTO info = new BidlabelDTO();
            #region 将开标情况 单位 总价添加到招标表
            var QuestioningDB = _IQuestioningService.Find(updateinfo.QuesId ?? 0);
            if (QuestioningDB != null)
            {
                QuestioningDB.Zbdw = updateinfo.Unit == 0 ? 0 : updateinfo.Unit;
                QuestioningDB.Zje = updateinfo.TotalPrices;
                _IQuestioningService.Update(QuestioningDB);
            }
            #endregion
            //查询单位是否有值
            int Count = _IBidlabelService.GetQueryable(p => p.QuesId == updateinfo.QuesId && p.Bidlabel1 == 0).Count();
            if (Count == 1)
            {
                var Lable = _IBidlabelService.GetQueryable(p => p.QuesId == updateinfo.QuesId && p.Bidlabel1 == 0).FirstOrDefault();
                var updateinfoss = _IBidlabelService.Find(Lable.Id);
                updateinfoss.Name = updateinfo.Name;
                updateinfoss.WinningUnit = updateinfo.Unit == 0 ? 0 : updateinfo.Id;
                updateinfoss.BidPrices = updateinfo.TotalPrices;
                updateinfoss.BidPrice = updateinfo.UnitPrice;
                updateinfoss.BidUser = updateinfo.Personnel;
                updateinfoss.QuesId = updateinfo.QuesId;
                updateinfoss.Bidlabel1 = 0;
                updateinfoss.Zbdwid = updateinfo.Unit;
                _IBidlabelService.Update(updateinfoss);
            }
            else
            {
                info.Name = updateinfo.Name;
                info.WinningUnit = updateinfo.Unit == 0 ? 0 : updateinfo.Id;
                info.BidPrices = updateinfo.TotalPrices;
                info.BidPrice = updateinfo.UnitPrice;
                info.BidUser = updateinfo.Personnel;
                info.QuesId = updateinfo.QuesId;
                info.Bidlabel1 = 0;
                info.Zbdwid = updateinfo.Unit;
                _IBidlabelService.AddSaveInfro(info);
            }
            return GetResult();
        }
    }
}