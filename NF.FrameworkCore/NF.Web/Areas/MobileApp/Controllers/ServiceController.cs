using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.ViewModel.APPModels;
using NF.Common.Models;
using NF.IBLL;
using Microsoft.Extensions.Logging;
using NF.Web.Controllers;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.Web.Utility;
using Microsoft.AspNetCore.Cors;
using NF.Common.Utility;
using LhCode;

namespace NF.Web.Areas.MobileApp.Controllers
{
    [Area("MobileApp")]
    [Route("MobileApp/[controller]/[action]")]
    public class ServiceController : Controller
    {
        
        /// <summary>
        /// 用户
        /// </summary>
        private IAPPUserInforService _IAPPUserInforService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _iUserInforService;
        public ServiceController(
            IAPPUserInforService IAPPUserInforService
             ,IUserInforService iUserInforService
         )
        {
            _IAPPUserInforService = IAPPUserInforService;
            _iUserInforService = iUserInforService;
        }

        public IActionResult Indexer()
        {
            return View();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[EnableCors("AllowSpecificOrigin")]
        public IActionResult CheckLogin(UserLog user)
        {
            RequstResult reult = new RequstResult();
            var result = new Result { success = false, msg = "登录失败" };
            var sessionCode = string.Empty;
            int cc = LhLicense.MobileUserNumber;
            var currnum = _iUserInforService.GetMobileUserTurn(user.UserName);
          
            if (currnum > cc && user.UserName != "admin")
            {//用户数5个+1个系统管理员
                reult.Msg = $"您的移动用户数是：{cc},移动用户数已经超过许可用户数！";
                reult.Code = 0;
                reult.Tag = 10;
                return new CustomResultJson(reult);
            }
            else
            {
                reult = _IAPPUserInforService.APPCheckLogin(user.UserName, user.Password);
                if (Convert.ToInt32(reult.Tag) == 4)
                {
                    reult.Msg = "数据库链接为空！";
                }
                else if (Convert.ToInt32(reult.Tag) == 1)
                {
                    result.success = true;
                    result.msg = Convert.ToString(reult.User_id);
                }

            }
            
            string json=JsonUtility.SerializeObject(result);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, user.callback)); 
           
        }
    }
}