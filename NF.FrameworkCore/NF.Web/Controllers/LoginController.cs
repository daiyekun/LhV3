using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LhCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NF.Common.Models;
using NF.Common.SessionExtend;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Controllers
{

    [RequestFormLimits(ValueCountLimit = int.MaxValue)]
    public class LoginController : Controller
    {
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _iUserInforService = null;
        /// <summary>
        /// 映射 AutoMapper
        /// </summary>
        private IMapper _mapper { get; set; }
        /// <summary>
        /// 登录日志
        /// </summary>
        private ILoginLogService _ILoginLogService = null;

        private IDepartmentService _IDepartmentService;
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger _logger;



        public LoginController(
            IUserInforService iUserInforService,
            IMapper mapper,
            ILoginLogService _iLoginLogService,
            IDepartmentService IDepartmentService,
            ILogger<HomeController> logger

            )
        {
            _iUserInforService = iUserInforService;
            _mapper = mapper;
            _ILoginLogService = _iLoginLogService;
            _IDepartmentService = IDepartmentService;
            _logger = logger;
            // _accessor = accessor;

        }
        public IActionResult Index()
        {
            return View();
        }
       

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="uName">登陆名</param>
        /// <param name="pwd">密码</param>
        /// <param name="vCode">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckLogin(string uName, string pwd, string vCode)
        {
            RequstResult reult = new RequstResult();
            var sessionCode = string.Empty;
            try
            {
                sessionCode = HttpContext.Session.GetSessionString(StaticData.NFVerifyCode);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                reult.Msg = "分布式缓存链接为空！";
                reult.Code = 0;
                reult.Tag = 5;
                return new CustomResultJson(reult);

            }
            if(uName== SystemStaticData.SuperAdminName)
            {//超级管理员
                if (string.IsNullOrEmpty(sessionCode))
                {
                    reult.Msg = "由于分布式数据库问题导致没能获取验证码,请检查Redis服务是否启动";
                    reult.Code = 0;
                    reult.Tag = 5;
                    return new CustomResultJson(reult);
                }
                else if (!vCode.ToUpper().Equals(sessionCode.ToUpper()))
                {
                    reult.Code = 0;
                    reult.Msg = "验证码错误！";

                }
                else
                {
                    string md5pwd = MD5Encrypt.Encrypt(pwd);
                    if (md5pwd.Equals(SystemStaticData.SuperAdminPwd))
                    {//验证通过
                        SessionUserInfo sessionUser = new SessionUserInfo();
                        sessionUser.Id = -10000;
                        sessionUser.LastName = "";
                        sessionUser.FirstName = "";
                        sessionUser.DisplyName = "超级管理员";
                        sessionUser.DepartmentId = 1;
                        sessionUser.Name = SystemStaticData.SuperAdminName;
                        HttpContext.Session.SetObjectAsJson(StaticData.NFUser, sessionUser);
                        HttpContext.Session.SetInt32(StaticData.NFUserId, sessionUser.Id);
                        HttpContext.Session.SetInt32(StaticData.NFUserDeptId, sessionUser.DepartmentId ?? 0);
                        reult.RetValue = null;
                        reult.Data = sessionUser;
                        reult.Tag = 1;//标识验证通过
                        AddLoginLog(sessionUser);
                    }
                    else
                    {
                        reult.Code = 0;
                        reult.Msg = "验证码错误！";
                    }
                }

            }
            else 
            {

            //var list=_iUserInforService.GetQueryable(a => a.Ustart == 1).Select(a=>new { a.Name,a.Id}).ToList();
            int cc = LhLicense.UserNumber;
            if (LhLicense.IsBinFa)
            {
                var currnum = _iUserInforService.GetBinFaYongFuSHu();
                if (currnum > cc && uName != "admin")
                {//用户数5个+1个系统管理员
                    reult.Msg = $"当前系统并发数过多，超用户数许可！";
                    reult.Code = 0;
                    reult.Tag = 10;
                    return new CustomResultJson(reult);
                }

            }
            else
            {
                var currnum = _iUserInforService.GetUserTurn(uName); //list.Where(a => a.Name != "admin").Count();
                if (currnum > cc && uName != "admin")
                {//用户数5个+1个系统管理员
                    reult.Msg = $"您的用户数是：{cc}，超用户数许可！";
                    reult.Code = 0;
                    reult.Tag = 10;
                    return new CustomResultJson(reult);
                }
            }

            if (string.IsNullOrEmpty(sessionCode))
            {
                reult.Msg = "由于分布式数据库问题导致没能获取验证码,请检查Redis服务是否启动";
                reult.Code = 0;
                reult.Tag = 5;
                return new CustomResultJson(reult);
            }
            else if (!vCode.ToUpper().Equals(sessionCode.ToUpper()))
            {
                reult.Code = 0;
                reult.Msg = "验证码错误！";

            }
            else
            {
                reult = _iUserInforService.CheckLogin(uName, pwd);
                if (Convert.ToInt32(reult.Tag) == 4)
                {
                    _logger.LogError(reult.Msg);
                    reult.Msg = "数据库链接为空！";
                }
                else if (Convert.ToInt32(reult.Tag) == 1)
                {
                    var userInfo = (UserInfor)reult.RetValue;
                    var sessionUser = _mapper.Map<SessionUserInfo>(userInfo);
                    HttpContext.Session.SetObjectAsJson(StaticData.NFUser, sessionUser);
                    HttpContext.Session.SetInt32(StaticData.NFUserId, sessionUser.Id);
                    HttpContext.Session.SetInt32(StaticData.NFUserDeptId, sessionUser.DepartmentId ?? 0);
                    reult.RetValue = null;
                    reult.Data = sessionUser;
                    AddLoginLog(sessionUser);
                }
            }
            }
            return new CustomResultJson(reult);
        }
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="sessionUser"></param>
        private void AddLoginLog(SessionUserInfo sessionUser)
        {
            LoginLog loginLog = new LoginLog();
            loginLog.CreateDatetime = DateTime.Now;
            loginLog.LoginUserId = sessionUser.Id;
            loginLog.LoginIp = HttpContext.GetUserIp();//_accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            loginLog.Result = 0;
            loginLog.Status = 0;//日志初始状态
            _ILoginLogService.Add(loginLog);

        }

        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        public IActionResult ExitLogin()
        {

            HttpContext.Session.Remove(StaticData.NFUser);
            HttpContext.Session.Remove(StaticData.NFUserId);
            return new CustomResultJson(new RequstResult
            {
                Code = 0,
                Msg = "",
            });
            // return RedirectToAction("Index");

        }
        public IActionResult LoginOut()
        {

            return View();
        }
        /// <summary>
        /// 登录超时
        /// </summary>
        /// <returns></returns>
        public IActionResult LoginTimeOut()
        {
            var requstResult = new RequstResult()
            {
                Msg = "登录超时,请重新登录！",
                Code = 0,
                RetValue = -1,
            };

            return new CustomResultJson(requstResult);
        }
    }
}