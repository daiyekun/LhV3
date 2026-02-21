using NF.WeiXin.Lib.Common;
using NF.WeiXin.Lib.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.WeiXin.Lib.Utility
{
    /// <summary>
    /// 微信应用控制台
    /// </summary>
    public  class WxConsoleUtility
    {

        /// <summary>
        /// 设置控制台模板
        /// https://qyapi.weixin.qq.com/cgi-bin/agent/set_workbench_template?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string SetConsolTemp(string postdata)
        {
            try
            {
                var TokeInfo = WeixinUtiliy.GetAccessTokenStr();
                string URL = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/set_workbench_template?access_token={0}", TokeInfo);
                //Log4netHelper.Info("菜单：" + PostMenusData);
                var rescode = RequestUtility.HttpPost4(URL, postdata);
                return rescode;

            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return "err";
            }
        }
        /// <summary>
        /// 获取模板
        ///  https://qyapi.weixin.qq.com/cgi-bin/agent/get_workbench_template?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string GetConselTemp()
        {
            try
            {
                var TokeInfo = WeixinUtiliy.GetAccessTokenStr();
                string URL = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/get_workbench_template?access_token={0}", TokeInfo);
                //Log4netHelper.Info("菜单：" + PostMenusData);
                var postdata = "{\"agentid\":"+ Constant.Agentid + "}";
                var rescode = RequestUtility.HttpPost4(URL, postdata);
                return rescode;

            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return "err";
            }
        }


        /// <summary>
        /// 设置用户控制台数据
        ///  https://qyapi.weixin.qq.com/cgi-bin/agent/set_workbench_data?access_token=ACCESS_TOKEN
        /// </summary>
        /// <returns></returns>
        public static string SubMitConsolData(string postdata)
        {
            try
            {
                var TokeInfo = WeixinUtiliy.GetAccessTokenStr();
                string URL = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/set_workbench_data?access_token={0}", TokeInfo);
                //Log4netHelper.Info("菜单：" + PostMenusData);
                var rescode = RequestUtility.HttpPost4(URL, postdata);
                return rescode;

            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return "err";
            }
        }
        /// <summary>
        /// 获取数据data
        /// </summary>
        /// <returns></returns>
        public static WxKztInfo GetConsoleData()
        {
            string AuthUrl = $"{Constant.WxAppBaseURL}/WxRuKou/WxHttpRedirect";
            ;
            IList<WxKeyData> listdata = new List<WxKeyData>();

            WxKeyData keyData0 = new WxKeyData();
            keyData0.key = "待处理";
            keyData0.data = "0";
            keyData0.jump_url = WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "NFDaiChuLi");
            listdata.Add(keyData0);

             keyData0 = new WxKeyData();
            keyData0.key = "我通过";
            keyData0.data = "0";
            keyData0.jump_url = WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "WoTongGuo");
            listdata.Add(keyData0);

             keyData0 = new WxKeyData();
            keyData0.key = "我打回";
            keyData0.data = "0";
            keyData0.jump_url = WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "WoDaHui");
            listdata.Add(keyData0);

            var wxdata = new WxKztInfo();
            wxdata.agentid = Constant.Agentid;
            wxdata.items = listdata;

            return wxdata;


        }

        /// <summary>
        ///组装用户数据
        /// </summary>
        /// <param name="data">数据data</param>
        /// <param name="wxcode">微信账号</param>
        /// <returns></returns>
        public static UserConsoleInfo GetUserConsolData(string wxcode,Dictionary<string,string> data)
        {
            IList<WxKeyData> listdata = new List<WxKeyData>();
            foreach (var key in data.Keys)
            {
                var currdata = new WxKeyData();
                currdata.key = key;
                currdata.data = data[key];
                currdata.jump_url = Getjumpurl(key);
                listdata.Add(currdata);

            }
            var _keydata = new Keydata();
            _keydata.items = listdata;
            UserConsoleInfo userConsole = new UserConsoleInfo();
            userConsole.userid = wxcode;
            userConsole.keydata = _keydata;
            userConsole.agentid= Constant.Agentid;
            return userConsole;



        }
        /// <summary>
        /// 获取url
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string Getjumpurl(string key)
        {
            string AuthUrl = $"{Constant.WxAppBaseURL}/WxRuKou/WxHttpRedirect";
            switch (key)
            {
                case "待处理":
                    return WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "NFDaiChuLi");
                case "我通过":
                    return WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "WoTongGuo");
                case "我打回":
                    return WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "WoDaHui");
                default:
                    return "";
            }

        }
    }
}
