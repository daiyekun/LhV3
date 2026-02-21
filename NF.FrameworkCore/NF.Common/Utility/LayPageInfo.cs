using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
    /// <summary>
    /// LayUI Tabe分页对象
    /// </summary>
    public class LayPageInfo<T> where T : class, new()
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = 0;
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; } = "";
        /// <summary>
        /// 总数
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> data { get; set; }

      
    }

    public class APPLayPageInfo<T> where T : class, new()
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> items { get; set; }


    }
    public class APPWFLayPageInfo<T> where T : class, new()
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> WFInstanceList { get; set; }


    }
}
