using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 组用户
    /// </summary>
   public partial class GroupUserService
    {
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="pageInfo">用户对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns>用户列表</returns>
        public LayPageInfo<UserInforDTO> GetListUser(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda)
        {

            var tempquery = Db.Set<UserInfor>().Include(a=>a.Department).AsNoTracking().Where(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<UserInfor>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<UserInfor>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            LastName = a.LastName,
                            FirstName = a.FirstName,
                            DisplyName = a.DisplyName,
                            Sex = a.Sex,
                            Age = a.Age,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Email = a.Email,
                            EntryDatetime = a.EntryDatetime,
                            Ustart = a.Ustart,
                            DeptName = a.Department.Name,

                        };
            var local = from a in query.AsEnumerable()
                        select new UserInforDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            LastName = a.LastName,
                            FirstName = a.FirstName,
                            DisplyName = a.DisplyName,
                            Sex = a.Sex,
                            Age = a.Age,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Email = a.Email,
                            EntryDatetime = a.EntryDatetime,
                            Ustart = a.Ustart,
                            DeptName = a.DeptName,
                            SexDic = EmunUtility.GetDesc(typeof(SexEnum), a.Sex)
                        };
            return new LayPageInfo<UserInforDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }

        /// <summary>
        /// 用户组中间表
        /// </summary>
        /// <param name="userIds">用户IDS</param>
        /// <param name="groupId">组ID</param>
        /// <returns></returns>
        public int Delete(string userIds, int groupId)
        {
            string sqlstr = "delete GroupUser where UserId in(" + userIds + ") and GroupId=" + groupId;
            return ExecuteSqlCommand(sqlstr);
        }
    }
}
