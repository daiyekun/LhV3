using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WebAPI.Utility
{
    public class RedisConnUtility
    {
        /// <summary>
        /// Redis 链接
        /// </summary>
        /// <param name="services"></param>
        public static void RedisConfig(IServiceCollection services, IConfiguration Configuration)
        {
            string redisConnectiong = Configuration.GetConnectionString("Redis:RedisSessionConnection");
            string RedisInstanceName = Configuration.GetConnectionString("Redis:RedisSessionInstanceName");
            int teimout = Configuration.GetValue<int>("Redis:SessionTimeOut", 20);
            //添加session
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisConnectiong;
                options.InstanceName = RedisInstanceName;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(teimout); //session活期时间
               options.Cookie.HttpOnly = true;//设为httponly不允许客户端获取cokie
               // options.CookieName = ".NFContractSystem";
            });
            //添加redis
            


        }

    }
}
