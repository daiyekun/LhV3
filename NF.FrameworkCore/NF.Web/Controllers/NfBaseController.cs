using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NF.Common.Utility;
using Microsoft.AspNetCore.Http;
using NF.Web.Utility;
using NF.Common.Models;
using Microsoft.AspNetCore.Routing;

namespace NF.Web.Controllers
{
    /// <summary>
    /// 拦截器-判断用户是否登录
    /// </summary>
    public abstract class NfBaseController : Controller
    {
        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public int SessionCurrUserId { get; set; }
        /// <summary>
        /// 当前登录用户所属部门
        /// </summary>
        public int SessionCurrUserDeptId{ get; set; }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            byte[] result;
            filterContext.HttpContext.Session.TryGetValue(StaticData.NFUser, out result);
            if (result == null)
            {
                
                if (this.IsAjaxRequest(filterContext.HttpContext.Request))//检查请求头
                {
                    filterContext.Result = new RedirectResult("~/Login/LoginTimeOut");

                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login/LoginOut");
                    

                }
                return;

            }
            else
            {
                SessionCurrUserId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
                SessionCurrUserDeptId = HttpContext.Session.GetInt32(StaticData.NFUserDeptId) ?? 0;
            }
           


            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// 返回 IActionResult
        /// </summary>
        /// <param name="requstResult">需要返回的数据对象</param>
        /// <returns></returns>
        protected IActionResult GetResult(RequstResult requstResult=null)
        {
            if (requstResult == null)
            {
                requstResult = new RequstResult()
                {
                    Msg = "操作成功",
                    Code = 0,
                    RetValue=0,
                };
            }
            return new CustomResultJson(requstResult);
        }
       /// <summary>
       /// 判断是不是ajax
       /// </summary>
       /// <param name="request"></param>
       /// <returns></returns>
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}