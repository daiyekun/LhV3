using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Model
{
    /// <summary>
    /// Base控件
    /// </summary>
    public class VstoContentControl
    {
        /// <summary>
        /// 控件ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Tag{get;set;}
        /// <summary>
        /// 标题
        /// </summary>
        public string Title{get;set;}
        /// <summary>
        /// 对象范围
        /// </summary>
        public Range Range{get;set;}
        /// <summary>
        /// 显示值
        /// </summary>
        public string Text { get; set; }


        
    }

    public class VstoRichTextContentControl : VstoContentControl
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string PlaceholderText { get; set; }

    }
}
