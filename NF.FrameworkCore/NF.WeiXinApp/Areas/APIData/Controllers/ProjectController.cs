using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.WeiXinApp.Extend;
using NF.WeiXinApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NF.WeiXinApp.Areas.APIData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IContractInfoService _IContractInfoService;
        private ISysPermissionModelService _ISysPermissionModelService;
        private IProjectManagerService _IProjectManagerService;
        private IProjAttachmentService _IProjAttachmentService;
        private IAppInstService _IAppInstService;
        public ProjectController(IContractInfoService IContractInfoService,
            IProjAttachmentService IProjAttachmentService,
            IAppInstService IAppInstService,
               ISysPermissionModelService ISysPermissionModelService,
            IProjectManagerService IProjectManagerService)
        {
            _IAppInstService = IAppInstService;
            _IProjAttachmentService = IProjAttachmentService;
            _IProjectManagerService = IProjectManagerService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContractInfoService = IContractInfoService;
        }
        // GET: api/<ProjectController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("Prlist")]
        public string Get(int page, int limit, string keyWord, string Wxzh)
        {
            PageparamInfo pageParam = new PageparamInfo();
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo != null)
            {
                var UsId = usinfo.Id;
                var UsDc = usinfo.DepartmentId;
                var pageInfo = new PageInfo<ProjectManager>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<ProjectManager>();
                predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, keyWord, pageParam.search, UsId, UsDc ?? 0));
                Expression<Func<ProjectManager, object>> orderbyLambda = null;
                bool IsAsc = false;
                switch (pageParam.orderField)
                {

                    default:
                        orderbyLambda = a => a.Id;
                        break;

                }
                if (pageParam.orderType == "asc")
                    IsAsc = true;
                var layPage = _IProjectManagerService.GetWxProjectList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
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
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ProjectManager, bool>> GetQueryExpression(PageInfo<ProjectManager> pageInfo, string keyWord, int? search, int usid, int usdc)
        {
            var predicateAnd = PredicateBuilder.True<ProjectManager>();
            var predicateOr = PredicateBuilder.False<ProjectManager>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetProjectListPermissionExpression("queryprojectlist", usid, usdc));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            return predicateAnd;
        }
        [HttpGet("GetCountViwe")]
        public string GetProjectViwe(int id)
        {
            var info = _IProjectManagerService.ShowWxViewMode(id);

            return new RequestData(data: info).ToWxJson();
        }
        // GET api/<ProjectController>/5
        [HttpGet("GetProjFileViwe")]
        public IList<WxProjAttachmen> GetProjFileViwe(int id)
        {
            var info = _IProjAttachmentService.WxShowViews(id);

            return info;
        }
        [HttpGet("GetFundStatistics")]

        public string GetFundStatistics(int id)
        {
            var info = _IProjectManagerService.WxGetFundStatistics(id);

            return new RequestData(data: info).ToWxJson();
        }
        [HttpGet("GetXmXgSk")]
        public string GetXmXgSk(int id, int Type)
        {
            //var info = _IProjectManagerService.WxGetFundStatistics(id);

            var pageInfo = new NoPageInfo<ContractInfo>();
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            predicateAnd = predicateAnd.And(a => a.ProjectId == id && a.IsDelete == 0 && a.FinanceType == Type);
            predicateAnd = predicateAnd.And(a => a.ContState == (int)ContractState.Execution
            || a.ContState == (int)ContractState.Terminated || a.ContState == (int)ContractState.Completed);
            var layPage = _IProjectManagerService.GetXmXgSk(pageInfo, predicateAnd, a => a.Id, false);
            return new RequestData(data: layPage).ToWxJson();
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
            //return new CustomResultJson(layPage);
            return null;
        }

        [HttpGet("CheckWflowText")]
        public string CheckWflowText(int SpId, int WfItem, string UserId, int SpType)
        {
            FlowPerm e = new FlowPerm();
            //SpId: currId,
            //        WfItem: $wfitem,
            //        UserId: $userId,
            //        SpType: 7//合同

            e.SpId = SpId;
            e.WfItem = WfItem;
            e.UserId = UserId;
            e.SpType = SpType;
            var info = _IAppInstService.GetFlowPermission(e);
            return new RequestData(data: info).ToWxJson();
        }


    }
}
