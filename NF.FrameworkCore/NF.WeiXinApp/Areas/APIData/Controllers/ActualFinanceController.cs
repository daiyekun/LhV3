using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models.Common;
using NF.WeiXinApp.Extend;
using NF.WeiXinApp.Utility;
using NF.WeiXinApp.Utility.Common;
using NF.WeiXinApp.Utility.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Areas.APIData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActualFinanceController : ControllerBase
    {
        /// <summary>
        /// 实际资金
        /// </summary>
        private IContActualFinanceService _IContActualFinanceService;
        /// <summary>
        /// AutoMapper
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// 计划资金
        /// </summary>
        private IContPlanFinanceService _IContPlanFinanceService;
        /// <summary>
        /// 合同统计字段
        /// </summary>
        private IContStatisticService _IContStatisticService;
        /// <summary>
        /// 发票
        /// </summary>
        private IContInvoiceService _IContInvoiceService;
        /// <summary>
        /// 合同
        /// </summary>
        private IContractInfoService _IContractInfoService;
        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        /// <summary>
        /// 资金附件
        /// </summary>
        private IActFinceFileService _IActFinceFileService;

        private IProjectManagerService _IProjectManagerService;
        public ActualFinanceController(IContActualFinanceService IContActualFinanceService,
          IMapper IMapper, ISysPermissionModelService ISysPermissionModelService,
          IContStatisticService IContStatisticService,
            IActFinceFileService IActFinceFileService
           , IContPlanFinanceService IContPlanFinanceService
           , IContInvoiceService IContInvoiceService
           , IContractInfoService IContractInfoService
           , IRemindService IRemindService
           , IUserInforService IUserInforService
           , IProjectManagerService IProjectManagerService)
        {
            _IActFinceFileService = IActFinceFileService;
            _IContActualFinanceService = IContActualFinanceService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContStatisticService = IContStatisticService;
            _IContPlanFinanceService = IContPlanFinanceService;
            _IContInvoiceService = IContInvoiceService;
            _IContractInfoService = IContractInfoService;
            _IRemindService = IRemindService;
            _IUserInforService = IUserInforService;
            _IProjectManagerService = IProjectManagerService;
        }
        [HttpGet("Actual")]
        public string Get(int page, int limit, string keyWord, string Wxzh, int Ftype, int search)
        {
            PageparamInfo pageParam = new PageparamInfo();
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo != null)
            {
                var UsId = usinfo.Id;
                var UsDc = usinfo.DepartmentId;
                var pageInfo = new PageInfo<ContActualFinance>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<ContActualFinance>();
                predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, keyWord, Ftype, search, UsId, UsDc ?? 0));
                var layPage = _IContActualFinanceService.GetWxMainList(pageInfo, predicateAnd, a => a.Id, false);
                return layPage.ToWxJson();
            }
            else
            {
                var layPage = new LayPageInfo<ProjectManager>();
                return layPage.ToWxJson();
            }
            //  return layPage.data.ToWxJson();
        }

        /// <summary>
        /// 绑定或者修改值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        [HttpGet("GetSjZjViwe")]
        public string GetSjZjViwe(int Id)
        {

            var Data = _IContActualFinanceService.WxShowView(Id);
            return new RequestData(data: Data).ToWxJson();

        }

        /// <summary>
        /// 计划资金核销
        /// </summary>
        [HttpGet("GetWxHx")]
        public string GetWxHx(int Htid, int Id)
        {
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == Htid);
            var pageInfo = new NoPageInfo<ContPlanFinance>();
            var layPage = _IContPlanFinanceService.WxGetPlanCheckList(pageInfo, predicateAnd, a => a.Id, true, Id);
            return layPage.ToWxJson();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="finceId">资金ID</param>
        /// <returns></returns>
        [HttpGet("GetWxSjzjFile")]
        public string GetWxSjzjFile(int Id, string Wxzh)
        {
            var usid = 0;
            try
            {
                var usinfo = _IContractInfoService.Yhinfo(Wxzh);
                usid = usinfo.Id;
            }
            catch (Exception)
            {

                usid = 0;
            }


            var pageInfo = new NoPageInfo<Model.Models.ActFinceFile>();
            var predicateAnd = PredicateBuilder.True<Model.Models.ActFinceFile>();
            var predicateOr = PredicateBuilder.False<Model.Models.ActFinceFile>();
            predicateOr = predicateOr.Or(a => a.ActId == -usid && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.ActId == Id && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IActFinceFileService.WxGetList(pageInfo, predicateAnd, a => a.Id, false);
            return layPage.ToWxJson();
        }
        /// <summary>
        /// 获取当前合同下的核销明细
        /// </summary>
        /// <param name="contId">当前合同ID</param>
        /// <returns></returns>
        [HttpGet("HxDetail")]
        public string HxDetail(int Id)
        {
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == Id);
            var pageInfo = new NoPageInfo<ContActualFinance>();
            var layPage = _IContActualFinanceService.GetChkList(pageInfo, predicateAnd, a => a.Id, true);
            return layPage.ToWxJson();
        }
        /// <summary>
        /// 根据合同ID查询计划资金
        /// </summary>
        /// <returns></returns>
        [HttpGet("Jfzj")]
        public string Jfzj(int Id)
        {
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == Id);
            var pageInfo = new NoPageInfo<ContPlanFinance>();
            var layPage = _IContPlanFinanceService.GetWxListSecod(pageInfo, predicateAnd, a => a.Id, true);
            return layPage.ToWxJson();
        }

        #region 公共部分

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContActualFinance, bool>> GetQueryExpression(PageInfo<ContActualFinance> pageInfo, string keyWord, int financeType, int? search, int UsId, int UsDc)
        {
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            var predicateOr = PredicateBuilder.False<ContActualFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.FinceType == financeType);
             predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetActFinanceListPermissionExpression((financeType == 0 ? "querycollcontview" : "querypaycontview"), UsId, UsDc));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Cont != null && a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont != null && a.Cont.Code.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.ConfirmUser != null && a.ConfirmUser.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.CreateUser != null && a.CreateUser.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => (a.Cont != null && a.Cont.Comp != null) && a.Cont.Comp.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }

            if ((search ?? 0) > 0)
            {
                var content = financeType == 0 ? "待处理的实际收款" : "待处理的实际付款";
                predicateAnd = predicateAnd.And(_IRemindService.GetActualFinanceExpression(content));

            }

            return predicateAnd;

        }

        /// <summary>
        /// 根据合同ID查询发票
        /// </summary>
        /// <returns></returns>
        public string GetInvoiceByContId(int contId, int actId)
        {
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == contId && (a.InState == 2 || a.InState == 3));
            var pageInfo = new NoPageInfo<ContInvoice>();
            var layPage = _IContInvoiceService.GetActInvoiceList(pageInfo, predicateAnd, a => a.Id, true, actId);
            return layPage.ToWxJson();
        }


        /// <summary>
        /// 获取当前合同下的核销明细
        /// </summary>
        /// <param name="contId">当前合同ID</param>
        /// <returns></returns>
        public string GetChkDetail(int contId)
        {
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == contId);
            var pageInfo = new NoPageInfo<ContActualFinance>();
            var layPage = _IContActualFinanceService.GetChkList(pageInfo, predicateAnd, a => a.Id, true);
            return layPage.ToWxJson();
        }


        #endregion
    }
}
