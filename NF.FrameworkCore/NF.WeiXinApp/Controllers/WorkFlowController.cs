using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Controllers
{
    public class WorkFlowController : Controller
    {
        /// <summary>
        /// 待处理(发起审批该我审批的)
        /// </summary>
        /// <returns></returns>
        public IActionResult DaiChuLi(string wxzh)
        {
            ViewData["wxzh"] = wxzh;
            return View();
        }
        /// <summary>
        /// 已处理
        /// </summary>
        /// <returns></returns>
        public IActionResult YiChuLi(string wxzh)
        {
            ViewData["wxzh"] = wxzh;
            return View();
        }
        /// <summary>
        /// 被打回
        /// </summary>
        /// <returns></returns>
        public IActionResult BeiDaHui(string wxzh)
        {
            ViewData["wxzh"] = wxzh;
            return View();
        }

        /// <summary>
        /// 已发起（我发起的）
        /// </summary>
        /// <returns></returns>
        public IActionResult YiFaQi(string wxzh)
        {
            ViewData["wxzh"] = wxzh;
            return View();
        }
        
        /// <summary>
        /// 我通过（我审批过的）
        /// </summary>
        /// <returns></returns>
        public IActionResult WooTongGuo(string wxzh,int HtTye)
        {
            ViewData["HtTye"] = HtTye;
            ViewData["wxzh"] = wxzh;
            return View();
        }
        /// <summary>
        /// 我打回
        /// </summary>
        /// <returns></returns>
        public IActionResult WooDaHui(string wxzh, int HtTye)
        {
            ViewData["HtTye"] = HtTye;
            ViewData["wxzh"] = wxzh;
            return View();
        }

        
    }
}
