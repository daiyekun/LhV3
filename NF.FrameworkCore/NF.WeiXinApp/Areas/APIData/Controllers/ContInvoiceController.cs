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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Areas.APIData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContInvoiceController : ControllerBase
    {
        /// <summary>
        /// 发票操作
        /// </summary>
        private IContInvoiceService _IContInvoiceService;
        /// <summary>
        /// AutoMapper
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// 合同统计字段
        /// </summary>
        private IContStatisticService _IContStatisticService;
        private IAppInstService _IAppInstService;
        /// <summary>
        /// 合同操作
        /// </summary>
        private IContractInfoService _IContractInfoService;
        //发票明细
        private IInvoDescriptionService _IInvoDescriptionService;
        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        private IProjectManagerService _IProjectManagerService;
        private IContPlanFinanceService _IContPlanFinanceService;
        private IInvoFileService _IInvoFileService;
        public ContInvoiceController(IContInvoiceService IContInvoiceService,
              IMapper IMapper, ISysPermissionModelService ISysPermissionModelService
            , IContractInfoService IContractInfoService
            , IInvoFileService IInvoFileService
            , IContStatisticService IContStatisticService
            , IInvoDescriptionService IInvoDescriptionService
            , IRemindService IRemindService
            , IUserInforService IUserInforService
            , IProjectManagerService IProjectManagerService
            , IContPlanFinanceService IContPlanFinanceService
            , IAppInstService IAppInstService
           )

        {
            _IAppInstService = IAppInstService;
            _IInvoFileService = IInvoFileService;
            _IProjectManagerService = IProjectManagerService;
            _IContInvoiceService = IContInvoiceService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContractInfoService = IContractInfoService;
            _IContStatisticService = IContStatisticService;
            _IInvoDescriptionService = IInvoDescriptionService;
            _IRemindService = IRemindService;
            _IUserInforService = IUserInforService;
            _IContPlanFinanceService = IContPlanFinanceService;
        }

        /// <summary>
        /// 发票大列列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInvoce")]
        public string GetInvoce(int page, int limit, string keyWord, string Wxzh, int Itype)
        {
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo != null)
            {
                var pageInfo = new PageInfo<ContInvoice>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<ContInvoice>();
                predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, keyWord, Itype, usinfo.Id, usinfo.DepartmentId ?? 0));
                var layPage = _IContInvoiceService.GetWxMainList(pageInfo, predicateAnd, a => a.Id, false);
                return layPage.ToWxJson();
            }
            else
            {
                var layPage = new LayPageInfo<ContInvoice>();
                return layPage.ToWxJson();
            }

        }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContInvoice, bool>> GetQueryExpression(PageInfo<ContInvoice> pageInfo, string keyWord, int financeType, int usid, int usD)
        {
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
            var predicateOr = PredicateBuilder.False<ContInvoice>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.Cont.FinanceType == financeType);
       
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetInvoiceListPermissionExpression((financeType == 0 ? "querycollcontview" : "querypaycontview"), usid, usD));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Cont != null && a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont != null && a.Cont.Code.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.CreateUser!=null&&a.CreateUser.DisplyName.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.ConfirmUser!=null&& a.ConfirmUser.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => !string.IsNullOrEmpty(a.InCode) && a.InCode.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }


            return predicateAnd;

        }
        [HttpGet("GetFpViwe")]
        public string GetFpViwe(int id)
        {
            var layPage = _IContInvoiceService.WxShowView(id);
            return new RequestData(data: layPage).ToWxJson();


        }

        [HttpGet("FpFile")]
        public string FpFile(int Id, string wxzh)
        {
            var usid = 0;
            try
            {
                var usinfo = _IContractInfoService.Yhinfo(wxzh);
                usid = usinfo.Id;
            }
            catch (Exception)
            {

                usid = 0;
            }

            var pageInfo = new NoPageInfo<Model.Models.InvoFile>();
            var predicateAnd = PredicateBuilder.True<Model.Models.InvoFile>();
            var predicateOr = PredicateBuilder.False<Model.Models.InvoFile>();
            predicateOr = predicateOr.Or(a => a.InvoId == -usid && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.InvoId == Id && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IInvoFileService.GeWxtList(pageInfo, predicateAnd, a => a.Id, false);
            return layPage.ToWxJson();
        }
        [HttpGet("XmSpLs")]
        public string XmSpLs(int id, int objType, string UsName)
        {//ObjType  审批对象 AppObjId 项目id

            var usinfo = _IContractInfoService.Yhinfo(UsName);
            var pageInfo = new NoPageInfo<AppInst>();
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            predicateAnd = predicateAnd.And(a => a.ObjType == objType && a.AppObjId == id);

            var layPage = _IAppInstService.GetAppHistList(pageInfo, usinfo.Id, predicateAnd, a => a.Id, true);
            return layPage.ToWxJson();

        }
        /// <summary>
        /// 获取发票明细列表
        /// </summary>
        /// <param name="InvId">发票ID</param>
        /// <returns>发票明细列表</returns>
        [HttpGet("Fpdetail")]
        public string Fpdetail(int Id, string wxzh)
        {
            var usid = 0;
            try
            {
                var usinfo = _IContractInfoService.Yhinfo(wxzh);
                usid = usinfo.Id;
            }
            catch (Exception)
            {

                usid = 0;
            }
            var pageInfo = new NoPageInfo<InvoDescription>();
            var predicateAnd = PredicateBuilder.True<InvoDescription>();
            predicateAnd = predicateAnd.And(a => (a.ContInvoId == Id || a.ContInvoId == -usid) && a.IsDelete == 0);
            var layPage = _IInvoDescriptionService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return layPage.ToWxJson();
        }

    }
}
