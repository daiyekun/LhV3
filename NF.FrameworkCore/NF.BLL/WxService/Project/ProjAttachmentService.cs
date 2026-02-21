using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
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
using System.Threading.Tasks;

namespace NF.BLL
{
    public partial class ProjectManagerService
    {


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<ProjectManagerViewDTO> GetWxProjectList<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda,
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
                            UserDeptId =Convert.ToInt32( this.Db.GetRedisUserFieldValue(a.CreateUserId, "DepartmentId")),
                            //RedisValueUtility.GetRedisUserDeptId(a.CreateUserId),//创建人部门
                            CategoryId = a.CategoryId,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItem = a.WfItem ?? -2,
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
        /// 查看微信信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ProjectViewWxModel ShowWxViewMode(int Id)
        {
            var query = from a in _ProjectManagerSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CategoryId = a.CategoryId,//类别ID
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
                            PrincipalUserId = a.PrincipalUserId,
                            BudgetCollectCurrencyId = a.BudgetCollectCurrencyId,//收款币种
                            BudgetPayCurrencyId = a.BudgetPayCurrencyId,//付款币种
                            WfState = a.WfState,//流程状态
                            WfCurrNodeName = a.WfCurrNodeName,//当前节点
                            WfItem = a.WfItem,//审批事项
                            ProjectSource = a.ProjectSource

                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectViewWxModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CategoryId = a.CategoryId,//项目类别id
                            XmJlRq = a.CreateDateTime,//建立时间
                            //CreateUserName = a.CreateUserName,
                            //PriUserName = a.PriUserName,//负责人
                            XmJlrName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),//建立人
                            XmFzrName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),//负责人
                            PrincipalUserId = a.PrincipalUserId,//赋值人ID
                            ProjTypeName = this.Db.GetRedisDataDictionaryValue(a.CategoryId, DataDictionaryEnum.projectType),
                            //RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),//项目类别
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            XmJhKsSj = a.PlanBeginDateTime,//计划开始时间
                            XmJhWcsSj = a.PlanCompleteDateTime,//计划完成时间
                            XmSjKsSj = a.ActualBeginDateTime,//时间开始时间
                            XmSjWcSj = a.ActualCompleteDateTime,//实际完成时间
                            BugetCollectAmountMoney = a.BugetCollectAmountMoney,//计划收款 项目预算收款
                            BudgetPayAmountMoney = a.BudgetPayAmountMoney,//计划付款 项目预算付款
                            Pstate = a.Pstate,
                            XmState = EmunUtility.GetDesc(typeof(ProjStateEnum), a.Pstate ?? 0),//项目状态
                            XmYsSk = a.BugetCollectAmountMoney.ThousandsSeparator(),//预算收款 千分位
                            XmYsFk = a.BudgetPayAmountMoney.ThousandsSeparator(),//计划付款千分位
                            XmSkBz = a.BudgetCollectCurrencyId,//收款币种
                            XmFkBz = a.BudgetPayCurrencyId,//付款币种
                            XmSkBzName = RedisHelper.HashGet($"{StaticData.RedisCurrencyKey}:{a.BudgetCollectCurrencyId}", "ShortName"),//a.BudgetCollectCurrencyName,//币种名称
                            XmFkBzName = RedisHelper.HashGet($"{StaticData.RedisCurrencyKey}:{a.BudgetPayCurrencyId}", "ShortName"),//a.BudgetPayCurrencyName,//付款币种
                            WfState = a.WfState ?? 0,
                            ProjectSource = a.ProjectSource,
                            WfItem = a.WfItem ?? -2,//审批事项
                            ProjectSourceName = this.Db.GetRedisDataDictionaryValue(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource),
                            //DataDicUtility.GetDicValueToRedis(a.ProjectSource ?? 0, DataDictionaryEnum.ProjectSource)
                        };

            var teminfo = local.FirstOrDefault();
            return teminfo;





        }



        /// <summary>
        /// 获取资金统计
        /// </summary>
        /// <param name="项目ID">合同对方ID</param>

        /// <returns></returns>
        public WxXmZjTj WxGetFundStatistics(int projId)
        {
            WxXmZjTj fund = new WxXmZjTj();
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
        public LayPageInfo<WxXmXgSk> GetXmXgSk<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.Db.Set<ContractInfo>().Include(a => a.Comp).AsTracking().Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
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
                            CompId = a.CompId,
                            CompName = a.Comp == null ? "" : a.Comp.Name,
                            CurrencyId = a.CurrencyId,
                            FinanceType = a.FinanceType,


                        };
            var local = from a in query.AsEnumerable()
                        select new WxXmXgSk
                        {
                            HtSId = a.Id,
                            HtSName = a.Name,//名称
                            HtSCode = a.Code,//编号
                            HtSContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            HtSContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            HtSContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            HtSCompId = a.CompId ?? 0,
                            HtSCompName = a.CompName,
                            HtSCurrName = RedisValueUtility.GetCurrencyName(a.CurrencyId, fileName: "Name"),//币种
                            HtSFinceTypeName = EmunUtility.GetDesc(typeof(FinceType), a.FinanceType)//合同性质



                        };
            return new LayPageInfo<WxXmXgSk>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

    }
}
