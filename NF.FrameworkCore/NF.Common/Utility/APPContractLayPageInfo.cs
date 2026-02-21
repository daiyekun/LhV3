using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
    /// <summary>
    /// LayUI Tabe分页对象
    /// </summary>
    public class APPContractLayPageInfo<T> where T : class, new()
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
}
