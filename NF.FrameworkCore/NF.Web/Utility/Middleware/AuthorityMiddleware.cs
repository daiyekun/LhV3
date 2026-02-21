using Microsoft.AspNetCore.Http;
using NF.Common.Utility;
using NF.QuartzNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Utility.Middleware
{
    /// <summary>
    /// Session验证中间件
    /// </summary>
    public class AuthorityMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorityMiddleware(RequestDelegate nex)
        {
            _next = nex;

        }

        public async  Task Invoke(HttpContext context)
        {

            
           

            await _next(context);
            // c.StratJob();



        }

        
    }
}
