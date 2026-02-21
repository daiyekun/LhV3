using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.APPModels;

using System;
using System.Linq;
using System.Linq.Expressions;
namespace NF.Web.Areas.MobileApp.Controllers
{

    [Area("MobileApp")]
    [Route("MobileApp/[controller]/[action]")]
    public class Finance_actualController : Controller
    {
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// APP实际资金
        /// </summary>
        private IFinance_actual _IFinance_actual;

        private IContractInfoService _IContractInfoService;
        /// <summary>
        /// 用户
        /// </summary>
        private IAPPUserInforService _IAPPUserInforService;
        public Finance_actualController(
          ISysPermissionModelService ISysPermissionModelService, IFinance_actual IFinance_actual, IAPPUserInforService IAPPUserInforService
         )
        {
            _ISysPermissionModelService = ISysPermissionModelService;
            _IFinance_actual = IFinance_actual; //IAPPSompanylist IAPPSompanylist,
            _IAPPUserInforService = IAPPUserInforService;
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContActualFinance, bool>> GetQueryExpression(PageInfo<ContActualFinance> pageInfo, string keyWord, int userid, int DEPid)
        {
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            var predicateOr = PredicateBuilder.False<ContActualFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetActFinanceListPermissionExpression("querycustomerlist", userid, DEPid));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont.Code.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            return predicateAnd;
        }
        /// <summary>
        /// 列表-实际资金
        /// </summary>
        /// <param name='pageParam'>请求参数</param>
        /// <returns></returns>
        public IActionResult GetMainList(Woocompany contract)
        {
            var type = contract.type ?? "0"; //0-收款；1-付款
                                             //      var type = contract.Ctype == 0 ? 0 : 1;
            var keyword = contract; //搜索关键字
            var start = contract.start != null ? Convert.ToInt32(contract.start) : 0;
            var limit = contract.limit != null ? Convert.ToInt32(contract.limit) : 20;
            int pageNum = start / limit;//共有页数 
            var result = new ResultContract { totalCount = "", items = null };
            int DEPid = _IAPPUserInforService.SelectDEPid(Convert.ToInt32(contract.userId));
            var pageInfo = new PageInfo<ContActualFinance>(pageIndex: start, pageSize: limit);
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            //权限判断
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, contract.keyword, Convert.ToInt32(contract.userId), DEPid));
            Expression<Func<ContActualFinance, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (contract.orderField)
            {
                default:
                    orderbyLambda = a => a.Id;
                    break;
            }
            if (contract.orderType == "asc")
                IsAsc = true;
            var layPage = _IFinance_actual.GetMainList(pageInfo, predicateAnd, orderbyLambda, IsAsc, Convert.ToInt32(type), start, limit);
            string json = JsonUtility.SerializeObject(layPage);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, contract.callback));
        }
        /// <summary>
        /// 根据id获取实际资金基本信息
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IActionResult XJZJ_XQ(string detailId, string callback) {
            var data = _IFinance_actual.XJZJ_XQ(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        
        }
        /// <summary>
        /// 根据id获取合同信息
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IActionResult XJZJ_HT(string detailId, string callback)
        {
            var data = _IFinance_actual.XJZJ_HT(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));

        }

        public IActionResult XJZJ_JHZJ(string detailId, string callback)
        {
            var data = _IFinance_actual.XJZJ_JHZJ(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));

        }



        public IActionResult Index(Woocompany contract)
        {
            return View();
        }
    }
}