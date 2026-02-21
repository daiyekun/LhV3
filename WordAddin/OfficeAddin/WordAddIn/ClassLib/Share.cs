using WordTool=Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordAddIn.ClassLib
{
    public class Share
    {
        /// <summary>
        /// 任务窗口
        /// </summary>
        public static WordTool.CustomTaskPane customTaskPane;
        /// <summary>
        /// 当前文档
        /// </summary>
        public static Microsoft.Office.Interop.Word.Document document;
    }
}
