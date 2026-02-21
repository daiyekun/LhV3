using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 查看页面流程图实例类
    /// </summary>
    public class AppFlowNodeDataJson
    {
        /// <summary>
        /// 流程标题
        /// </summary>

        public string title { get; set; }
        /// <summary>
        /// 节点字典
        /// </summary>

        public Dictionary<string, AppInstNodeViwDTO> nodes { get; set; }

        /// <summary>
        /// 节点连线
        /// </summary>

        public Dictionary<string, AppInstNodeLineViwDTO> lines { get; set; }
        /// <summary>
        /// 区域
        /// </summary>

        public Dictionary<string, AppInstNodeAreaViewDTO> areas { get; set; }
        /// <summary>
        /// 节点连线区域总个数-暂时不用
        /// </summary>

        public int initNum { get; set; }

    }
    /// <summary>
    /// 审批实例节点
    /// </summary>
    public class AppInstNodeViwDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string strid { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 与左边距距离
        /// </summary>
        public int? left { get; set; }
        /// <summary>
        /// 与头部距距离
        /// </summary>
        public int? top { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int? height { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int? width { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public bool alt { get; set; }
        /// <summary>
        /// true 边框高亮
        /// </summary>
        public bool marked { get; set; }

    }
    /// <summary>
    /// 节点连线
    /// </summary>
    public class AppInstNodeLineViwDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string strid { get; set; }
        /// <summary>
        /// 描述
        /// </summary>

        public string name { get; set; }
        /// <summary>
        /// 类型（虚实）
        /// </summary>

        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double M { get; set; }
        /// <summary>
        /// 开始节点
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 指向节点
        /// </summary>

        public string to { get; set; }
        /// <summary>
        /// 是否虚线
        /// </summary>
        public bool dash { get; set; }
        /// <summary>
        /// True 高亮
        /// </summary>
        public bool marked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool alt { get; set; }

    }

    /// <summary>
    /// 实例节点区域
    /// </summary>
    public class AppInstNodeAreaViewDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string strid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 距离左边距离
        /// </summary>
        public int? left { get; set; }
        /// <summary>
        /// 距离头部距离
        /// </summary>
        public int? top { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int? width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int? height { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public bool alt { get; set; }

    }

}
