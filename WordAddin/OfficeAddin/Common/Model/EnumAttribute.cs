using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Model
{
    /// <summary>
    /// 枚举项描述特性
    /// </summary>
    public class EnumItemAttribute : Attribute
    {
        /// <summary>
        /// 枚举描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 枚举值
        /// </summary>
        public int Value { get; set; }
    }
    /// <summary>
    /// 合同文本操作类型枚举
    /// </summary>
    public class EnumContTextOptionAttribute : EnumItemAttribute
    {
        /// <summary>
        /// 请求类型
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// 对应类型
        /// </summary>
        public Type ClassType { get; set; }

    }
    /// <summary>
    /// 枚举类的属性
    /// </summary>
    public class EnumClassAttribute : Attribute
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// 空值
        /// </summary>
        public int None { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public int Default { get; set; }
        /// <summary>
        /// 是否本地化
        /// </summary>
        public bool IsLocal { get; set; }
        /// <summary>
        /// 是否有默认值
        /// </summary>
        public bool HasDefault
        {
            get
            {
                return Default >= Min && Default <= Max;
            }
        }
    }
   
}
