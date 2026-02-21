using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.APPModels;
using NF.ViewModel.Models.Common;

namespace NF.Web.Areas.MobileApp.Controllers
{
    [Area("MobileApp")]
    [Route("MobileApp/[controller]/[action]")]
    public class APPInvoiceListController : Controller
    {

        /// <summary>
        /// 用户
        /// </summary>
        private IAPPUserInforService _IAPPUserInforService;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        //
        public IAPPInvoice _IAPPInvoice;

        public APPInvoiceListController(IAPPUserInforService IAPPUserInforService, ISysPermissionModelService ISysPermissionModelService, IAPPInvoice IAPPInvoice)
        {
            _IAPPUserInforService = IAPPUserInforService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IAPPInvoice = IAPPInvoice;
        }
        public IActionResult APPfinanceInvoiceList(APPPageparamInfo pageParam)
        {
            //var type= pageParam.fType; //0-开票；1-收票
            var keyword = pageParam.keyWord; //搜索关键字
            var start = pageParam.page;
            var limit = pageParam.limit;
            int pageNum = start / limit;//共有页数 
            var result = new ResultContract { totalCount = "", items = null };
            int DEPid = _IAPPUserInforService.SelectDEPid(pageParam.userId);
            //分页查询
            //var pageInfo = new PageInfo<ContInvoice>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            //条件
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
            //权限
            predicateAnd = predicateAnd.And(GetQueryExpression(pageParam.keyWord, pageParam.fType, pageParam.userId, DEPid));
            //查询
            var layPage = _IAPPInvoice.APPGetMainList(predicateAnd, a => a.Id, false, start, limit);
            //newjson
            string json = JsonUtility.SerializeObject(layPage);
            //跨域jsop处理
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, pageParam.callback));
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContInvoice, bool>> GetQueryExpression(string keyWord, int financeType,int userId,int DEPid)
        {
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
            var predicateOr = PredicateBuilder.False<ContInvoice>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.Cont.FinanceType == financeType);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetInvoiceListPermissionExpression((financeType == 0 ? "querycollcontview" : "querypaycontview"), userId, DEPid));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont.Code.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.CreateUser.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.ConfirmUser.Name.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.InCode.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }


            return predicateAnd;

        }

        /// <summary>
        /// APP根据合同ID查询 详情
        /// </summary>
        /// <param name="CONTRID">合同ID</param>
        /// <returns></returns>
        public IActionResult APPfinanceInvoiceDert(string detailId, string callback)
        {
            var data = _IAPPInvoice.APPfinanceInvoiceDert(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }
        /// <summary>
        /// APP根据合同ID查询 详情
        /// </summary>
        /// <param name="CONTRID">合同ID</param>
        /// <returns></returns>
        public IActionResult APPfinanceInvoiceCONT(string detailId, string callback)
        {
            var data = _IAPPInvoice.APPfinanceInvoiceCONT(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }

    }
}