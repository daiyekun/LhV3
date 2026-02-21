using Microsoft.AspNetCore.Mvc;
using NF.WeiXin.Lib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Utility
{
    /// <summary>
    /// 自定义返回JSON 格式
    /// </summary>
    public class WxResultJson : IActionResult
    {
        private object _Data = null;
        public WxResultJson(object data)
        {
            _Data = data;
        }
        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            byte[] bytes = Encoding.UTF8.GetBytes(JsonUtility.SerializeObject(this._Data));
            return context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Count());





        }
    }
}
