using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using NF.Model.Models;
using NF.Common.Utility;
using NF.ViewModel.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.Utility;
using NF.BLL.Common;
using NF.ViewModel;
using NF.Common.Extend;
using NF.ViewModel.Models.Finance.Enums;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 合同对方类
    /// </summary>
    public partial class CompanyService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<Company>();
            //不等于fieldInfo.CurrId是排除修改的时候
            predicateAnd = predicateAnd.And(a=>a.IsDelete==0&& a.Ctype == fieldInfo.ObjType&&a.Id!= fieldInfo.Id);
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
        /// 公司类型
        /// </summary>
        /// <returns>公司类型名称</returns>
        private string GetCompTypeName(int? CompTypeId)
        {

            return DataDicUtility.GetDicValueToRedis(CompTypeId, DataDictionaryEnum.companyType);
        }
        /// <summary>
        /// 信用等级名称
        /// </summary>
        /// <returns>返回信息等级名称</returns>
        private string GetCareditName(int? CareditId)
        {
            return DataDicUtility.GetDicValueToRedis(CareditId, DataDictionaryEnum.customerCaredit);
        }

        //
        /// <summary>
        /// 级别
        /// </summary>
        /// <param name="LevelId">级别ID</param>
        /// <param name="comptype">0：客户，1：供应商，2：其他对方</param>
        /// <returns></returns>
        private string GetLevelName(int? LevelId,int comptype )
        {
            if (LevelId == null)
            {
                return "";
            }
            else {
            DataDictionaryEnum customerLevel= DataDictionaryEnum.customerLevel;
            switch (comptype)
            {
                case 0:
                    customerLevel = DataDictionaryEnum.customerLevel;
                    break;
                case 1:
                    customerLevel = DataDictionaryEnum.supplierLevel;
                    break;
                case 2:
                    customerLevel = DataDictionaryEnum.otherLevel;
                    break;

            }

                return DataDicUtility.GetDicValueToRedis(LevelId, customerLevel);
            }

           
        }
       
        

        public LayPageInfo<CompanyViewDTO> GetList<s>(PageInfo<Company> pageInfo, Expression<Func<Company, bool>> whereLambda,
            Expression<Func<Company, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _CompanySet//.Include(a=>a.CreateUser)//.AsTracking()
                .Where<Company>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if(!(pageInfo is NoPageInfo<Company>)) { //分页
            tempquery = tempquery.Skip<Company>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<Company>(pageInfo.PageSize);
            }
             //ILisCountry> listcountry = RedisHelper.StringGetToList<Country>("Nf-CountryListAll");
           // IList<Province> listprovince = RedisHelper.StringGetToList<Province>("Nf-ProvinceListAll");
            //IList<City> listcity = RedisHelper.StringGetToList<City>("Nf-CityListAll");
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId= a.CreateUserId,
                            //CreateUserDisplayName = RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.Id}", "Displ?yName"), //a.CreateUser.DisplyName,
                            Cstate = a.Cstate,
                            Reserve1=a.Reserve1,
                            Reserve2 = a.Reserve2,
                            CompTypeId = a.CompTypeId,//公司类型
                           // CompTypeName= a.CompType.Name,//公司类型
                            FirstContact = a.FirstContact,//首要联系人
                            FirstContactMobile= a.FirstContactMobile,//首要联系人移动电话
                            FirstContactTel=a.FirstContactTel,//首要联系人办公电话
                            CareditId=a.CareditId,//信用等级ID
                            //CareditName= a.Caredit.Name,//信用等级
                            //LevelName =a.Level.Name,//单位级别
                            LevelId=a.LevelId,//单位级别
                            CompClassId = a.CompClassId,//公司类别
                            //CompanyTypeClass = a.CompClass.Name,//公司类别
                            PrincipalUserId= a.PrincipalUserId,
                            //PrincipalUserDisplayName = a.PrincipalUser.DisplyName,//负责人
                            Trade=a.Trade,//行业
                            CountryId=a.CountryId,//国家ID
                            ProvinceId=a.ProvinceId,//省
                            CityId= a.CityId,//城市ID
                            Ctype = a.Ctype,
                            WfState=a.WfState,
                            WfItem=a.WfItem,
                            WfCurrNodeName=a.WfCurrNodeName,
                        };
            var local = from a in query.AsEnumerable()
                        select new CompanyViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplayName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //  RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"), 
                            Cstate = a.Cstate,
                            CstateDic = EmunUtility.GetDesc(typeof(CompStateEnum), a.Cstate),
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            CompTypeId = a.CompTypeId,
                            CompTypeName = GetCompTypeName(a.CompTypeId), //公司类型
                            FirstContact = a.FirstContact,//首要联系人
                            FirstContactMobile = a.FirstContactMobile,//首要联系人移动电话
                            FirstContactTel = a.FirstContactTel,//首要联系人办公电话
                            CareditName = GetCareditName(a.CareditId),
                            LevelName = GetLevelName(a.LevelId, a.Ctype ?? -1),
                            CompanyTypeClass = CompanyUtility.CompanyTypeClass(a.CompClassId, a.Ctype ?? -1),//公司类别
                            CompClassId = a.CompClassId,
                            //  PrincipalUserDisplayName = (a.PrincipalUserId??0)==0?"": RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName").ToString(),//a.PrincipalUserDisplayName,//负责人
                            PrincipalUserDisplayName = (a.PrincipalUserId ?? 0) == 0 ? "" : this.Db.GetRedisUserFieldValue(a.CreateUserId).ToString(),//a.PrincipalUserDisplayName,//负责人
                            Trade = a.Trade,
                            CountryName = RedisHelper.HashGet($"{StaticData.RedisCountryKey}:{a.CountryId}", "Name"), //listcountry.Any(p=>p.Id== a.CountryId)? listcountry.FirstOrDefault(p => p.Id == a.CountryId).Name:"",
                            ProvinceName = RedisHelper.HashGet($"{StaticData.RedisProvinceKey}:{a.ProvinceId}", "Name"),  //listprovince.Any(p => p.Id == a.ProvinceId) ? listprovince.FirstOrDefault(p => p.Id == a.ProvinceId).Name : "",
                            CityName = RedisHelper.HashGet($"{StaticData.RedisCityKey}:{a.CityId}", "Name"),  //listcity.Any(p => p.Id == a.CityId) ? listcity.FirstOrDefault(p => p.Id == a.CityId).Name : "",
                            UserDeptId = Convert.ToInt32(this.Db.GetRedisUserFieldValue(a.CreateUserId, "DepartmentId")) ,
                            
                           // RedisValueUtility. GetRedisUserDeptId(a.CreateUserId),
                            WfState = a.WfState,
                            WfCurrNodeName=a.WfCurrNodeName,
                            WfItemDic= FlowUtility.GetMessionDic(a.WfItem ?? -1,0),
                            WfStateDic= EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState??-1),

                        };
            return new LayPageInfo<CompanyViewDTO>()
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
            string sqlstr = "update Company set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }

        public int GetIsHt(string Ids)
        {
            
            var listIds = StringHelper.String2ArrayInt(Ids);
            var ht = Db.Set<ContractInfo>().Where(a => listIds.Contains(a.CompId ?? 0)&&a.IsDelete==0).ToList();
            var jg = ht.Count();
            return jg;
        }

        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public CompanyViewDTO ShowView(int Id)
        {
            var query = from a in _CompanySet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId=a.CreateUserId,
                            Cstate = a.Cstate,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            FirstContact = a.FirstContact,//首要联系人
                            FirstContactMobile = a.FirstContactMobile,//首要联系人移动电话
                            FirstContactTel = a.FirstContactTel,//首要联系人办公电话
                            FirstContactPosition = a.FirstContactPosition,//职位
                            CareditId = a.CareditId,
                            LevelId = a.LevelId,
                            CompClassId = a.CompClassId,
                            CompTypeId = a.CompTypeId,
                            PrincipalUserId = a.PrincipalUserId,
                            Trade = a.Trade,//行业
                            CountryId = a.CountryId,
                            ProvinceId = a.ProvinceId,
                            CityId = a.CityId,
                            Address = a.Address,//地址
                            PostCode = a.PostCode,//邮编
                            Tel = a.Tel,//电话
                            Fax = a.Fax,//传真
                            RegisterCapital = a.RegisterCapital,
                            RegisterAddress = a.RegisterAddress,
                            FoundDateTime = a.FoundDateTime,
                            ExpirationDateTime = a.ExpirationDateTime,
                            InvoiceTitle = a.InvoiceTitle,//抬头
                            TaxIdentification = a.TaxIdentification,//纳税人识别号
                            InvoiceAddress = a.InvoiceAddress,//发票地址
                            InvoiceTel = a.InvoiceTel,//发票电话
                            BankName = a.BankName,//银行
                            BankAccount = a.BankAccount,//账号
                            PaidUpCapital = a.PaidUpCapital,
                            LegalPerson = a.LegalPerson,//法人
                            WebSite = a.WebSite,//网址
                            FirstContactQq = a.FirstContactQq,//首要联系人
                            FirstContactEmail = a.FirstContactEmail,//Email
                            BusinessScope = a.BusinessScope,//业务范围
                            Ctype = a.Ctype,//标识是不是客户
                            WfState=a.WfState,//流程状态
                        };
            var local = from tempInfo in query.AsEnumerable()
                       
                            select new CompanyViewDTO
                            {
                                Id = tempInfo.Id,
                                Name = tempInfo.Name,
                                Code = tempInfo.Code,
                                CreateDateTime = tempInfo.CreateDateTime,
                                CreateUserDisplayName = this.Db.GetRedisUserFieldValue(tempInfo.CreateUserId),
                                //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{tempInfo.CreateUserId}", "DisplyName"),//tempInfo.CreateUser.DisplyName,
                                Cstate = tempInfo.Cstate,
                                Reserve1 = tempInfo.Reserve1,
                                Reserve2 = tempInfo.Reserve2,
                                CompTypeName = GetCompTypeName(tempInfo.CompTypeId),//tempInfo.CompType.Name,//公司类型
                                FirstContact = tempInfo.FirstContact,//首要联系人
                                FirstContactMobile = tempInfo.FirstContactMobile,//首要联系人移动电话
                                FirstContactTel = tempInfo.FirstContactTel,//首要联系人办公电话
                                FirstContactPosition = tempInfo.FirstContactPosition,//职位
                                CareditName = GetCareditName(tempInfo.CareditId),//tempInfo.Caredit.Name,//信用等级
                                CareditId = tempInfo.CareditId,
                                LevelName = GetLevelName(tempInfo.LevelId,tempInfo.Ctype??-1), //tempInfo.Level.Name,//单位级别
                                LevelId = tempInfo.LevelId,
                                CompanyTypeClass = CompanyUtility.CompanyTypeClass(tempInfo.CompClassId,tempInfo.Ctype??-1), //tempInfo.CompClass.Name,//公司类别
                                CompClassId = tempInfo.CompClassId,
                                CompTypeId = tempInfo.CompTypeId,
                                PrincipalUserDisplayName = (tempInfo.PrincipalUserId ?? 0) == 0 ? "" : RedisHelper.HashGet($"{StaticData.RedisUserKey}:{tempInfo.PrincipalUserId}", "DisplyName").ToString(),//负责人
                                PrincipalUserId = tempInfo.PrincipalUserId,
                                Trade = tempInfo.Trade,//行业
                                CountryId = tempInfo.CountryId,
                                ProvinceId = tempInfo.ProvinceId,
                                CityId = tempInfo.CityId,
                                Address = tempInfo.Address,//地址
                                PostCode = tempInfo.PostCode,//邮编
                                Tel = tempInfo.Tel,//电话
                                Fax = tempInfo.Fax,//传真
                                RegisterCapital =string.IsNullOrEmpty(tempInfo.RegisterCapital)?"0.00": tempInfo.RegisterCapital,
                                RegisterAddress = tempInfo.RegisterAddress,
                                FoundDateTime = tempInfo.FoundDateTime,
                                ExpirationDateTime = tempInfo.ExpirationDateTime,
                                InvoiceTitle = tempInfo.InvoiceTitle,//抬头
                                TaxIdentification = tempInfo.TaxIdentification,//纳税人识别号
                                InvoiceAddress = tempInfo.InvoiceAddress,//发票地址
                                InvoiceTel = tempInfo.InvoiceTel,//发票电话
                                BankName = tempInfo.BankName,//银行
                                BankAccount = tempInfo.BankAccount,//账号
                                PaidUpCapital =string.IsNullOrEmpty(tempInfo.PaidUpCapital)?"0.00": tempInfo.PaidUpCapital,
                                LegalPerson = tempInfo.LegalPerson,//法人
                                WebSite = tempInfo.WebSite,//网址
                                FirstContactQq = tempInfo.FirstContactQq,//首要联系人
                                FirstContactEmail = tempInfo.FirstContactEmail,//Email
                                BusinessScope = tempInfo.BusinessScope,//业务范围
                                Ctype = tempInfo.Ctype,//标识是不是客户
                                CountryName = RedisHelper.HashGet($"{StaticData.RedisCountryKey}:{tempInfo.CountryId}", "Name"),
                                ProvinceName = RedisHelper.HashGet($"{StaticData.RedisProvinceKey}:{tempInfo.ProvinceId}", "Name"),
                                CityName = RedisHelper.HashGet($"{StaticData.RedisCityKey}:{tempInfo.CityId}", "Name"),
                                WfState = tempInfo.WfState??0,//流程状态
                            };
                return local.FirstOrDefault();
            
            

        }
        
       
        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="currUserId">当前用户ID</param>
        public int  UpdateItems(int Id,int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"update CompContact set CompanyId={Id} where CompanyId={-currUserId};");
            strsql.Append($"update CompDescription set CompanyId={Id} where CompanyId={-currUserId};");
            strsql.Append($"update CompAttachment set CompanyId={Id} where CompanyId={-currUserId};");
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
            strsql.Append($"delete CompContact  where CompanyId={-currUserId};");
            strsql.Append($"delete CompDescription  where CompanyId={-currUserId};");
            strsql.Append($"delete CompAttachment  where CompanyId={-currUserId};");
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

                case "InvoiceTitle"://发票抬头
                case "TaxIdentification":
                case "InvoiceTel":
                case "InvoiceAddress":
                case "BankName":
                case "BankAccount":
                case "Address":
                case "Trade":
                case "PostCode":
                case "WebSite":
                case "FirstContact":
                case "FirstContactTel":
                case "FirstContactPosition":
                case "FirstContactMobile":
                case "FirstContactEmail":
                case "Fax":
                case "FirstContactQq":
                case "Reserve1":
                case "Reserve2":
                    sqlstr = $"update  Company set {info.FieldName}='{info.FieldValue}' where Id={info.Id}";
                    break;
                case "PrincipalUserDisplayName"://负责人
                    sqlstr = $"update  Company set PrincipalUserId={info.FieldValue} where Id={info.Id}";
                    break;
                case "Cstate"://状态
                    var state = Convert.ToByte(info.FieldValue);
                    sqlstr = $"update  Company set Cstate={state} where Id={info.Id}";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }
        /// <summary>
        /// 获取资金统计
        /// </summary>
        /// <param name="compId">合同对方ID</param>

        /// <returns></returns>
        public FundCalcul GetFundStatistics(int compId)
        {
            FundCalcul fund = new FundCalcul();
            //合同金额
            var htje = this.Db.Set<ContractInfo>().Where(a => a.CompId == compId&&(
            a.ContState==(int)ContractState.Execution
            || a.ContState==(int)ContractState.Terminated
            || a.ContState == (int)ContractState.Completed)).Sum(a => (decimal?)a.AmountMoney ?? 0);
            
            //已收/付金额
            var sfkje = this.Db.Set<ContActualFinance>().Include(a => a.Cont)
                .Where(a => a.Cont != null &&a.Cont.CompId== compId&&(a.Cont.ContState == (int)ContractState.Execution
            || a.Cont.ContState == (int)ContractState.Terminated
            || a.Cont.ContState == (int)ContractState.Completed)
            && a.Astate == (int)ActFinanceStateEnum.Confirmed).Sum(a => (decimal?)a.AmountMoney ?? 0);
            //未收付款金额
            var wsfkje = htje - sfkje;
            //已开票金额
            var yskfpje = this.Db.Set<ContInvoice>().Include(a => a.Cont)
               .Where(a => a.Cont != null && a.Cont.CompId == compId && (a.Cont.ContState == (int)ContractState.Execution
           || a.Cont.ContState == (int)ContractState.Terminated
           || a.Cont.ContState == (int)ContractState.Completed)
           && (a.InState == (int)InvoiceStateEnum.Invoicing|| a.InState == (int)InvoiceStateEnum.ReceiptInvoice)).Sum(a => (decimal?)a.AmountMoney ?? 0);
            //未开票金额
            var wyskfpje = htje - yskfpje;
            //财务应收/付==>已开票金额—已收金额
            var cwysf = yskfpje - sfkje;
            //财务预收/付==>已收金额—已开票金额
            var cwyjsf = sfkje - yskfpje;

            fund.HtJeThod = htje.ThousandsSeparator();
            fund.CompAtmThod = sfkje.ThousandsSeparator();
            fund.NoCompAtmThod = wsfkje.ThousandsSeparator();
            fund.CompInThod = yskfpje.ThousandsSeparator();
            fund.NoCompInThod = wyskfpje.ThousandsSeparator();
            fund.CaiYsThod = (cwysf<=0?0:cwysf).ThousandsSeparator();
            fund.CaiYjThod =( cwyjsf<=0?0: cwyjsf).ThousandsSeparator();

            return fund;

        }

        /// <summary>
        /// 查询项目列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<ProjectManagerViewDTO> GetProjList<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
             Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.Db.Set<ProjectManager>().AsTracking().Where<ProjectManager>(whereLambda.Compile()).AsQueryable();
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
                            CreateUserId = a.CreateUserId,
                            PrincipalUserId = a.PrincipalUserId,
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
                            PriUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId??0),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                            //ProjTypeName = RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),//项目类别//a.ProjTypeName,//项目类别
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
                            UserDeptId = Convert.ToInt32(this.Db.GetRedisUserFieldValue(a.CreateUserId, "DepartmentId")),
                           // RedisValueUtility.GetRedisUserDeptId(a.CreateUserId),//创建人部门
                            CategoryId = a.CategoryId,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                            ProjectSource = a.ProjectSource,
                            ProjectSourceName = this.Db.GetRedisDataDictionaryValue(a.ProjectSource??0, DataDictionaryEnum.ProjectSource),
                           // DataDicUtility.GetDicValueToRedis(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource)
                        };
            return new LayPageInfo<ProjectManagerViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        //相关标的
        /// <summary>
        /// 标的大列表查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContSubjectMatterListDTO> GetSubmitList<s>(PageInfo<ContSubjectMatter> pageInfo, Expression<Func<ContSubjectMatter, bool>> whereLambda, Expression<Func<ContSubjectMatter, s>> orderbyLambda, bool isAsc)
        {
            IList<BusinessCategoryDTO> listcates = RedisHelper.StringGetToList<BusinessCategoryDTO>("NF-BcCateGoryListAll");
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContSubjectMatter>()
                .Include(a => a.Cont)//.ThenInclude(a=>a.Comp)
                .Include(a => a.BcInstance)
                // .Include(a => a.Cont).ThenInclude(a => a.CreateUser)
                .AsTracking().Where<ContSubjectMatter>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContSubjectMatter>))
            {
                tempquery = tempquery.Skip<ContSubjectMatter>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContSubjectMatter>(pageInfo.PageSize);
            }
            var query = from a in tempquery.AsQueryable()
                            //join b in Db.Set<BcInstance>()
                            //on a.BcInstanceId equals b.Id into g
                            //from c in g.DefaultIfEmpty()
                        select new
                        {
                            Id = a.Id,
                            ContId = a.ContId,
                            Name = a.Name,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Unit = a.Unit,
                            Amount = a.Amount,
                            Price = a.Price,
                            Remark = a.Remark,
                            DiscountRate = a.DiscountRate,
                            SubTotalRate = a.SubTotalRate,
                            SubTotal = a.SubTotal,
                            SalePrice = a.SalePrice,
                            AmountMoney = a.AmountMoney,
                            PlanDateTime = a.PlanDateTime,
                            Lb = a.BcInstance == null ? -1 : a.BcInstance.LbId,//c==null?-1:c.LbId,
                            BcName=a.BcInstance==null?"": a.BcInstance.Name,//单品名称
                            BcCode = a.BcInstance == null ? "" : a.BcInstance.Code,//单品编号
                            ContName = a.Cont == null ? "" : a.Cont.Name,
                            ContNo = a.Cont == null ? "" : a.Cont.Code,
                            CompName = (a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "",//合同对方
                            ComplateAmount = a.ComplateAmount,
                            SjJfRq = a.SjJfRq,
                            ContState = a.Cont == null ? -1 : a.Cont.ContState,
                            IsFromCategory = a.IsFromCategory,//标的类型
                            SubState = a.SubState


                        };

            var list = query.ToList();
            var local = from a in query.AsEnumerable()
                        select new ContSubjectMatterListDTO
                        {
                            Id = a.Id,
                            ContId = a.ContId,
                            ContName = a.ContName,
                            ContNo = a.ContNo,
                            SubName = a.Name,//名称
                            Unit = a.Unit,//单位
                            PriceThod = a.Price.ThousandsSeparator(),
                            Amountstr = a.Amount.ThousandsSeparator(),//数量
                            TotalThod = ((a.Price ?? 0) * (a.Amount ?? 0)).ThousandsSeparator(),//小计
                            SalePriceThod = a.SalePrice.ThousandsSeparator(),//报价
                            Zkl = ((a.SalePrice ?? 0) / (a.Price ?? 0)).ConvertToPercent(),//折扣率
                            Remark = a.Remark,//备注
                            CompName = a.CompName,//合同对方
                            CreateDateTime = a.CreateDateTime,//创建时间
                            PlanDateTime = a.PlanDateTime,//计划交付时间
                            ComplateAmount = a.ComplateAmount ?? 0,//已交付数量
                            NotDelNum = ((a.Amount ?? 0) - (a.ComplateAmount ?? 0)),//未交付数量
                            JfBl = GetJfBl(a.ComplateAmount ?? 0, a.Amount ?? 0),
                            SjJfRq = a.SjJfRq,//实际交付日期
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),//建立人
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),//合同状态
                            ContState = a.ContState,//合同状态
                            CatePath = GetBcLb(a.IsFromCategory, a.Lb, listcates),//所属类别
                            SubState = a.SubState ?? 0,//交付状态
                            BcName = a.BcName,//单品名称
                            BcCode = a.BcCode,//单品编号
                        };
            return new LayPageInfo<ContSubjectMatterListDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        /// <summary>
        /// 类别
        /// </summary>
        /// <param name="IsFromCategory">
        /// 标的类型（是否关联品类等存值）
        ///是否为品类标的
        ///0：普通标的，非业务类(NULL也是)
        ///1：品类关联标的，业务类
        ///2：导入的Excel
        //</param>
        /// <param name="lb"></param>
        /// <param name="listcates">品类类别</param>
        /// <returns></returns>
        private string GetBcLb(int? IsFromCategory, int? lb, IList<BusinessCategoryDTO> listcates)
        {
            if ((IsFromCategory ?? -1) <= 0)
            {
                return "非业务品类";
            }
            else
            {
                return BcCateUtility.GetCatePath((lb ?? -1), listcates);
            }

        }
        /// <summary>
        /// 交付比例
        /// </summary>
        /// <param name="DelNum">交付数量</param>
        /// <param name="Amount">数量</param>
        /// <returns></returns>
        private string GetJfBl(decimal DelNum, decimal Amount)
        {
            if (DelNum <= 0 || Amount <= 0)
            {
                return DelNum.ConvertToPercent();
            }
            else
            {
                return (DelNum / Amount).ConvertToPercent();
            }

        }


    }
}
