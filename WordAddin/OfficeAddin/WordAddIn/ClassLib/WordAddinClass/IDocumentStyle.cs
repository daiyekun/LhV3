using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 文档样式接口
    /// </summary>
  public  interface IDocumentStyle
    {
        /// <summary>
        /// 设置文档内容
        /// </summary>
        void SetControlContent(Microsoft.Office.Interop.Word.Application application);
    }
}
