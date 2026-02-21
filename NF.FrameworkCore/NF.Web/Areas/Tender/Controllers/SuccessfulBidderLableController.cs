using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace NF.Web.Areas.Tender.Controllers
{
    /// <summary>
    /// 招标信息
    /// </summary>
    [Area("Tender")]
    [Route("Tender/[controller]/[action]")]
    public class SuccessfulBidderLableController : NfBaseController
    {
        public IMapper _IMapper;
        public ITenderInforService _ITenderInforService;
        public ISuccessfulBidderLableService _ISuccessfulBidderLableService;
        public IOpeningSituationInforService _IOpeningSituationInforService;
        public SuccessfulBidderLableController(IMapper IMapper,
            ISuccessfulBidderLableService ISuccessfulBidderLableService,
            IOpeningSituationInforService IOpeningSituationInforService,
            ITenderInforService ITenderInforService)
        {
            _IMapper = IMapper;
            _ITenderInforService = ITenderInforService;
            _ISuccessfulBidderLableService = ISuccessfulBidderLableService;
            _IOpeningSituationInforService = IOpeningSituationInforService;
        }

        /// <summary>
        /// 新建中标人
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立中标人", OptionLogTypeEnum.Add, "建立中标人", true)]
        public IActionResult SaveKbr(int contId, SuccessfulBidderLableDTO tendererNameLabel)
        {
            var saveInfo = _IMapper.Map<SuccessfulBidderLable>(tendererNameLabel);
            saveInfo.SuccessName = "名称";
            saveInfo.SuccessUntiId = 0;
            saveInfo.SuccTotalPrice = 0;
            saveInfo.SuccUitprice = 0;
            saveInfo.SuccId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.TenderId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
            _ISuccessfulBidderLableService.Add(saveInfo);

            return GetResult();

        }

     
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<SuccessfulBidderLable>();
            var predicateAnd = PredicateBuilder.True<SuccessfulBidderLable>();
            var predicateOr = PredicateBuilder.False<SuccessfulBidderLable>();
            predicateOr = predicateOr.Or(a => a.TenderId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.TenderId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _ISuccessfulBidderLableService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _ISuccessfulBidderLableService.Delete(Ids);
            return GetResult();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetZbList(int contId, int? limit, int? page, string keyWord)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<OpeningSituationInfor>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<OpeningSituationInfor>();
            var predicateOr = PredicateBuilder.False<OpeningSituationInfor>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1 && (a.TenderId == contId || a.TenderId == -this.SessionCurrUserId));//表示没有删除的数据
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.OpenSituationName.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _ISuccessfulBidderLableService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 条件判断
        /// </summary>
        /// <param name="saveInfo"></param>
        /// <returns></returns>
        private RequstResult CheckContSave(IList<SuccessfulBidderLableDTO> saveInfo, int contId, int UserId)
        {
            var requstResult = new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,
                RetValue = 0,
            };
            if (saveInfo.Count > 1)
            {
                int? UnId = 0;
                for (int i = 0; i < saveInfo.Count; i++)
                {
                    foreach (var item in saveInfo)
                    {
                        if (item.SuccessUntiId != 0)
                        {
                            if (i == 0)
                            { UnId = item.SuccessUntiId; }
                            else if (i > 0)
                            {
                                if (item.SuccessUntiId != UnId)
                                {
                                    requstResult.RetValue = 1;
                                    requstResult.Msg = "开标单位必须相同！";
                                    UnId = item.SuccessUntiId;
                                }
                            }
                        }
                        else
                        {
                            requstResult.RetValue = 2;
                            requstResult.Msg = "不可提交无效数据！";
                            return requstResult;
                        }
                    }
                }
            }
            else
            {
                var queryplance = _ISuccessfulBidderLableService.GetQueryable(a => a.TenderId == -this.SessionCurrUserId || a.TenderId == contId && a.IsDelete == 0);
                var succesid = queryplance.FirstOrDefault();
                if (succesid != null)
                {
                    if (saveInfo.FirstOrDefault().SuccessUntiId != succesid.SuccessUntiId && succesid.SuccessUntiId != 1)
                    {
                        requstResult.RetValue = 1;
                        requstResult.Msg = "开标单位必须相同！";
                    }
                    ;
                }

            }
            return requstResult;
        }
        /// <summary>
        /// 保存中标单位
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(int contId, IList<SuccessfulBidderLableDTO> subs)
        {
            //var chkres = CheckContSave(subs, contId, -this.SessionCurrUserId);
            //if (Convert.ToInt32(chkres.RetValue) > 0)
            //{
            //return GetResult(chkres);
            //}
            //else
            //{
            _ISuccessfulBidderLableService.AddSave(subs, contId);
            return GetResult();
            //}

        }
        /// <summary>
        /// 保存招标人添加到中标单位
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveBZData(int contId)
        {
            var updateinfo = _IOpeningSituationInforService.Find(contId);
            SuccessfulBidderLableDTO info = new SuccessfulBidderLableDTO();
            #region 将开标情况 单位 总价添加到招标表
            var TenderDB = _ITenderInforService.Find(updateinfo.TenderId);
            if (TenderDB != null)
            {
                TenderDB.Zbdw = updateinfo.Unit == 0 ? 0 : updateinfo.Unit;
                TenderDB.Zje = updateinfo.TotalPrice;
                _ITenderInforService.Update(TenderDB);
            }
            #endregion
            //查询中标单位是否有值
            int Count = _ISuccessfulBidderLableService.GetQueryable(p => p.TenderId == updateinfo.TenderId && p.IsDelete == 0).Count();
            if (Count==1)
            {
                var Lable=_ISuccessfulBidderLableService.GetQueryable(p => p.TenderId == updateinfo.TenderId && p.IsDelete == 0).FirstOrDefault();
                var updateinfoss = _ISuccessfulBidderLableService.Find(Lable.Id);
                updateinfoss.SuccessName = updateinfo.OpenSituationName;
                updateinfoss.TenderId = updateinfo.TenderId;
                updateinfoss.SuccessUntiId = updateinfo.Unit == 0 ? 0 : updateinfo.Id;
                updateinfoss.SuccTotalPrice = updateinfo.TotalPrice;//总价
                updateinfoss.SuccUitprice = updateinfo.Uitprice;//单价
                updateinfoss.SuccId = updateinfo.UserId;//人员
                updateinfoss.IsDelete = 0;
                _ISuccessfulBidderLableService.Update(updateinfoss);
            }
            else
            {
                info.SuccessName = updateinfo.OpenSituationName;
                info.TenderId = updateinfo.TenderId;
                info.SuccessUntiId = updateinfo.Unit == 0 ? 0 : updateinfo.Id;
                info.SuccTotalPrice = updateinfo.TotalPrice;//总价
                info.SuccUitprice = updateinfo.Uitprice;//单价
                info.SuccId = updateinfo.UserId;//人员
                info.IsDelete = 0;
                _ISuccessfulBidderLableService.AddSaveInfro(info);
            }
            
            
            return GetResult();
        }

    }
}