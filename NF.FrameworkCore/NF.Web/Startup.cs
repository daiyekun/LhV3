using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NF.AutoMapper;
using NF.AutoMapper.Extend;
using NF.Common.Utility;
using NF.Web.Models;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.DI;
using NF.Web.Utility.Filters;
using NF.Web.Utility.Middleware;
using Rotativa.AspNetCore;
using SignalR.Server;

namespace NF.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        
        #region 原始依赖注入方法-微软自带的

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            Log4netHelper.Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Log4netHelper.Repository, new FileInfo(Environment.CurrentDirectory + "/Config/log4net.config"));

            //解决上传大文件问题
            services.Configure<FormOptions>(x =>
            {
                //x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartBoundaryLengthLimit = int.MaxValue;
               
            });
            //添加日志UI-》http://xxxxx:xx/logging
            services.AddLoggingFileUI();

          
            //Redis
            RedisConnUtility.RedisConfig(services, Configuration);
            //数据库链接
            DbContextUtility.GetDbContext(services, Configuration);
            //依赖注入
            ServicesDIUtility.ServicesDI(services);
            //定时器
            services.AddTimedJob();
            //注册AutoMapper文件
            Mappings.RegisterMappings(services);
            //AutoMapper
            //services.AddAutoMapper();
            //注册跨域
             CorsHelper.SetCorsOrigins(services, Configuration);
          
            services.AddSignalR();
           
            services.AddMvc(options =>
            {

                options.Filters.Add(typeof(CustomExceptionFilterAttribute));



            })
              .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
           .AddNewtonsoftJson();

           

        }

        #endregion


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()//提供文件目录访问形式
            //{
            //    FileProvider = new PhysicalFileProvider(@"D:\Study"),
            //    RequestPath = new PathString("/Study")
            //});
           //默认只能访问wwwroot下的，它表示是程序根目录
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
            app.UseStaticFiles(new StaticFileOptions
            {
                //配置除了默认的wwwroot文件中的静态文件以外的文件夹提供 Web 根目录外的文件 ,
                //经过此配置以后，就可以访问非wwwroot文件下的文件
                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "SysFiles")),
                RequestPath = "/SysFiles",
            });
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();
            //定时任务
            // app.UseTimedJob();


            //服务承载类
            //ServiceLocator.Instance = app.ApplicationServices;
            // app.UseMiddleware<Utility.Middleware.AuthorityMiddleware>();
            //生成PDF
            RotativaConfiguration.Setup(env.ContentRootPath);
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigin");
       
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LhHub>("/LhHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                      name: "areas", "areas",
                      pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
            
            
             
        }
    }
}
