using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 其他联系人
    /// </summary>
    public partial class CompContactService
    {
        /// <summary>
        /// 查询联系人
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<CompContactViewDTO> GetList<s>(PageInfo<CompContact> pageInfo, Expression<Func<CompContact, bool>> whereLambda, Expression<Func<CompContact, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DeptName = a.DeptName,
                            Position = a.Position,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Fax = a.Fax,
                            Email = a.Email,
                            Im = a.Im,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId=a.CreateUserId
                           // CreateUserDisplyName = a.CreateUser.DisplyName


                        };
            var local = from a in query.AsEnumerable()
                        select new CompContactViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DeptName = a.DeptName,
                            Position = a.Position,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Fax = a.Fax,
                            Email = a.Email,
                            Im = a.Im,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplyName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),// a.CreateUserDisplyName
                        };
            return new LayPageInfo<CompContactViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update CompContact set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public CompContactViewDTO ShowView(int Id)
        {
            var query = from a in _CompContactSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DeptName = a.DeptName,
                            Position = a.Position,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Fax = a.Fax,
                            Email = a.Email,
                            Im = a.Im,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplyName = a.CreateUser.DisplyName


                        };
            var local = from a in query.AsEnumerable()
                        select new CompContactViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DeptName = a.DeptName,
                            Position = a.Position,
                            Tel = a.Tel,
                            Mobile = a.Mobile,
                            Fax = a.Fax,
                            Email = a.Email,
                            Im = a.Im,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplyName = a.CreateUserDisplyName
                        };
            return local.FirstOrDefault();
        }
    }
}
