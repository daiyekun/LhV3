using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Extend;
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

namespace NF.BLL
{
    public partial class TenderInforService
    {
        /// <summary>
        /// 保存招标信息
        /// </summary>
        /// <param name="tenderInfor">招标信息</param>
        /// <returns>Id:招标ID</returns>
        public Dictionary<string, int> AddSave(TenderInfor tenderInfor)
        {
           // tenderInfor.Zbdw = Zbdw(tenderInfor.Id);
            var inof = Add(tenderInfor);
            //CreateHistroy(contractInfoHistory, inof);
            EventUtility eventUtility = new EventUtility();
            eventUtility.TenderInforEvent += UpdateItems;
            //eventUtility.ContInvoiceEvent += AddContStatic;
            eventUtility.ExceTenderInforEvent(inof);
            // UpdateItems(inof);
            //return inof;
            //Zbdw(tenderInfor.Id);
            //  tenderInfor.Zbdw = Zbdw(inof.Id);
            //Update(tenderInfor);
            return ResultContIds(tenderInfor);
        }

        public void Zbdw(int id)
        {

                StringBuilder strsql = new StringBuilder();
                var zbdwinfo = this.Db.Set<SuccessfulBidderLable>().Where(a => a.TenderId == id).FirstOrDefault();
                if (zbdwinfo != null)
                {
                    strsql.Append($"update TenderInfor set Zbdw={zbdwinfo.Zbdwid} ,Zje={zbdwinfo.SuccTotalPrice} where id={id};");
                    ExecuteSqlCommand(strsql.ToString());
                }
        }
        /// <summary>
        /// 返回招标ID
        /// </summary>
        /// <param name="contractInfo">合同</param>
        /// <returns></returns>
        private Dictionary<string, int> ResultContIds(TenderInfor TenderInfo)
        {
            var dic = new Dictionary<string, int>();
            dic.Add("Id", TenderInfo.Id);
            return dic;
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
        public LayPageInfo<TenderInforListViewDTO> GetList<s>(PageInfo<TenderInfor> pageInfo, Expression<Func<TenderInfor, bool>> whereLambda,
             Expression<Func<TenderInfor, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _TenderInforSet.Include(a => a.Project).Include(a => a.ZbdwNavigation).AsTracking().Where<TenderInfor>(whereLambda.Compile()).AsQueryable();
                pageInfo.TotalCount = tempquery.Count();

            
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<TenderInfor>))
            { //分页
                tempquery = tempquery.Skip<TenderInfor>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<TenderInfor>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            id = a.Id,
                            TenderUserId = a.TenderUserId,
                            ProjectId = a.ProjectId,
                            ProjectNo = a.ProjectNo,
                            Iocation = a.Iocation,
                            TenderDate = a.TenderDate,
                            ContractEnforcementDepId = a.ContractEnforcementDepId,
                            RecorderId = a.RecorderId,
                            ProjectName = a.Project == null ? "" : a.Project.Name,
                            TenderExpirationDate = a.TenderExpirationDate,
                            TenderStatus = a.TenderStatus,
                            TenderType = a.TenderType,
                            Zbdw=a.Zbdw,
                            //Unit =(Convert.ToInt32(a.SuccessfulBidderLable.Where(w=>w.TenderId==a.Id).FirstOrDefault().SuccessUnti)),
                            //UnitName = a.SuccessfulBidderLable.Where(w => w.TenderId == a.Id).First().SuccessUnti.UnitNavigation.Name
                            ZbswName=a.ZbdwNavigation==null ?"":a.ZbdwNavigation.Name,
                            
                            Zje =a.Zje
        };
            var local = from a in query.AsEnumerable()
                        select new TenderInforListViewDTO
                        {
                            Id = a.id,
                            TenderUserId=a.TenderUserId,
                            ProjectId=a.ProjectId,
                            TenderUserNAME = this.Db.GetRedisDeptNameValue(a.TenderUserId),
                            //RedisValueUtility.GetDeptName(a.TenderUserId),//经办机构
                            ProjectName = a.ProjectName,
                            ProjectNO=a.ProjectNo,
                            Iocation=a.Iocation,
                           //UnitName = a.UnitName,
                          //  Unit = a.Unit,
                            TenderDate = a.TenderDate,
                            ContractEnforcementDepName= this.Db.GetRedisDeptNameValue(a.ContractEnforcementDepId),
                            //RedisValueUtility.GetDeptName(a.ContractEnforcementDepId),//经办机构
                            RecorderName = this.Db.GetRedisUserFieldValue(a.RecorderId),
                            //RedisValueUtility.GetUserShowName(a.RecorderId), //创建人
                            TenderExpirationDate=a.TenderExpirationDate,
                            TenderStatus =a.TenderStatus==0?"未执行":"执行中",//经办机构
                            TenderStatusId= a.TenderStatus,
                            TenderType = a.TenderType,
                            TenderTypeName = this.Db.GetRedisDataDictionaryValue(a.TenderType ?? 0, DataDictionaryEnum.TenderType),
                            //DataDicUtility.GetDicValueToRedis(a.TenderType ?? 0, DataDictionaryEnum.TenderType),//询价类别
                            Zbdw = a.Zbdw,
                        ZbswName= a.ZbswName,
                            Zjethis = a.Zje.ThousandsSeparator()
                        };
            
