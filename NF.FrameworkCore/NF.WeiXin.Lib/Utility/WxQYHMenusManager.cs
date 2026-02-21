using NF.WeiXin.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.WeiXin.Lib.Utility
{
    /// <summary>
    /// 微信企业号菜单管理
    /// </summary>
    public  class WxQYHMenusManager
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        public string CreateMenus(string PostMenusData) 
        {
            try
            {
                var TokeInfo = WeixinUtiliy.GetAccessTokenStr() ;
                string URL = string.Format("https://qyapi.weixin.qq.com/cgi-bin/menu/create?access_token={0}&agentid={1}", TokeInfo, Constant.Agentid);
                //Log4netHelper.Info("菜单：" + PostMenusData);
                var rescode = RequestUtility.HttpPost4(URL, PostMenusData);
                return rescode;
               
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return "";
            }
         }
    }
}
