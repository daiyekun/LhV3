using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.IBLL;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.ReportForms.Controllers
{
    [Area("ReportForms")]
    [Route("ReportForms/[controller]/[action]")]
    public class ReportFormsController : Controller
    {  /// <summary>
       /// 合同操作
       /// </summary>
        private IContractInfoService _IContractInfoService;
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
        /// 标的
        /// </summary>
        private IContSubjectMatterService _IContSubjectMatterService;
        /// <summary>
        /// 实际资金
        /// </summary>
        private IContActualFinanceService _IContActualFinanceService;
        /// <summary>
        /// 发票
        /// </summary>
        private IContInvoiceService _IContInvoiceService;
        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;

        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        /// <summary>
        /// 自动生成编号
        /// </summary>
        private INoHipler _INoHipler;

        /// <summary>
        /// 数据字典
        /// </summary>
        private IDataDictionaryService _IDataDictionaryService;
        public ReportFormsController(IContractInfoService IContractInfoService, IMapper IMapper,
           ISysPermissionModelService ISysPermissionModelService, IContPlanFinanceService IContPlanFinanceService,
           IContSubjectMatterService IContSubjectMatterService,
           IContActualFinanceService IContActualFinanceService
           , IContInvoiceService IContInvoiceService
           , IRemindService IRemindService
           , IUserInforService IUserInforService
           , INoHipler INoHipler
           , IDataDictionaryService IDataDictionaryService)
        {
            _IContractInfoService = IContractInfoService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContPlanFinanceService = IContPlanFinanceService;
            _IContSubjectMatterService = IContSubjectMatterService;
            _IContActualFinanceService = IContActualFinanceService;
            _IContInvoiceService = IContInvoiceService;
            _IRemindService = IRemindService;
            _IUserInforService = IUserInforService;
            _INoHipler = INoHipler;
            _IDataDictionaryService = IDataDictionaryService;


        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 统计当前年份合同状态
        /// </summary>
        public Thzt TjNewtimezt() {
         return  _IContractInfoService.TjNewtimezt();



        }
        /// <summary>
        /// 统计当前年份合同金额与分数
        /// </summary>
        /// <returns></returns>
        public TjHtjenum TjNewHt()
        {
            return _IContractInfoService.TjNewHt();



        }
        /// <summary>
        /// 合同类别统计
        /// </summary>
        /// <returns></returns>
        public HtTypeTj Htlbjt()
        {
            return _IContractInfoService.Htlbjt();



        }
        /// <summary>
        /// 合同签订、应收、实收金额统计
        /// </summary>
        /// <returns></returns>
        public HtjeTj Htjetj(int time)
        {
            return _IContractInfoService.HtjeTj(time);
        }
        /// <summary>
        /// 合同资金往来
        /// </summary>
        /// <param name="time">年份</param>
        /// <returns></returns>
        public Htwlzjtj Htwlzjtj(int time)
        {
            return _IContractInfoService.Htwlzjtj(time);
        }

        public HtLyTj HtLyTj() {
        
        return _IContractInfoService.HtLyTj();
        }
    }
}