            return new LayPageInfo<TenderInforListViewDTO>()
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
        public TenderInforListViewDTO ShowView(int Id)
        {
            var query = from a in _TenderInforSet.Include(a => a.Project).AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            TenderUserId = a.TenderUserId,
                            ProjectId = a.ProjectId,
                            ProjectNo = a.ProjectNo,
                            Iocation = a.Iocation,
                            TenderDate = a.TenderDate,
                            ContractEnforcementDepId = a.ContractEnforcementDepId,
                            RecorderId = a.RecorderId,
                            ProjectName = a.Project == null ? "" : a.Project.Name,
                            TenderExpirationDate = a.TenderExpirationDate,
                            TenderStatus = a.TenderStatus,
                            WinningConditions=a.WinningConditions,
                            TenderType = a.TenderType,
                            Zbdw = a.Zbdw,
                            Zje = a.Zje
                        };
            var local = from a in query.AsEnumerable()
                        select new TenderInforListViewDTO
                        {
                            Id = a.Id,
                            TenderUserId=a.TenderUserId,
                            TenderUserNAME = this.Db.GetRedisDeptNameValue(a.TenderUserId),
                            //RedisValueUtility.GetDeptName(a.TenderUserId),//经办机构
                            ProjectName = a.ProjectName,
                            ProjectId=a.ProjectId,
                            ProjectNO = a.ProjectNo,
                            Iocation = a.Iocation,
                            TenderDate = a.TenderDate,
                            ContractEnforcementDepId= a.ContractEnforcementDepId,
                            ContractEnforcementDepName = this.Db.GetRedisDeptNameValue(a.ContractEnforcementDepId),
                            //RedisValueUtility.GetDeptName(a.ContractEnforcementDepId),//经办机构
                            RecorderName = this.Db.GetRedisUserFieldValue(a.RecorderId),
                            //RedisValueUtility.GetUserShowName(a.RecorderId), //创建人
                            RecorderId =a.RecorderId,
                            TenderExpirationDate = a.TenderExpirationDate,
                            TenderStatus = a.TenderStatus == 0 ? "未执行" : "执行中",//经办机构
                            WinningConditions=a.WinningConditions,
                            TenderType = a.TenderType,
                            TenderTypeName = this.Db.GetRedisDataDictionaryValue(a.TenderType ?? 0, DataDictionaryEnum.TenderType),
                            //DataDicUtility.GetDicValueToRedis(a.TenderType ?? 0, DataDictionaryEnum.TenderType),//询价类别
                            Zbdw = a.Zbdw,
                            Zje = a.Zje??0
                        };
            var teminfo = local.FirstOrDefault();
            return teminfo;
        }
        /// <summary>
        /// 修改合同信息
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <returns>Id:招标ID</returns>
        public Dictionary<string, int> UpdateSave(TenderInfor tenderInfor)
        {
            var inof = Update(tenderInfor);
            //保存历史表
            EventUtility eventUtility = new EventUtility();
            return ResultContIds(tenderInfor);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update TenderInfor set IS_DELETE=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }


        public int GetIsHt(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var ht = 0;
                ht = Db.Set<ContractInfo>().Where(a => listIds.Contains(a.Zbid??0)&&a.IsDelete==0).Count();
            return ht;
        }
        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        public void UpdateItems(TenderInfor tenderInfor)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"update OpeningSituationInfor set TenderId={tenderInfor.Id} where TenderId={-tenderInfor.CreateUserId};");
            strsql.Append($"update TendererNameLabel set TenderId={tenderInfor.Id} where TenderId={-tenderInfor.CreateUserId};");
            strsql.Append($"update SuccessfulBidderLable set TenderId={tenderInfor.Id} where TenderId={-tenderInfor.CreateUserId};");
            strsql.Append($"update QuestioningAttachment set ContId={tenderInfor.Id} where ContId={-tenderInfor.CreateUserId};");
            strsql.Append($"update WinningItem set TenderId={tenderInfor.Id} where TenderId={-tenderInfor.CreateUserId};");
           

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
            strsql.Append($"delete OpeningSituationInfor  where TenderId={-currUserId};");//开标情况
            strsql.Append($"delete TendererNameLabel  where TenderId={-currUserId};");//招标人
            strsql.Append($"delete SuccessfulBidderLable  where TenderId={-currUserId};");//中标单位
            strsql.Append($"delete TenderAttachment  where ContId={-currUserId};");//中标附件
            strsql.Append($"delete WinningItem  where TenderId={-currUserId};");//中标货物
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
        }

        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                //case "KaiGongDate"://状态
                //                   //  var KaiGongDate = Convert.ToByte(info.FieldValue);
                //    sqlstr = $"update  GongChengKgGl set KaiGongDate='{info.FieldValue}' where Id={info.Id}";
                //    break;
                case "TenderStatus"://状态
                    var state = Convert.ToByte(info.FieldValue);
                    sqlstr = $"update  TenderInfor set TenderStatus={state} where Id={info.Id}";
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
