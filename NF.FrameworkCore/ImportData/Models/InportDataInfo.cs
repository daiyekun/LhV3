using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportData.Models
{
    /// <summary>
    /// 导入数据对象
    /// </summary>
    public class InportDataInfo
    {
        /// <summary>
        /// Guid文件名称
        /// </summary>
        public string GfName { get; set; }
        /// <summary>
        /// 原始文件名称
        /// </summary>
        public string SfName { get; set; }
        /// <summary>
        /// 导入主体
        /// </summary>
        public int SelType { get; set; }
        /// <summary>
        /// 导入主体描述
        /// </summary>
        public string SelTypeDic { get; set; }

    }
}
