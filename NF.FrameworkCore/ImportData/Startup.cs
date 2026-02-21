using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using NF.AutoMapper;
using NF.AutoMapper.Extend;
using NF.Common.Utility;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.DI;
using NF.Web.Utility.Filters;
using Rotativa.AspNetCore;

namespace ImportData
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //解决上传大文件问题
            services.Configure<FormOptions>(x =>
            {
                //x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartBoundaryLengthLimit = int.MaxValue;

            });
            //添加日志UI-》http://xxxxx:xx/logging
            //services.AddLoggingFileUI();


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
         //   services.AddAutoMapper();
            //注册跨域
            CorsHelper.SetCorsOrigins(services, Configuration);
           
                services.AddSignalR();
            ////services.AddCors(options =>
            ////{
            ////    options.AddPolicy("default", policy => {

            ////        policy.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

            ////    });


            ////});
            //var urls = Configuration.GetConnectionString("CorsOrigins").Split('|');
            //services.AddCors(options => options.AddPolicy("AllowAll",
            //    policy => policy.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials())
            //);

            services.AddMvc(options =>
            {

                options.Filters.Add(typeof(CustomExceptionFilterAttribute));



            })
              .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
           .AddNewtonsoftJson();

           
           
        }

        #endregion


        #region 第三方注入工具AutoFac

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    //services.Configure<CookiePolicyOptions>(options =>
        //    //{
        //    //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        //    //    options.CheckConsentNeeded = context => true;
        //    //    options.MinimumSameSitePolicy = SameSiteMode.None;
        //    //});

        //    services.AddAutoMapper();

        //    services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute))).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        //    .AddJsonOptions(options => options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");

        //    //Redis
        //    RedisConnUtility.RedisConfig(services, Configuration);
        //    //数据库链接
        //    DbContextUtility.GetDbContext(services, Configuration);
        //    //注入访问类
        //    IContainer container = AutoFacServicesUtility.ServicesDI(services);

        //    return container.Resolve<IServiceProvider>();
        //}
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

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();
            //定时任务
            app.UseTimedJob();


            //服务承载类
            //ServiceLocator.Instance = app.ApplicationServices;
            // app.UseMiddleware<Utility.Middleware.AuthorityMiddleware>();
            //生成PDF
           // RotativaConfiguration.Setup(env.WebRootPath);
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigin");



            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                      name: "areas", "areas",
                      pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
