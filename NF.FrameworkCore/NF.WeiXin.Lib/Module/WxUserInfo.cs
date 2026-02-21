using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.WeiXin.Lib.Module
{
    /// <summary>
    /// 微信用户信息实体
    /// </summary>
   public  class WxUserInfo
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
       /// </summary>
       public int Subscribe { get; set; }
       /// <summary>
       /// 用户的标识，对当前公众号唯一 
       /// </summary>
       public string Openid { get; set; }
       /// <summary>
       /// 昵称
       /// </summary>
       public string Nickname { get; set; }
       /// <summary>
       /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
       /// </summary>
       public int Sex { get; set; }
       /// <summary>
       /// 所在城市
       /// </summary>
       public string City { get; set; }
       /// <summary>
       /// 所在国家
       /// </summary>
       public string Country { get; set; }
       /// <summary>
       /// 用户所在省份
       /// </summary>
       public string Province { get; set; }
       /// <summary>
       /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，
       /// 0代表640*640正方形头像），用户没有头像时该项为空。
       /// 若用户更换头像，原有头像URL将失效
       /// </summary>
       public string Headimgurl { get; set; }
       /// <summary>
       /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
       /// </summary>
       public string Subscribe_time { get; set; }
       /// <summary>
       /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：获取用户个人信息（UnionID机制） 
       /// </summary>
       public string Unionid { get; set; }
       /// <summary>
       /// 备注
       /// </summary>
       public string Remark { get; set; }
       /// <summary>
       /// 用户所在的分组ID 
       /// </summary>
       public int Groupid { get; set; }
       /// <summary>
       ///语言 
       /// </summary>
       public string Language { get; set; }



    }
    /// <summary>
    /// 微信用户
    /// </summary>

    public class WxUser
    {
        /// <summary>
        /// 微信账号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 手机驱动-手机设备号
        /// </summary>
        public string DeviceId { get; set; }
    }
}
