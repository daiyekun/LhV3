using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.WeiXin.Lib.Module
{
    /// <summary>
    /// 微信菜单
    /// </summary>
    public  class WxMenus
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// click 类型菜单的时候用
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 连接地址-view的时候使用
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<WxMenus> sub_button = new List<WxMenus>();
    }
}
