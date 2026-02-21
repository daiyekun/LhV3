using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
    public partial class ScheduleDetailService
    {
        /// <summary>
        /// 进度管理列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<ScheduleDetailViewDTO> GetList<s>(PageInfo<ScheduleDetail> pageInfo, Expression<Func<ScheduleDetail, bool>> whereLambda,
             Expression<Func<ScheduleDetail, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ScheduleDetailSet.Include(a => a.ScheduleSerNavigation).AsTracking().Where<ScheduleDetail>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ScheduleDetail>))
            { //分页
                tempquery = tempquery.Skip<ScheduleDetail>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ScheduleDetail>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            id = a.Id,
                            ScheduleName = a.ScheduleName,
                            ScheduleSer = a.ScheduleSer,
                            ScheduleSerName = a.ScheduleSerNavigation.ScheduleName,
                            Description =a.Description,
                            Pdescription = a.Pdescription,
                            PddateTime = a.PddateTime,
                            Wancheng = a.Wancheng,
                            State = a.State,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                        };
            var local = from a in query.AsEnumerable()
                        select new ScheduleDetailViewDTO
                        {
                            Id = a.id,
                            ScheduleName = a.ScheduleName,
                            ScheduleSer = a.ScheduleSer,
                            ScheduleSerName= a.ScheduleSerName,
                            Description = a.Description,
                            Pdescription = a.Pdescription,
                            PddateTime=a.PddateTime,
                            Wancheng = a.Wancheng,
                            State = a.State,
                            StateDic = EmunUtility.GetDesc(typeof(ScheduleDetailEnums), (a.State ?? 0)),
                            CreateUserId = a.CreateUserId,
                            CreateName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId ?? 0),
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyName = this.Db.GetRedisUserFieldValue(a.ModifyUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ModifyUserId ?? 0),
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                        };

            return new LayPageInfo<ScheduleDetailViewDTO>()
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
        public ScheduleDetailViewDTO ShowView(int Id)
        {
            var query = from a in _ScheduleDetailSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            id = a.Id,
                            ScheduleName = a.ScheduleName,
                            ScheduleSer = a.ScheduleSer,
                            ScheduleSerName = a.ScheduleSerNavigation.ScheduleName,
                            Description = a.Description,
                            Pdescription = a.Pdescription,
                            PddateTime = a.PddateTime,
                            Wancheng = a.Wancheng,
                            State = a.State,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                        };
            var local = from a in query.AsEnumerable()
                        select new ScheduleDetailViewDTO
                        {
                            Id = a.id,
                            ScheduleName = a.ScheduleName,
                            ScheduleSer = a.ScheduleSer,
                            ScheduleSerName = a.ScheduleSerName,
                            Description = a.Description,
                            Pdescription = a.Pdescription,
                            PddateTime = a.PddateTime,
                            Wancheng = a.Wancheng,
                            State = a.State,
                            StateDic = EmunUtility.GetDesc(typeof(ScheduleDetailEnums), (a.State ?? 0)),
                            CreateUserId = a.CreateUserId,
                            CreateName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId ?? 0),
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyName = this.Db.GetRedisUserFieldValue(a.ModifyUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ModifyUserId ?? 0),
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                        };
            var teminfo = local.FirstOrDefault();
            return teminfo;
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
                string sqlstr = "update ScheduleDetail set IsDelete=1 where Id in(" + Ids + ")";
                return ExecuteSqlCommand(sqlstr);

            
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int ADDAtt(int SchId, int userId)
        {
            string sqlstr = "update ScheduleDetailAttachment set ScheduledId=" + SchId + " where ScheduledId=" + userId + "";
            return ExecuteSqlCommand(sqlstr);
        }
        public int Selectjd(int SchId)
        {

            int count = Db.Set<ScheduleDetail>().Where(p => p.ScheduleSer == SchId&&p.IsDelete==0).Count();
            if (count>0)
            {
              return  Db.Set<ScheduleDetail>().Where(p => p.ScheduleSer == SchId && p.IsDelete == 0).Max(p=>p.Wancheng ??0);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int DeleteAtt(int userId)
        {
            string sqlstr = "delete ScheduleDetailAttachment  where ScheduledId = " + userId + "";
            return ExecuteSqlCommand(sqlstr);
        }

        public int StateUpdate(int SchId, int userId)
        {
            int count = Db.Set<ScheduleManagement>().Where(p => p.Id == SchId && p.Stalker == userId).Count();
            if (count == 1)
            {
                return 0;
            }
            return 1;
        }
        public int UpdateJDgl(int ScheduleSer, int Wancheng)
        {
            string sqlstr = $"update  ScheduleManagement set Wancheng={Wancheng} where Id={ScheduleSer}";
            if (Wancheng==100)
            {
                string sqlstr1 = $"update  ScheduleManagement set State={1} where Id={ScheduleSer}";
                ExecuteSqlCommand(sqlstr1);
            }
            return ExecuteSqlCommand(sqlstr);
        }
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                case "State"://状态
                    var state = Convert.ToByte(info.FieldValue);
                    sqlstr = $"update  ScheduleDetail set State={state} where Id={info.Id}";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }

        /// <summary>
        /// 修改多个字段
        /// </summary>
        /// <param name="fields">当前字段集合</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(IList<UpdateFieldInfo> fields)
        {
            StringBuilder sqlstr = new StringBuilder($"update  ScheduleDetail set ModifyUserId={fields[0].CurrUserId},ModifyDateTime='{DateTime.Now}'");
            foreach (var fd in fields)
            {
                switch (fd.FieldType)
                {
                    case "int":
                        sqlstr.Append($" ,{fd.FieldName}={Convert.ToInt32(fd.FieldValue)} ");
                        break;
                    case "string":
                        sqlstr.Append($" ,{fd.FieldName}='{fd.FieldValue}' ");
                        break;
                    default:
                        
                        break;

                }
            }
            sqlstr.Append($"where Id={Convert.ToInt32(fields[0].Id)}");
            if (!string.IsNullOrEmpty(sqlstr.ToString()))
                return ExecuteSqlCommand(sqlstr.ToString());
            return 0;
        }
    }
}
