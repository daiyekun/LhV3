using NF.WeiXin.Lib.Common;
using NF.WeiXin.Lib.Module;
using NF.WeiXin.Lib.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Tencent;

namespace NF.WeiXin.Lib.Utility
{
    public class WeixinUtiliy
    {
        /// <summary>
        /// 验证微信签名
        /// <param name="nonce">随机数</param>
        /// <param name="signature">微信签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="Token">访问令牌,设置企业应用的时候自己配置的</param>
        /// </summary>
        /// <returns>
        /// 校验签名以及校验服务器有效性
        ///</returns>
        
        public bool CheckSignature(string Token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { Token, timestamp, nonce };
            //将参数排序
            Array.Sort(ArrTmp);
            //讲几个参数拼接一起
            string tmpStr = string.Join("", ArrTmp);
            //SHA1加密-此处可以用MD5加密完全一致
            tmpStr = Md5Hash(tmpStr);//FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            Log4netHelper.Info("加密后字符串:"+tmpStr);
            if (tmpStr.Equals(signature))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public string Auth2(string echoStr,string msg_signature, string timestamp,string nonce)
        {
            #region 获取关键参数
            string token = Constant.CorpToken;// ConfigurationManager.AppSettings["CorpToken"];//从配置文件获取Token
            if (string.IsNullOrEmpty(token))
            {
               //LogTextHelper.Error(string.Format("CorpToken 配置项没有配置！"));
            }
            string encodingAESKey = Constant.EncodingAESKey; //ConfigurationManager.AppSettings["EncodingAESKey"];//从配置文件获取EncodingAESKey
            if (string.IsNullOrEmpty(encodingAESKey))
            {
               // LogTextHelper.Error(string.Format("EncodingAESKey 配置项没有配置！"));
            }
            string corpId = Constant.CorpId; //ConfigurationManager.AppSettings["CorpId"];//从配置文件获取corpId
            if (string.IsNullOrEmpty(corpId))
            {
               // LogTextHelper.Error(string.Format("CorpId 配置项没有配置！"));
            }
            #endregion

            //string echoStr = HttpContext.Current.Request.QueryString["echoStr"];
            //string msg_signature = HttpContext.Current.Request.QueryString["msg_signature"];//企业号的 msg_signature
            //string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            //string nonce = HttpContext.Current.Request.QueryString["nonce"];

            string decryptEchoString = "";
            if (CheckSignature(token, msg_signature, timestamp, nonce, corpId, encodingAESKey, echoStr, ref decryptEchoString))
            {
                if (!string.IsNullOrEmpty(decryptEchoString))
                {
                    return decryptEchoString;
                    //HttpContext.Current.Response.Write(decryptEchoString);
                    //HttpContext.Current.Response.End();
                }

            }
            

            return "";

        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private  string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
       

        /// <summary>
        /// 验证企业号签名
        /// </summary>
        /// <param name="token">企业号配置的Token</param>
        /// <param name="signature">签名内容</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">nonce参数</param>
        /// <param name="corpId">企业号ID标识</param>
        /// <param name="encodingAESKey">加密键</param>
        /// <param name="echostr">内容字符串</param>
        /// <param name="retEchostr">返回的字符串</param>
        /// <returns></returns>
        public bool CheckSignature(string token, string signature, string timestamp, string nonce, string corpId, string encodingAESKey, string echostr, ref string retEchostr)
        {
            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(token, encodingAESKey, corpId);
            int result = wxcpt.VerifyURL(signature, timestamp, nonce, echostr, ref retEchostr);
            if (result != 0)
            {
                //LogTextHelper.Error("ERR: VerifyURL fail, ret: " + result);
                return false;
            }

            return true;

           
        }

        /// <summary>
        /// 获取企业号的AccessToken
        /// </summary>
        /// <returns></returns>
        private static AccessToken GetAccessToken()
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", Constant.CorpId, Constant.Corpsecret);
            var responstr = RequestUtility.HttpGet(url);
            var AccessTokenInfo = JsonUtility.DeserializeObject<AccessToken>(responstr);
            return AccessTokenInfo;


        }

        /// <summary>
        /// 获取AccessTooken
        /// </summary>
        /// <returns></returns>
        public static string GetAccessTokenStr()
        {
            
            string wxaccwsssStr =$"{Constant.TokenRedisKeyStart}{Constant.Agentid.ToString()}";
            string Tokenstr = string.Empty;
            var Accesstokenstr = RedisHelper.StringGet(wxaccwsssStr);
            if (string.IsNullOrWhiteSpace(Accesstokenstr))
            {
                var TokenInfo = GetAccessToken();
                Tokenstr = TokenInfo.access_token;
                RedisHelper.StringSet(wxaccwsssStr, TokenInfo.access_token, TokenInfo.expires_in);
            }
            else
            {
                Tokenstr = Convert.ToString(Accesstokenstr);
            }

            return Tokenstr;

        }
    }
}
