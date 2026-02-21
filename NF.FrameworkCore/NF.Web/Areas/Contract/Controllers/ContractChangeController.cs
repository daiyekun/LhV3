using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.Contract.Controllers
{
    /// <summary>
    /// 合同变更相关处理
    /// </summary>
    [Area("Contract")]
    [Route("Contract/[controller]/[action]")]
    public class ContractChangeController : NfBaseController
    {
        /// <summary>
        /// 合同历史
        /// </summary>
        private IContractInfoHistoryService _IContractInfoHistoryService;
        /// <summary>
        /// 标的历史
        /// </summary>
        private IContSubjectMatterHistoryService _IContSubjectMatterHistoryService;
        /// <summary>
        /// 计划资金历史
        /// </summary>
        private IContPlanFinanceHistoryService _IContPlanFinanceHistoryService;
        /// <summary>
        /// 合同文本
        /// </summary>
        private IContTextHistoryService _IContTextHistoryService;
        public ContractChangeController(IContractInfoHistoryService IContractInfoHistoryService,
            IContSubjectMatterHistoryService IContSubjectMatterHistoryService
            , IContPlanFinanceHistoryService IContPlanFinanceHistoryService
            , IContTextHistoryService IContTextHistoryService)
        {
            _IContractInfoHistoryService = IContractInfoHistoryService;
            _IContSubjectMatterHistoryService = IContSubjectMatterHistoryService;
            _IContPlanFinanceHistoryService = IContPlanFinanceHistoryService;
            _IContTextHistoryService = IContTextHistoryService;
        }

        /// <summary>
        /// 合同变更
        /// </summary>
        /// <returns></returns>
        public IActionResult GetChangeList(PageparamInfo pageParam, int contId)
        {
            var predicateAnd = PredicateBuilder.True<ContractInfoHistory>();
            var pageInfo = new NoPageInfo<ContractInfoHistory>();
            predicateAnd = predicateAnd.And(a => a.ContId == contId);
            var layPage = _IContractInfoHistoryService.GetChangeList(pageInfo, predicateAnd, a => a.Id, true);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 选中查看合同
        /// </summary>
        /// <returns></returns>
        public IActionResult SelContView()
        {
            return View();

        }
        /// <summary>
        /// 选中对比查看合同
        /// </summary>
        /// <returns></returns>
        public IActionResult SelContCompareView()
        {
            return View();

        }
        /// <summary>
        /// 选择合同显示
        /// </summary>
        /// <returns></returns>
        public IActionResult SelChangeView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContractInfoHistoryService.SelChangeView(Id)


            });
        }
        /// <summary>
        /// 合同标的历史
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSubMatterHistoryList(PageparamInfo pageParam, int contId)
        {
            var predicateAnd = PredicateBuilder.True<ContSubjectMatterHistory>();
            var pageInfo = new PageInfo<ContSubjectMatterHistory>();
            predicateAnd = predicateAnd.And(a => a.ContHisId == contId);
            var layPage = _IContSubjectMatterHistoryService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 合同计划资金历史
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPlanceHistoryList(PageparamInfo pageParam, int contId)
        {
            var predicateAnd = PredicateBuilder.True<ContPlanFinanceHistory>();
            var pageInfo = new PageInfo<ContPlanFinanceHistory>();
            predicateAnd = predicateAnd.And(a => a.ContHisId == contId);
            var layPage = _IContPlanFinanceHistoryService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 合同文本历史
        /// </summary>
        /// <returns></returns>
        public IActionResult GetContTextHistoryList(PageparamInfo pageParam, int contId)
        {
            var predicateAnd = PredicateBuilder.True<ContTextHistory>();
            var pageInfo = new PageInfo<ContTextHistory>();
            predicateAnd = predicateAnd.And(a => a.ContHisId == contId);
            var layPage = _IContTextHistoryService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            return new CustomResultJson(layPage);

        }


    }
}