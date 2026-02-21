using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.IBLL;
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
    /// 用户角色中间表操作
    /// </summary>
    public partial class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="pageInfo">用户对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns>用户列表</returns>
        public LayPageInfo<UserInforDTO> GetListUser(PageInfo<UserInfor> pageInfo, Expression<Func<UserInfor, bool>> whereLambda,int roleId)
        {
            var listUIds = Db.Set<UserRole>().AsNoTracking().Where(a => a.RoleId == roleId).Select(a => a.UserId).Distinct().ToArray();
            var tempquery = Db.Set<UserInfor>().AsNoTracking().Include(a=>a.Department).Where<UserInfor>(whereLambda.Compile()).AsQueryable().Where(a=> listUIds.Contains(a.Id));
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
                            DeptName = a.Department.Name==null ? "":a.Department.Name,
                           // DeptName = a.Department.Name,

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
        /// 用户角色中间表
        /// </summary>
        /// <param name="userIds">用户IDS</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public int Delete(string userIds, int roleId)
        {
            string sqlstr = "delete UserRole where UserId in(" + userIds + ") and RoleId="+ roleId;
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 获取角色用户中间表
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <returns></returns>
        public IList<UserRole> GetListUserRole(Expression<Func<UserRole, bool>> whereLambda)
        {
            var tempquery = Db.Set<UserRole>().AsNoTracking().Where<UserRole>(whereLambda.Compile()).AsQueryable();
            return tempquery.ToList();
        }
        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="userRoles">保存用户角色</param>
        /// <param name="uId">当前用户ID</param>
        /// <returns></returns>
        public IEnumerable<UserRole> SetUserRole(IEnumerable<UserRole> userRoles, int uId)
        {
            string sqlstr = "delete UserRole where UserId=" + uId;
           ExecuteSqlCommand(sqlstr);
          return Add(userRoles);
        }
    }
}
