using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WebAPI.Utility.Filters
{
    public class CustomActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// Action 方法执行结束
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
           // throw new NotImplementedException();
        }
        /// <summary>
        /// Action方法执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            
             
        }
    }
}
