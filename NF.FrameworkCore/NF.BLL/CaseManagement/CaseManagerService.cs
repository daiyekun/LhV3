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
   public partial class CaseManagerService
    {
        /// <summary>
        /// 保存合同信息
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <param name="contractInfoHistory">合同历史信息</param>
        /// <returns>Id:合同ID、Hid:历史ID</returns>
        public CaseManager AddSave(CaseManager CaseManager)
        {

            
            var inof = Add(CaseManager);
            UpdateItems(inof);
            Lx(inof);
            return inof;
        }

        public void Lx(CaseManager contInfo) 
        {
            StringBuilder strsql = new StringBuilder();
            var currUserId = contInfo.ModifyUserId;

            strsql.Append($"INSERT INTO AjglXgLx(Name, Fznr, Ajglid) VALUES('{contInfo.LawyerName}', '{contInfo.HandRemark}', {contInfo.Id})");
            ExecuteSqlCommand(strsql.ToString());



        }

        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <param name="HisId">合同历史ID</param>
        public void UpdateItems(CaseManager contInfo)
        {
            StringBuilder strsql = new StringBuilder();
            var currUserId = contInfo.ModifyUserId;
         
            strsql.Append($"update AjglAttachment set Ajglid={contInfo.Id} where Ajglid={-currUserId};");
            strsql.Append($"update AjglSpwsAttachment set AjglSpwsid={contInfo.Id} where AjglSpwsid={-currUserId};");
            strsql.Append($"update AjglSsBqAttachment set AjglSsbqwjid={contInfo.Id} where AjglSsbqwjid={-currUserId};");
          
           
            ExecuteSqlCommand(strsql.ToString());

        }
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        public int ClearJunkItemData(int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"delete AjglAttachment  where Ajglid={-currUserId};");//合同备忘
            strsql.Append($"delete AjglSpwsAttachment  where AjglSpwsid={-currUserId};");//计划资金
            strsql.Append($"delete AjglSsBqAttachment  where AjglSsbqwjid={-currUserId};");//计划资金历史
          
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
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
        public LayPageInfo<AjjfDTO> GetList<s>(PageInfo<CaseManager> pageInfo, Expression<Func<CaseManager, bool>> whereLambda,
             Expression<Func<CaseManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _CaseManagerSet
                .Include(a => a.Dis)
               
                .AsTracking()
                .Where<CaseManager>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<CaseManager>))
            { //分页
                tempquery = tempquery.Skip<CaseManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<CaseManager>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Urgent = a.Urgent,
                            DisName = a.DisName,
                            DisCode = a.DisCode,
                            DisId = a.DisId,
                            Name = a.Name,
                            Code = a.Code,
                            HandDate = a.HandDate,
                            ContId = a.ContId,
                            HandlerId = a.HandlerId,
                            WoDw = a.WoDw,
                            Remark = a.Remark,
                            DisputeType = a.DisputeType,
                            Pawn = a.Pawn,
                            Amount = a.Amount,
                            AmoutDisc = a.AmoutDisc,
                            DisDic = a.DisDic,
                            CaseRemark = a.CaseRemark,
                            CaseBrief = a.CaseBrief,
                            CaseId = a.CaseId,
                            CaseFileName = a.CaseFileName,
                            CaseFilePath = a.CaseFilePath,
                            CourtName = a.CourtName,
                            BeginDate = a.BeginDate,
                            SpResult = a.SpResult,
                            SpFileName = a.SpFileName,
                            SpFilePath = a.SpFilePath,
                            LawyerName = a.LawyerName,
                            HandRemark = a.HandRemark,
                            PreType = a.PreType,
                            PreFileName = a.PreFileName,
                            PreFilePath = a.PreFilePath,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                            Dstate = a.Dstate,
                            AjName = a.Dis == null ? "" : a.Dis.Name,
                           

                        };
            var local = from a in query.AsEnumerable()
                        select new AjjfDTO
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Urgent = a.Urgent,
                            DisName = a.DisName,
                            DisCode = a.DisCode,
                            DisId = a.DisId,
                            Name = a.Name,
                            Code = a.Code,
                            HandDate = a.HandDate,
                            ContId = a.ContId,
                            HandlerId = a.HandlerId,
                            WoDw = a.WoDw,
                            Remark = a.Remark,
                            DisputeType = a.DisputeType,
                            Pawn = a.Pawn,
                            Amount = a.Amount,
                            AmoutDisc = a.AmoutDisc,
                            DisDic = a.DisDic,
                            CaseRemark = a.CaseRemark,
                            CaseBrief = a.CaseBrief,
                            CaseId = a.CaseId,
                            CaseFileName = a.CaseFileName,
                            CaseFilePath = a.CaseFilePath,
                            CourtName = a.CourtName,
                            BeginDate = a.BeginDate,
                            SpResult = a.SpResult,
                            SpFileName = a.SpFileName,
                            SpFilePath = a.SpFilePath,
                            LawyerName = a.LawyerName,
                            HandRemark = a.HandRemark,
                            PreType = a.PreType,
                            PreFileName = a.PreFileName,
                            PreFilePath = a.PreFilePath,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                            Dstate = a.Dstate,
                            ZtName=AJGLstate(a.Dstate),
                            UrgentName = this.Db.GetRedisDataDictionaryValue(a.Urgent , DataDictionaryEnum.Jjcd),//紧急程度
                            //DataDicUtility.GetDicValueToRedis(a.Urgent, DataDictionaryEnum.Jjcd),//紧急程度
                            DisputeName = this.Db.GetRedisDataDictionaryValue(a.DisputeType??0, DataDictionaryEnum.Jflb),//纠纷类别
                            //DataDicUtility.GetDicValueToRedis(a.DisputeType, DataDictionaryEnum.Jflb),//纠纷类别
                            PreTypeName = this.Db.GetRedisDataDictionaryValue(a.PreType ?? 0, DataDictionaryEnum.Bqlx),//安保类型
                                                                                                                       // DataDicUtility.GetDicValueToRedis(a.PreType, DataDictionaryEnum.Bqlx),//安保类型
                            ContIdName = this.Db.GetRedisUserFieldValue(a.ContId),//负责人Name
                                                                                  //RedisValueUtility.GetUserShowName(a.ContId),//负责人Name
                            HandlerIdName = this.Db.GetRedisUserFieldValue(a.HandlerId??0),//记录人
                       //     RedisValueUtility.GetUserShowName(a.HandlerId ?? 0),//记录人
                             CName = this.Db.GetRedisUserFieldValue(a.HandlerId??0),//记录人
                                                                                 //RedisValueUtility.GetUserShowName(a.HandlerId ?? 0),//建立人
                        };
            return new LayPageInfo<AjjfDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }

        public string AJGLstate(int? it) 
        {
            var s = "";
            if (it==0)
            {
                s = "已立项";
            }
            else if (it == 1)
            {
                s = "一审中";
            }
            else if (it == 2)
            {
                s = "二审中";
            }
            else if (it == 3)
            {
                s = "执行中";
            }
            else if (it == 4)
            {
                s = "已结案";
            }

            return s;
        }
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public AjjfDTO ShowView(int Id)
        {
            var query = from a in _CaseManagerSet
                         .Include(a => a.Dis)
                        .AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Urgent = a.Urgent,
                            DisName = a.DisName,
                            DisCode = a.DisCode,
                            DisId = a.DisId,
                            Name = a.Name,
                            Code = a.Code,
                            HandDate = a.HandDate,
                            ContId = a.ContId,
                            HandlerId = a.HandlerId,
                            WoDw = a.WoDw,
                            Remark = a.Remark,
                            DisputeType = a.DisputeType,
                            Pawn = a.Pawn,
                            Amount = a.Amount,
                            AmoutDisc = a.AmoutDisc,
                            DisDic = a.DisDic,
                            CaseRemark = a.CaseRemark,
                            CaseBrief = a.CaseBrief,
                            CaseId = a.CaseId,
                            CaseFileName = a.CaseFileName,
                            CaseFilePath = a.CaseFilePath,
                            CourtName = a.CourtName,
                            BeginDate = a.BeginDate,
                            SpResult = a.SpResult,
                            SpFileName = a.SpFileName,
                            SpFilePath = a.SpFilePath,
                            LawyerName = a.LawyerName,
                            HandRemark = a.HandRemark,
                            PreType = a.PreType,
                            PreFileName = a.PreFileName,
                            PreFilePath = a.PreFilePath,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                            Dstate = a.Dstate,
                            AjName = a.Dis == null ? "" : a.Dis.Name,

                        };
            var local = from a in query.AsEnumerable()
                        select new AjjfDTO
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Urgent = a.Urgent,
                            DisName = a.DisName,
                            DisCode = a.DisCode,
                            DisId = a.DisId,
                            Name = a.Name,
                            Code = a.Code,
                            HandDate = a.HandDate,
                            ContId = a.ContId,
                            HandlerId = a.HandlerId,
                            WoDw = a.WoDw,
                            Remark = a.Remark,
                            DisputeType = a.DisputeType,
                            Pawn = a.Pawn,
                            Amount = a.Amount,
                            AmoutDisc = a.AmoutDisc,
                            DisDic = a.DisDic,
                            CaseRemark = a.CaseRemark,
                            CaseBrief = a.CaseBrief,
                            CaseId = a.CaseId,
                            CaseFileName = a.CaseFileName,
                            CaseFilePath = a.CaseFilePath,
                            CourtName = a.CourtName,
                            BeginDate = a.BeginDate,
                            SpResult = a.SpResult,
                            SpFileName = a.SpFileName,
                            SpFilePath = a.SpFilePath,
                            LawyerName = a.LawyerName,
                            HandRemark = a.HandRemark,
                            PreType = a.PreType,
                            PreFileName = a.PreFileName,
                            PreFilePath = a.PreFilePath,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ModifyUserId = a.ModifyUserId,
                            ModifyDateTime = a.ModifyDateTime,
                            IsDelete = a.IsDelete,
                            Dstate = a.Dstate,
                            //UrgentName = DataDicUtility.GetDicValueToRedis(a.Urgent, DataDictionaryEnum.Jjcd),//紧急程度
                            //DisputeName = DataDicUtility.GetDicValueToRedis(a.DisputeType, DataDictionaryEnum.Jflb),//纠纷类别
                            //PreTypeName = DataDicUtility.GetDicValueToRedis(a.PreType, DataDictionaryEnum.Bqlx),//安保类型
                            //ContIdName = RedisValueUtility.GetUserShowName(a.ContId),//负责人Name
                            //HandlerIdName = RedisValueUtility.GetUserShowName(a.HandlerId ?? 0),//记录人
                            //CName = RedisValueUtility.GetUserShowName(a.HandlerId ?? 0),//建立人
                            UrgentName = this.Db.GetRedisDataDictionaryValue(a.Urgent, DataDictionaryEnum.Jjcd),//紧急程度
                            //DataDicUtility.GetDicValueToRedis(a.Urgent, DataDictionaryEnum.Jjcd),//紧急程度
                            DisputeName = this.Db.GetRedisDataDictionaryValue(a.DisputeType ?? 0, DataDictionaryEnum.Jflb),//纠纷类别
                            //DataDicUtility.GetDicValueToRedis(a.DisputeType, DataDictionaryEnum.Jflb),//纠纷类别
                            PreTypeName = this.Db.GetRedisDataDictionaryValue(a.PreType ?? 0, DataDictionaryEnum.Bqlx),//安保类型
                                                                                                                       // DataDicUtility.GetDicValueToRedis(a.PreType, DataDictionaryEnum.Bqlx),//安保类型
                            ContIdName = this.Db.GetRedisUserFieldValue(a.ContId),//负责人Name
                                                                                  //RedisValueUtility.GetUserShowName(a.ContId),//负责人Name
                            HandlerIdName = this.Db.GetRedisUserFieldValue(a.HandlerId ?? 0),//记录人
                                                                                             //     RedisValueUtility.GetUserShowName(a.HandlerId ?? 0),//记录人
                            CName = this.Db.GetRedisUserFieldValue(a.HandlerId ?? 0),//记录人
                                                                                     //RedisValueUtility.GetUserShowName(a.HandlerId ?? 0),//建立人
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
        public bool UpdateSave(CaseManager CaseManager)
        {
            var inof = Update(CaseManager);
            Lx(CaseManager);
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
                strsql.Append($"update CaseManager set IsDelete = 1 where Id in (" + Ids + ")");
            }
            else if (i == 1)
            {
                strsql.Append($"update CaseManager set IsDelete = 1 where Id in (" + Ids + ");");
            }
            return ExecuteSqlCommand(strsql.ToString());
        }



        /// <summary>
        /// 修改多个字段
        /// </summary>
        /// <param name="fields">当前字段集合</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(IList<UpdateFieldInfo> fields)
        {
            StringBuilder sqlstr = new StringBuilder($"update  CaseManager set ModifyUserId={fields[0].CurrUserId},ModifyDateTime='{DateTime.Now}'");
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
                        var time = "";
                        if (fd.FieldValue == null)
                        {
                            time = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            time = fd.FieldValue;
                        }
                        sqlstr.Append($" ,{fd.FieldName}='{time}' ");
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
