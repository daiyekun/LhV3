using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.WeiXin.Lib.Utility
{
   /// <summary>
   /// 微信企业号消息管理
   /// </summary>
    public class WxQYHMsgManager
    {

        /// <summary>
        /// 企业号微信新闻消息发送
        /// </summary>
        /// <returns></returns>
        public static string WxMsgSend(string PostNewsData) 
        {

            try
            {
                var TokeInfo = WeixinUtiliy.GetAccessTokenStr();
                var URL = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", TokeInfo);

                var rescode = RequestUtility.HttpPost4(URL, PostNewsData);
                return rescode;

            }
            catch (Exception)
            {

                throw new Exception();
            }
          
                 
        
        }
    }
}
