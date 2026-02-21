using NF.IBLL;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using NF.Common.Utility;
using NF.ViewModel.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NF.ViewModel.Extend.Enums;
using LhCode;

namespace NF.BLL
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public partial class LoginLogService
    {
        
        public LayPageInfo<LoginLogDTO> GetList(PageInfo<LoginLog> pageInfo, Expression<Func<LoginLog, bool>> whereLambda)
        {
            var superAdmin = SystemStaticData.SuperAdminName;
            var tempquery = Db.Set<LoginLog>().Include(a => a.LoginUser).AsNoTracking().Where<LoginLog>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<LoginLog>((pageInfo.PageIndex - 1) * pageInfo.PageSize)
                 .Take<LoginLog>(pageInfo.PageSize);
            var query = from a in tempquery
                       select new 
                       {
                           Id=a.Id,
                           LoginUserId=a.LoginUserId,
                           RequestNetIp=a.RequestNetIp,
                           LoginIp=a.LoginIp,
                           Result=a.Result,
                           CreateDatetime=a.CreateDatetime,
                           LoginUserName= a.LoginUserId==-10000? superAdmin : a.LoginUser.Name
                       };
           
            var local = from a in query.AsEnumerable()
                        select new LoginLogDTO
                        {
                            Id = a.Id,
                            LoginUserId = a.LoginUserId,
                            RequestNetIp = a.RequestNetIp,
                            LoginIp = a.LoginIp,
                            Result = a.Result,
                            CreateDatetime = a.CreateDatetime,
                            LoginUserName = a.LoginUserName,
                            ResultDic = EmunUtility.GetDesc(typeof(LoginState), a.Result??0)

                        };
            return new LayPageInfo<LoginLogDTO>() {
                data= local.ToList(),
                count= pageInfo.TotalCount,
                code=0
                

            };




    }

    
             

    }
}
