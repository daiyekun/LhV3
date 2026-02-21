using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.AdvQuerySearch
{
    public class AdvJbcxQueryInfo
    {
        public string head { get; set; }
        /// <summary>
        /// and/or
        /// </summary>
        public string prefix { get; set; }
        public string mode { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public string field { get; set; }

        public List<children> children { get; set; }

        public int id { get; set; }
    }
    public class children
    {
        public int id { get; set; }
        /// <summary>
        /// and/or
        /// </summary>
        public string prefix { get; set; }
        public string mode { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 判断类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }

        public int? groupId { get; set; }
    }
}
