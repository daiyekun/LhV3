using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Model
{
    /// <summary>
    /// 数据仓库
    /// </summary>
   public class StoreData
    {
        /// <summary>
        /// 插件版本号
        /// </summary>
        public const string PlinVisNo = "VSTO272.STD.20170905";
        ///// <summary>
        ///// 请求URL
        ///// </summary>
        public static string RequestBaseUrl;
        /// <summary>
        /// 请求用户名称
        /// </summary>
        public static string RequestUserName;
        /// <summary>
        /// 请求用户ID
        /// </summary>
        public static int requestUserId;
        /// <summary>
        /// 请求模板ID
        /// </summary>
        public static int requestTempId;
        /// <summary>
        /// 请求操作类型
        /// </summary>
        public static string RequestOptionType;
       /// <summary>
       /// Word请求枚举
       /// </summary>
        public static ContTextOption contTextOption;
       /// <summary>
       /// Word请求枚举值
       /// </summary>
        public static int contTextOptionValue =-100;
       /// <summary>
       /// 比较合同文本ID
       /// </summary>
        public static int comTextId;
        /// <summary>
        /// 请求类型2
        /// </summary>
        public static int requestType2; 
    }
}
