using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    public partial class InquiryService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<Inquiry>();
            //不等于fieldInfo.CurrId是排除修改的时候
            predicateAnd = predicateAnd.And(a =>  a.Id != fieldInfo.Id);//a.IsDelete == 0 &&
            switch (fieldInfo.FieldName)
            {
            
                case "ProjectNumber":
                    predicateAnd = predicateAnd.And(a => a.ProjectNumber == fieldInfo.FieldValue);
                    break;
            }
            //   var sds= predicateAnd.
            return false;
        }
        /// <summary>
        /// 添加询价基本信息
        /// </summary>
        /// <param name="contractInfo"></param>
        /// <returns></returns>
        public Dictionary<string, int> AddSave(Inquiry contractInfo)
        {
            var inof = Add(contractInfo);
            UpdateItems(inof);
            //Zbdw(inof.Id);
            return ResultContIds(contractInfo);
        }

        public void Zbdw(int id)
        {
            try
            {
                StringBuilder strsql = new StringBuilder();
                var zbdwinfo = this.Db.Set<TheWinningUnit>().Where(a => a.LnquiryId == id).FirstOrDefault();
                if (zbdwinfo!=null)
                {
                    strsql.Append($"update Inquiry set Zbdw={zbdwinfo.Zbdwid??0},Zje={zbdwinfo.BidPrices??0} where id={id};");
                    ExecuteSqlCommand(strsql.ToString());
                }
               
               
               
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// 返回合同ID和历史合同ID
        /// </summary>
        /// <param name="contractInfo">询价</param>

        /// <returns></returns>
        private Dictionary<string, int> ResultContIds(Inquiry contractInfo)
        {
            var dic = new Dictionary<string, int>();
            dic.Add("Id", contractInfo.Id);
           
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
        /// 
        public LayPageInfo<InquiryListDTO> GetList<s>(PageInfo<Inquiry> pageInfo, Expression<Func<Inquiry, bool>> whereLambda,
           Expression<Func<Inquiry, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _InquirySet.Include(a => a.ProjectNameNavigation).Include(a => a.ZbdwNavigation).AsTracking().Where<Inquiry>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<Inquiry>))
            { //分页
                tempquery = tempquery.Skip<Inquiry>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<Inquiry>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        where a.IsDelete!=1
                        select new
                        {
                            Id = a.Id,
                            Inquirer=a.Inquirer,
                            ProjectNumber = a.ProjectNumber,
                            ProjectId = a.ProjectName,
                            ProjectName =a.ProjectNameNavigation == null?"":a.ProjectNameNavigation.Name,
                            Sites = a.Sites,
                            TheWinningConditions = a.TheWinningConditions,
                            Times = a.Times,
                            UsefulLife = a.UsefulLife,
                            Recorder = a.Recorder,
                            ContractExecuteBranch = a.ContractExecuteBranch,
                            InState=a.InState,
                            CreateUserId = a.CreateUserId,
                            InquiryType = a.InquiryType,
                            ZbdwName = a.ZbdwNavigation == null ? "" : a.ZbdwNavigation.Name,
                            Zbdw =a.Zbdw,
                            Zje = a.Zje

                        };
            var local = from a in query.AsEnumerable()
                        select new InquiryListDTO
                        {
                            Id = a.Id,
                            Recorder = a.Recorder,
                            RecorderName = this.Db.GetRedisUserFieldValue(a.Recorder ?? -1),
                           // RedisValueUtility.GetUserShowName((byte)a.Recorder), //记录人
                            DeptName = this.Db.GetRedisUserFieldValue(a.Inquirer ?? -1),
                            //RedisValueUtility.GetDeptName(a.Inquirer ?? -2),//询价人
                            // DeptId = a.Inquirer,//询价人id
                            MdeptName = this.Db.GetRedisDeptNameValue(a.ContractExecuteBranch ?? -2),
                       //     RedisValueUtility.GetDeptName(a.ContractExecuteBranch ?? -2), //合同执行部门
                            Times = a.Times,
                            Sites = a.Sites,
                            ProjectId= a.ProjectId??0,
                            ProjectName = a.ProjectName,
                            UsefulLife = a.UsefulLife,
                            ProjectNumber = a.ProjectNumber,
                            TheWinningConditions = a.TheWinningConditions,
                            InState = a.InState,
                            InStateDic = EmunUtility.GetDesc(typeof(ZXYStateEnums), (a.InState??0)),
                            CreateUserId = a.CreateUserId,
                            InquiryType = a.InquiryType,
                            InquiryTypeName = this.Db.GetRedisDataDictionaryValue(a.InquiryType ?? 0, DataDictionaryEnum.InquiryType),
                            //DataDicUtility.GetDicValueToRedis(a.InquiryType ?? 0, DataDictionaryEnum.InquiryType),//询价类别
                            Zbdw = a.Zbdw,
                            Zje = a.Zje??0,
                            ZbdwName = a.ZbdwName,
                            Zjethis = a.Zje.ThousandsSeparator()
                        };
            return new LayPageInfo<InquiryListDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 根据id查询项目名称
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        private string  xmmz(int? projectName)
        {
            var xmName = "";
            xmName = Db.Set<ProjectManager>().AsNoTracking().FirstOrDefault(a => a.Id == projectName).Name;
            if (xmName != null)
            {
                return xmName;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public InquiryDTO ShowView(int Id)
        {
            var query = from a in _InquirySet.Include(a =>a.ProjectNameNavigation) .AsNoTracking()
                       where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Inquirer = a.Inquirer,
                            ProjectNumber = a.ProjectNumber,
                            ProjectNames = a.ProjectNameNavigation == null?"":a.ProjectNameNavigation.Name,
                            ProjectName = a.ProjectName,
                            Sites = a.Sites,
                            TheWinningConditions = a.TheWinningConditions,
                            Times = a.Times,
                            UsefulLife = a.UsefulLife,
                            Recorder = a.Recorder,
                            ContractExecuteBranch = a.ContractExecuteBranch,
                              InState = a.InState,
                            InquiryType = a.InquiryType,
                            Zbdw = a.Zbdw,
                            Zje = a.Zje,

                        };
            var local = from a in query.AsEnumerable()
                        select new InquiryDTO
                        {
                            Id = a.Id,
                            RecorderS = this.Db.GetRedisUserFieldValue(a.Recorder ?? -1),
                            //RedisValueUtility.GetUserShowName((byte)a.Recorder), //记录人
                            Recorder=a.Recorder,//记录人id
                            //RedisValueUtility.GetDeptName(a.TenderUserId)
                            InquirerNameS = this.Db.GetRedisUserFieldValue(a.Inquirer ?? -1),
                            //RedisValueUtility.GetDeptName(a.Inquirer??-2),//询价人
                            Inquirer = a.Inquirer,//询价人id

                            ContractExecuteBranchName = this.Db.GetRedisDeptNameValue(a.ContractExecuteBranch ?? -2),

                            //RedisValueUtility.GetDeptName(a.ContractExecuteBranch ?? -2), //合同执行部门
                            ContractExecuteBranch = a.ContractExecuteBranch , //合同执行部门id
                            Times = a.Times,
                            Sites = a.Sites,
                            ProjectNames = a.ProjectNames,
                            ProjectName=a.ProjectName,
                            UsefulLife = a.UsefulLife,
                            ProjectNumber = a.ProjectNumber,
                            TheWinningConditions = a.TheWinningConditions,
                            InState = a.InState,
                            Zbdw = a.Zbdw??0,
                            Zje = a.Zje??0,
                         InquiryType = a.InquiryType,//合同类别ID
                          InquiryTypeName = this.Db.GetRedisDataDictionaryValue(a.InquiryType ?? 0, DataDictionaryEnum.InquiryType),
                            //DataDicUtility.GetDicValueToRedis(a.InquiryType ?? 0, DataDictionaryEnum.InquiryType)//询价
                        };
            var teminfo = local.FirstOrDefault();
            return teminfo;
        }
        public int Delete(string Ids)
        {
            string sqlstr = "update  Inquiry set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }

        public int GetIsHt(string Ids) 
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var ht = 0;
            ht = Db.Set<ContractInfo>().Where(a => listIds.Contains(a.Xjid ?? 0) && a.IsDelete == 0).Count();
            return ht;
        }
        public Dictionary<string, int> UpdateSave(Inquiry tenderInfor)
        {
           
            var inof = Update(tenderInfor);
         UpdateItems(tenderInfor);
            //保存历史表
            EventUtility eventUtility = new EventUtility();
            return ResultContIds(tenderInfor);
        }

        //public Dictionary<string, int> UpdateSave(Inquiry contractInfo)
        //{
        //    contractInfo.InState = 0;
        //    contractInfo.IsDelete = 0;
        //    var inof = Update(contractInfo);
          
        //    EventUtility eventUtility = new EventUtility();
        //    return ResultContIds(contractInfo);
        //}
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public int UpdateField(IList<UpdateFieldInfo> fields)
        {
            StringBuilder sqlstr = new StringBuilder($"update  Inquiry set ModifyUserId={fields[0].CurrUserId}");
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

        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                //case "KaiGongDate"://状态
                //                   //  var KaiGongDate = Convert.ToByte(info.FieldValue);
                //    sqlstr = $"update  GongChengKgGl set KaiGongDate='{info.FieldValue}' where Id={info.Id}";
                //    break;
                case "InState"://状态
                    var state = Convert.ToByte(info.FieldValue);
                    sqlstr = $"update  Inquiry set InState={state} where Id={info.Id}";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }

        /// <summary>
        ///询价标签页赋值
        /// </summary>
        /// <param name="contInfo"></param>
        /// <param name="infoHistory"></param>
        public void UpdateItems(Inquiry inof)
        {
            StringBuilder strsql = new StringBuilder();
            var currUserId = inof.ModifyUserId;
            strsql.Append($"update WinningInq set Inqid={inof.Id} where Inqid={-currUserId};");
            strsql.Append($"update OpenTenderCondition  set LnquiryId={inof.Id} where LnquiryId={-currUserId};");
            strsql.Append($"update Inquirer  set InquiryId={inof.Id} where InquiryId={-currUserId};");
            strsql.Append($"update TheWinningUnit set LnquiryId={inof.Id} where LnquiryId={-currUserId};");

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

            strsql.Append($"delete InquiryAttachment  where ContId={-currUserId};");//附件
            strsql.Append($"delete OpenTenderCondition  where LnquiryId={-currUserId};");//中标货物
            strsql.Append($"delete TheWinningUnit  where LnquiryId={-currUserId};");//中标单位
            strsql.Append($"delete Inquirer   where InquiryId={-currUserId};");                                                               //strsql.Append($"delete ContConsult  where ContId={-currUserId};");//合同查阅人

            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
        }
        /// <summary>
        /// 查询季凌燕的id
        /// </summary>
        /// <param name="name">季凌燕</param>
        /// <returns></returns>
        public byte? Yh(string name) {
            byte? id = 0;
            try
            {
                id =(byte?)Db.Set<UserInfor>().Where(a => a.Name == name).FirstOrDefault().Id;
            }
            catch (Exception)
            {

                return id;
            }
            
            return id;
        
        }
    }
}
