using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.AdvQuerySearch
{
    /// <summary>
    /// 高级查询最外层
    /// </summary>
    public class AdvQueryInfo
    {
        /// <summary>
        /// and/or
        /// </summary>
        public string logicalOperator { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string groupname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subgroupname { get; set; }

        public string rowlevel { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        public string conditionFieldVal { get; set; }
        /// <summary>
        /// 查询表达式equal:等于
        /// </summary>
        public string conditionOptionVal { get; set; }

        public ConditionValueVal conditionValueVal { get; set; }
        /// <summary>
        /// 左边值-通常时范围时候
        /// </summary>
        public ConditionValueVal conditionValueLeftVal { get; set; }
        /// <summary>
        /// 左边值--通常时范围时候
        /// </summary>
        public ConditionValueVal conditionValueRightVal { get; set; }

    }
    /// <summary>
    /// 查询值
    /// </summary>
    public class ConditionValueVal
    {
        /// <summary>
        /// 值
        /// </summary>
        public string value;
        /// <summary>
        /// 显示值
        /// </summary>
        public string text;
    }
}
