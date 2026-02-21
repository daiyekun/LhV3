using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 多选实体对象
    /// </summary>
    public class SelectMultiple
    {
        /// <summary>
        /// 显示值
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 默认
        /// </summary>
        public string Selected { get; set; } = "";
        /// <summary>
        /// 是否禁止选择
        /// </summary>
        public string Disabled { get; set; } = "";
    }

    /// <summary>
    /// 多选树实体
    /// </summary>
    public class SelectMulTreeInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; } = false;
        /// <summary>
        /// 选中节点
        /// </summary>
        public bool Selected { get; set; } = false;
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<SelectMulTreeInfo> Children { get; set; }

    }
}
