using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Utility;

namespace NF.Web.Areas.MobileApp.Controllers
{
    [Area("MobileApp")]
    [Route("MobileApp/[controller]/[action]")]
    public class APPworkflowlistController : Controller
    {
        /// <summary>
        /// 审批
        /// </summary>
        private IAPPWOrkList _IAPPWOrkList;

        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;
        public APPworkflowlistController(IAPPWOrkList IAPPWOrkList, IRemindService IRemindService)
        {
            _IAPPWOrkList = IAPPWOrkList;
            _IRemindService = IRemindService;
        }


        /// <summary>
        /// 待处理
        /// </summary>
        /// <returns></returns>
        public IActionResult workflowlist(APPwork pageParam)
        {
            //var pageInfo = new PageInfo<AppInst>(pageIndex: pageParam.start, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            var start = pageParam.start != null ? Convert.ToInt32(pageParam.start) : 0;
            var limit = pageParam.limit != 0 ? Convert.ToInt32(pageParam.limit) : 20;
            if (!string.IsNullOrEmpty(pageParam.keyword))
            {
                predicateOr = predicateOr.Or(a => a.AppObjName.Contains(pageParam.keyword));
                predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(pageParam.keyword));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            int type = -1;
            //根据类型搜索
            switch (pageParam.wftype)
            {

                case "客户":
                    type = 0;
                    break;
                case "供应商":
                    type = 1;
                    break;
                case "其他对方":
                    type = 2;
                    break;
                case "合同":
                    type = 3;
                    break;
                case "收票":
                    type = 4;
                    break;
                case "开票":
                    type = 5;
                    break;
                case "付款":
                    type = 6;
                    break;
                case "项目":
                    type = 7;
                    break;
                case "询价":
                    type = 8;
                    break;
                case "洽谈":
                    type = 9;
                    break;
                case "招标":
                    type = 10;
                    break;
                default:
                    type = -1;
                    break;
            }
            predicateAnd = predicateAnd.And(a => a.ObjType == type);

            string json = "";
            if (pageParam.ftype == 0)//已发起
            {
                predicateAnd = predicateAnd.And(a => a.StartUserId == pageParam.userId);
                var layPage = _IAPPWOrkList.GetAppSponsorList(predicateAnd, a => a.StartDateTime, false, start, limit);
                json = JsonUtility.SerializeObject(layPage);
            }
            else if (pageParam.ftype==1)//待处理
            {
                var layPage = _IAPPWOrkList.GetAppWorkList(pageParam.userId, predicateAnd, a => a.Id, false, start, limit);
                json = JsonUtility.SerializeObject(layPage);
            }
            else if (pageParam.ftype == 2)//已处理
            {
                predicateAnd = predicateAnd.And(p => p.AppState == (int)AppInstEnum.AppState1 && p.StartUserId == pageParam.userId);
               // predicateAnd = predicateAnd.And(_IRemindService.GetWfIntanceExpression("已通过的审批", pageParam.userId));
                var layPage = _IAPPWOrkList.GetAppProcessedList(pageParam.userId, predicateAnd, a => a.Id, false, start, limit);
                json = JsonUtility.SerializeObject(layPage);
            }
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, pageParam.callback));
        }
        /// <summary>
        /// 已发起列表数据
        /// </summary>
        /// <returns></returns>
        //public IActionResult GetAppSponsorList(APPwork pageParam)
        //{
        //    var pageInfo = new PageInfo<AppInst>(pageIndex: pageParam.start, pageSize: pageParam.limit);
        //    var predicateAnd = PredicateBuilder.True<AppInst>();
        //    var predicateOr = PredicateBuilder.False<AppInst>();
        //    predicateAnd = predicateAnd.And(a => a.StartUserId == pageParam.userId);
        //    if (!string.IsNullOrEmpty(pageParam.keyword))
        //    {
        //        predicateOr = predicateOr.Or(a => a.AppObjName.Contains(pageParam.keyword));
        //        predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(pageParam.keyword));
        //        predicateAnd = predicateAnd.And(predicateOr);
        //    }
        //    var layPage = _IAPPWOrkList.GetAppSponsorList(pageInfo, predicateAnd, a => a.StartDateTime, false);
        //    return new CustomResultJson(layPage);
        //}
    }
}