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
    /// 项目时间
    /// </summary>
  public partial  class ProjScheduleService
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
        public LayPageInfo<ProjScheduleViewDTO> GetList<s>(PageInfo<ProjSchedule> pageInfo, Expression<Func<ProjSchedule, bool>> whereLambda, Expression<Func<ProjSchedule, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Pitem = a.Pitem,
                            PlanBeginDateTime = a.PlanBeginDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualBeginDateTime = a.ActualBeginDateTime,
                            ActualCompleteDateTime = a.ActualCompleteDateTime,
                            //CreateUserName = a.CreateUser.DisplyName,
                            CreateUserId= a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            Remark = a.Remark,


                        };
            var local = from a in query.AsEnumerable()
                        select new ProjScheduleViewDTO
                        {
                            Id = a.Id,
                            Pitem = a.Pitem,
                            PlanBeginDateTime = a.PlanBeginDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualBeginDateTime = a.ActualBeginDateTime,
                            ActualCompleteDateTime = a.ActualCompleteDateTime,
                            //CreateUserName = a.CreateUserName,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CreateDateTime = a.CreateDateTime,
                            Remark = a.Remark,
                        };
            return new LayPageInfo<ProjScheduleViewDTO>()
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
            string sqlstr = "update ProjSchedule set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ProjScheduleViewDTO ShowView(int Id)
        {
            var query = from a in _ProjScheduleSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Pitem = a.Pitem,
                            PlanBeginDateTime = a.PlanBeginDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualBeginDateTime=a.ActualBeginDateTime,
                            ActualCompleteDateTime= a.ActualCompleteDateTime,
                            // CreateUserName = a.CreateUser.DisplyName,
                            CreateUserId=a.CreateUserId,
                            CreateDateTime =a.CreateDateTime,
                            Remark=a.Remark,

                        };
            var local = from a in query.AsEnumerable()
                        select new ProjScheduleViewDTO
                        {
                            Id = a.Id,
                            Pitem = a.Pitem,
                            PlanBeginDateTime = a.PlanBeginDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualBeginDateTime = a.ActualBeginDateTime,
                            ActualCompleteDateTime = a.ActualCompleteDateTime,
                            // CreateUserName = a.CreateUserName,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CreateDateTime = a.CreateDateTime,
                            Remark = a.Remark,
                        };
            return local.FirstOrDefault();
        }
    }
}
