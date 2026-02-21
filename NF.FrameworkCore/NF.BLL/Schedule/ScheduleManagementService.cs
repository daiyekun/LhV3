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
    public partial class ScheduleManagementService
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
        public LayPageInfo<ScheduleManagementViewDTO> GetList<s>(PageInfo<ScheduleManagement> pageInfo, Expression<Func<ScheduleManagement, bool>> whereLambda,
             Expression<Func<ScheduleManagement, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ScheduleManagementSet.AsTracking().Where<ScheduleManagement>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ScheduleManagement>))
            { //分页
                tempquery = tempquery.Skip<ScheduleManagement>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ScheduleManagement>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                       // where a.Id==6
                        select new
                        {
                            id = a.Id,
                            ScheduleName=a.ScheduleName,
                            ScheduleSer = a.ScheduleSer,
                            Priority = a.Priority,
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            Description = a.Description,
                            Designee = a.Designee,
                            Stalker = a.Stalker,
                            JhCreateDateTime = a.JhCreateDateTime,
                            JhCompleteDateTime = a.JhCompleteDateTime,
                            SjCreateDateTime = a.SjCreateDateTime,
                            SjCompleteDateTime = a.SjCompleteDateTime,
                            State = a.State,
                            Wancheng = a.Wancheng,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                        };
            var local = from a in query.AsEnumerable()
                        select new ScheduleManagementViewDTO
                        {
                            Id = a.id,
                            ScheduleName = a.ScheduleName,
                            ScheduleSer = a.ScheduleSer,
                            Priority = a.Priority,
                            PriorityDic = this.Db.GetRedisDataDictionaryValue(a.Priority, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.Priority, DataDictionaryEnum.PrioritySource),//优先级
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleAttributionDic = this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),
                            //DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),//任务归属
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            ScheduleDuixiangName = SelectDuix(this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                            //SelectDuix(DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang??0),
                            Description = a.Description,
                            Designee = a.Designee,
                            DesigneeName= this.Db.GetRedisUserFieldValue(a.Designee),
                            //RedisValueUtility.GetUserShowName(a.Designee),
                            Stalker = a.Stalker,
                            StalkerName = this.Db.GetRedisUserFieldValue(a.Designee),
                            //RedisValueUtility.GetUserShowName(a.Stalker),
                            JhCreateDateTime = a.JhCreateDateTime,
                            JhCompleteDateTime = a.JhCompleteDateTime,
                            SjCreateDateTime = a.SjCreateDateTime,
                            SjCompleteDateTime = a.SjCompleteDateTime,
                            State = a.State,
                            StateDic = EmunUtility.GetDesc(typeof(ScheduleEnums), (a.State ?? 0)),
                            Wancheng = a.Wancheng,
                            CreateUserId = a.CreateUserId,
                            CreateName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId??0),
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyName = this.Db.GetRedisUserFieldValue(a.ModifyUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ModifyUserId ?? 0),
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                        };

            return new LayPageInfo<ScheduleManagementViewDTO>()
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
        public ScheduleManagementViewDTO ShowView(int Id)
        {
            var query = from a in _ScheduleManagementSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            id = a.Id,
                            ScheduleName = a.ScheduleName,
                            ScheduleSer = a.ScheduleSer,
                            Priority = a.Priority,
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            Description = a.Description,
                            Designee = a.Designee,
                            Stalker = a.Stalker,
                            JhCreateDateTime = a.JhCreateDateTime,
                            JhCompleteDateTime = a.JhCompleteDateTime,
                            SjCreateDateTime = a.SjCreateDateTime,
                            SjCompleteDateTime = a.SjCompleteDateTime,
                            State = a.State,
                            Wancheng = a.Wancheng,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                        };
            var local = from a in query.AsEnumerable()
                        select new ScheduleManagementViewDTO
                        {
                            Id = a.id,
                            ScheduleName = a.ScheduleName,
                            ScheduleSer = a.ScheduleSer,
                            Priority = a.Priority,
                            PriorityDic = this.Db.GetRedisDataDictionaryValue(a.Priority, DataDictionaryEnum.PrioritySource),
                            //DataDicUtility.GetDicValueToRedis(a.Priority, DataDictionaryEnum.PrioritySource),//优先级
                            ScheduleAttribution = a.ScheduleAttribution,
                            ScheduleAttributionDic = this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),
                            //DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource),//任务归属
                            ScheduleDuixiang = a.ScheduleDuixiang,
                            ScheduleDuixiangName = SelectDuix(this.Db.GetRedisDataDictionaryValue(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                            //SelectDuix(DataDicUtility.GetDicValueToRedis(a.ScheduleAttribution, DataDictionaryEnum.ScheduleAttributionSource), a.ScheduleDuixiang ?? 0),
                            Description = a.Description,
                            Designee = a.Designee,
                            DesigneeName = this.Db.GetRedisUserFieldValue(a.Designee),
                            //RedisValueUtility.GetUserShowName(a.Designee),
                            Stalker = a.Stalker,
                            StalkerName = this.Db.GetRedisUserFieldValue(a.Stalker),
                            //RedisValueUtility.GetUserShowName(a.Stalker),
                            JhCreateDateTime = a.JhCreateDateTime,
                            JhCompleteDateTime = a.JhCompleteDateTime,
                            SjCreateDateTime = a.SjCreateDateTime,
                            SjCompleteDateTime = a.SjCompleteDateTime,
                            State = a.State,
                            StateDic = EmunUtility.GetDesc(typeof(ScheduleEnums), (a.State ?? 0)),
                            Wancheng = a.Wancheng,
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
            string sqlstr = "update ScheduleManagement set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }

        public string SelectDuix(string Guishu,int DxId)
        {
            string Name = "";
            if (DxId>0)
            {
                switch (Guishu)
                {
                    case "客户":
                        Name=Db.Set<Company>().Find(DxId).Name;
                        break;
                    case "供应商":
                        Name = Db.Set<Company>().Find(DxId).Name;
                        break;
                    case "其他对方":
                        Name = Db.Set<Company>().Find(DxId).Name;
                        break;
                    case "项目":
                        Name = Db.Set<ProjectManager>().Find(DxId).Name;
                        break;
                    case "收款合同":
                        try
                        {
                            Name = Db.Set<ContractInfo>().Find(DxId).Name;
                        }
                        catch (Exception)
                        {

                            Name = "";
                        }
                     
                        break;
                    case "付款合同":
                    //    Name = Db.Set<ContractInfo>().Find(DxId).Name;
                        try
                        {
                            Name = Db.Set<ContractInfo>().Find(DxId).Name;
                        }
                        catch (Exception)
                        {

                            Name = "";
                        }

                        break;
                    case "招标":
                        Name = Db.Set<TenderInfor>().Find(DxId).Project.Name;
                        break;
                    case "询价":
                        Name = Db.Set<Inquiry>().Find(DxId).ProjectNameNavigation.Name;
                        break;
                    case "洽谈":
                        Name = Db.Set<Questioning>().Find(DxId).ProjectNameNavigation.Name;
                        break;
                    case "收票":
                        Name = Db.Set<ContInvoice>().Find(DxId).InCode;
                        break;
                    case "开票":
                        Name = Db.Set<ContInvoice>().Find(DxId).InCode;
                        break;
                    case "实际收款":
                        Name = Db.Set<ContActualFinance>().Find(DxId).Cont.Name;
                        break;
                    case "实际付款":
                        Name = Db.Set<ContActualFinance>().Find(DxId).Cont.Name;
                        break;
                    case "收款合同文本":
                        Name = Db.Set<ContText>().Find(DxId).Name;
                        break;
                    case "付款合同文本":
                        Name = Db.Set<ContText>().Find(DxId).Name;
                        break;
                    case "收款合同明细":
                        Name = Db.Set<ContSubDelivery>().Find(DxId).Sub.Name;
                        break;
                    case "付款合同明细":
                        Name = Db.Set<ContSubDelivery>().Find(DxId).Sub.Name;
                        break;
                    default:
                        break;
                }
            }
            return Name;


        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int ADDAtt(int SchId, int userId)
        {
            string sqlstr ="update ScheduleManagementAttachment set SchedulemId="+ SchId + " where SchedulemId=" + userId + "";
            string sqlstr1 = "update ScheduleList set ScheduleId=" + SchId + " where ScheduleId=" + userId + "";
            ExecuteSqlCommand(sqlstr1);
            return ExecuteSqlCommand(sqlstr);
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int DeleteAtt(int userId)
        {
            string sqlstr = "delete ScheduleManagementAttachment  where SchedulemId = " + userId + "";
            string sqlstr1 = "delete ScheduleList  where ScheduleId = " + userId + "";
            ExecuteSqlCommand(sqlstr1);
            return ExecuteSqlCommand(sqlstr);
        }

        public int StateUpdate(int SchId, int userId)
        {
           int count=Db.Set<ScheduleManagement>().Where(p => p.Id == SchId&&p.Designee== userId).Count();
            if (count==1)
            {
                return 0;
            }
            return 1;
        }
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                case "State"://状态
                    var state = Convert.ToByte(info.FieldValue);
                    sqlstr = $"update  ScheduleManagement set State={state} where Id={info.Id}";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }
    }
}
