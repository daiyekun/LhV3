using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.ViewModel.APPModels;
using NF.Common.Models;
using NF.IBLL;
using Microsoft.Extensions.Logging;
using NF.Web.Controllers;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.Web.Utility;
using Microsoft.AspNetCore.Cors;
using NF.Common.Utility;
using NF.IBLL.MobileApp.CONTRACT;
using System.Linq.Expressions;
using NF.ViewModel;

namespace NF.Web.Areas.MobileApp.Controllers
{
    [Area("MobileApp")]
    [Route("MobileApp/[controller]/[action]")]
    public class ContractListController : Controller
    {
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// APP合同列表
        /// </summary>
        private IAAPPContractList _IAAPPContractList;
        /// <summary>
        /// 用户
        /// </summary>
        private IAPPUserInforService _IAPPUserInforService;

        public ContractListController(
            ISysPermissionModelService ISysPermissionModelService, IAAPPContractList IAAPPContractList, IAPPUserInforService IAPPUserInforService
         )
        {
            _ISysPermissionModelService = ISysPermissionModelService;
            _IAAPPContractList = IAAPPContractList;
            _IAPPUserInforService = IAPPUserInforService;
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContractInfo, bool>> GetQueryExpression(string keyWord,int userid,int DEPid)
        {
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            var predicateOr = PredicateBuilder.False<ContractInfo>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);

            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractListPermissionExpression("querycollcontlist", userid, DEPid));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }

            return predicateAnd;
        }
        /// <summary>
        /// APP查看合同列表-
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(Woocontract contract)
        {
            var type = contract.type ?? "0"; //0-收款；1-付款
            var keyword = contract; //搜索关键字
            var start = contract.start != null ? Convert.ToInt32(contract.start) : 0;
            var limit = contract.limit != null ? Convert.ToInt32(contract.limit) : 20;
            int pageNum = start / limit;//共有页数 
            var result = new ResultContract { totalCount = "", items =null};
            //获取用户部门ID
            int DEPid = _IAPPUserInforService.SelectDEPid(Convert.ToInt32(contract.userId));
            //var pageInfo = new PageInfo<ContractInfo>(pageIndex: start, pageSize: limit);
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            predicateAnd = predicateAnd.And(GetQueryExpression(contract.keyword,Convert.ToInt32(contract.userId), DEPid));
            Expression<Func<ContractInfo, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (contract.orderField)
            {
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (contract.orderType == "asc")
                IsAsc = false;
            var layPage = _IAAPPContractList.GetList(predicateAnd, orderbyLambda, IsAsc,Convert.ToInt32(type), start,limit);
            string json = JsonUtility.SerializeObject(layPage);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, contract.callback));

        }

        /// <summary>
        /// APP根据合同ID查询 详情
        /// </summary>
        /// <param name="CONTRID">合同ID</param>
        /// <returns></returns>
        public IActionResult GetIDList(string detailId, string callback)
        {
            var data = _IAAPPContractList.GetIDView(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json,callback));
        }
        /// <summary>
        /// APP根据合同ID查询合同文本
        /// </summary>
        /// <param name="detailId">合同id</param>
        /// <param name="callback">jsonp</param>
        /// <returns></returns>
        public IActionResult AppcontractDetailFile1(int detailId, string callback)
        {
            var data = _IAAPPContractList.AppcontractDetailFile1(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }
        /// <summary>
        /// 根据合同ID查询合同文附件
        /// </summary>
        /// <param name="detailId">合同id</param>
        /// <param name="callback">jsonp</param>
        /// <returns></returns>
        public IActionResult AppcontractDetailFile2(int detailId, string callback)
        {
            var data = _IAAPPContractList.AppcontractDetailFile2(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }
        /// <summary>
        /// APP根据合同ID查询计划资金
        /// </summary>
        /// <param name="detailId">合同id</param>
        /// <param name="callback">jsonp</param>
        /// <returns></returns>
        public IActionResult AppcontractDetailAFinance(int detailId, string callback)
        {
            var data = _IAAPPContractList.AppcontractDetailAFinance(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }

        /// <summary>
        /// APP根据合同ID查询实际资金
        /// </summary>
        /// <param name="detailId">合同id</param>
        /// <param name="callback">jsonp</param>
        /// <returns></returns>
        public IActionResult AppfinanceActualList(int detailId, string callback)
        {
            var data = _IAAPPContractList.AppfinanceActualList(Convert.ToInt32(detailId));
            string json = JsonUtility.SerializeObject(data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }

        /// <summary>
        /// APP根据合同ID查询资金统计
        /// </summary>
        public IActionResult AppcontractDetailFinanceStat(int detailId, string callback)
        {
            var Data = _IAAPPContractList.GetContractStatic(detailId);
            string json = JsonUtility.SerializeObject(Data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }

        /// <summary>
        /// APP根据合同ID查询合同备忘
        /// </summary>
        public IActionResult AppcontractDetailRemark(int detailId, string callback)
        {
            var Data = _IAAPPContractList.AppcontractDetailRemark(detailId);
            string json = JsonUtility.SerializeObject(Data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }
        /// <summary>
        /// APP根据合同ID查询审批记录
        /// </summary>
        public IActionResult AppcontractDetailWorkflow(int detailId, string callback)
        {
            var Data = _IAAPPContractList.AppcontractDetailWorkflow(detailId);
            string json = JsonUtility.SerializeObject(Data);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        }
    }

}