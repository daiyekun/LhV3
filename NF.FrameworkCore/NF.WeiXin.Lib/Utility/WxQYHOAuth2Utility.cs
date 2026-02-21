using NF.WeiXin.Lib.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NF.WeiXin.Lib.Utility
{
    /// <summary>
    /// 企业授权帮助类
    /// https://open.weixin.qq.com/connect/oauth2/authorize?appid=CORPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect
    /// </summary>
    public class WxQYHOAuth2Utility
    {

        /// <summary>
        /// 获取授权连接
        /// </summary>
        /// <param name="corpId">企业ID</param>
        /// <param name="redirectUrl">重定向地址，需要urlencode，这里填写的应是服务开发方的回调地址</param>
        /// <param name="scope">授权作用域，拥有多个作用域用逗号（,）分隔</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写任意参数值，最多128字节</param>
        /// <param name="responseType">默认为填code</param>
        /// <returns>URL</returns>
        public static string GetAuthorizeUrl(string corpId, string redirectUrl, string responseType = "code", string scope = "snsapi_base", string state = "20170302")
        {

            string url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={corpId}&redirect_uri={HttpUtility.UrlEncode(redirectUrl)}&response_type={responseType}&scope={scope}&state={state}#wechat_redirect";

              //string url=string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect",
              //  corpId, HttpUtility.UrlEncode(redirectUrl),responseType,scope,state
              //  );

               
            return  url;
        }
        /// <summary>
        /// 网页授权
        /// </summary>
        /// <param name="corpId">企业ID</param>
        /// <param name="redirectUrl">重定向地址，需要urlencode，这里填写的应是服务开发方的回调地址</param>
        /// <param name="responseType">默认为填code</param>
        /// <param name="scope">授权作用域，拥有多个作用域用逗号（,）分隔</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写任意参数值，最多128字节</param>
        /// <param name="agentid">应用ID，必须，不然在普通微信测试正常，在企业微信测试跳转时就提示必须在企业微信客户端打开</param>
        /// <returns></returns>
        public static string GetAuthorizeURL(string corpId, string redirectUrl, int agentid, string responseType = "code", string scope = "snsapi_base",  string state = "20170302")
        {
            string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&agentid={4}&state={5}#wechat_redirect"
                , corpId, HttpUtility.UrlEncode(redirectUrl), responseType, scope, agentid, state);
            return url;

        }
        /// <summary>
        /// 根据code获取userId
        /// https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=ACCESS_TOKEN&code=CODE
        /// </summary>
        /// <param name="code">通过成员授权获取到的code，每次成员授权带上的code将不一样，code只能使用一次，10分钟未被使用自动过期 </param>
        /// <param name="access_token">令牌</param>
        /// <returns></returns>
        public static string GetUserIdByCode(string access_token,string code) 
        {
            string _url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}"
                , access_token,code);

            var responsetext = RequestUtility.HttpGet(_url);
            string jsonstr = responsetext;//"{\"subscribe\":1}";
            return jsonstr;
        
        
        }
        ///// <summary>
        ///// 设置用户信息
        ///// </summary>
        public static WxUser SetSessionUser(string access_token, string code)
        {

           
                var jsonstr = GetUserIdByCode(access_token, code);
            Log4netHelper.Info($"登录用户："+ jsonstr);
            return JsonUtility.DeserializeObject<WxUser>(jsonstr);

        }


    }

}
