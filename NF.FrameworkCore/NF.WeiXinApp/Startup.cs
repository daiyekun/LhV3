using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NF.AutoMapper;
using NF.WeiXin.Lib.Utility;
using NF.WeiXinApp.Utility;
using NF.WeiXinApp.Utility.Common;
using NF.WeiXinApp.Utility.DI;
using NF.WeiXinApp.Utility.Filters;
using NF.WeiXinApp.Utility.Quartz;

using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace NF.WeiXinApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //初始化配置文件
            new AppsettingsUtility().Initial(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //日志文件指定
            Log4netHelper.Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Log4netHelper.Repository, new FileInfo(Environment.CurrentDirectory + "/Config/log4net.config"));

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["ConnectionStrings:Redis:RedisSessionConnection"]; //设置Redis服务器地址
                options.InstanceName = Configuration["ConnectionStrings:Redis:RedisSessionInstanceName"];
            });

            var sessres = int.TryParse(Configuration["ConnectionStrings:Redis:SessionTimeOut"], out int sessiontimout);
            services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromMinutes(sessres ? sessiontimout : 30); //设置Session闲置超时时间(有效时间周期)
                opts.Cookie.HttpOnly = true;

            });
            RedisConnUtility.RedisConfig(services, Configuration);
            //数据库链接
            DbContextUtility.GetDbContext(services, Configuration);
            //依赖注入
            ServicesDIUtility.ServicesDI(services);

            //注册AutoMapper文件
            Mappings.RegisterMappings(services);
            //AutoMapper
          //  services.AddAutoMapper();
            //注册跨域
            //CorsHelper.SetCorsOrigins(services, Configuration);
            //注入HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "合同系统API", Version = "v1" });
            });

            #region 定时任务
            QuartzUtility.QuarztInit(Configuration["ConnectionStrings:QuartzWxCron"]);
            #endregion

            if (ThreadPool.SetMinThreads(32, 32))
            {
                Parallel.For(0, 32, a => Thread.Sleep(100));
            }
            services.AddMvc(options =>
            {

                options.Filters.Add(typeof(CustomExceptionFilterAttribute));



            });



        }
        /// <summary>
        /// Certificate validation callback.
        /// </summary>
        private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (error == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error.ToString());

            return false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "合同系统API v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            //这是添加的扩张方法
            //扩展访问不是wwwroot下文件
            app.UseStaticFiles(new StaticFileOptions
            {
                //配置除了默认的wwwroot文件中的静态文件以外的文件夹提供 Web 根目录外的文件 ,
                //经过此配置以后，就可以访问非wwwroot文件下的文件
                FileProvider = new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads",
            });
           

            app.UseRouting();

            app.UseAuthorization();
            //使用Session
            app.UseSession();
            //app.UseCors("AllowSpecificOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
