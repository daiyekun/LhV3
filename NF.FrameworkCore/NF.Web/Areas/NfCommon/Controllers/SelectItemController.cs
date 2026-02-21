using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.NfCommon.Controllers
{
    [Area("NfCommon")]
    [Route("NfCommon/[controller]/[action]")]
    public class SelectItemController : NfBaseController
    {
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// 合同操作
        /// </summary>
        private IContractInfoService _IContractInfoService;
        /// <summary>
        /// 合同模板
        /// </summary>
        private IContTxtTemplateService _IContTxtTemplateService;
        public SelectItemController(ISysPermissionModelService ISysPermissionModelService
                                   , IContractInfoService IContractInfoService
            , IContTxtTemplateService IContTxtTemplateService)
        {
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContractInfoService= IContractInfoService;
            _IContTxtTemplateService = IContTxtTemplateService;

        }

        /// <summary>
        /// 选择合同
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectContract()
        {
            return View();
        }
        /// <summary>
        /// 选择列表
        /// </summary>
        /// <param name="pageParam">请求对象</param>
        /// <param name="selType">选择类型</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam,int selType)
        {
            var pageInfo = new PageInfo<ContractInfo>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            switch (selType)
            {
                case 0://收票->付款合同&执行中
                    predicateAnd = predicateAnd.And(a=>a.FinanceType==1&&a.ContState==1);
                    break;
                case 1://开票->收款合同&执行中
                    predicateAnd = predicateAnd.And(a => a.FinanceType == 0 && a.ContState == 1);
                    break;
                case 2://实际收款->收款合同&执行中
                    predicateAnd = predicateAnd.And(a => a.FinanceType == 0 && a.ContState == 1);
                    break;
                case 3://实际付款->付款合同&执行中
                    predicateAnd = predicateAnd.And(a => a.FinanceType == 1 && a.ContState == 1);
                    break;

            }
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord));
            var layPage = _IContractInfoService.GetList(pageInfo, predicateAnd, a=>a.Id, false);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContractInfo, bool>> GetQueryExpression(PageInfo<ContractInfo> pageInfo, string keyWord)
        {
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            var predicateOr = PredicateBuilder.False<ContractInfo>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);

            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractListPermissionExpression("querycollcontlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.PrincipalUser!=null&& a.PrincipalUser.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.PrincipalUser!=null&&a.PrincipalUser.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }

            return predicateAnd;
        }
        /// <summary>
        /// 选择单品
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectBcInstance()
        {
            return View();
        }
        /// <summary>
        /// 选择合同模板
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectTxtTemp()
        {
            return View();
        }
        /// <summary>
        /// 合同模板
        /// </summary>
        /// <param name="pageParam"></param>
        /// <param name="deptId">经办机构</param>
        /// <param name="htLb">合同类别</param>
        /// <returns></returns>
        public IActionResult GetTxtTempList(PageparamInfo pageParam,int deptId,int htLb)
        {
            var predicateAnd = PredicateBuilder.True<ContTxtTemplate>();
            var predicateOr = PredicateBuilder.False<ContTxtTemplate>();
            var pageInfo = new PageInfo<ContTxtTemplate>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            //predicateAnd = predicateAnd.And(p => p.TepType == htLb);
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
           var layPage= _IContTxtTemplateService.GetList(pageInfo, predicateAnd, a => a.Id, true, deptId, htLb);
            return new CustomResultJson(layPage);

        }
    }
}