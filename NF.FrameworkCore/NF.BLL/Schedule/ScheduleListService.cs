using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Schedule;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
namespace NF.BLL
{
    public partial class ScheduleListService
    {


        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ScheduleListViewDTO> GetList<s>(PageInfo<ScheduleList> pageInfo, Expression<Func<ScheduleList, bool>> whereLambda, Expression<Func<ScheduleList, s>> orderbyLambda, bool isAsc)
        {
            var tempquery  = _ScheduleListSet.Include(a => a.Schedule).AsTracking().Where<ScheduleList>(whereLambda.Compile()).AsQueryable();
            //GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc).Include(a => a.Schedule).AsTracking().AsQueryable();
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            ScheduleName=a.ScheduleName,
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            Description = a.Description,
                            Descriptionms = a.Descriptionms,
                            Tixing = a.Tixing,
                            Designee = a.Designee,
                            Stalker = a.Stalker,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            ScheduleId = a.ScheduleId,
                            IsDelete = a.IsDelete,
                            Schedule=a.Schedule

                        };
            var local = from a in query.AsEnumerable()
                        select new ScheduleListViewDTO
                        {
                            Id = a.Id,
                            ScheduleName = a.ScheduleName,
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleAttributionDic= this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),
                            //DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),//任务归属
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            ScheduleDuixiangName = SelectDuix(this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                                //DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                            Description = a.Description,
                            Descriptionms = a.Descriptionms,
                            Tixing = a.Tixing,
                            TixingName = this.Db.GetRedisUserFieldValue(a.Tixing),
                            //RedisValueUtility.GetUserShowName(a.Tixing),
                            Designee = a.Designee,
                            DesigneeName = this.Db.GetRedisUserFieldValue(a.Designee),
                            //RedisValueUtility.GetUserShowName(a.Designee),
                            Stalker = a.Stalker,
                            StalkerName = this.Db.GetRedisUserFieldValue(a.Stalker),
                            //RedisValueUtility.GetUserShowName(a.Stalker ),
                            CreateUserId = a.CreateUserId,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId ?? 0),
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyUserName= this.Db.GetRedisUserFieldValue(a.ModifyUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ModifyUserId ?? 0),
                            ModifyDateTime = a.ModifyDateTime,
                            ScheduleId = a.ScheduleId,
                            IsDelete = a.IsDelete,
                            // myid=a.Schedule.Mystate
                            Jdname=  a.Schedule == null ? "" : a.Schedule.ScheduleName,
                          

                        };
            return new LayPageInfo<ScheduleListViewDTO>()
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
            string sqlstr = "update ScheduleList set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ScheduleListViewDTO ShowView(int Id)
        {
            var query = from a in _ScheduleListSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            ScheduleName = a.ScheduleName,
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            Description = a.Description,
                            Descriptionms = a.Descriptionms,
                            Tixing = a.Tixing,
                            Designee = a.Designee,
                            Stalker = a.Stalker,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            ScheduleId = a.ScheduleId,
                            IsDelete = a.IsDelete,

                        };
            var local = from a in query.AsEnumerable()
                        select new ScheduleListViewDTO
                        {
                            Id = a.Id,
                            ScheduleName = a.ScheduleName,
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleAttributionDic = this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),
                            //DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),//任务归属
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            ScheduleDuixiangName = SelectDuix(this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                            //SelectDuix(DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                            Description = a.Description,
                            Descriptionms = a.Descriptionms,
                            Tixing = a.Tixing,
                            TixingName = this.Db.GetRedisUserFieldValue(a.Tixing),
                            //RedisValueUtility.GetUserShowName(a.Tixing),
                            Designee = a.Designee,
                            DesigneeName = this.Db.GetRedisUserFieldValue(a.Tixing),
                            //RedisValueUtility.GetUserShowName(a.Designee),
                            Stalker = a.Stalker,
                            StalkerName = this.Db.GetRedisUserFieldValue(a.Stalker),
                            //RedisValueUtility.GetUserShowName(a.Stalker),
                            CreateUserId = a.CreateUserId,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId ?? 0),
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            ScheduleId = a.ScheduleId,
                            IsDelete = a.IsDelete,
                        };
            return local.FirstOrDefault();
        }

        public string SelectDuix(string Guishu, int DxId)
        {
            string Name = "";
            if (DxId > 0)
            {
                switch (Guishu)
                {
                    case "客户":
                        Name = Db.Set<Company>().Find(DxId).Name;
                        break;
                    case "供应商":
                        Name = Db.Set<Company>().Find(DxId).Name;
                        break;
                    case "其他对方":
                        Name = Db.Set<Company>().Find(DxId).Name;
                        break;
                    default:
                        break;
                }
            }
            return Name;


        }


        public int UpdateField(IList<UpdateFieldInfo> fields)
        {
            StringBuilder sqlstr = new StringBuilder($"update  ScheduleList set Myjdtime='{DateTime.Now}'");
            foreach (var fd in fields)
            {
                switch (fd.FieldType)
                {
                    case "int":
                        sqlstr.Append($" ,{fd.FieldName}={Convert.ToInt32(fd.FieldValue)} ");
                        break;
                    case "float":
                        sqlstr.Append($" ,{fd.FieldName}={Convert.ToDouble(fd.FieldValue)} ");
                        break;
                    default:
                        sqlstr.Append($" ,{fd.FieldName}='{fd.FieldValue}' ");
                        break;

                }
            }
            sqlstr.Append($"where Id={Convert.ToInt32(fields[0].Id)}");
            if (!string.IsNullOrEmpty(sqlstr.ToString()))
                return ExecuteSqlCommand(sqlstr.ToString());
            return 0;
        }
        public LayPageInfo<ScheduleListViewDTO> GetListdesk<s>(PageInfo<ScheduleList> pageInfo, Expression<Func<ScheduleList, bool>> whereLambda, Expression<Func<ScheduleList, s>> orderbyLambda, bool isAsc, List<int?> ids,int usid)
        {
            var tempquery = _ScheduleListSet.Include(a => a.Schedule).AsTracking().Where<ScheduleList>(whereLambda.Compile()).AsQueryable();
           
            var query = from a in tempquery
                        where a.Schedule.State==1&& ids.Contains(a.Mystate)
                        select new
                        {
                            Id = a.Id,
                            ScheduleName = a.ScheduleName,
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            Description = a.Description,
                            Descriptionms = a.Descriptionms,
                            Tixing = a.Tixing,
                            Designee = a.Designee,
                            Stalker = a.Stalker,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            ScheduleId = a.ScheduleId,
                            IsDelete = a.IsDelete,
                            Schedule = a.Schedule,
                            Myjdtime = a.Myjdtime,
                            Mystate = a.Mystate,
                         
                        };
            var local = from a in query.AsEnumerable()
                        select new ScheduleListViewDTO
                        {
                            Id = a.Id,
                            ScheduleName = a.ScheduleName,
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleAttributionDic = this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),
                            //DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),//任务归属
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            ScheduleDuixiangName = SelectDuix(this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                            //SelectDuix(DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                            Description = a.Description,
                            Descriptionms = a.Descriptionms,
                            Tixing = a.Tixing,
                            TixingName = this.Db.GetRedisUserFieldValue(a.Tixing),
                            //RedisValueUtility.GetUserShowName(a.Tixing),
                            Designee = a.Designee,
                            DesigneeName = this.Db.GetRedisUserFieldValue(a.Designee),
                            //RedisValueUtility.GetUserShowName(a.Designee),
                            Stalker = a.Stalker,
                            StalkerName = this.Db.GetRedisUserFieldValue(a.Stalker),
                            //RedisValueUtility.GetUserShowName(a.Stalker),
                            CreateUserId = a.CreateUserId,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId ?? 0),
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyUserName = this.Db.GetRedisUserFieldValue(a.ModifyUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ModifyUserId ?? 0),
                            ModifyDateTime = a.ModifyDateTime,
                            ScheduleId = a.ScheduleId,
                            IsDelete = a.IsDelete,
                            Jdname = a.Schedule == null ? "" : a.Schedule.ScheduleName,
                            JdtataTime = a.Myjdtime,
                            Mystate = a.Mystate,
                          MystateName=   EmunUtility.GetDesc(typeof(DescEnum), (a.Mystate ?? 0)),

                        };
            return new LayPageInfo<ScheduleListViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 跟踪工作台状态流转
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ty"></param>
        /// <returns></returns>
        public int Updstate(int id, int ty)
        {
            string sqlstr = "";
            if (ty==1)
            {
                 sqlstr = "update ScheduleList set Mystate=0  where Id in(" + id + ")";
            }
            else
            {
                 sqlstr = "update ScheduleList set Mystate=4 where Id in(" + id + ")";
            }
            return ExecuteSqlCommand(sqlstr);
        }

        /// <summary>
        /// 根据任务名字查找任务id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SJZd(string name) 
        {
            var id = 0;
            try
            {
                id = Db.Set<DataDictionary>().Where(a => a.Name == name && a.DtypeNumber == 36).FirstOrDefault().Id;
            }
            catch (Exception)
            {

                return id;
            }

            return id;
        }


    }
}
