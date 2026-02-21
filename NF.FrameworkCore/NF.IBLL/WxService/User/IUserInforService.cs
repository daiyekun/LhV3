using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NF.ViewModel.Models.WeiXinModels;

namespace NF.IBLL
{
    /// <summary>
    /// 微信用户
    /// </summary>
   public partial  interface IUserInforService
    {
        /// <summary>
        /// 根据微信ID（微信账号）-微信通讯录唯一标识
        /// 获取用户信息
        /// </summary>
        /// <param name="WxUserId">微信账号</param>
        /// <returns></returns>
        WxUserInfo GetWxUserById(string WxUserId);
       
    }
}
