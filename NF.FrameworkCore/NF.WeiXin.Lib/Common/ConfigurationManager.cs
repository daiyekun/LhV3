using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace NF.WeiXin.Lib.Common
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class WxConfigurationManager
    {
        static IConfiguration Configuration;
        static WxConfigurationManager()
        {
            var baseDir = AppContext.BaseDirectory;
            Configuration = new ConfigurationBuilder()
           .SetBasePath(baseDir)
           .Add(new JsonConfigurationSource() { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
           .Build();
        }

        public static T GetAppSettings<T>(string key) where T : class, new()
        {
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(Configuration.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;


        }
    }
    
}
