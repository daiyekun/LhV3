using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NF.BLL;
using NF.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Utility.DI
{
    /// <summary>
    /// 服务注入工具-第三方目前不适用
    /// </summary>
    public class AutoFacServicesUtility
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IContainer ServicesDI(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            //在这里填写你的注入服务
            containerBuilder.RegisterType<UserInforService>().As<IUserInforService>();
            containerBuilder.RegisterType<DataDictionaryService>().As<IDataDictionaryService>();
            containerBuilder.RegisterType<DepartmentService>().As<IDepartmentService>();
            containerBuilder.Populate(services);
            IContainer container = containerBuilder.Build();
            return container;
        }

    }
}
