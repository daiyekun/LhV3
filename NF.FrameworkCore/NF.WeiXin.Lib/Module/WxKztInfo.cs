using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.WeiXin.Lib.Module
{
    public class WxKztInfo
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public int agentid { get; set; }
        /// <summary>
        /// 若type指定为 “keydata”，且需要设置企业级别默认数据，
        /// 则需要设置关键数据型模版数据,数据结构参考“关键数据型
        /// </summary>
        public string type { get; set; } = "keydata";

        public IList<WxKeyData> items { get; set; }
        /// <summary>
        /// 是否覆盖用户工作台的数据。设置为true的时候，会覆盖企业所有用户当前设置的数据。
        /// 若设置为false,则不会覆盖用户当前设置的所有数据。默认为false
        /// </summary>
        public bool replace_user_data { get; set; } = true;




    }

    #region 用户推送时使用实体
    /// <summary>
    /// 用户工作台数据
    /// </summary>
    public class UserConsoleInfo
    { 
        /// <summary>
        /// 应用ID
        /// </summary>
        public int agentid { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; } = "keydata";
        /// <summary>
        /// 用户数据
        /// </summary>

        public Keydata keydata { get; set; }


    }
    /// <summary>
    /// 用户KEy
    /// </summary>
    public class Keydata
    {
        public IList<WxKeyData> items { get; set; }
    }

    #endregion


    /// <summary>
    /// 关键数据key
    /// </summary>

    public class WxKeyData
    {
        /// <summary>
        /// key
        /// </summary>
        public string key
        {
            get;set;
        }
        /// <summary>
        /// 数值
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string jump_url { get; set; }

    }
}