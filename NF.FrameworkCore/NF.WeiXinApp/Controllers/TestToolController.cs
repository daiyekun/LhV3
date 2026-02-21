using Microsoft.AspNetCore.Mvc;
using NF.WeiXin.Lib.Utility;
using NF.WeiXinApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Controllers
{
    /// <summary>
    /// 测试工具类-开发时使用
    /// </summary>
    public class TestToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取AccessToken测试
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAccessToken()
        {
           return new WxResultJson(WeixinUtiliy.GetAccessTokenStr());
        }
    }
}
