using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.WeiXin.Lib.Module
{
   /// <summary>
   /// 微信消息处理业务实体
   /// </summary>
   public class WxMsgInfo
    {
       /// <summary>
       /// 开发者微信号 
       /// </summary>
       public string ToUserName{get;set;}
       /// <summary>
       /// 发送方帐号（一个OpenID）
       /// </summary>
       public string FromUserName{get;set;}
      /// <summary>
      /// 消息产生时间
      /// </summary>
       public string CreateTime{get;set;}
       /// <summary>
       /// 消息类型
       /// </summary>
       public string MsgType{get;set;}
       /// <summary>
       /// 文本消息内容
       /// </summary>
       public string Content { get; set; }
       /// <summary>
       /// 事件类型
       /// </summary>
       public string Event { get; set; }
       /// <summary>
       /// 事件KEY值，与自定义菜单接口中KEY值对应
       /// </summary>
       public string EventKey { get; set; }


    }

     

}
