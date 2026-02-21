using System;
using System.Collections.Generic;
using System.Text;
using NF.ViewModel.APPModels;
using NF.Model.Models;
using NF.Common.Models;

namespace NF.IBLL
{
   public partial interface IAPPUserInforService : IBaseService<UserInfor>
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="LoginName">登陆名称</param>
        /// <param name="LoginPwd">登陆密码</param>
        /// <returns></returns>
        RequstResult APPCheckLogin(string LoginName, string LoginPwd);
        /// <summary>
        /// 根据员工ID查询部门ID
        /// </summary>
        /// <param name="UserID">员工ID</param>
        /// <returns></returns>
        int SelectDEPid(int UserID);
    }
}
