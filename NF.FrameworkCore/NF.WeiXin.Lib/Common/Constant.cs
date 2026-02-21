using System;
using System.Collections.Generic;
using System.Text;

namespace NF.WeiXin.Lib.Common
{

    /// <summary>
    /// 静态数据
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// 企业号ID
        /// </summary>
        public static string CorpId = "";
        /// <summary>
        /// Token
        /// </summary>
        public static  string Token ="";

        /// <summary>
        /// 加密密钥
        /// </summary>
        public static  string EncodingAESKey = "";
        /// <summary>
        /// 管理组的凭证密钥-在权限管理下可以看到
        /// </summary>
        public static  string Corpsecret = "";
        /// <summary>
        /// 微信后台请求地址
        /// </summary>
        public static  string WxAPPRequestUrl ="";
        /// <summary>
        /// 回调里设置Token
        /// </summary>
        public static string CorpToken = "";
        /// <summary>
        /// 微信请求域名地址
        /// </summary>
        public static string WxAppBaseURL = "";
        /// <summary>
        /// 微信企业应用ID 
        /// </summary>
        public static  int Agentid =0;
        /// <summary>
        /// AccessToken存储Redis前缀
        /// </summary>
        public static readonly string TokenRedisKeyStart="WxAccessToken";
        /// <summary>
        /// 存储文件的服务器地址
        /// </summary>

        public static  string WxDownloadurl = "";
        /// <summary>
        /// 到期提醒请求地址
        /// </summary>
        public static string MsgReqBaseURL = "";

    



    }
}
