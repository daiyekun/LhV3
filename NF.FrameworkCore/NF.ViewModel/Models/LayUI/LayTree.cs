using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.LayUI
{
    /// <summary>
    /// Layui树对象
    /// </summary>
    public class LayTree
    {
        
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool spread { get; set; } = true;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool  Checked { get; set; } = false;
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool disabled { get; set; } = false;
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<LayTree> children = new List<LayTree>();

        

    }
    
}
