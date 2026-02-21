using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NF.Common.Models;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NF.WeiXinApp.Utility.Filters
{
    public class CustomAction2CommitFilterAttribute : ActionFilterAttribute
    {
        #region Identity
       // private readonly ILogger<CustomAction2CommitFilterAttribute> _logger;
        //private readonly CacheClientDB _cacheClientDB;
        private static string KeyPrefix = "2CommitFilter";

        //public CustomAction2CommitFilterAttribute(ILogger<CustomAction2CommitFilterAttribute> logger)
        //{
        //    this._logger = logger;
        //    //this._cacheClientDB = cacheClientDB;
        //}
        #endregion

        /// <summary>
        /// 防重复提交周期  单位秒
        /// </summary>
        public int TimeOut = 5;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string url = context.HttpContext.Request.Path.Value;
            string argument = JsonConvert.SerializeObject(context.ActionArguments);
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string agent = context.HttpContext.Request.Headers["User-Agent"];
            string sInfo = $"{url}-{argument}-{ip}-{agent}";
            string summary = MD5Helper.MD5EncodingOnly(sInfo);
            string totalKey = $"{KeyPrefix}-{summary}";

            string result = RedisHelper.StringGet(totalKey);
            if (string.IsNullOrEmpty(result))
            {
                RedisHelper.StringSet(totalKey, "1", TimeSpan.FromSeconds(5));
                    
               // this._cacheClientDB.Add(totalKey, "1", TimeSpan.FromSeconds(3));//3秒有效期
               // this._logger.LogInformation($"CustomAction2CommitFilterAttribute:{sInfo}");
            }
            else
            {
                //已存在
                //this._logger.LogWarning($"CustomAction2CommitFilterAttribute重复请求:{sInfo}");
              var requst=
               context.Result = new JsonResult(new RequstResult()
               {
                   Msg = $"请勿重复提交，{this.TimeOut}s之后重试",
                   Code = 0,
                   RetValue = 0,
               });
            }

            //CurrentUser currentUser = context.HttpContext.GetCurrentUserBySession();
            //if (currentUser == null)
            //{
            //    //if (this.IsAjaxRequest(context.HttpContext.Request))
            //    //{ }
            //    context.Result = new RedirectResult("~/Fourth/Login");
            //}
            //else
            //{
            //    this._logger.LogDebug($"{currentUser.Name} 访问系统");
            //}
        }
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
