using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同模板
    /// </summary>
   public  class ContTxtTemplateDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///模板类别
        /// </summary>
        public int TepType { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string DeptIds { get; set; }
        /// <summary>
        /// 合同文本类别
        /// </summary>
        public int TextType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Tstate { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Vesion { get; set; }
        /// <summary>
        /// 明细标题
        /// </summary>
        public string MingXiTitle { get; set; }
        /// <summary>
        /// 字段显示方式0：统一显示，1：岸业务品类
        /// </summary>
        public int FieldType { get; set; }
        /// <summary>
        /// 是否显示标题
        /// </summary>
        public int ShowSub { get; set; }
        /// <summary>
        /// 合同文本类别
        /// </summary>
        public string TepTypes { get; set; }
    }
}
