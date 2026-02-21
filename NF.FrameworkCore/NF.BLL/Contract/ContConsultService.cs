using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 合同查阅人
    /// </summary>
    public partial  class ContConsultService
    {
        /// <summary>
        /// 查询合同查阅人
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContConsultViewDTO> GetList<s>(PageInfo<ContConsult> pageInfo, Expression<Func<ContConsult, bool>> whereLambda, Expression<Func<ContConsult, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery.Distinct()
                        select new
                        {
                            Id = a.Id,
                            UserId = a.UserId
                        };
            var local = from a in query.AsEnumerable()
                        select new ContConsultViewDTO
                        {
                            Id = a.Id,
                            UserId = a.UserId,
                            UserName = this.Db.GetRedisUserFieldValue(a.UserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.UserId??0,"Name"),
                            DisplayName = this.Db.GetRedisUserFieldValue(a.UserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.UserId ?? 0),
                            DeptName = this.Db.GetRedisUserFieldValue(a.UserId??0, "DeptName"),
                          //  RedisValueUtility.GetUserShowName(a.UserId ?? 0, "DeptName"),
                        };
            return new LayPageInfo<ContConsultViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "delete ContConsult where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
    }
}
