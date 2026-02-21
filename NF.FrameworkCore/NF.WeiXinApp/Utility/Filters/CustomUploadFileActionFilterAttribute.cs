using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Utility.Filters
{
    /// <summary>
    /// 文件上传执行方法Filter-比如设定跨域
    /// </summary>
    public class CustomUploadFileActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //设置跨越
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
           
        }
    }
}
