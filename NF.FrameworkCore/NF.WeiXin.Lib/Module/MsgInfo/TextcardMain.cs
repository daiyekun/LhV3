using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.WeiXin.Lib.Module.MsgInfo
{
    /// <summary>
    /// 文本卡片主体信息
    /// </summary>
    public class TextcardMain:BaseMainMsg
    {

        /// <summary>
        /// / 按钮文字。 默认为“详情”， 不超过4个文字，超过自动截断。
        /// </summary>
        public string btntxt { get; set; } = "详情";
        
    }
}
