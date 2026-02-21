using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Controllers
{
    public class CommonController : Controller
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        /// <returns></returns>
        public IActionResult SuccMag()
        {
            return View();
        }
        /// <summary>
        /// 操作失败
        /// </summary>
        /// <returns></returns>
        public IActionResult FailMsg(string errmsg)
        {
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                ViewData["errmsg"] = errmsg;
            }
            else
            {
                ViewData["errmsg"] = "";
            }
            
            return View();
        }
    }
}
