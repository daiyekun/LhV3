using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Extend.Enums
{
    /// <summary>
    /// 数据字典枚举
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum LoginState
    {
        /// <summary>
        /// 登录成功：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "登录成功")]
        succeed =0,
        /// <summary>
        /// 登录失败:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "登录失败")]
        notEnabled =1,
        /// <summary>
        /// 账号不存在：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "账号不存在")]
        notExist =2,
        /// <summary>
        /// 密码错误:3
        /// </summary>
        [EnumItem(Value = 3, Desc = "密码错误")]
        wrongPassword =3,





    }
    /// <summary>
    /// 性别
    /// </summary>
    [EnumClass(Max = 3, Min = 0, None = -1)]
    public enum SexEnum
    {
        /// <summary>
        /// 女
        /// </summary>
      [EnumItem(Value = 0, Desc = "女")]
      Female = 0,
      /// <summary>
      /// 男
      /// </summary>
      [EnumItem(Value = 1, Desc = "男")]
        Male =1,
      


    }
}
