using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class RequestMsg
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
    }
}
