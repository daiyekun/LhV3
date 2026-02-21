using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NF.Common.Models;
using NF.WeiXin.Lib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Utility.Filters
{

    /// <summary>
    /// 异常处理Filter
    /// </summary>
    public class CustomExceptionFilterAttribute: ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
      
        public CustomExceptionFilterAttribute(IWebHostEnvironment hostingEnvironment,IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)//异常有没有被处理过
            {
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                string actionName = (string)filterContext.RouteData.Values["action"];
              
                if (this.IsAjaxRequest(filterContext.HttpContext.Request))//检查请求头
                {
                    filterContext.Result = new JsonResult(
                         new AjaxResult()
                         {
                             Result = DoResult.Failed,
                             Msg = "系统出现异常，请联系管理员",
                             DebugMessage = filterContext.Exception.Message,
                             Code=5
                            
                         }//这个就是返回的结果
                    );
                }
                else
                {
                    var result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
                    result.ViewData = new ViewDataDictionary(_modelMetadataProvider, filterContext.ModelState);
                    result.ViewData.Add("Exception", filterContext.Exception);
                    filterContext.Result = result;
                }
                filterContext.ExceptionHandled = true;

                Log4netHelper.Error(filterContext.Exception.Message);
            }
        }


        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
