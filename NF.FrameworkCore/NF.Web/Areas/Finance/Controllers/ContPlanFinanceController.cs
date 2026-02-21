using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Finance.Controllers
{
    /// <summary>
    /// 计划资金
    /// </summary>
    [Area("Finance")]
    [Route("Finance/[controller]/[action]")]

    public class ContPlanFinanceController : NfBaseController
    {
        private IContPlanFinanceService _IContPlanFinanceService;
        private IMapper _IMapper;
        private ISysPermissionModelService _ISysPermissionModelService;
        private IRemindService _IRemindService;
        private IUserInforService _IUserInforService;
        public ContPlanFinanceController(IContPlanFinanceService IContPlanFinanceService, 
            IMapper IMapper, 
            ISysPermissionModelService ISysPermissionModelService,
            IRemindService IRemindService
            , IUserInforService IUserInforService)
        {
            _IUserInforService = IUserInforService;
            _IContPlanFinanceService = IContPlanFinanceService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IRemindService = IRemindService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<ContPlanFinance>();
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            var predicateOr = PredicateBuilder.False<ContPlanFinance>();
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.ContId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);

            var layPage = _IContPlanFinanceService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 计划资金大列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetMainList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new PageInfo<ContPlanFinance>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.requestType,pageParam.search));
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryContPlanFinance(pageParam, _IUserInforService, _IContPlanFinanceService));
            }
            var layPage = _IContPlanFinanceService.GetMainList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            return View();

        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立计划资金", OptionLogTypeEnum.Add, "建立计划资金", true)]
        public IActionResult Save(ContPlanFinanceDTO ContPlanFinanceDTO)
        {

            var saveInfo = _IMapper.Map<ContPlanFinance>(ContPlanFinanceDTO);
            
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.ContId = (ContPlanFinanceDTO.ContId ?? 0) <= 0 ? -this.SessionCurrUserId : ContPlanFinanceDTO.ContId;
            //_IContPlanFinanceService.Add(saveInfo);
            _IContPlanFinanceService.AddSave(saveInfo, ContPlanFinanceDTO.IsFramework==1);
            return GetResult();

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改计划资金", OptionLogTypeEnum.Update, "修改计划资金", true)]
        public IActionResult UpdateSave(ContPlanFinanceDTO ContPlanFinanceDTO)
        {
            if (ContPlanFinanceDTO.Id > 0)
            {
                var updateinfo = _IContPlanFinanceService.Find(ContPlanFinanceDTO.Id);
                var updatedata = _IMapper.Map(ContPlanFinanceDTO, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                // _IContPlanFinanceService.Update(updatedata);
                _IContPlanFinanceService.UpdateSave(updatedata);
            }

            return GetResult();

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids,bool isFra=false)
        {
            _IContPlanFinanceService.Delete(Ids, isFra);
            return GetResult();
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContPlanFinanceService.ShowView(Id)


            });
        }
        /// <summary>
        /// 收款计划资金
        /// </summary>
        /// <returns></returns>
        public IActionResult PlanFinanceCollIndex()
        {
            return View();
        }
        /// <summary>
        /// 付款计划资金
        /// </summary>
        /// <returns></returns>
        public IActionResult PlanFinancePayIndex()
        {
            return View();
        }


        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContPlanFinance, bool>> GetQueryExpression(PageInfo<ContPlanFinance> pageInfo,string keyWord,int financeType,int? searchType)
        {
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            var predicateOr = PredicateBuilder.False<ContPlanFinance>();
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont.Code.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont.Comp.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            
            predicateAnd = predicateAnd.And(a => a.Ftype == financeType && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetFinanceListPermissionExpression((financeType == 0 ? "querycollcontview" : "querypaycontview"), this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if ((searchType ?? 0) > 0)
            {
                var content = financeType == 0 ? "到期的计划收款" : "到期的计划付款";
                predicateAnd = predicateAnd.And(_IRemindService.GetPlanFinanceExpression(content));
            }
            return predicateAnd;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {

            var pageInfo = new NoPageInfo<ContPlanFinance>();
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord, exportRequestInfo.FType,exportRequestInfo.search));
            if (exportRequestInfo.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
            }
            var layPage = _IContPlanFinanceService.GetMainList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, (exportRequestInfo.FType==0? "计划收款":"计划付款"), layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }


    }
}