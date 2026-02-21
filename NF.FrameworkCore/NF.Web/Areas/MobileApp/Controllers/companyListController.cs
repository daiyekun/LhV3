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
    public class companyListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// APP客户列表
        /// </summary>
        private IAPPSompanylist _IAPPSompanylist;
        /// <summary>
        /// 用户
        /// </summary>
        private IAPPUserInforService _IAPPUserInforService;
        public companyListController(
          ISysPermissionModelService ISysPermissionModelService, IAPPSompanylist IAPPSompanylist, IAPPUserInforService IAPPUserInforService
         )
        {
            _ISysPermissionModelService = ISysPermissionModelService;
            _IAPPSompanylist = IAPPSompanylist; //IAPPSompanylist IAPPSompanylist,
            _IAPPUserInforService = IAPPUserInforService;
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<Model.Models.Company, bool>> GetQueryExpression(PageInfo<Model.Models.Company> pageInfo, string keyWord, int userid, int DEPid)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            var predicateOr = PredicateBuilder.False<Model.Models.Company>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetCmpListPermissionExpression("querycustomerlist", userid, DEPid));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            return predicateAnd;
        }
        /// <summary>
        /// 列表-客户
        /// </summary>
        /// <param name='pageParam'>请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(Woocompany contract)
        {
            var type = contract.type ?? "0"; //0-收款；1-付款
                                             //      var type = contract.Ctype == 0 ? 0 : 1;
            var keyword = contract; //搜索关键字
            var start = contract.start != null ? Convert.ToInt32(contract.start) : 0;
            var limit = contract.limit != null ? Convert.ToInt32(contract.limit) : 20;
            int pageNum = start / limit;//共有页数 
            var result = new ResultContract { totalCount = "", items = null };
            int DEPid = _IAPPUserInforService.SelectDEPid(Convert.ToInt32(contract.userId));
            var pageInfo = new PageInfo<Model.Models.Company>(pageIndex: start, pageSize: limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            //权限判断
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, contract.keyword, Convert.ToInt32(contract.userId), DEPid));
            Expression<Func<Model.Models.Company, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (contract.orderField)
            {
                default:
                    orderbyLambda = a => a.Id;
                    break;
            }
            if (contract.orderType == "asc")
                IsAsc = true;
            //   var layPage = _IAPPSompanylist.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            var layPage = _IAPPSompanylist.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc, Convert.ToInt32(type), start,  limit);
            //   var layPage = _IAPPSompanylist.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc, Convert.ToInt32(type));
            string json = JsonUtility.SerializeObject(layPage);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, contract.callback));
        }



        /// <summary>
        /// 根据客户ID查询 详情
        /// </summary>
        /// <param name="CONTRID">客户ID</param>
        /// <returns></returns>
        public IActionResult GetIDList(string detailId, string callback)
        {
            var data = _IAPPSompanylist.GetIDView(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));

        }
        /// <summary>
        /// 客户-联系人
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IActionResult companyDetailContact(string detailId, string callback)
        {
           var data = _IAPPSompanylist.companyDetailContact(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));

        }
        /// <summary>
        /// 客户-附件
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IActionResult Company_accessory(string detailId, string callback)
        {
            var data = _IAPPSompanylist.Company_accessory(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));

        }
    }
}