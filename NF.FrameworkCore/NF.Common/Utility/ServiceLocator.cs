using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
    /// <summary>
    /// 服务承载对象
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// 服务承载实例
        /// </summary>
        public static IServiceProvider Instance { get; set; }
    }
}
