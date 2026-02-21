using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.WeiXin.Lib.Module
{
    /// <summary>
    /// 微信企业号用户信息实体
    /// </summary>
    public class WxQYHUserInfocs
    {
        /// <summary>
        /// 成员UserID。对应管理端的帐号
        /// </summary>
        public string Userid { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name{get;set;}
        /// <summary>
        /// 部门ID列表
        /// </summary>
        public string Department{get;set;}
        /// <summary>
        /// 部门ID集合
        /// </summary>
        public IList<int> DeptIds { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position{get;set;}
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile{get;set;}
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender{get;set;}
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email{get;set;}
        /// <summary>
        /// 微信号
        /// </summary>
        public string Weixinid{get;set;}
        /// <summary>
        /// 头像url。注：如果要获取小图将url最后的"/0"改成"/64"即可
        /// </summary>
         public string Avatar{get;set;}
        /// <summary>
        /// 关注状态: 1=已关注，2=已禁用，4=未关注 
        /// </summary>
         public string Status{get;set;}
        /// <summary>
        /// 扩展属性
        /// </summary>
         public string Extattr{get;set;}
        /// <summary>
        /// 页面授权时候产生
         /// 手机设备号(由微信在安装时随机生成，删除重装会改变，升级不受影响，同一设备上不同的登录账号生成的deviceid也不同) 
        /// </summary>
         public string DeviceId { get; set; }



    }
}
