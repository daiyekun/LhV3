using NF.ViewModel.Models.WeiXinModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
    /// <summary>
    /// 微信-应用操作
    /// </summary>
    public partial class UserInforService
    {
        public  WxUserInfo GetWxUserById(string WxUserId)
        {
            var query = from a in this._UserInforSet
                        where a.WxCode== WxUserId && a.IsDelete == 0
                        select new
                        {
                            UserId = a.Id,
                            Uname = a.Name,
                            UdisName = a.DisplyName,
                            UdepName = a.Department.Name,
                            Utel = a.Tel,
                            Umobile = a.Mobile,
                            Uemail= a.Email,
                            Sex = a.Sex,
                            

                        };
            var local = from a in query.AsEnumerable()
                        select new WxUserInfo
                        {
                            UserId = a.UserId,
                            Uname = a.Uname,
                            UdisName = a.UdisName,
                            UdepName = a.UdepName,
                            Utel = a.Utel,
                            Umobile = a.Umobile,
                            Uemail = a.Uemail,
                            Sex = a.Sex,

                        };
            return local.FirstOrDefault();
        }

    }
}
