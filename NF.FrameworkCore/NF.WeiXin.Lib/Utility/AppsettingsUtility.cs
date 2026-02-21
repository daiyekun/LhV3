using Microsoft.Extensions.Configuration;
using NF.WeiXin.Lib.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.WeiXin.Lib.Utility
{
    /// <summary>
    /// 配置文件读取
    /// </summary>
    public class AppsettingsUtility
    {
        public void Initial(IConfiguration configuration)
        {
            //公司ID
            Constant.CorpId = configuration["WeiXinStrings:corpid"];
            //应用ID
            var res= int.TryParse(configuration["WeiXinStrings:appInfo:agentId"], out int yyId);
            Constant.Agentid = res? yyId:0;
            //secret是企业应用里面用于保障数据安全的“钥匙”,数据加密保证数据安全
            Constant.Corpsecret = configuration["WeiXinStrings:appInfo:secret"];
            //微信请求外网地址
            Constant.WxAppBaseURL = configuration["WeiXinStrings:huidiao:wxAppBaseURL"];
            //回调url
            Constant.WxAPPRequestUrl= configuration["WeiXinStrings:huidiao:url"];
            //与回调之间加密密码
            Constant.EncodingAESKey = configuration["WeiXinStrings:huidiao:encodingAESKey"];
            //设置token
            Constant.CorpToken= configuration["WeiXinStrings:huidiao:corpToken"];
            //文件下载地址
            Constant.WxDownloadurl= configuration["ConnectionStrings:WxDownloadurl"];
           
        }

    }
}
