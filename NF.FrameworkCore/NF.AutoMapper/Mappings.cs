using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NF.AutoMapper
{
    /// <summary>
    /// 注册关系映射
    /// </summary>
    public static class Mappings
    {
        public static void RegisterMappings(this IServiceCollection service)
        {
            //获取所有IProfile实现类
            var allType =
            Assembly
               .GetEntryAssembly()//获取默认程序集
               .GetReferencedAssemblies()//获取所有引用程序集
               .Select(Assembly.Load)
               .SelectMany(y => y.DefinedTypes)
               .Where(type => typeof(IProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));
            service.AddAutoMapper(allType.ToArray());

            //foreach (var typeInfo in allType)
            //{
            //    var type = typeInfo.AsType();
            //    if (type.Equals(typeof(IProfile)))
            //    {
            //        //注册映射
            //        Mapper.Initialize(y =>
            //        {
            //            y.AddProfiles(type); // Initialise each Profile classe
            //        });
            //    }
            //}
        }
    }
}
