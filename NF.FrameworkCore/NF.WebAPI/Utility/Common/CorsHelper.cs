using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NF.Common.Utility;

namespace NF.WebAPI.Utility.Common
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
                    //允许所有跨域
                    builder.SetIsOriginAllowed(a=>true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                   

                });
            });
            
            
        }
    }
}
