using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.ContTxtTemplate
{
   public class ContTempSubFiled
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 字段ID
        /// </summary>
        public int SubFieldId { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段内容
        /// </summary>
        public string Sval { get; set; }
        /// <summary>
        /// 历史模板ID
        /// </summary>
        public int TempHistId { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int SortFd { get; set; }


    }
    /// <summary>
    /// Checkbox 数据源
    /// </summary>
    public class SubChkField
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string type { get; set; } = "文本型";
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool on { get; set; }
    }
    /// <summary>
    /// 选择标的字段实体
    /// </summary>
    public class SelFields
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 字段ID
        /// </summary>
        public string Name { get; set; }

    }
}
