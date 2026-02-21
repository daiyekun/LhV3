using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Utility
{
    public class DbContextUtility
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        /// <param name="services"></param>
        public static void GetDbContext(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<NFDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
            //如果不加如下，就需要在控制器使用构造函数注入
            services.AddScoped<DbContext>(provider => provider.GetService<NFDbContext>());
        }
    }
}
