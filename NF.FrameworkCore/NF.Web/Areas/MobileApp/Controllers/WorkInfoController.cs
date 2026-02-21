using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.Web.Utility;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NF.Web.Areas.MobileApp.Controllers
{
    [Area("MobileApp")]
    [Route("MobileApp/[controller]/[action]")]
    public class WorkInfoController : Controller
    {
        private IAPPWOrkList _IAPPWOrkList;
        public WorkInfoController(
            IAPPWOrkList IAPPWOrkList
         )
        {
            _IAPPWOrkList = IAPPWOrkList;
        }
        // GET: /<controller>/
        /// <summary>
        /// 合同审批单
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IActionResult Index(int workflowId)
        {
            //查询合同基本信息
            APPWFLayPageInfo <ContractInfoHistoryViewDTO> data= _IAPPWOrkList.GetIDWorkList(workflowId);
            //查询审批信息多条
            //ViewData["Date"]= new List<AppInst>();
            ViewData["data"] = _IAPPWOrkList.AppcontractSPDDetail(workflowId);
            return View(data);
        } 
        /// <summary>
        /// 客户审批单
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IActionResult CustomerDetails(int workflowId)
        {
            //查询客户基本信息
            APPWFLayPageInfo<CompanyViewDTO> data = _IAPPWOrkList.GetCUIDWorkList(workflowId);
            //查询审批信息多条
            //ViewData["Date"]= new List<AppInst>();
            ViewData["data"] = _IAPPWOrkList.AppcontractSPDDetail(workflowId);
            return View(data);
        }
        /// <summary>
        /// 供应商审批单
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IActionResult CompanyDetails(int workflowId)
        {
            //供应商审基本信息
            APPWFLayPageInfo<CompanyViewDTO> data = _IAPPWOrkList.GetCUIDWorkList(workflowId);
            //查询审批信息多条
            //ViewData["Date"]= new List<AppInst>();
            ViewData["data"] = _IAPPWOrkList.AppcontractSPDDetail(workflowId);
            return View(data);
        }  
        /// <summary>
        /// 其他客户基本信息
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IActionResult ComQitaDetails(int workflowId)
        {
            //其他基本信息
            APPWFLayPageInfo<CompanyViewDTO> data = _IAPPWOrkList.GetCUIDWorkList(workflowId);
            //查询审批信息多条
            //ViewData["Date"]= new List<AppInst>();
            ViewData["data"] = _IAPPWOrkList.AppcontractSPDDetail(workflowId);
            return View(data);
        }
        /// <summary>
        /// 收票
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IActionResult ShoupiaoDetails(int workflowId)
        {
            //收票基本信息
            APPWFLayPageInfo<ContInvoiceListViewDTO> data = _IAPPWOrkList.GetInvIDWorkList(workflowId);
            //查询审批信息多条
            //ViewData["Date"]= new List<AppInst>();
            ViewData["data"] = _IAPPWOrkList.AppcontractSPDDetail(workflowId);
            return View(data);
        }
        /// <summary>
        /// 开票
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IActionResult KaipiaoDetails(int workflowId)
        {
            //开票基本信息
            APPWFLayPageInfo<ContInvoiceListViewDTO> data = _IAPPWOrkList.GetInvIDWorkList(workflowId);
            //查询审批信息多条
            //ViewData["Date"]= new List<AppInst>();
            ViewData["data"] = _IAPPWOrkList.AppcontractSPDDetail(workflowId);
            return View(data);
        }
        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IActionResult FukuanDetails(int workflowId)
        {
            //付款基本信息
            APPWFLayPageInfo<ContActualFinanceListViewDTO> data = _IAPPWOrkList.GetIFukuanIDWorkList(workflowId);
            //查询审批信息多条
            //ViewData["Date"]= new List<AppInst>();
            ViewData["data"] = _IAPPWOrkList.AppcontractSPDDetail(workflowId);
            return View(data);
        }
        /// <summary>
        /// 项目
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IActionResult PRROJDetails(int workflowId)
        {
            //项目基本信息
            APPWFLayPageInfo<ProjectManagerViewDTO> data = _IAPPWOrkList.GetIPRonIDWorkList(workflowId);
            //查询审批信息多条
            //ViewData["Date"]= new List<AppInst>();
            ViewData["data"] = _IAPPWOrkList.AppcontractSPDDetail(workflowId);
            return View(data);
        }
        /// <summary>
        /// 同意时提交意见
        /// </summary>
        /// <returns></returns>
        public IActionResult SubmitAgreeOption(AppWorkcs optionInfo)
        {
            var result = new Result { success = false, msg = "操作失败" };
            if (optionInfo.state==2)
            {
                optionInfo.OptRes = 1;
                
                if (_IAPPWOrkList.SubmintOption(optionInfo)==1)
                {
                    result.success = true;
                    result.msg = "操作成功";
                }
            }
            else if(optionInfo.state == 5)
            {
                optionInfo.OptRes = 2;
                if (_IAPPWOrkList.SubmintDisagreeOption(optionInfo))
                {
                    result.success = true;
                    result.msg = "操作成功";
                }
            }
            string json = JsonUtility.SerializeObject(result); 
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, optionInfo.callback));
        }
        /// <summary>
        /// 返回 IActionResult
        /// </summary>
        /// <param name="requstResult">需要返回的数据对象</param>
        /// <returns></returns>
        protected IActionResult GetResult(RequstResult requstResult = null)
        {
            if (requstResult == null)
            {
                requstResult = new RequstResult()
                {
                    Msg = "操作成功",
                    Code = 0,
                    RetValue = 0,
                };
            }
            return new CustomResultJson(requstResult);
        }

    }
}
