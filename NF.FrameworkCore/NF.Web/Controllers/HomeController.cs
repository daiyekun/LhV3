using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Web.Models;
using NF.Common.SessionExtend;
using Microsoft.AspNetCore.Http;
using NF.Common.Utility;
using NF.ViewModel.Models;
using NF.IBLL;
using NF.ViewModel.Models.Common;
using System.Text;
using NF.Web.Utility;
using NF.Common.Models;
using NF.Model.Models;
using NF.ViewModel;
using System.Linq.Expressions;
using NF.BLL;

namespace NF.Web.Controllers
{
    public class HomeController : Controller
    {
        public ISysModelService _ISysModelService;
        public IRoleModuleService _IRoleModuleService;
        public IUserModuleService _IUserModuleService;
        public IAppInstService _IAppInstService;
        public IRemindService _IRemindService;
        public IContractInfoService _IContractInfoService;
        private ISysPermissionModelService _ISysPermissionModelService;
        private IProjectManagerService _IProjectManagerService;

        public HomeController(
            ISysModelService ISysModelService,
            IRoleModuleService IRoleModuleService,
            IUserModuleService IUserModuleService,
            IAppInstService IAppInstService,
            IRemindService IRemindService,
            IContractInfoService IContractInfoService,
            ISysPermissionModelService ISysPermissionModelService,
            IProjectManagerService IProjectManagerService)
        {
            _ISysModelService = ISysModelService;
            _IRoleModuleService = IRoleModuleService;
            _IUserModuleService = IUserModuleService;
            _IAppInstService = IAppInstService;
            _IRemindService = IRemindService;
            _IContractInfoService = IContractInfoService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IProjectManagerService = IProjectManagerService;
        }

        public IActionResult Index()
        {
            ViewData["CurrUserId"] = HttpContext.Session.GetInt32(StaticData.NFUserId);
            var user = HttpContext.Session.GetObjectFromJson<SessionUserInfo>(StaticData.NFUser);
            ViewData["DisplyName"] = user.DisplyName;
            //ViewData["SysLeftTree"] =ShowSysLeftTree();
            ViewData["ids"] = sum();
            return View();
        }

        public IActionResult Dbsx() {
            ViewData["ids"] = sum();
            return View();
        }
        public int sum() 
        {
            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            var userDeptId = HttpContext.Session.GetInt32(StaticData.NFUserDeptId) ?? 0;
            var msgInfo = _IAppInstService.GetConsoleMsgNumber(userId);
            _IRemindService.GetConsoleReminder(msgInfo, userId, userDeptId);

            //待审批
            var Dsp =Convert.ToInt32( msgInfo.PedingNum);
            //已通过
            var Ytg = Convert.ToInt32(msgInfo.YiTongGuo);
            //被打回 BeiDahuiNum
            var Bdh = Convert.ToInt32(msgInfo.BeiDahuiNum);
            //到期计划的收款 DqjhskNum
            var Dqjhsk = Convert.ToInt32(msgInfo.DqjhskNum);
            //到期计划的付款 DqjhfkNum
            var Dqjhfk = Convert.ToInt32(msgInfo.DqjhfkNum);
           // 待处理的实际收款
            var dcls = Convert.ToInt32(msgInfo.DclsjskNum);
            // 待处理的实际付款
            var dclf = Convert.ToInt32(msgInfo.DclsjfkNum);
            //    待处理的收票
            var dclsp = Convert.ToInt32(msgInfo.DclspNum);
            //    待处理的开票
            var dclkp = Convert.ToInt32(msgInfo.DclkpNum);
            //    到期收款合同
            var dqskht = Convert.ToInt32(msgInfo.DqSkHtNum);
            //     到期付款合同
            var dqFkht = Convert.ToInt32(msgInfo.DqFkHtNum);

            var Jdpdtx = Convert.ToInt32(msgInfo.Jdpdtx);



            var ids = Dsp + Ytg + Bdh + Dqjhsk + Dqjhfk+ dcls+ dclf+ dclsp + dclkp + dqskht+ dqFkht;
            return ids;
        }
        /// <summary>
        /// 创建左侧树（目前只支持3级，考虑到一个系统菜单没必要更多级，所以没去创建无限极）
        /// </summary>
        public IActionResult ShowSysLeftTree()
        {
            RequstResult reult = new RequstResult();
            reult.Code = 0;
            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId);
            var predicateAnd = PredicateBuilder.True<Model.Models.UserModule>();
            predicateAnd = predicateAnd.And(a => a.UserId == userId);
            //用户ID分配的模块ID
            //var query0 = _IUserModuleService.GetQueryable(predicateAnd);
           
