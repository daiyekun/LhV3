using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NF.AutoMapper;
using NF.Common.Utility;
using NF.WebAPI.Utility;
using NF.WebAPI.Utility.Common;
using NF.WebAPI.Utility.DI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
          


            //Redis
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
            CorsHelper.SetCorsOrigins(services, Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "合同系统API", Version = "v1" });
            });
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

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigin");



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
