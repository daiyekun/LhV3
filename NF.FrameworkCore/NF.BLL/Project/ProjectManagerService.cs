using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NF.Common.Extend;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.Utility;
using NF.ViewModel;
using NF.ViewModel.Models.Finance.Enums;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 项目管理
    /// </summary>
   public partial class ProjectManagerService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<ProjectManager>();
            //不等于fieldInfo.CurrId是排除修改的时候
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0  && a.Id != fieldInfo.Id);
            switch (fieldInfo.FieldName)
            {
                case "Name":
                    predicateAnd = predicateAnd.And(a => a.Name == fieldInfo.FieldValue);
                    break;
                case "Code":
                    predicateAnd = predicateAnd.And(a => a.Code == fieldInfo.FieldValue);
                    break;

            }
            return GetQueryable(predicateAnd).AsNoTracking().Any();

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
        public LayPageInfo<ProjectManagerViewDTO> GetList<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
             Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ProjectManagerSet.AsTracking().Where<ProjectManager>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ProjectManager>))
            { //分页
                tempquery = tempquery.Skip<ProjectManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ProjectManager>(pageInfo.PageSize);
            }
          
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUser.DisplyName,
                            //PriUserName= a.PrincipalUser.DisplyName,
                            CreateUserId=a.CreateUserId,
                            PrincipalUserId=a.PrincipalUserId,
                            //ProjTypeName= a.Category.Name,
                            CategoryId=a.CategoryId,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime=a.PlanBeginDateTime,
                            PlanCompleteDateTime=a.PlanCompleteDateTime,
                            ActualBeginDateTime=a.ActualBeginDateTime,
                            ActualCompleteDateTime=a.ActualCompleteDateTime,
                            BugetCollectAmountMoney=a.BugetCollectAmountMoney,
                            BudgetPayAmountMoney= a.BudgetPayAmountMoney,
                            Pstate= a.Pstate,
                            WfState=a.WfState,//流程状态
                            WfCurrNodeName=a.WfCurrNodeName,//当前节点
                            WfItem=a.WfItem,//审批事项
                            ProjectSource = a.ProjectSource

                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectManagerViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUserName,
                            //PriUserName = a.PriUserName,//负责人
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                           // RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            PriUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId??0),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                            ProjTypeName = this.Db.GetRedisDataDictionaryValue(a.CategoryId, DataDictionaryEnum.projectType),
                            //RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),//项目类别//a.ProjTypeName,//项目类别
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,//计划开始时间
                            PlanCompleteDateTime = a.PlanCompleteDateTime,//计划完成时间
                            ActualBeginDateTime = a.ActualBeginDateTime,//时间开始时间
                            ActualCompleteDateTime = a.ActualCompleteDateTime,//实际完成时间
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,//计划收款
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,//计划付款
                            Pstate = a.Pstate,
                            PstateDic = EmunUtility.GetDesc(typeof(ProjStateEnum), a.Pstate??0),
                            BugetCollectAmountMoneyThod= a.BugetCollectAmountMoney.ThousandsSeparator(),//计划收款千分位
                            BudgetPayAmountMoneyThod = a.BudgetPayAmountMoney.ThousandsSeparator(),//计划付款千分位
                            UserDeptId = Convert.ToInt32(this.Db.GetRedisUserFieldValue(a.CreateUserId, "DepartmentId")),
                            //RedisValueUtility.GetRedisUserDeptId(a.CreateUserId),//创建人部门
                            CategoryId = a.CategoryId,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                            ProjectSource = a.ProjectSource,
                            ProjectSourceName = this.Db.GetRedisDataDictionaryValue(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource),
                            //DataDicUtility.GetDicValueToRedis(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource)
                        };
            return new LayPageInfo<ProjectManagerViewDTO>()
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
            string sqlstr = "update ProjectManager set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        public DELETElist GetIsHt(string Ids)
        {
            DELETElist u = new DELETElist();
            var listIds = StringHelper.String2ArrayInt(Ids);
            var Xminfo = Db.Set<ProjectManager>().Where(a => listIds.Contains(a.Id)).ToList();
            var nums = 0;
            foreach (var item in Xminfo)
            {
               
                   var XMly = Db.Set<DataDictionary> ().Where(a =>a.Id==item.ProjectSource&&a.IsDelete==0).FirstOrDefault().Name;
                if (XMly == "招标")
                {
                    u.DateName = "招标";
                    u.Num = Db.Set<TenderInfor>().Where(a => listIds.Contains(a.ProjectId) && a.IsDelete == 0).Count();
                }
                else if (XMly == "询价")
                {
                    u.DateName = "询价";
                    u.Num = Db.Set<TenderInfor>().Where(a => listIds.Contains(a.ProjectId) && a.IsDelete == 0).Count();
                } 
                else if (XMly == "洽谈")
                {
                    u.DateName = "洽谈";
                    u.Num = Db.Set<TenderInfor>().Where(a => listIds.Contains(a.ProjectId) && a.IsDelete == 0).Count();
                }
            }
            return u;
        }

        public DELETElist GetIsHtlist(string Ids)
        {
            DELETElist u = new DELETElist();

            var listIds = StringHelper.String2ArrayInt(Ids);
            var   nums = Db.Set<ContractInfo>().Where(a => listIds.Contains(a.ProjectId??0) && a.IsDelete == 0).Count();
            u.Num = nums;
            u.DateName = "合同";
            return u;
        }


            public int XMLB(string name) {
            try
            {
                var id = Db.Set<DataDictionary>().Where(a => a.Name == name).FirstOrDefault().Id;
                return id;
            }
            catch (Exception)
            {

                return 0;
            }
           
        }

        public int Ren(string name) {
            try
            {
                var id = Db.Set<UserInfor>().Where(a => a.Name == name).FirstOrDefault().Id;
                return id;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ProjectManagerViewDTO ShowView(int Id)
        {
            var query = from a in _ProjectManagerSet.AsNoTracking()
                        where a.Id == Id
                        select new 
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CategoryId= a.CategoryId,//类别ID
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,//计划开始时间
                            PlanCompleteDateTime = a.PlanCompleteDateTime,//计划完成时间
                            ActualBeginDateTime = a.ActualBeginDateTime,//时间开始时间
                            ActualCompleteDateTime = a.ActualCompleteDateTime,//实际完成时间
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,//计划收款
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,//计划付款
                            Pstate = a.Pstate,
                            PrincipalUserId=a.PrincipalUserId,
                            BudgetCollectCurrencyId=a.BudgetCollectCurrencyId,//收款币种
                            BudgetPayCurrencyId=a.BudgetPayCurrencyId,//付款币种
                            WfState=a.WfState,
                            ProjectSource=a.ProjectSource
                            //BudgetCollectCurrencyName= a.BudgetCollectCurrency.ShortName,
                            //BudgetPayCurrencyName=a.BudgetPayCurrency.ShortName


                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectManagerViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CategoryId = a.CategoryId,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUserName,
                            //PriUserName = a.PriUserName,//负责人
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                          //  RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            PriUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                            PrincipalUserId = a.PrincipalUserId,//赋值人ID
                            ProjTypeName = this.Db.GetRedisDataDictionaryValue(a.CategoryId, DataDictionaryEnum.projectType),

                          //  RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}","Name"),//项目类别
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,//计划开始时间
                            PlanCompleteDateTime = a.PlanCompleteDateTime,//计划完成时间
                            ActualBeginDateTime = a.ActualBeginDateTime,//时间开始时间
                            ActualCompleteDateTime = a.ActualCompleteDateTime,//实际完成时间
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,//计划收款
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,//计划付款
                            Pstate = a.Pstate,
                            PstateDic = EmunUtility.GetDesc(typeof(ProjStateEnum), a.Pstate ?? 0),
                            BugetCollectAmountMoneyThod = a.BugetCollectAmountMoney.ThousandsSeparator(),//计划收款千分位
                            BudgetPayAmountMoneyThod = a.BudgetPayAmountMoney.ThousandsSeparator(),//计划付款千分位
                            BudgetCollectCurrencyId = a.BudgetCollectCurrencyId,//收款币种
                            BudgetPayCurrencyId = a.BudgetPayCurrencyId,//付款币种
                            BudgetCollectCurrencyName= RedisHelper.HashGet($"{StaticData.RedisCurrencyKey}:{a.BudgetCollectCurrencyId}", "ShortName"),//a.BudgetCollectCurrencyName,//币种名称
                            BudgetPayCurrencyName = RedisHelper.HashGet($"{StaticData.RedisCurrencyKey}:{a.BudgetPayCurrencyId}", "ShortName"),//a.BudgetPayCurrencyName,//付款币种
                            WfState = a.WfState??0,
                            ProjectSource = a.ProjectSource,
                            ProjectSourceName = this.Db.GetRedisDataDictionaryValue(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource),
                            //DataDicUtility.GetDicValueToRedis(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource)
                        };

            return local.FirstOrDefault();



        }


        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="currUserId">当前用户ID</param>
        public int UpdateItems(int Id, int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"update ProjSchedule set ProjectId={Id} where ProjectId={-currUserId};");
            strsql.Append($"update ProjDescription set ProjectId={Id} where ProjectId={-currUserId};");
            strsql.Append($"update ProjAttachment set ProjectId={Id} where ProjectId={-currUserId};");
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());

        }
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        public int ClearJunkItemData(int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"delete ProjSchedule  where ProjectId={-currUserId};");
            strsql.Append($"delete ProjDescription  where ProjectId={-currUserId};");
            strsql.Append($"delete ProjAttachment  where ProjectId={-currUserId};");
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
        }
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {

              
                case "Reserve1":
                case "Reserve2":
                    sqlstr = $"update  ProjectManager set {info.FieldName}='{info.FieldValue}' where Id={info.Id}";
                    break;
                case "PriUserName"://负责人
                    sqlstr = $"update  ProjectManager set PrincipalUserId={info.FieldValue} where Id={info.Id}";
                    break;
                case "Pstate"://项目状态
                    var state = Convert.ToInt32(info.FieldValue);
                    sqlstr = $"update  ProjectManager set Pstate={state} where Id={info.Id}";
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
        public  int UpdateField(IList<UpdateFieldInfo> fields)
        {
            StringBuilder sqlstr = new StringBuilder($"update  ProjectManager set ModifyUserId={fields[0].CurrUserId},ModifyDateTime='{DateTime.Now}'");
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
        /// <summary>
        /// 首页项目列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ProjectManagerConsoleDTO> GetConsoleProjList<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
            Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ProjectManagerSet.AsTracking().Where<ProjectManager>(whereLambda.Compile());
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.AsQueryable().OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.AsQueryable().OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ProjectManager>))
            { //分页
                tempquery = tempquery.Skip<ProjectManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ProjectManager>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        let skht = Db.Set<ContractInfo>().Where(p => p.ProjectId == a.Id).Select(p => new { p.Id, p.FinanceType, p.AmountMoney })
                        let skhtIds = skht.Where(a => a.FinanceType == 0).Select(a => a.Id).ToList()//收款合同ID
                        let fkhtIds = skht.Where(a => a.FinanceType == 1).Select(a => a.Id).ToList()//付款合同ID
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                           skhtje= skht.Where(a=>a.FinanceType==0).Sum(a=>a.AmountMoney??0),//收款合同金额
                           fkhtje= skht.Where(a => a.FinanceType == 1).Sum(a => a.AmountMoney ?? 0),//付款合同金额
                           skje=Db.Set<ContStatistic>().Where(p=>skhtIds.Contains(p.ContId??0)).Sum(p=>p.CompActAm??0),
                           fkje = Db.Set<ContStatistic>().Where(p => fkhtIds.Contains(p.ContId ?? 0)).Sum(p => p.CompActAm ?? 0)
                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectManagerConsoleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            XmSkHtJeThod=a.skhtje.ThousandsSeparator(),
                            XmFkHtJeThod=a.fkhtje.ThousandsSeparator(),
                            XmSkJeThod=a.skje.ThousandsSeparator(),
                            XmFkJeThod=a.fkje.ThousandsSeparator()
                        };


            return new LayPageInfo<ProjectManagerConsoleDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        public LayPageInfo<ProjectManagerViewDTO> GetList1<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
          Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ProjectManagerSet.AsTracking().Where<ProjectManager>(whereLambda.Compile()).AsQueryable();
           
           // tempquery = tempquery.Where(a => a.ProjectSource == dataid); 
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ProjectManager>))
            { //分页
                tempquery = tempquery.Skip<ProjectManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ProjectManager>(pageInfo.PageSize);
            }
            var dataid = Db.Set<DataDictionary>().Where(a => a.Name == "招标").FirstOrDefault().Id;
            var ssd = tempquery.Where(s => s.ProjectSource == dataid);
            var query = from a in ssd
                       // where a.ProjectSource== dataid
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUser.DisplyName,
                            //PriUserName= a.PrincipalUser.DisplyName,
                            CreateUserId = a.CreateUserId,
                            PrincipalUserId = a.PrincipalUserId,
                            //ProjTypeName= a.Category.Name,
                            CategoryId = a.CategoryId,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualBeginDateTime = a.ActualBeginDateTime,
                            ActualCompleteDateTime = a.ActualCompleteDateTime,
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,
                            Pstate = a.Pstate,
                            WfState = a.WfState,//流程状态
                            WfCurrNodeName = a.WfCurrNodeName,//当前节点
                            WfItem = a.WfItem,//审批事项
                            ProjectSource = a.ProjectSource

                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectManagerViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUserName,
                            //PriUserName = a.PriUserName,//负责人
                          CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                                               PriUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                           ProjTypeName = this.Db.GetRedisDataDictionaryValue(a.CategoryId, DataDictionaryEnum.projectType),
                            //RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),//项目类别//a.ProjTypeName,//项目类别
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,//计划开始时间
                            PlanCompleteDateTime = a.PlanCompleteDateTime,//计划完成时间
                            ActualBeginDateTime = a.ActualBeginDateTime,//时间开始时间
                            ActualCompleteDateTime = a.ActualCompleteDateTime,//实际完成时间
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,//计划收款
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,//计划付款
                            Pstate = a.Pstate,
                           PstateDic = EmunUtility.GetDesc(typeof(ProjStateEnum), a.Pstate ?? 0),
                            BugetCollectAmountMoneyThod = a.BugetCollectAmountMoney.ThousandsSeparator(),//计划收款千分位
                            BudgetPayAmountMoneyThod = a.BudgetPayAmountMoney.ThousandsSeparator(),//计划付款千分位
                                                                                       //*  UserDeptId = Convert.ToInt32(this.Db.GetRedisUserFieldValue(a.CreateUserId)),
                                                                                                   //RedisValueUtility.GetRedisUserDeptId(a.CreateUserId),//创建人部门
                            CategoryId = a.CategoryId,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                                WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                                 WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                            ProjectSource = a.ProjectSource,
                             ProjectSourceName = this.Db.GetRedisDataDictionaryValue(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource),
                            //DataDicUtility.GetDicValueToRedis(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource)
                        };
            return new LayPageInfo<ProjectManagerViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        public LayPageInfo<ProjectManagerViewDTO> GetList2<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
          Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ProjectManagerSet.AsTracking().Where<ProjectManager>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ProjectManager>))
            { //分页
                tempquery = tempquery.Skip<ProjectManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ProjectManager>(pageInfo.PageSize);
            }
            var dataid = Db.Set<DataDictionary>().Where(a => a.Name == "询价").FirstOrDefault().Id;
            var ssd = tempquery.Where(s => s.ProjectSource == dataid);
            var query = from a in ssd
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUser.DisplyName,
                            //PriUserName= a.PrincipalUser.DisplyName,
                            CreateUserId = a.CreateUserId,
                            PrincipalUserId = a.PrincipalUserId,
                            //ProjTypeName= a.Category.Name,
                            CategoryId = a.CategoryId,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualBeginDateTime = a.ActualBeginDateTime,
                            ActualCompleteDateTime = a.ActualCompleteDateTime,
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,
                            Pstate = a.Pstate,
                            WfState = a.WfState,//流程状态
                            WfCurrNodeName = a.WfCurrNodeName,//当前节点
                            WfItem = a.WfItem,//审批事项
                            ProjectSource = a.ProjectSource

                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectManagerViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUserName,
                            //PriUserName = a.PriUserName,//负责人
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            PriUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                            ProjTypeName = this.Db.GetRedisDataDictionaryValue(a.CategoryId, DataDictionaryEnum.projectType),
                            //RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),//项目类别//a.ProjTypeName,//项目类别
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,//计划开始时间
                            PlanCompleteDateTime = a.PlanCompleteDateTime,//计划完成时间
                            ActualBeginDateTime = a.ActualBeginDateTime,//时间开始时间
                            ActualCompleteDateTime = a.ActualCompleteDateTime,//实际完成时间
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,//计划收款
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,//计划付款
                            Pstate = a.Pstate,
                            PstateDic = EmunUtility.GetDesc(typeof(ProjStateEnum), a.Pstate ?? 0),
                            BugetCollectAmountMoneyThod = a.BugetCollectAmountMoney.ThousandsSeparator(),//计划收款千分位
                            BudgetPayAmountMoneyThod = a.BudgetPayAmountMoney.ThousandsSeparator(),//计划付款千分位
                            UserDeptId =Convert.ToInt32(this.Db.GetRedisUserFieldValue(a.PrincipalUserId??0, "DepartmentId")),
                            //RedisValueUtility.GetRedisUserDeptId(a.CreateUserId),//创建人部门
                            CategoryId = a.CategoryId,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                            ProjectSource = a.ProjectSource,
                            ProjectSourceName = this.Db.GetRedisDataDictionaryValue(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource),
                            //DataDicUtility.GetDicValueToRedis(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource)
                        };
            return new LayPageInfo<ProjectManagerViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        public LayPageInfo<ProjectManagerViewDTO> GetList3<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
          Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ProjectManagerSet.AsTracking().Where<ProjectManager>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ProjectManager>))
            { //分页
                tempquery = tempquery.Skip<ProjectManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ProjectManager>(pageInfo.PageSize);
            }
            var dataid = 0;
            try
            {
                 dataid = Db.Set<DataDictionary>().Where(a => a.Name == "洽谈").FirstOrDefault().Id;
            }
            catch (Exception)
            {

                dataid = 0;
            }
          
            var ssd = tempquery.Where(s => s.ProjectSource == dataid);
            var query = from a in ssd
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUser.DisplyName,
                            //PriUserName= a.PrincipalUser.DisplyName,
                            CreateUserId = a.CreateUserId,
                            PrincipalUserId = a.PrincipalUserId,
                            //ProjTypeName= a.Category.Name,
                            CategoryId = a.CategoryId,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualBeginDateTime = a.ActualBeginDateTime,
                            ActualCompleteDateTime = a.ActualCompleteDateTime,
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,
                            Pstate = a.Pstate,
                            WfState = a.WfState,//流程状态
                            WfCurrNodeName = a.WfCurrNodeName,//当前节点
                            WfItem = a.WfItem,//审批事项
                            ProjectSource = a.ProjectSource

                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectManagerViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            //CreateUserName = a.CreateUserName,
                            //PriUserName = a.PriUserName,//负责人
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            PriUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                            ProjTypeName = this.Db.GetRedisDataDictionaryValue(a.CategoryId, DataDictionaryEnum.projectType),
                          //  RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),//项目类别//a.ProjTypeName,//项目类别
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PlanBeginDateTime = a.PlanBeginDateTime,//计划开始时间
                            PlanCompleteDateTime = a.PlanCompleteDateTime,//计划完成时间
                            ActualBeginDateTime = a.ActualBeginDateTime,//时间开始时间
                            ActualCompleteDateTime = a.ActualCompleteDateTime,//实际完成时间
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,//计划收款
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,//计划付款
                            Pstate = a.Pstate,
                            PstateDic = EmunUtility.GetDesc(typeof(ProjStateEnum), a.Pstate ?? 0),
                            BugetCollectAmountMoneyThod = a.BugetCollectAmountMoney.ThousandsSeparator(),//计划收款千分位
                            BudgetPayAmountMoneyThod = a.BudgetPayAmountMoney.ThousandsSeparator(),//计划付款千分位
                            UserDeptId =Convert.ToInt32(this.Db.GetRedisUserFieldValue(a.PrincipalUserId??0, "DepartmentId")),
                            //RedisValueUtility.GetRedisUserDeptId(a.CreateUserId),//创建人部门
                            CategoryId = a.CategoryId,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                            ProjectSource = a.ProjectSource,
                            ProjectSourceName = this.Db.GetRedisDataDictionaryValue(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource),
                            //DataDicUtility.GetDicValueToRedis(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource)
                        };
            return new LayPageInfo<ProjectManagerViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        /// <summary>
        /// 获取资金统计
        /// </summary>
        /// <param name="项目ID">合同对方ID</param>

        /// <returns></returns>
        public ProjFundCalcul GetFundStatistics(int projId)
        {
            ProjFundCalcul fund = new ProjFundCalcul();
            #region 收款合同
            //收款合同金额
            var htje = this.Db.Set<ContractInfo>().Where(a => a.ProjectId == projId && (
            a.ContState == (int)ContractState.Execution
            || a.ContState == (int)ContractState.Terminated
            || a.ContState == (int)ContractState.Completed)
            && a.FinanceType == (int)FinceType.Put).Sum(a => (decimal?)a.AmountMoney ?? 0);

            //已收金额
            var sfkje = this.Db.Set<ContActualFinance>().Include(a => a.Cont)
                .Where(a => a.Cont != null && a.Cont.ProjectId == projId && (a.Cont.ContState == (int)ContractState.Execution
            || a.Cont.ContState == (int)ContractState.Terminated
            || a.Cont.ContState == (int)ContractState.Completed)
            && a.Astate == (int)ActFinanceStateEnum.Confirmed
            && a.FinceType == (int)FinceType.Put).Sum(a => (decimal?)a.AmountMoney ?? 0);
            //未收款金额
            var wsfkje = htje - sfkje;
            //已开票金额
            var yskfpje = this.Db.Set<ContInvoice>().Include(a => a.Cont)
               .Where(a => a.Cont != null && a.Cont.CompId == projId && (a.Cont.ContState == (int)ContractState.Execution
           || a.Cont.ContState == (int)ContractState.Terminated
           || a.Cont.ContState == (int)ContractState.Completed)
           && (a.InState == (int)InvoiceStateEnum.Invoicing || a.InState == (int)InvoiceStateEnum.ReceiptInvoice)
           && a.Cont.FinanceType == (int)FinceType.Put).Sum(a => (decimal?)a.AmountMoney ?? 0);
            //未开票金额
            var wykfpje = htje - yskfpje;
            //财务应收==>已开票金额—已收金额
            var cwysf = yskfpje - sfkje;
            //财务预收==>已收金额—已开票金额
            var cwyjsf = sfkje - yskfpje;

            fund.SkHtJeThod = htje.ThousandsSeparator();
            fund.SkCompAtmThod = sfkje.ThousandsSeparator();
            fund.NoSkCompAtmThod = wsfkje.ThousandsSeparator();
            fund.SkCompInThod = yskfpje.ThousandsSeparator();
            fund.NoSkCompInThod = wykfpje.ThousandsSeparator();
            fund.SkCaiYsThod = (cwysf <= 0 ? 0 : cwysf).ThousandsSeparator();
            fund.SKCaiYjThod = (cwyjsf <= 0 ? 0 : cwyjsf).ThousandsSeparator();


            #endregion 收款合同

            #region 付款合同
           
            //付款款合同金额
            var fkhtje = this.Db.Set<ContractInfo>().Where(a => a.ProjectId == projId && (
            a.ContState == (int)ContractState.Execution
            || a.ContState == (int)ContractState.Terminated
            || a.ContState == (int)ContractState.Completed)
            && a.FinanceType == (int)FinceType.pay).Sum(a => (decimal?)a.AmountMoney ?? 0);

            //已付款金额
            var ffkje = this.Db.Set<ContActualFinance>().Include(a => a.Cont)
                .Where(a => a.Cont != null && a.Cont.ProjectId == projId && (a.Cont.ContState == (int)ContractState.Execution
            || a.Cont.ContState == (int)ContractState.Terminated
            || a.Cont.ContState == (int)ContractState.Completed)
            && a.Astate == (int)ActFinanceStateEnum.Confirmed
            && a.FinceType == (int)FinceType.pay).Sum(a => (decimal?)a.AmountMoney ?? 0);
            //未付款金额
            var fkwsfkje = fkhtje - ffkje;
            //已收票金额
            var fkkfpje = this.Db.Set<ContInvoice>().Include(a => a.Cont)
               .Where(a => a.Cont != null && a.Cont.CompId == projId && (a.Cont.ContState == (int)ContractState.Execution
           || a.Cont.ContState == (int)ContractState.Terminated
           || a.Cont.ContState == (int)ContractState.Completed)
           && (a.InState == (int)InvoiceStateEnum.Invoicing || a.InState == (int)InvoiceStateEnum.ReceiptInvoice)
           && a.Cont.FinanceType == (int)FinceType.pay).Sum(a => (decimal?)a.AmountMoney ?? 0);
            //未开票金额
            var fkwykfpje = fkhtje - fkkfpje;
            //财务应付==>已收票金额—已收金额
            var fkcwysf = fkkfpje - ffkje;
            //财务预收==>已收金额—已开票金额
            var fkcwyjsf = ffkje - fkkfpje;

            fund.FkHtJeThod = fkhtje.ThousandsSeparator();
            fund.FkCompAtmThod = ffkje.ThousandsSeparator();
            fund.NoFkCompAtmThod = fkwsfkje.ThousandsSeparator();
            fund.FkCompInThod = fkkfpje.ThousandsSeparator();
            fund.NoFkCompInThod = fkwykfpje.ThousandsSeparator();
            fund.FkCaiYsThod = (fkcwysf <= 0 ? 0 : fkcwysf).ThousandsSeparator();
            fund.FKCaiYjThod = (fkcwyjsf <= 0 ? 0 : fkcwyjsf).ThousandsSeparator();


            #endregion



            return fund;

        }

        /// <summary>
        /// 查询相关合同
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ProjContract> GetContsByProjId<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.Db.Set<ContractInfo>().Include(a=>a.Comp).AsTracking().Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContractInfo>))
            { //分页
                tempquery = tempquery.Skip<ContractInfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContractInfo>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            ContTypeId = a.ContTypeId,
                            AmountMoney = a.AmountMoney,
                            ContState = a.ContState,
                            CompId=a.CompId,
                            CompName = a.Comp == null ? "" : a.Comp.Name,
                            CurrencyId=a.CurrencyId,
                            FinanceType=a.FinanceType,


                        };
            var local = from a in query.AsEnumerable()
                        select new ProjContract
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            CompId = a.CompId??0,
                            CompName=a.CompName,
                            CurrName = RedisValueUtility.GetCurrencyName(a.CurrencyId, fileName:"Name"),//币种
                            FinceTypeName = EmunUtility.GetDesc(typeof(FinceType), a.FinanceType)//合同性质



                        };
            return new LayPageInfo<ProjContract>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

    }
}

