using System;
using System.Collections.Generic;
using System.Text;
using NF.Model.Models;
using NF.Common.Models;
using NF.IBLL;
using System.Linq;
using NF.Common.Utility;
using Microsoft.EntityFrameworkCore;

namespace NF.BLL
{
    public partial class APPUserInforService : BaseService<UserInfor>, IAPPUserInforService
    {
        private DbSet<UserInfor> _APPUserInforSet = null;
        public APPUserInforService(DbContext dbContext)
           : base(dbContext)
        {
            _APPUserInforSet = base.Db.Set<UserInfor>();
        }

        public APPUserInforService() { }
        /// <summary>
        /// 登陆校验
        /// </summary>
        /// <param name="LoginName">登陆名称</param>
        /// <param name="LoginPwd">登陆密码</param>
        /// <returns>返回结果集</returns>
        public RequstResult APPCheckLogin(string LoginName, string LoginPwd)
        {
            RequstResult resultData = new RequstResult { Code = 0 };

            bool exist = false;
            var query = _APPUserInforSet.Where(a => a.Name == LoginName && a.IsDelete != 1);
            try
            {
                exist = query.Any();
            }
            catch (Exception ex)
            {
                resultData.Tag = 4;
                resultData.Msg = ex.Message;
                return resultData;


            }
            if (!exist)
            {
                resultData.Tag = -1;
                resultData.Msg = "此用户不存在！";

            }
            else
            {
                string md5pwd = MD5Encrypt.Encrypt(LoginPwd);
                md5pwd = MD5Encrypt.Encrypt(md5pwd);
                var info = query.Where(a => a.Pwd == md5pwd).FirstOrDefault();
                if (info == null)
                {
                    resultData.Tag = 0;
                    resultData.Msg = "密码错误！";

                }
                else if (info.Ustart != 1)
                {
                    resultData.Tag = 3;
                    resultData.Msg = "此用户未启用，请联系管理员！";
                }
                else
                {
                    resultData.Tag = 1;
                    resultData.Code = 0;
                    resultData.User_id = query.FirstOrDefault().Id;
                    resultData.RetValue = query.FirstOrDefault(); 
                }



            }

            return resultData;
        }
        /// <summary>
        /// 根据员工ID查询部门ID
        /// </summary>
        /// <param name="UserID">员工ID</param>
        /// <returns></returns>
        public int SelectDEPid(int UserID)
        {
            int ID = 0;
          var query = _APPUserInforSet.Where(a => a.Id == UserID && a.IsDelete != 1);
            if (query.Count()>0)
            {
                ID = query.FirstOrDefault().DepartmentId??0 ;
            }
            return ID;
        }

    }
}
