using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
    /// <summary>
    /// Newtonsoft.Json 系列化规则
    /// </summary>
    public class LowercaseContractResolver: DefaultContractResolver
    {
        /// <summary>
        /// 重新属性方法
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
        
    }
}
