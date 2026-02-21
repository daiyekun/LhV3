using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.WeiXin.Lib.Common;
using NF.WeiXin.Lib.Module;
using NF.WeiXin.Lib.Utility;
using NF.WeiXinApp.Extend;
using NF.WeiXinApp.Models;
using NF.WeiXinApp.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  IAppInstService _IAppInstService;
        private IContractInfoService _IContractInfoService;
        public HomeController(ILogger<HomeController> logger
            , IAppInstService IAppInstService
            , IContractInfoService IContractInfoService
            )
        {
            _IContractInfoService = IContractInfoService;
            _IAppInstService = IAppInstService;
            _logger = logger;
        }
        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            
            var usName = HttpContext.Session.GetString("WxUserId");
            WeiXin.Lib.Utility.Log4netHelper.Info($"登录用户：" + usName);
            ViewData["WxCurrUserId"] = usName;

          //  ViewData["WxCurrUserId"] = HttpContext.Session.GetString("WxUserId");
            return View();
        }
        public IActionResult Invo()
        {

            return View();
        }
        /// <summary>
        /// 审批
        /// </summary>
        /// <returns></returns>
        public IActionResult ApproveIndex()
        {
            // <a href="/WorkFlow/DaiChuLi?wxzh=@ViewData["WxCurrUserId"]" class="weui-cell_access">
            //< a href = "/WorkFlow/BeiDaHui?wxzh=@ViewData["WxCurrUserId"]" class="weui-cell_access">
            
            var usName = HttpContext.Session.GetString("WxUserId");
            WeiXin.Lib.Utility.Log4netHelper.Info($"登录用户：" + usName);
            ViewData["WxCurrUserId"] = usName;
            var Dcl = 0;
            var Bdh = 0;

            try
            {
                Dcl = WxDcl(1, 10, "", usName);
                Bdh = WxBdh(1, 10, "", usName);
            }
            catch (Exception)
            {

                ViewData["Dcl"] = Dcl;
                ViewData["Bdh"] = Bdh;
            }
            
            ViewData["Dcl"] = Dcl;
            ViewData["Bdh"] = Bdh;

            return View();
        }
         public int WxDcl(int page, int limit, string keyWord, string Wxzh)
        {
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo!=null)
            {
                var UsId = usinfo.Id;
                var UsDc = usinfo.DepartmentId;
                var pageInfo = new PageInfo<AppInst>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<AppInst>();
                var predicateOr = PredicateBuilder.False<AppInst>();

                if (!string.IsNullOrEmpty(keyWord))
                {
                    predicateOr = predicateOr.Or(a => a.AppObjName.Contains(keyWord));
                    predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(keyWord));
                    predicateAnd = predicateAnd.And(predicateOr);
                }
                var layPage = _IAppInstService.GetAppWxDclList(pageInfo, UsId, predicateAnd, a => a.StartDateTime, true);
                //  return new CustomResultJson(layPage);
                return layPage.data.Count;
            }
            else
            {
                return 0;
            }
            
          
        }

        public int WxBdh(int page, int limit, string keyWord, string Wxzh)
        {
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo!=null)
            {
                var UsId = usinfo.Id;
                var UsDc = usinfo.DepartmentId;
                var pageInfo = new PageInfo<AppInst>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<AppInst>();
                var predicateOr = PredicateBuilder.False<AppInst>();
                predicateAnd = predicateAnd.And(a => a.StartUserId == UsId && a.AppState == 3 && (a.NewInstId ?? 0) <= 0);
                if (!string.IsNullOrEmpty(keyWord))
                {
                    predicateOr = predicateOr.Or(a => a.AppObjName.Contains(keyWord));
                    predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(keyWord));
                    predicateAnd = predicateAnd.And(predicateOr);
                }
                var layPage = _IAppInstService.GetBdhList(pageInfo, predicateAnd, a => a.StartDateTime, false);
                return layPage.data.Count;//return new CustomResultJson(layPage);


            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 我的
        /// </summary>
        /// <returns></returns>
        public IActionResult WooIndex()
        {
            var usName = HttpContext.Session.GetString("WxUserId");
            WeiXin.Lib.Utility.Log4netHelper.Info($"登录用户：" + usName);
            ViewData["WxCurrUserId"] = usName;
            return View();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public IActionResult InitData()
        {
            return View();
        }
        public IActionResult Eees()
        {
            return View();
        }
        /// <summary>
        /// 初始化菜单
        /// http://localhost:8029/Home/InitMenus
        /// </summary>
        /// <returns></returns>
        public IActionResult InitMenus()
        {
            
            string AuthUrl = $"{Constant.WxAppBaseURL}/WxRuKou/WxHttpRedirect";
            var listmenu = new List<WxMenus>()
             {
               new WxMenus(){
                    type="view",
                    name="菜单",
                    url=WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId,AuthUrl,agentid:Constant.Agentid,state:"NFCaiDan")//$"{AuthUrl}?state=NFCaiDan"//,//
              },
                new WxMenus(){
                   name="审批",
                  sub_button={
                   new WxMenus(){
                  type="view",
                  name="待处理",
                  url=WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId,AuthUrl,agentid:Constant.Agentid,state:"NFDaiChuLi")

                  },
                  new WxMenus(){
                  type="view",
                  name="我通过",
                  url=WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId,AuthUrl,agentid:Constant.Agentid,state:"WoTongGuo")
                  },
                   new WxMenus(){
                  type="view",
                  name="我打回",
                  url=WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId,AuthUrl,agentid:Constant.Agentid,state:"WoDaHui")
                  }
                  }

              }
              // new WxMenus(){
              //      type="view",
              //      name="审批",
              //      url=WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId,AuthUrl,agentid:Constant.Agentid,state:"NFWorkflow"),//$"{AuthUrl}?state=NFWorkflow"//
              //}
              // ,
              // new WxMenus(){
              //      type="view",
              //      name="测试菜单",
              //      url=WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId,AuthUrl,agentid:Constant.Agentid, state:"NFWorkflow"),//$"{AuthUrl}?state=NFWorkflow"//
              //},
              // new WxMenus(){
              //     name="审批",
              //    sub_button={
              //     new WxMenus(){
              //    type="click",
              //    name="已处理",
              //    key="htxt_sp_ycl",
              //    },
              //    new WxMenus(){
              //    type="click",
              //    name="待处理",
              //    key="htxt_sp_dcl",
              //    },
              //     new WxMenus(){
              //    type="click",
              //    name="已发起",
              //    key="htxt_sp_yfq",
              //    }
              //    }

              //}
            
              //new WxMenus(){
              //    type="click",
              //    name="关于",
              //    key="htxt_gy"
              //}


                                              };
            var str = WeiXin.Lib.Utility.JsonUtility.SerializeObject(listmenu).Replace("\\u0026", "&");
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            strb.Append("{\"button\":");
            strb.Append(str);
            strb.Append("}");
            var postdata = strb.ToString();
            WxQYHMenusManager mmsg = new WxQYHMenusManager();
            var rescode = mmsg.CreateMenus(postdata);
            return new WxResultJson(rescode);


        }
        /// <summary>
        /// 初始化控制台
        /// http://localhost:8029/Home/InitAppConsole
        /// </summary>
        /// <returns></returns>
        public IActionResult InitAppConsole()
        {
           var data = WxConsoleUtility.GetConsoleData();
           var str = WeiXin.Lib.Utility.JsonUtility.SerializeObject(data).Replace("\\u0026", "&");
           var rescode = WxConsoleUtility.SetConsolTemp(str);
           return new WxResultJson(rescode);
        }


        /// <summary>
        /// 初始化控制台
        /// http://localhost:8029/Home/TempId
        /// </summary>
        /// <returns></returns>
        public IActionResult TempId()
        {
           
            var rescode = WxConsoleUtility.GetConselTemp();
            return new WxResultJson(rescode);
        }
        /// <summary>
        /// 模拟测试用户
        /// http://localhost:8029/Home/TestUserConsol
        /// 如果测试某账号请修改daiyekun 为对应企业微信账号
        /// </summary>
        /// <returns></returns>

        public IActionResult TestUserConsol()
        {
            var dic = new Dictionary<string,string>();
            dic.Add("待处理", "10");
            dic.Add("我通过", "10");
            dic.Add("我打回", "6");
            // var data = WxConsoleUtility.GetUserConsolData("daiyekun", dic);
            var data = WxConsoleUtility.GetUserConsolData("caolifei", dic);
            var strdata = WeiXin.Lib.Utility.JsonUtility.SerializeObject(data).Replace("\\u0026", "&");
           var recode= WxConsoleUtility.SubMitConsolData(strdata);
            return new WxResultJson(recode);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