            var listmodeIds = _IUserModuleService.GetUserModules(predicateAnd).Select(a => a.ModuleId).ToList();
            //用户对应角色下的模块ID
            var rolemodeIds = _IRoleModuleService.GetModelIdsByUserId(userId ?? 0);
            listmodeIds.AddRange(rolemodeIds);
            IList<LeftTree> leftTree = _ISysModelService.GetLeftTree(listmodeIds, userId??0);
            StringBuilder stb = new StringBuilder();
            foreach (var item in leftTree)
            {//一级
                if (item.ChildNode == null || item.ChildNode.Count() <= 0)
                {
                    stb.Append("<li data-name = \"" + item.No + "\" class=\"layui-nav-item\">");
                    stb.Append(string.Format("<a lay-href=\"{0}\" lay-tips=\"{1}\" lay-direction=\"2\">", item.Href, item.Name));
                    stb.Append(string.Format("<i class=\"layui-icon {0}\"></i>", item.Ico));
                    stb.Append(string.Format("<cite>{0}</cite>", item.Name));
                    stb.Append("</a></li>");
                }
                else
                {
                    stb.Append(string.Format("<li data-name=\"{0}\" class=\"layui-nav-item\">", item.No));
                    stb.Append(string.Format("<a href = \"javascript:;\" lay-tips=\"{0}\" lay-direction=\"2\">", item.Name));
                    stb.Append(string.Format("<i class=\"layui-icon {0}\"></i>", item.Ico));
                    stb.Append(string.Format("<cite>{0}</cite>", item.Name));
                    stb.Append("</a>");
                    stb.Append("<dl class=\"layui-nav-child\">");
                    foreach (var child0 in item.ChildNode)
                    {//二级
                        if (child0.ChildNode == null || child0.ChildNode.Count() <= 0)
                        {
                            //stb.Append(string.Format("<dd><a lay-href=\"{0}\">{1}</a></dd>", child0.Href, child0.Name));
                            stb.Append(string.Format("<dd><a lay-href=\"{0}\">", child0.Href));
                            stb.Append(string.Format("<i class=\"layui-icon {0}\"></i>", child0.Ico));
                            stb.Append(string.Format(" <cite>{0}</cite>", child0.Name));
                            stb.Append("</dd></a>");
                        }
                        else
                        {
                            stb.Append(string.Format("<dd data-name=\"{0}\">", child0.No));
                            stb.Append("<a href = \"javascript:;\">");
                            stb.Append(string.Format("<i class=\"layui-icon {0}\"></i>", child0.Ico));
                            stb.Append(string.Format(" <cite>{0}</cite>", child0.Name));
                            stb.Append("</a>");
                            stb.Append("<dl class=\"layui-nav-child\">");
                            foreach (var child1 in child0.ChildNode)
                            {//三级
                                stb.Append(string.Format("<dd data-name=\"{0}\">", child1.Ico));
                                stb.Append(string.Format("<a lay-href=\"{0}\">", child1.Href));
                                stb.Append(string.Format("<i class=\"layui-icon {0}\"></i>", child1.Ico));
                                stb.Append(string.Format("<cite>{0}</cite>", child1.Name));
                                stb.Append("</a>");
                                stb.Append("</dd>");

                            }
                            stb.Append("</dl>");
                            stb.Append("</dd>");
                        }


                    }
                    stb.Append("</dl>");
                    stb.Append("</li>");

                }


            }
            reult.Data = stb.ToString();
            return new CustomResultJson(reult);

        }
        public IActionResult Console()
        {
            return View();
        }
        /// <summary>
        /// 首页消息提醒
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMsgInfo()
        {
            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            var userDeptId = HttpContext.Session.GetInt32(StaticData.NFUserDeptId) ?? 0;
            var msgInfo = _IAppInstService.GetConsoleMsgNumber(userId);
          var sd=  _IRemindService.GetConsoleReminder(msgInfo, userId, userDeptId);
            RequstResult reult = new RequstResult();
            reult.Code = 0;
            reult.Data = sd;
            return new CustomResultJson(reult);
        }
        /// <summary>
        /// 执行中合同
        /// </summary>
        /// <returns></returns>
        public IActionResult GetContratsZXZ(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<ContractInfo>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            var userDeptId = HttpContext.Session.GetInt32(StaticData.NFUserDeptId) ?? 0;
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            //var predicateOr = PredicateBuilder.False<ContractInfo>();

            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            predicateAnd = predicateAnd.And(a => a.ContState == (int)ContractState.Execution);

            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractListPermissionExpression("querycollcontlist", userId, userDeptId));
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractListPermissionExpression("querypaycontlist", userId, userDeptId));
            Expression<Func<ContractInfo, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Code":
                    orderbyLambda = a => a.Code;
                    break;
                case "ContAmThon"://合同金额
                    orderbyLambda = a => a.AmountMoney;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;
            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IContractInfoService.GetListConsoleContracts(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 项目列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetProjects(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<ProjectManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            var userDeptId = HttpContext.Session.GetInt32(StaticData.NFUserDeptId) ?? 0;
            var predicateAnd = PredicateBuilder.True<ProjectManager>();
            //var predicateOr = PredicateBuilder.False<ContractInfo>();

            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            predicateAnd = predicateAnd.And(a => a.Pstate == (int)ProjStateEnum.Execution);

            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetProjectListPermissionExpression("queryprojectlist", userId,userDeptId));

            Expression<Func<ProjectManager, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Code":
                    orderbyLambda = a => a.Code;
                    break;
               
                default:
                    orderbyLambda = a => a.Id;
                    break;
            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IProjectManagerService.GetConsoleProjList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 进度
        /// </summary>
        /// <returns></returns>
        public IActionResult GetProgressData()
        {
            return new CustomResultJson(new RequstResult
            {
                Code = 0,
                Data = _IContractInfoService.GetProgress()
            });

        }
        #region 代办事项
        public IActionResult DbsxData()
        {


            var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
            var userDeptId = HttpContext.Session.GetInt32(StaticData.NFUserDeptId) ?? 0;
            var msgInfo = _IAppInstService.GetConsoleMsgNumber(userId);
            _IRemindService.GetConsoleReminder(msgInfo, userId, userDeptId);
           


            ////待审批
            //var Dsp = msgInfo.PedingNum;
            ////已通过
            //var Ytg = msgInfo.YiTongGuo;
            ////被打回 BeiDahuiNum
            //var Bdh = msgInfo.BeiDahuiNum;
            ////到期计划的收款 DqjhskNum
            //var Dqjhsk = msgInfo.DqjhskNum;
            ////到期计划的付款 DqjhfkNum
            //var Dqjhfk = msgInfo.DqjhfkNum;
            Dictionary<string,string > dict = new Dictionary<string,string >();
            dict.Add("待审批", msgInfo.PedingNum);
            dict.Add("已通过", msgInfo.YiTongGuo);
            dict.Add("被打回", msgInfo.BeiDahuiNum);
            dict.Add("到期计划的收款", msgInfo.DqjhskNum);
            dict.Add("到期计划的付款", msgInfo.DqjhfkNum);
            dict.Add("待处理的实际收款", msgInfo.DclsjskNum);
            dict.Add("待处理的实际付款", msgInfo.DclsjfkNum);
            dict.Add("待处理的收票", msgInfo.DclspNum);
            dict.Add("待处理的开票", msgInfo.DclkpNum);
            dict.Add("到期收款合同", msgInfo.DqSkHtNum);
            dict.Add("到期付款合同", msgInfo.DqFkHtNum);
          
            // List<DbsxDatas> Datas = new List<DbsxDatas>();
            //foreach (var key in dict)
            //{
            //    Datas.
            //    Datas.Add()
            //    foreach (var values  in Datas)
            //    {
            //        values.titlt = key.Key;
            //        values.num = key.Value;
            //    }
            //}

            var sd = dict.ToList();


            ResultData reult = new ResultData();
            reult.code = 0;
            reult.data = sd;
            return new CustomResultJson(reult);


        }


        #endregion


    }
}
