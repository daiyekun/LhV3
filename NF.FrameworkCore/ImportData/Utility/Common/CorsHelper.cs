using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NF.Common.Utility;

namespace NF.Web.Utility.Common
{
    /// <summary>
    /// 操作跨域
    /// </summary>
    public class CorsHelper
    {
        public static void SetCorsOrigins(IServiceCollection services, IConfiguration Configuration)
        {


            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader()
                   // .WithOrigins("http://localhost:1009")
                    .WithOrigins(StringHelper.Strint2ArrayString1(Configuration.GetConnectionString("CorsOrigins")).ToArray())
                    .AllowCredentials();
                });
            });
            
            
        }
    }
}
