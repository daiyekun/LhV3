using Microsoft.EntityFrameworkCore;
using NF.BLL.Common;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Finance.Enums;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    public partial class APPWOrkList : BaseService<AppInst>, IAPPWOrkList
    {
        private DbSet<AppInst> _AppInstSet = null;
        private DbSet<ContractInfo> _ContractInfoSet = null;
        public APPWOrkList(DbContext dbContext)
               : base(dbContext)
            {
                _AppInstSet = base.Db.Set<AppInst>();
                _ContractInfoSet = base.Db.Set<ContractInfo>();
        }
        public APPWOrkList() { }
        
        #region 待处理
        /// <summary>
        /// 待处理-参数介绍见接口
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public APPWFLayPageInfo<AppPendingListDTO> GetAppWorkList<s>(int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc, int start, int limit)
        {
            var predicateAnd = PredicateBuilder.True<AppInst>();
            predicateAnd = predicateAnd.And(whereLambda);
            var groupIds = Db.Set<GroupUser>().Where(a => a.UserId == sessionUserId).AsNoTracking().Select(a => a.GroupId).ToList();

            var querynode = Db.Set<AppInstNodeInfo>().Where(a => groupIds.Contains(a.GroupId) && a.NodeState == 1);
            var queryoption = Db.Set<AppInstOpin>().AsNoTracking().Where(a => a.CreateUserId == sessionUserId);
            var query0 = from n in querynode
                         join g in queryoption
                         on n.InstId equals g.InstId

                         select new
                         {
                             g.InstId
                            ,
                             g.NodeStrId
                         };
            var apparr = query0.ToList();
            var arraynodes = querynode.ToList();
            IList<int> tempIds = new List<int>();
            foreach (var item in arraynodes)
            {
                if (!apparr.Any(a => a.InstId == item.InstId && a.NodeStrId == item.NodeStrId))
                {
                    tempIds.Add(item.InstId ?? 0);
                }

            }
            predicateAnd = predicateAnd.And(a => (tempIds.Any(c => c == a.Id)));
            var tempquery = _AppInstSet.AsTracking().Where<AppInst>(predicateAnd.Compile()).AsQueryable();
            //Where<ContActualFinance>(whereLambda.Compile()).AsQueryable();
            //where a.ObjType == 3
            int totalCount = tempquery.Count(); //共有记录数
            int pageNum = start / limit;//共有页数 
            tempquery = tempquery.Skip(limit * pageNum).Take(limit);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            ObjType = a.ObjType,
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            AppObjAmount = a.AppObjAmount,
                            Mission = a.Mission,
                            CurrentNodeId = a.CurrentNodeId,
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            AppState = a.AppState,
                            StartUserId = a.StartUserId,
                            FinceType = a.FinceType
                        };
            var local = from a in query.AsEnumerable()
                        select new AppPendingListDTO
                        {
                            Id = a.Id,
                            ObjType = a.ObjType,//审批对象id
                            //审批对象
                            ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType),
                            //审批对象id
                            AppObjId = a.AppObjId,
                            //审批对象名称
                            AppObjName = a.AppObjName,
                            //审批对象编号
                            AppObjNo = a.AppObjNo,
                            //审批事项id
                            Mission = a.Mission,
                            //审批事项
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                            //当前节点id
                            CurrentNodeId = a.CurrentNodeId,
                            //当前节点名称
                            CurrentNodeName = a.CurrentNodeName,
                            //发起时间
                            StartDateTime = a.StartDateTime,
                            //发起人
                            StartUserName = this.Db.GetRedisUserFieldValue(a.StartUserId ?? -1),
                           // RedisValueUtility.GetUserShowName(a.StartUserId ?? 0),
                            //审批状态
                            AppState = a.AppState,
                            //状态描述
                            AppStateDic = EmunUtility.GetDesc(typeof(AppInstEnum), a.AppState),
                            //审批对象金额
                            AppObjAmount = a.AppObjAmount ?? 0,
                            AppObjAmountThod = a.AppObjAmount.ThousandsSeparator(),
                            //资金性质
                            FinceType = a.FinceType
                        };
          
            return new APPWFLayPageInfo<AppPendingListDTO>()
            {
                totalCount = totalCount,
                WFInstanceList = local.ToList(),
            };
        }
        public APPWFLayPageInfo<AppPendingListDTO> GetAppSponsorList<s>(Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc, int start, int limit)
        {
            var tempquery = this.Db.Set<AppInst>().AsTracking().Where(whereLambda.Compile());
            int totalCount = tempquery.Count(); //共有记录数
            int pageNum = start / limit;//共有页数 
            tempquery = tempquery.Skip(limit * pageNum).Take(limit);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            ObjType = a.ObjType,
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            AppObjAmount = a.AppObjAmount,
                            Mission = a.Mission,
                            CurrentNodeId = a.CurrentNodeId,
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            AppState = a.AppState,
                            FinceType = a.FinceType,
                            AppSecObjId = a.AppSecObjId,
                            StartUserId=a.StartUserId

                        };
            var local = from a in query.AsEnumerable()
                        select new AppPendingListDTO
                        {
                            Id = a.Id,
                            ObjType = a.ObjType,
                            ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType),
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            Mission = a.Mission,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                            CurrentNodeId = a.CurrentNodeId,
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            AppState = a.AppState,
                            AppStateDic = EmunUtility.GetDesc(typeof(AppInstEnum), a.AppState),
                            AppObjAmount = a.AppObjAmount ?? 0,
                            AppObjAmountThod = a.AppObjAmount.ThousandsSeparator(),
                            FinceType = a.FinceType,
                            AppSecObjId = a.AppSecObjId,//次要字段ID，比如实际资金时合同ID
                             //发起人
                            StartUserName = this.Db.GetRedisUserFieldValue(a.StartUserId ?? -1),
                           // RedisValueUtility.GetUserShowName(a.StartUserId ?? 0)

                        };
         
            return new APPWFLayPageInfo<AppPendingListDTO>()
            {
                totalCount = totalCount,
                WFInstanceList = local.ToList(),
            };
        }

        /// <summary>
        /// 已处理-参数介绍见接口
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public APPWFLayPageInfo<AppProcessedListDTO> GetAppProcessedList<s>(int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc, int start, int limit)
        {
            var predicateAnd = PredicateBuilder.True<AppInst>();
            predicateAnd = predicateAnd.And(whereLambda);

            var tempquery = _AppInstSet.AsTracking().Where(predicateAnd.Compile());
            //pageInfo.TotalCount = tempquery.Count();
            //tempquery = tempquery.Skip<AppInst>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<AppInst>(pageInfo.PageSize);
            int totalCount = tempquery.Count(); //共有记录数
            int pageNum = start / limit;//共有页数 
            tempquery = tempquery.Skip(limit * pageNum).Take(limit);
            var query = from a in tempquery
                        join
                        b in Db.Set<AppInstOpin>().AsNoTracking()
                        on a.Id equals b.InstId
                        where b.CreateUserId == sessionUserId
                        select new
                        {
                            Id = a.Id,
                            ObjType = a.ObjType,
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            AppObjAmount = a.AppObjAmount,
                            Mission = a.Mission,
                            CurrentNodeId = a.CurrentNodeId,
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            AppState = a.AppState,
                            StartUserId = a.StartUserId,
                            Option = b.Opinion,
                            OptionDate = b.CreateDatetime,
                            FinceType = a.FinceType,
                            AppSecObjId = a.AppSecObjId,




                        };
            var local = from a in query.AsEnumerable()
                        select new AppProcessedListDTO
                        {
                            Id = a.Id,
                            ObjType = a.ObjType,
                            ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType),
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            Mission = a.Mission,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                            CurrentNodeId = a.CurrentNodeId,
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            StartUserName = this.Db.GetRedisUserFieldValue(a.StartUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.StartUserId ?? 0),
                            AppState = a.AppState,
                            AppStateDic = EmunUtility.GetDesc(typeof(AppInstEnum), a.AppState),
                            AppObjAmount = a.AppObjAmount ?? 0,
                            AppObjAmountThod = a.AppObjAmount.ThousandsSeparator(),
                            Option = a.Option,
                            OptionDate = a.OptionDate,
                            FinceType = a.FinceType,
                            AppSecObjId = a.AppSecObjId,


                        };
          
            return new APPWFLayPageInfo<AppProcessedListDTO>()
            {
                totalCount = totalCount,
                WFInstanceList = local.ToList(),
            };
        }
        #endregion
        /// <summary>
        /// 查询合同基本信息
        /// </summary>
        /// <param name="sessionUserId"></param>
        /// <returns></returns>
        public APPWFLayPageInfo<ContractInfoHistoryViewDTO> GetIDWorkList(int sessionUserId)
        {
            var query =_AppInstSet.AsTracking().Where(p => p.AppObjId == sessionUserId).FirstOrDefault();
                var contrac = from a in _ContractInfoSet.AsNoTracking()
                            where a.Id == query.AppObjId
                              select new
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Code = a.Code,
                                OtherCode = a.OtherCode,
                                ContSourceId = a.ContSourceId,
                                ContTypeId = a.ContTypeId,
                                IsFramework = a.IsFramework,
                                ContDivision = a.ContDivision,
                                CompId = a.CompId,
                                CompName = a.Comp.Name,
                                ProjName = a.Project.Name,
                                AmountMoney = a.AmountMoney,
                                CurrencyId = a.CurrencyId,
                                CurrencyRate = a.CurrencyRate,
                                EstimateAmount = a.EstimateAmount,
                                AdvanceAmount = a.AdvanceAmount,
                                StampTax = a.StampTax,
                                CreateUserId = a.CreateUserId,
                                CreateDateTime = a.CreateDateTime,
                                SngnDateTime = a.SngnDateTime,
                                EffectiveDateTime = a.EffectiveDateTime,
                                PlanCompleteDateTime = a.PlanCompleteDateTime,
                                ActualCompleteDateTime = a.ActualCompleteDateTime,
                                DeptId = a.DeptId,
                                ProjectId = a.ProjectId,
                                ContState = a.ContState,
                                MainDeptId = a.MainDeptId,
                                Reserve1 = a.Reserve1,
                                Reserve2 = a.Reserve2,
                                Comp3Name = a.CompId3Navigation.Name,
                                Comp4Name = a.CompId4Navigation.Name,
                                CompId3 = a.CompId3,
                                CompId4 = a.CompId4,
                                PrincipalUserId = a.PrincipalUserId,
                                FinanceTerms = a.FinanceTerms,
                                PerformanceDateTime = a.PerformanceDateTime,
                                ModificationRemark = a.ModificationRemark,
                                FinanceType=a.FinanceType

                              };
                     var  local = from a in contrac.AsEnumerable()
                            select new ContractInfoHistoryViewDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Code = a.Code,
                                OtherCode = a.OtherCode,
                                ContSourceId = a.ContSourceId,
                                ContTypeId = a.ContTypeId,
                                IsFramework = a.IsFramework,
                                ContDivision = (a.ContDivision ?? 0).ToString(),
                                CompId = a.CompId,
                                AmountMoney = a.AmountMoney,
                                CurrencyId = a.CurrencyId,
                                CurrencyRate = a.CurrencyRate,
                                EstimateAmount = a.EstimateAmount,
                                AdvanceAmount = a.AdvanceAmount,
                                StampTax = a.StampTax,
                                CreateUserId = a.CreateUserId,
                                CreateDateTime = a.CreateDateTime,
                                SngnDateTime = a.SngnDateTime,
                                EffectiveDateTime = a.EffectiveDateTime,
                                PlanCompleteDateTime = a.PlanCompleteDateTime,
                                ActualCompleteDateTime = a.ActualCompleteDateTime,
                                DeptId = a.DeptId,
                                ProjectId = a.ProjectId,
                                ContState = a.ContState,
                                MainDeptId = a.MainDeptId,
                                Reserve1 = a.Reserve1,
                                Reserve2 = a.Reserve2,
                                //合同类别
                                ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                                //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别                                                                                        //合同来源
                                ContSName = this.Db.GetRedisDataDictionaryValue(a.ContSourceId ?? 0, DataDictionaryEnum.contSource),
                                //DataDicUtility.GetDicValueToRedis(a.ContSourceId, DataDictionaryEnum.contSource),
                                CompName = a.CompName,//合同对方
                                ProjName = a.ProjName,//项目名称
                                ContPro = EmunUtility.GetDesc(typeof(ContractProperty), a.IsFramework ?? 0),//合同属性
                                ContSum = a.ContDivision > 0 ? "是" : "否",
                                ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                                CurrencyName = RedisValueUtility.GetCurrencyName(a.CurrencyId), ///币种
                                Rate = a.CurrencyRate ?? 1,//汇率
                                EsAmountThod = a.EstimateAmount.ThousandsSeparator(),//预估金额
                                AdvAmountThod = a.AdvanceAmount.ThousandsSeparator(),//预收预付
                                StampTaxThod = a.StampTax.ThousandsSeparator(),//千分位
                                CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                                //RedisValueUtility.GetUserShowName(a.CreateUserId),//创建人
                                PrincipalUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId??0),
                                //RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? 0),//负责人
                                DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                                //RedisValueUtility.GetDeptName(a.DeptId ?? -2),
                                MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),
                                //RedisValueUtility.GetDeptName(a.MainDeptId ?? -2),
                                StateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                                Comp3Name = a.Comp3Name,
                                Comp4Name = a.Comp4Name,
                                CompId3 = a.CompId3,
                                CompId4 = a.CompId4,
                                FinanceTerms = a.FinanceTerms,//资金条款
                                PerformanceDateTime = a.PerformanceDateTime,
                                ChangeDesc = a.ModificationRemark,//变更说明
                                FinanceType = a.FinanceType
                            };
                return new APPWFLayPageInfo<ContractInfoHistoryViewDTO>()
                {
                    WFInstanceList = local.ToList(),
                };
        }
        /// <summary>
        /// 查询客户id基本信息
        /// </summary>
        /// <param name="comid">客户id</param>
        /// <returns></returns>
        public APPWFLayPageInfo<CompanyViewDTO> GetCUIDWorkList(int comid)
        {
            var tempquery = Db.Set<Company>().AsNoTracking();
            var query = from a in tempquery where a.Id==comid
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            //CreateUserDisplayName = RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.Id}", "DisplyName"), //a.CreateUser.DisplyName,
                            Cstate = a.Cstate,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            CompTypeId = a.CompTypeId,//公司类型
                                                      // CompTypeName= a.CompType.Name,//公司类型
                            FirstContact = a.FirstContact,//首要联系人
                            FirstContactMobile = a.FirstContactMobile,//首要联系人移动电话
                            FirstContactTel = a.FirstContactTel,//首要联系人办公电话
                            CareditId = a.CareditId,//信用等级ID
                            //CareditName= a.Caredit.Name,//信用等级
                            //LevelName =a.Level.Name,//单位级别
                            LevelId = a.LevelId,//单位级别
                            CompClassId = a.CompClassId,//公司类别
                            //CompanyTypeClass = a.CompClass.Name,//公司类别
                            PrincipalUserId = a.PrincipalUserId,
                            //PrincipalUserDisplayName = a.PrincipalUser.DisplyName,//负责人
                            Trade = a.Trade,//行业
                            CountryId = a.CountryId,//国家ID
                            ProvinceId = a.ProvinceId,//省
                            CityId = a.CityId,//城市ID
                            Ctype = a.Ctype,
                            WfState = a.WfState,
                            WfItem = a.WfItem,
                            WfCurrNodeName = a.WfCurrNodeName,
                        };
            var local = from a in query.AsEnumerable()
                        select new CompanyViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplayName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            Cstate = a.Cstate,
                            CstateDic = EmunUtility.GetDesc(typeof(CompStateEnum), a.Cstate),
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            CompTypeId = a.CompTypeId,
                            FirstContact = a.FirstContact,//首要联系人
                            FirstContactMobile = a.FirstContactMobile,//首要联系人移动电话
                            FirstContactTel = a.FirstContactTel,//首要联系人办公电话
                            CompanyTypeClass = CompanyUtility.CompanyTypeClass(a.CompClassId, a.Ctype ?? -1),//公司类别
                            CompClassId = a.CompClassId,
                            PrincipalUserDisplayName = (a.PrincipalUserId ?? 0) == 0 ? "" : RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName").ToString(),//a.PrincipalUserDisplayName,//负责人
                            Trade = a.Trade,
                            CountryName = RedisHelper.HashGet($"{StaticData.RedisCountryKey}:{a.CountryId}", "Name"), //listcountry.Any(p=>p.Id== a.CountryId)? listcountry.FirstOrDefault(p => p.Id == a.CountryId).Name:"",
                            ProvinceName = RedisHelper.HashGet($"{StaticData.RedisProvinceKey}:{a.ProvinceId}", "Name"),  //listprovince.Any(p => p.Id == a.ProvinceId) ? listprovince.FirstOrDefault(p => p.Id == a.ProvinceId).Name : "",
                            CityName = RedisHelper.HashGet($"{StaticData.RedisCityKey}:{a.CityId}", "Name"),  //listcity.Any(p => p.Id == a.CityId) ? listcity.FirstOrDefault(p => p.Id == a.CityId).Name : "",
                            UserDeptId = Convert.ToInt32(this.Db.GetRedisUserFieldValue(a.CreateUserId, "DepartmentId")),
                            //RedisValueUtility.GetRedisUserDeptId(a.CreateUserId),
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                        };
            return new APPWFLayPageInfo<CompanyViewDTO>()
            {
                WFInstanceList = local.ToList(),
            };
        }
        /// <summary>
        /// 查询发票基本信息
        /// </summary>
        /// <param name="comid">发票id</param>
        /// <returns></returns>
        public APPWFLayPageInfo<ContInvoiceListViewDTO> GetInvIDWorkList(int comid)
        {
            var tempquery = Db.Set<ContInvoice>().AsNoTracking();
            var query = from a in tempquery
                        where a.Id == comid
                        join g in _ContractInfoSet
                         on a.ContId equals g.Id
                        select new
                        {
                            Name = g.Name,
                            code=g.Code,
                            InType=a.InType,
                            InCode = a.InCode,
                            AmountMoney=a.AmountMoney,
                            MakeOutDateTime = a.MakeOutDateTime,
                            CreateUserId=a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ContTypeId = g.ContTypeId,
                            CompId = g.CompId,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContInvoiceListViewDTO
                        {
                            //合同名称
                            ContName=a.Name,
                            //合同编号
                            ContCode=a.code,
                            //合同类别
                            ContCategoryName= this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),
                            //合同对方
                            CompName = SetName(a.CompId??0),
                            //发票类型
                            InTypeName = this.Db.GetRedisDataDictionaryValue(a.InType ?? 0, DataDictionaryEnum.InvoiceType),
                            //DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                            //发票号
                            InCode=a.InCode,
                            //付款金额
                            AmountMoneyThod=a.AmountMoney.ThousandsSeparator(),
                            //开票日期
                            MakeOutDateTime=a.MakeOutDateTime,
                            //创建人
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            //创建时间
                            CreateDateTime =a.CreateDateTime

                        };
            return new APPWFLayPageInfo<ContInvoiceListViewDTO>()
            {
                WFInstanceList = local.ToList(),
            };
        }
        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="comid"></param>
        /// <returns></returns>
        public APPWFLayPageInfo<ContActualFinanceListViewDTO> GetIFukuanIDWorkList(int comid)
        {
            var tempquery = Db.Set<ContActualFinance>().AsNoTracking();
            var query = from a in tempquery
                        where a.Id == comid
                        join g in _ContractInfoSet
                         on a.ContId equals g.Id
                        select new
                        {
                            Name = g.Name,
                            code = g.Code,
                            FinceType = a.FinceType,
                            AmountMoney = a.AmountMoney,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ContTypeId = g.ContTypeId,
                            CompId = g.CompId,
                            Account = a.Account,
                            //发票号
                            Bank = a.Bank,
                            SettlementMethod=a.SettlementMethod
                        };
            var local = from a in query.AsEnumerable()
                        select new ContActualFinanceListViewDTO
                        {
                            //合同名称
                            ContName = a.Name,
                            //合同编号
                            ContCode = a.code,
                            //合同类别
                            ContCategoryName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),
                            //合同对方
                            CompName = SetName(a.CompId ?? 0),
                            //发票类型
                            //InTypeName = DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                            //银行
                            Account = a.Account,
                            //发票号
                            Bank = a.Bank,
                            //付款金额
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            //结算方式
                            SettlementMethodDic = this.Db.GetRedisDataDictionaryValue(a.SettlementMethod ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementMethod, DataDictionaryEnum.SettlModes),
                            //创建人
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            //创建时间
                            CreateDateTime = a.CreateDateTime

                        };
            return new APPWFLayPageInfo<ContActualFinanceListViewDTO>()
            {
                WFInstanceList = local.ToList(),
            };
        }
        /// <summary>
        /// 项目
        /// </summary>
        /// <param name="comid"></param>
        /// <returns></returns>
        public APPWFLayPageInfo<ProjectManagerViewDTO> GetIPRonIDWorkList(int comid)
        {
            var tempquery = Db.Set<ProjectManager>().AsNoTracking();
            var query = from a in tempquery
                        where a.Id == comid
                        select new
                        {
                            Name=a.Name,
                            Code = a.Code,
                            CategoryId=a.CategoryId,
                            BugetCollectAmountMoney=a.BugetCollectAmountMoney,
                            BudgetPayAmountMoney=a.BudgetPayAmountMoney,
                            PrincipalUserId=a.PrincipalUserId,
                            CreateDateTime=a.CreateDateTime,
                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectManagerViewDTO
                        {
                            //项目名称
                            Name = a.Name,
                            //项目编号
                            Code = a.Code,
                            //类别
                            ProjTypeName = this.Db.GetRedisDataDictionaryValue(a.CategoryId, DataDictionaryEnum.projectFileType),
                            //DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.projectFileType),
                            //预算收款
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,
                            //预算付款
                            BudgetPayAmountMoney=a.BudgetPayAmountMoney,
                            //负责人
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.PrincipalUserId??0),
                            //创建时间
                            CreateDateTime = a.CreateDateTime

                        };
            return new APPWFLayPageInfo<ProjectManagerViewDTO>()
            {
                WFInstanceList = local.ToList(),
            };
        }
        public string SetName(int id)

        {
            var tempquery = Db.Set<Company>().AsNoTracking().Where(p => p.Id == id).FirstOrDefault();
            return tempquery.Name;

        }
        /// <summary>
        /// 提交审批意见
        /// </summary>
        /// <param name="submitOption">提交审批意见对象</param>
        /// <returns></returns>
        public int SubmintOption(AppWorkcs submitOption)
        {
            try
            {
            var result = 1;
            var appinst = Db.Set<AppInst>().Where(a => a.Id == submitOption.InstId).FirstOrDefault();
            //var tempappinst = appinst;
            var currNodeInfo = Db.Set<AppInstNodeInfo>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
            var currNode = Db.Set<AppInstNode>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
            SaveOption(submitOption, appinst);
            if (currNodeInfo.Nrule == (int)NodeNruleEnum.All)
            {//全部通过
             //当前节点需要参加的审批人员数
               result = AllApprove(submitOption, result, appinst, currNodeInfo, currNode);

            }
            else if (currNodeInfo.Nrule == (int)NodeNruleEnum.AtWill)
            {//任意通过
                result = ApproveToNextNode(submitOption, result, appinst, currNodeInfo, currNode);
            }



            this.Db.SaveChanges();

            return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        #region 不同意时提交意见

        /// <summary>
        /// 不同意时清理缓存
        /// </summary>
        /// <param name="submitOption">意见</param>
        /// <returns></returns>
        public bool SubmintDisagreeOption(AppWorkcs submitOption)
        {
            try
            {
                var appinst = Db.Set<AppInst>().Where(a => a.Id == submitOption.InstId).FirstOrDefault();
                appinst.AppState = 3;
                appinst.CompleteDateTime = DateTime.Now;
                var currNodeInfo = Db.Set<AppInstNodeInfo>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
                currNodeInfo.NodeState = 3;

                var currNode = Db.Set<AppInstNode>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
                currNode.NodeState = 3;
                currNode.CompDateTime = DateTime.Now;
                var objdata = InitUpdateData(submitOption);
                objdata.WfState = 3;
                UpdateObjectState(objdata);
                var opion = GetOtpionInfo(submitOption, appinst);
                opion.Result = 5;
                this.Db.Set<AppInstOpin>().Add(opion);
                this.Db.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        #endregion


        /// <summary>
        /// 保存意见
        /// </summary>
        private void SaveOption(AppWorkcs submitOption, AppInst appInst)
        {
            var option = GetOtpionInfo(submitOption, appInst);//意见对象
            this.Db.Set<AppInstOpin>().Add(option);
        }
        /// <summary>
        /// 获取审批意见对象
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <param name="submitOption">提交意见对象</param>
        /// <returns>返回审批意见实体对象</returns>
        private AppInstOpin GetOtpionInfo(AppWorkcs submitOption, AppInst appInst)
        {
            AppInstOpin appInstOpin = new AppInstOpin();
            appInstOpin.InstId = submitOption.InstId;
            appInstOpin.NodeId = appInst.CurrentNodeId;
            appInstOpin.NodeStrId = appInst.CurrentNodeStrId;
            appInstOpin.CreateUserId = submitOption.SubmitUserId;
            appInstOpin.CreateDatetime = DateTime.Now;
            appInstOpin.Opinion = submitOption.Option;
            return appInstOpin;
        }

        /// <summary>
        /// 全部通过
        /// </summary>
        /// <param name="submitOption">提交意见</param>
        /// <param name="result">状态标识</param>
        /// <param name="appinst">审批实例</param>
        /// <param name="currNodeInfo">当前节点信息</param>
        /// <param name="currNode">当前节点</param>
        /// <returns></returns>
        private int AllApprove(AppWorkcs submitOption, int result, AppInst appinst, AppInstNodeInfo currNodeInfo, AppInstNode currNode)
        {
            int currNodeAppUserNumber = this.Db.Set<AppGroupUser>().AsNoTracking()
              .Where(a => a.InstId == appinst.Id && a.NodeStrId == appinst.CurrentNodeStrId).Count();
            //已经审批通过人数
            int currAppedNumber = this.Db.Set<AppInstOpin>().AsNoTracking()
                .Where(a => a.NodeStrId == appinst.CurrentNodeStrId
                && a.InstId == appinst.Id).Count();
            if ((currAppedNumber + 1) == currNodeAppUserNumber)
            {//表示最后一个审批人
                result = ApproveToNextNode(submitOption, result, appinst, currNodeInfo, currNode);

            }//else如果不是最后一个人只需要添加意见就好，而添加意见直接放到最后。此时不需要做什么

            return result;
        }

        /// <summary>
        /// 节点通过为“全部通过”，并且是最后一个人审批的时候
        /// </summary>
        /// <param name="submitOption">提交意见</param>
        /// <param name="result">状态标识</param>
        /// <param name="appinst">审批实例</param>
        /// <param name="currNodeInfo">当前节点信息</param>
        /// <param name="currNode">当前节点</param>
        /// <returns></returns>
        private int ApproveToNextNode(AppWorkcs submitOption, int result, AppInst appinst, AppInstNodeInfo currNodeInfo, AppInstNode currNode)
        {
            List<AppInstNodeLine> listlines = this.Db.Set<AppInstNodeLine>()
                .Where(a => a.From == appinst.CurrentNodeStrId && a.InstId == appinst.Id).Select(a => a).ToList();
            var NextNodeIds = listlines.Select(a => a.To).ToList();
            var NextNodes = this.Db.Set<AppInstNode>()
              .Where(a => NextNodeIds.Contains(a.NodeStrId) && a.InstId == appinst.Id).Select(a => a).ToList();
            if (NextNodes.Count() == 1)
            {
                LineFlowMapping(submitOption, currNodeInfo, currNode, listlines, NextNodes, appinst);

            }
            else
            {//多个分支节点,目前设计不会走
                result = BranchFlowMapping(submitOption, currNodeInfo, currNode, listlines, NextNodes);

            }

            return result;
        }
        /// <summary>
        /// 多分支
        /// </summary>
        /// <param name="submitOption">提交意见对象</param>
        /// <param name="currNodeInfo">当前节点信息</param>
        /// <param name="currNode">当前节点</param>
        /// <param name="listlines">节点连线集合</param>
        /// <param name="NextNodes">下一个节点集合</param>
        /// <returns></returns>
        private int BranchFlowMapping(AppWorkcs submitOption, AppInstNodeInfo currNodeInfo, AppInstNode currNode, List<AppInstNodeLine> listlines, List<AppInstNode> NextNodes)
        {
            var NextNodeIds = NextNodes.Select(a => a.NodeStrId).ToList();
            List<AppInstNodeInfo> NextNodeInfos = this.Db.Set<AppInstNodeInfo>().Where(a => NextNodeIds.Contains(a.NodeStrId) && a.InstId == submitOption.InstId).Select(a => a).ToList();
            var strNodeId = GetNextNodeStrId(submitOption, NextNodeInfos);
            UpdateObjectInfo updata = InitUpdateData(submitOption);
            if (string.IsNullOrEmpty(strNodeId))
            {//标识下一个节点集合包含结束节点
                var endint = (int)NodeTypeEnum.NType1;
                if (NextNodes.Select(a => a.Type).Contains(endint))
                {
                    var endnode = NextNodes.Where(a => a.Type == endint).FirstOrDefault(); updata.WfState = 2;
                    //修改数据状态
                    UpdateObjectState(updata);
                    endnode.Marked = 1;
                    var endToline = listlines.Where(a => a.To == endnode.NodeStrId).FirstOrDefault();
                    endToline.Marked = 1;

                }
                else
                {
                    return -1;//没有找到满足条件的分支节点
                }

            }
            else
            {//找找到分支节点
                var findNextNode = NextNodes.Where(a => a.NodeStrId == strNodeId).FirstOrDefault();
                findNextNode.Marked = 1;
                findNextNode.NodeState = 1;
                var findNextNodeInfo = NextNodeInfos.Where(a => a.NodeStrId == strNodeId).FirstOrDefault();
                findNextNodeInfo.NodeState = 1;
                var findnexToline = listlines.Where(a => a.To == strNodeId).FirstOrDefault();
                findnexToline.Marked = 1;


            }
            currNode.NodeState = (int)NodeStateEnum.YiTongGuo;
            currNodeInfo.NodeState = (int)NodeStateEnum.YiTongGuo;

            return 1;
        }

        /// <summary>
        /// 修改审批对象状态
        /// </summary>
        private void UpdateObjectState(UpdateObjectInfo updateObjectInfo)
        {
            switch (updateObjectInfo.ObjType)
            {
                case FlowObjEnums.Customer://客户
                case FlowObjEnums.Supplier://供应商
                case FlowObjEnums.Other://其他对方
                    {
                        var company = this.Db.Set<Company>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                        if (updateObjectInfo.WfState == 1)
                        {
                            company.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                        }
                        else if (updateObjectInfo.WfState == 2)
                        {
                            company.WfState = 2;//审批通过
                            company.Cstate = (int)CompStateEnum.Audited;//数据状态
                            company.WfCurrNodeName = "";
                            company.WfItem = null;
                        }
                        else if (updateObjectInfo.WfState == 3)
                        {//被打回
                            company.WfState = 3;//被打回
                        }
                    }
                    break;

                case FlowObjEnums.project://项目
                    {
                        var projInfo = this.Db.Set<ProjectManager>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                        if (updateObjectInfo.WfState == 1)
                        {
                            projInfo.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                        }
                        else if (updateObjectInfo.WfState == 2)
                        {
                            projInfo.WfState = 2;//审批通过
                            projInfo.Pstate = (int)ProjStateEnum.Approve;//审批通过
                            projInfo.WfCurrNodeName = "";
                            projInfo.WfItem = null;
                        }
                        else if (updateObjectInfo.WfState == 3)
                        {//被打回
                            projInfo.WfState = 3;//被打回
                        }
                    }
                    break;
                case FlowObjEnums.Contract://合同
                    {
                        var continfo = this.Db.Set<ContractInfo>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                        if (updateObjectInfo.WfState == 1)
                        {
                            continfo.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                        }
                        else if (updateObjectInfo.WfState == 2)
                        {
                            continfo.WfState = 2;//审批通过
                            continfo.ContState = (int)ContractState.Approve;//审批通过
                            continfo.WfCurrNodeName = "";
                            continfo.WfItem = null;
                        }
                        else if (updateObjectInfo.WfState == 3)
                        {//被打回
                            continfo.WfState = 3;//被打回
                        }
                    }
                    break;
                case FlowObjEnums.payment://付款
                    {
                        var actFinance = this.Db.Set<ContActualFinance>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                        if (updateObjectInfo.WfState == 1)
                        {
                            actFinance.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                        }
                        else if (updateObjectInfo.WfState == 2)
                        {
                            actFinance.WfState = 2;//审批通过
                            actFinance.Astate = (int)ActFinanceStateEnum.Submitted;//(int)ActFinanceStateEnum.Approved;//审批通过
                            actFinance.WfCurrNodeName = "";
                            actFinance.WfItem = null;
                        }
                        else if (updateObjectInfo.WfState == 3)
                        {//被打回
                            actFinance.WfState = 3;//被打回
                        }
                    }
                    break;
                case FlowObjEnums.InvoiceIn://收票
                case FlowObjEnums.InvoiceOut://开票
                    {
                        var contInvoice = this.Db.Set<ContInvoice>().Where(a => a.Id == updateObjectInfo.ObjId).FirstOrDefault();
                        if (updateObjectInfo.WfState == 1)
                        {
                            contInvoice.WfCurrNodeName = updateObjectInfo.WfCurrNodeName;//当前审批节点
                        }
                        else if (updateObjectInfo.WfState == 2)
                        {
                            contInvoice.WfState = 2;//审批通过
                            contInvoice.InState = (int)InvoiceStateEnum.Submitted;//(int)ActFinanceStateEnum.Approved;//审批通过
                            contInvoice.WfCurrNodeName = "";
                            contInvoice.WfItem = null;
                        }
                        else if (updateObjectInfo.WfState == 3)
                        {//被打回
                            contInvoice.WfState = 3;//被打回
                        }
                    }
                    break;

            }




        }

        /// <summary>
        /// 初始化修改数据对象
        /// </summary>
        /// <param name="submitOption">提交审批意见</param>
        /// <returns></returns>
        private UpdateObjectInfo InitUpdateData(AppWorkcs submitOption)
        {
            var updata = new UpdateObjectInfo();
            updata.ObjId = submitOption.ObjId;
            updata.ObjType = (FlowObjEnums)submitOption.ObjType;
            return updata;
        }

        /// <summary>
        /// 查找满足条件的节点Id 
        /// </summary>
        /// <param name="submitOption">提交意见</param>
        /// <param name="NextNodeInfos">下一个节点信息集合</param>
        /// <returns></returns>
        private string GetNextNodeStrId(AppWorkcs submitOption, List<AppInstNodeInfo> NextNodeInfos)
        {
            string nodestrId = "";
            foreach (var node in NextNodeInfos)
            {
                if ((node.IsMin == 0 && node.IsMax == 0)
                    && (submitOption.ObjMoney > node.Min && submitOption.ObjMoney < node.Max))
                {

                    nodestrId = node.NodeStrId;
                    break;

                }
                else if ((node.IsMin == 1 && node.IsMax == 0)
                   && (submitOption.ObjMoney >= node.Min && submitOption.ObjMoney < node.Max))
                {
                    nodestrId = node.NodeStrId;
                    break;
                }
                else if ((node.IsMin == 1 && node.IsMax == 1)
                   && (submitOption.ObjMoney >= node.Min && submitOption.ObjMoney <= node.Max))
                {
                    nodestrId = node.NodeStrId;
                    break;
                }
                else if ((node.IsMin == 0 && node.IsMax == 1)
                    && (submitOption.ObjMoney > node.Min && submitOption.ObjMoney <= node.Max))
                {
                    nodestrId = node.NodeStrId;
                    break;
                }

            }

            return nodestrId;
        }

        /// <summary>
        /// 直线程匹配
        /// </summary>
        /// <param name="submitOption">审批意见</param>
        /// <param name="currNodeInfo">当前节点信息</param>
        /// <param name="currNode">当前节点</param>
        /// <param name="listlines">下一节点连线集合</param>
        /// <param name="NextNodes">下一节点集合（分支就会参数集合）</param>
        /// <param name="appinst">当前审批实例对象</param>
        private void LineFlowMapping(AppWorkcs submitOption, AppInstNodeInfo currNodeInfo, AppInstNode currNode, List<AppInstNodeLine> listlines, List<AppInstNode> NextNodes, AppInst appinst)
        {
            UpdateObjectInfo updata = InitUpdateData(submitOption);
            if (NextNodes.First().Type == (int)NodeTypeEnum.NType1)
            { //表示审批结束，当前节点已经是最后一个审批节点
                updata.WfState = 2;
                appinst.AppState = 2;//通过
                //修改数据状态
                // UpdateObjectState(updata);
            }
            else
            {
                updata.WfState = 1;
                updata.WfCurrNodeName = NextNodes.FirstOrDefault().Name;//当前节点
            }
            //修改数据状态
            UpdateObjectState(updata);
            //当前节点
            currNode.NodeState = (int)NodeStateEnum.YiTongGuo;
            currNodeInfo.NodeState = (int)NodeStateEnum.YiTongGuo;
            currNode.CompDateTime = DateTime.Now;
            //下一个节点
            var nextNode = NextNodes.FirstOrDefault();
            nextNode.Marked = 1;
            nextNode.NodeState = 1;
            nextNode.ReceDateTime = DateTime.Now;
            if (nextNode.Type != (int)NodeTypeEnum.NType1 && nextNode.Type != (int)NodeTypeEnum.NType0)
            {
                var nextnodeInfo = this.Db.Set<AppInstNodeInfo>()
                    .Where(a => a.InstId == submitOption.InstId && a.NodeStrId == nextNode.NodeStrId).FirstOrDefault();
                new NF.BLL.MobileApp.Work.INsertTixing().newSelectTixing(nextnodeInfo.NodeStrId, nextnodeInfo.InstId ?? 0, this.Db);
                nextnodeInfo.NodeState = 1;
            }
            //审批实例
            appinst.CurrentNodeId = nextNode.Id;
            appinst.CurrentNodeName = nextNode.Name;
            appinst.CurrentNodeStrId = nextNode.NodeStrId;
            appinst.CompleteDateTime = DateTime.Now;
            //连线颜色
            listlines.FirstOrDefault().Marked = 1;
        }

        public List<APPcontractSPD> AppcontractSPDDetail(int Id)
        {
            var instid = _AppInstSet.AsTracking().Where(p => p.AppObjId == Id).FirstOrDefault().Id;
            var tempquery = Db.Set<AppInstOpin>().AsTracking().Where(p=>p.InstId== instid);
            var Query = from a in tempquery
                        join g in Db.Set<AppInstNode>().AsTracking()
                         on a.NodeId equals g.Id
                        select new
                        {
                            Id = a.Id,
                            CreateUserId=a.CreateUserId,
                            CreateDatetime= a.CreateDatetime,
                            Opinion = a.Opinion,
                            NodeIdna=g.Name,
                            
                        };
            var local = from a in Query.AsEnumerable()
                        select new APPcontractSPD
                        {
                            Id = a.Id,
                           Opin = a.Opinion,
                            CurrentNodeName = a.NodeIdna,
                            CuName =this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId??0),
                            copnData = a.CreateDatetime,
                        };
            return local.ToList();
        }
    }
}
