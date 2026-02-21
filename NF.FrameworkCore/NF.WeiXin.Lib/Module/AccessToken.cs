using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.WeiXin.Lib.Module
{
    /// <summary>
    /// 令牌
    /// </summary>
    public class AccessToken
    {    
        /// <summary>
        /// 令牌凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        public int expires_in { get; set; }

        
    }
}
