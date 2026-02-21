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
    public class finance_planController : Controller
    {
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// APP客户列表
        /// </summary>
        private IAPPfinance_plan _IAPPfinance_plan;
        private IRemindService _IRemindService;
        /// <summary>
        /// 用户
        /// </summary>
        private IAPPUserInforService _IAPPUserInforService;
        public finance_planController(
          ISysPermissionModelService ISysPermissionModelService, IAPPfinance_plan IAPPfinance_plan, IRemindService IRemindService, IAPPUserInforService IAPPUserInforService
         )
        {
            _ISysPermissionModelService = ISysPermissionModelService;
            _IAPPfinance_plan = IAPPfinance_plan;
            _IAPPUserInforService = IAPPUserInforService;
            _IRemindService = IRemindService;
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContPlanFinance, bool>> GetQueryExpression(PageInfo<ContPlanFinance> pageInfo, string keyWord, int userid, int DEPid)
        {
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            var predicateOr = PredicateBuilder.False<ContPlanFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetFinanceListPermissionExpression("querycustomerlist", userid, DEPid));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont.Code.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            return predicateAnd;
        }
        public IActionResult GetMainList(Woocompany contract)
        {
            var type = contract.type ?? "0"; //0-收款；1-付款
                                              // var type = contract.fType == 0 ? 0 : 1;
            var keyword = contract; //搜索关键字
            var start = contract.start != null ? Convert.ToInt32(contract.start) : 0;
            var limit = contract.limit != null ? Convert.ToInt32(contract.limit) : 20;
            int pageNum = start / limit;//共有页数 
            var result = new ResultContract { totalCount = "", items = null };
            int DEPid = _IAPPUserInforService.SelectDEPid(Convert.ToInt32(contract.userId));
            var pageInfo = new PageInfo<ContPlanFinance>(pageIndex: start, pageSize: limit);
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            //权限判断
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, contract.keyword, Convert.ToInt32(contract.userId), DEPid));
            Expression<Func<ContPlanFinance, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (contract.orderField)
            {
                default:
                    orderbyLambda = a => a.Id;
                    break;
            }
            if (contract.orderType == "asc")
                IsAsc = true;

            var layPage = _IAPPfinance_plan.GetMainList(pageInfo, predicateAnd, orderbyLambda, IsAsc, Convert.ToInt32(type), start, limit);
            //  var layPage = _IAPPfinance_plan.GetMainList(pageInfo, predicateAnd, orderbyLambda, IsAsc, Convert.ToInt32(type));
            string json = JsonUtility.SerializeObject(layPage);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, contract.callback));



        }
        public IActionResult Index()
        {
            return View();
        }
    }
}