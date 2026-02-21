using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.WeiXin.Lib.Module
{
    /// <summary>
    /// 消息主体
    /// </summary>
    public  class BaseMainMsg
    {
        /// <summary>
        /// 标题，不超过128个字节，超过会自动截断 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述，不超过512个字节，超过会自动截断 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 点击后跳转的链接。 
        /// </summary>
        public string url { get; set; }
    }
}
