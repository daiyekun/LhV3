using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.Common
{
    /// <summary>
    /// LayUI Tree数据结构
    /// </summary>
    public class TreeInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool spread { get; set; } = false;
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<TreeInfo> children { get; set; }

    }
    /// <summary>
    /// XTree显示需要对象
    /// </summary>
    public class XTree
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<XTree> data = new List<XTree>();
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; } = false;
    }
    /// <summary>
    /// 左侧菜单
    /// </summary>
    public class LeftTree
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        public string Ico { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<LeftTree> ChildNode { get; set; }

    }
    /// <summary>
    /// TreeSelect前端组件对象
    /// </summary>
    public class TreeSelectInfo
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string title { get; set; }
        public string name { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int id { get; set; }
        public bool open { get; set; } = true;

        public bool Checked { get; set; } = false;
        public IList<TreeSelectInfo> children;

    }
}
