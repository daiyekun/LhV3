using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
  public partial  class DisputeManagerService
    {
        /// <summary>
        /// 保存合同信息
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <param name="contractInfoHistory">合同历史信息</param>
        /// <returns>Id:合同ID、Hid:历史ID</returns>
        public DisputeManager AddSave(DisputeManager DisputeManager)
        {
           
           
            var inof = Add(DisputeManager);

            return inof;
        }


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<jFsbDTO> GetList<s>(PageInfo<DisputeManager> pageInfo, Expression<Func<DisputeManager, bool>> whereLambda,
             Expression<Func<DisputeManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _DisputeManagerSet
                .Include(a => a.Comp)
                .Include(a => a.Cont)
                .Include(a => a.Handler)
                .AsTracking()
                .Where<DisputeManager>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DisputeManager>))
            { //分页
                tempquery = tempquery.Skip<DisputeManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DisputeManager>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Title=a.Title,
                            Code = a.Code,
                            Urgent = a.Urgent,
                            CompId = a.CompId,
                            HandlerId = a.HandlerId,
                            HandDate = a.HandDate,
                            ContId = a.ContId,
                            DisputeType = a.DisputeType,
                            CompName= a.Comp == null ? "" : a.Comp.Name,
                            ContIdName = a.Cont == null ? "" : a.Cont.Name,
                            HandlerIdName = a.Handler == null ? "" : a.Handler.Name,
                            Amount = a.Amount,
                            CompInfo = a.CompInfo,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                            Dstate = a.Dstate,
                          
                        };
            var local = from a in query.AsEnumerable()
                        select new jFsbDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Title=a.Title,
                            UrgentName = this.Db.GetRedisDataDictionaryValue(a.Urgent, DataDictionaryEnum.Jjcd),
                            //DataDicUtility.GetDicValueToRedis(a.Urgent, DataDictionaryEnum.Jjcd),//紧急程度
                            //ZdName(a.Urgent),
                            CompId = a.CompId,
                            HandlerId = a.HandlerId,
                            HandDate = a.HandDate,
                            ContId = a.ContId,
                            DisputeName = this.Db.GetRedisDataDictionaryValue(a.DisputeType, DataDictionaryEnum.Jflb),
                           // DataDicUtility.GetDicValueToRedis(a.DisputeType, DataDictionaryEnum.Jflb),//纠纷类别
                            //ZdName(a.DisputeType),
                            Amount = a.Amount,
                            CompInfo = a.CompInfo,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                            Dstate = a.Dstate,
                             CompName = a.CompName,
                             ContIdName =a.ContIdName,
                             HandlerIdName=a.HandlerIdName,
                            CName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? 0),//
                                                                                     //RedisValueUtility.GetUserShowName(a.CreateUserId??0)
                        };
            return new LayPageInfo<jFsbDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }

        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public jFsbDTO ShowView(int Id)
        {
            var query = from a in _DisputeManagerSet
                         .Include(a => a.Comp)
                .Include(a => a.Cont)
                .Include(a => a.Handler)
                        .AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Title = a.Title,
                            Code = a.Code,
                            Urgent = a.Urgent,
                            CompId = a.CompId,
                            HandlerId = a.HandlerId,
                            HandDate = a.HandDate,
                            ContId = a.ContId,
                            DisputeType = a.DisputeType,
                            CompName = a.Comp == null ? "" : a.Comp.Name,
                            ContIdName = a.Cont == null ? "" : a.Cont.Name,
                            HandlerIdName = a.Handler == null ? "" : a.Handler.Name,
                            Amount = a.Amount,
                            CompInfo = a.CompInfo,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                            Dstate = a.Dstate,
                        };
            var local = from a in query.AsEnumerable()
                        select new jFsbDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Title = a.Title,
                            UrgentName = this.Db.GetRedisDataDictionaryValue(a.Urgent, DataDictionaryEnum.Jjcd),
                            //DataDicUtility.GetDicValueToRedis(a.Urgent, DataDictionaryEnum.Jjcd),//紧急程度
                            Urgent= a.Urgent,
                            //ZdName(a.Urgent),
                            CompId = a.CompId,
                            HandlerId = a.HandlerId,
                            HandDate = a.HandDate,
                            ContId = a.ContId,
                            DisputeName = this.Db.GetRedisDataDictionaryValue(a.DisputeType, DataDictionaryEnum.Jflb),
                            //DataDicUtility.GetDicValueToRedis(a.DisputeType, DataDictionaryEnum.Jflb),//纠纷类别
                            DisputeType = a.DisputeType,
                            //ZdName(a.DisputeType),
                            Amount = a.Amount,
                            CompInfo = a.CompInfo,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                            Dstate = a.Dstate,
                            CompName = a.CompName,
                            ContIdName = a.ContIdName,
                            HandlerIdName = a.HandlerIdName,
                            CName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? 0),//
                                                                                        //RedisValueUtility.GetUserShowName(a.CreateUserId ?? 0)

                        };

            var teminfo = local.FirstOrDefault();
          
            return teminfo;
        }
        /// <summary>
        /// 修改合同信息
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <param name="contractInfoHistory">合同历史信息</param>
        /// <returns>Id:合同ID、Hid:历史ID</returns>
        public bool UpdateSave(DisputeManager disputeManager)
        {
            var inof = Update(disputeManager);
           
            return inof;
        }


        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids, int i)
        {
            StringBuilder strsql = new StringBuilder();
            if (i == 0)
            {
                strsql.Append($"update DisputeManager set IsDelete = 1 where Id in (" + Ids + ")");
            }
            else if (i == 1)
            {
                strsql.Append($"update DisputeManager set IsDelete = 1 where Id in (" + Ids + ");");
            }
            return ExecuteSqlCommand(strsql.ToString());
        }
    }
}
