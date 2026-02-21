using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.WeiXin.Lib.Module
{
   public class WxAppMsgInfo
    {
        /// <summary>
        /// 成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。
        /// 特殊情况：指定为@all，则向关注该企业应用的全部成员发送 
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数 
        /// </summary>
        public string toparty { get; set; }
        /// <summary>
        /// 标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数 
        /// </summary>
        public string totag { get; set; }
        /// <summary>
        /// 消息类型，此时固定为：news （不支持主页型应用）
        /// </summary>
        public string msgtype { get; set; } = "textcard";//默认文本卡片消息
        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        public int agentid { get; set; }
        /// <summary>
        /// 新闻主体
        /// </summary>
        
    }
}
