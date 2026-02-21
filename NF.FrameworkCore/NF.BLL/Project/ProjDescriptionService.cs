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
    /// 项目说明
    /// </summary>
   public partial class ProjDescriptionService
    {
        /// <summary>
        /// 查询备忘录
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ProjDescriptionViewDTO> GetList<s>(PageInfo<ProjDescription> pageInfo, Expression<Func<ProjDescription, bool>> whereLambda, Expression<Func<ProjDescription, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Pitem = a.Pitem,
                            ProjContent = a.ProjContent,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId= a.CreateUserId
                            //CreateUserName = a.CreateUser.DisplyName


                        };
            var local = from a in query.AsEnumerable()
                        select new ProjDescriptionViewDTO
                        {
                            Id = a.Id,
                            Pitem = a.Pitem,
                            ProjContent = a.ProjContent,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUserName
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                        };
            return new LayPageInfo<ProjDescriptionViewDTO>()
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
            string sqlstr = "update ProjDescription set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ProjDescriptionViewDTO ShowView(int Id)
        {
            var query = from a in _ProjDescriptionSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Pitem = a.Pitem,
                            ProjContent = a.ProjContent,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUser.DisplyName
                            CreateUserId= a.CreateUserId


                        };
            var local = from a in query.AsEnumerable()
                        select new ProjDescriptionViewDTO
                        {
                            Id = a.Id,
                            Pitem = a.Pitem,
                            ProjContent = a.ProjContent,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                           // RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            //CreateUserName = a.CreateUserName
                        };
            return local.FirstOrDefault();
        }
    }
}
