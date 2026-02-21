using System;
using System.Collections.Generic;
using System.Text;

namespace NF.WeiXin.Lib.Module
{
    /// <summary>
    /// 微信返回字符串对象
    /// 返回格式：{"errcode":0,"errmsg":"ok"}
    /// </summary>
    public class WxResponse
    {
        /// <summary>
        /// 状态码 0：成功
        /// </summary>
        public int errcode;
        /// <summary>
        /// 消息 “ok”
        /// </summary>
        public string errmsg;
    }
}
