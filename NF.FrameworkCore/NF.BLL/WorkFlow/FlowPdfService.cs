using Microsoft.EntityFrameworkCore;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NF.Common.Utility;
using NF.BLL.Common;
using NF.ViewModel.Extend.Enums;
using NF.Common.Extend;
using NF.ViewModel.Models.Utility;
using System.IO;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 审批单
    /// </summary>
    public partial  class FlowPdfService:BaseService<AppInst>, IFlowPdfService
    {
        #region 基础

        private DbSet<AppInst> _AppInstSet = null;
        public FlowPdfService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstSet = base.Db.Set<AppInst>();
        }
        public FlowPdfService() { }

        #endregion

        #region 合同对方
        /// <summary>
        /// 合同对方
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>合同对方审批单对象</returns>
        public CompanyInfo GetCompFlowPdfData(AppInst appInst)
        {
            CompanyInfo companyInfo = GetCompanyInfo(appInst.AppObjId);
            companyInfo.DicWfData = GetWfOptions(appInst);
            return companyInfo;

        }
        /// <summary>
        /// 获取合同对方信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>合同对方相关信息</returns>
        private CompanyInfo GetCompanyInfo(int compId)
        {
            var query = from a in Db.Set<Company>().AsNoTracking()
                       where a.Id == compId
                       select new
                       {
                          Id= a.Id,
                          Name=a.Name,
                          Code=a.Code,
                          PrincipalUserId=a.PrincipalUserId,
                          CreateDateTime=a.CreateDateTime,
                          CreateUserId=a.CreateUserId,
                          Trade=a.Trade,//行业
                          CompClassId=a.CompClassId,//类别
                           Ctype=a.Ctype,

                       };
            var local = from a in query.AsEnumerable()
                        select new CompanyInfo
                        {
                            Id=a.Id,
                            Name=a.Name,
                            Code=a.Code,
                            CateName= CompanyUtility.CompanyTypeClass(a.CompClassId, a.Ctype ?? -1),
                            PrincipalUser= this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                            Trade=a.Trade,
                            CreateDate=a.CreateDateTime.ToString("yyyy-MM-dd"),
                            CreateUserName= this.Db.GetRedisUserFieldValue(a.CreateUserId),

                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName")

                        };
            return local.FirstOrDefault();

        }





        #endregion

        #region 项目
        /// <summary>
        /// 项目审批单信息
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>项目审批单对象</returns>
        public ProjectInfo GetProjectFlowPdfData(AppInst appInst)
        {
            ProjectInfo companyInfo = GetProjectInfo(appInst.AppObjId);
            companyInfo.DicWfData = GetWfOptions(appInst);
            return companyInfo;

        }
        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>项目相关信息</returns>
        private ProjectInfo GetProjectInfo(int compId)
        {
            var query = from a in Db.Set<ProjectManager>().AsNoTracking()
                        where a.Id == compId
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            PrincipalUserId = a.PrincipalUserId,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            CategoryId = a.CategoryId,//类别
                            jhsk=a.BugetCollectAmountMoney,//计划收款
                            jhfk=a.BudgetPayAmountMoney,//计划付款

                        };
            var local = from a in query.AsEnumerable()
                        select new ProjectInfo
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CateName = this.Db.GetRedisDataDictionaryValue(a.CategoryId, DataDictionaryEnum.projectType),
                            //RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),
                            PrincipalUser = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                            CreateDate = a.CreateDateTime.ToString("yyyy-MM-dd"),
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            jhskThod= a.jhsk.ThousandsSeparator(),
                            jhfkThod = a.jhfk.ThousandsSeparator(),
                        };
            return local.FirstOrDefault();

        }


        #endregion

        #region 合同

        /// <summary>
        /// 合同审批单信息
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>合同审批单对象</returns>
        public ContractPdfInfo GetContractFlowPdfData(AppInst appInst)
        {
            ContractPdfInfo contractInfo = GetContractInfo(appInst.AppObjId);
            contractInfo.DicWfData = GetWfOptions(appInst);
            return contractInfo;

        }

        /// <summary>
        /// 合同信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>合同相关信息</returns>
        private ContractPdfInfo GetContractInfo(int contId)
        {
            var query = from a in Db.Set<ContractInfo>().AsNoTracking()
                        where a.Id == contId
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            PrincipalUserId = a.PrincipalUserId,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            ContTypeId = a.ContTypeId,//类别
                            AmountMoney = a.AmountMoney,//合同金额
                            CompanyName = a.Comp.Name,//合同对方


                        };
            var local = from a in query.AsEnumerable()
                        select new ContractPdfInfo
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CateName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),

                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),
                            PrincipalUser = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),

                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName"),
                            CreateDate = a.CreateDateTime.ToString("yyyy-MM-dd"),
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            CompanyName = a.CompanyName,
                        };
            return local.FirstOrDefault();

        }
        #endregion

        #region 付款审批单

        /// <summary>
        /// 付款审批单信息
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>付款审批单对象</returns>
        public PaymentPdfInfo GetPaymentFlowPdfData(AppInst appInst)
        {
            PaymentPdfInfo paymentInfo = GetPaymentInfo(appInst.AppObjId);
            paymentInfo.DicWfData = GetWfOptions(appInst);
            return paymentInfo;

        }
        /// <summary>
        /// 付款单信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>付款单相关信息</returns>
        private PaymentPdfInfo GetPaymentInfo(int payId)
        {
            var query = from a in Db.Set<ContActualFinance>().AsNoTracking()
                        where a.Id == payId
                        select new
                        {
                            Id = a.Id,
                            ContName =a.Cont.Name,
                            ContCode = a.Cont.Code,
                            ContTypeId = a.Cont.ContTypeId,//类别
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            CompanyName = a.Cont.Comp.Name,//合同对方
                            AmountMoney = a.AmountMoney,//付款金额
                            Bank=a.Bank,
                            Account=a.Account,
                            Sett=a.SettlementMethod,
                        };
            var local = from a in query.AsEnumerable()
                        select new PaymentPdfInfo
                        {

                            Id = a.Id,
                            ContName = a.ContName,
                            ContCode = a.ContCode,
                            CateName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),
                            CreateDate = a.CreateDateTime.ToString("yyyy-MM-dd"),
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CompanyName = a.CompanyName,//合同对方
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            Bank = a.Bank,
                            Account = a.Account,
                            SettDic = this.Db.GetRedisDataDictionaryValue(a.Sett ?? 0, DataDictionaryEnum.SettlModes),

                            //DataDicUtility.GetDicValueToRedis(a.Sett, DataDictionaryEnum.SettlModes),//结算方式
                           
                        };
            return local.FirstOrDefault();

        }
        #endregion


        #region 收票

        /// <summary>
        /// 收票审批单信息
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>收票审批单对象</returns>
        public InvoiceInPdfInfo GetInvoiceInFlowPdfData(AppInst appInst)
        {
            InvoiceInPdfInfo invoiceInfo = GetInvoiceInInfo(appInst.AppObjId);
            invoiceInfo.DicWfData = GetWfOptions(appInst);
            return invoiceInfo;

        }

        /// <summary>
        /// 收票审批单信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>收票审批单相关信息</returns>
        private InvoiceInPdfInfo GetInvoiceInInfo(int invoiceId)
        {
            var query = from a in Db.Set<ContInvoice>().AsNoTracking()
                        where a.Id == invoiceId
                        select new
                        {
                            Id = a.Id,
                            ContName = a.Cont.Name,
                            ContCode = a.Cont.Code,
                            ContTypeId = a.Cont.ContTypeId,//类别
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            CompanyName = a.Cont.Comp.Name,//合同对方
                            AmountMoney = a.AmountMoney,//付款金额
                            InType=a.InType,
                            MakeOutDateTime=a.MakeOutDateTime,//开具时间
                            InCode = a.InCode,//发票号
                        };
            var local = from a in query.AsEnumerable()
                        select new InvoiceInPdfInfo
                        {

                            Id = a.Id,
                            ContName = a.ContName,
                            ContCode = a.ContCode,
                            CateName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),
                            CreateDate = a.CreateDateTime.ToString("yyyy-MM-dd"),
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CompanyName = a.CompanyName,//合同对方
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            InTypeDic = this.Db.GetRedisDataDictionaryValue(a.InType ?? 0, DataDictionaryEnum.InvoiceType),

                            //DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                            MakeOutDateTime =  a.MakeOutDateTime.ConvertToString(),
                            InCode=a.InCode,

                        };
            return local.FirstOrDefault();

        }
        #endregion 


        #region 开票

        /// <summary>
        /// 收票审批单信息
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>收票审批单对象</returns>
        public InvoiceOutPdfInfo GetInvoiceOutFlowPdfData(AppInst appInst)
        {
            InvoiceOutPdfInfo invoiceInfo = GetInvoiceOutInfo(appInst.AppObjId);
            invoiceInfo.DicWfData = GetWfOptions(appInst);
            return invoiceInfo;

        }

        /// <summary>
        /// 收票审批单信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>收票审批单相关信息</returns>
        private InvoiceOutPdfInfo GetInvoiceOutInfo(int invoiceId)
        {
            var query = from a in Db.Set<ContInvoice>().AsNoTracking()
                        where a.Id == invoiceId
                        select new
                        {
                            Id = a.Id,
                            ContName = a.Cont.Name,
                            ContCode = a.Cont.Code,
                            ContTypeId = a.Cont.ContTypeId,//类别
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            CompanyName = a.Cont.Comp.Name,//合同对方
                            AmountMoney = a.AmountMoney,//付款金额
                            InType = a.InType,
                            MakeOutDateTime = a.MakeOutDateTime,//开具时间
                            InCode = a.InCode,//发票号
                            InTitle=a.InTitle,//抬头
                            NaShuiHao=a.TaxpayerIdentification,//纳税人识别号
                            InAddress=a.InAddress,//地址
                            InTel=a.InTel,//电话
                            BankName=a.BankName,//银行
                            BankAccount=a.BankAccount,//账号
                        };
            var local = from a in query.AsEnumerable()
                        select new InvoiceOutPdfInfo
                        {

                            Id = a.Id,
                            ContName = a.ContName,
                            ContCode = a.ContCode,
                            CateName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),
                            CreateDate = a.CreateDateTime.ToString("yyyy-MM-dd"),
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CompanyName = a.CompanyName,//合同对方
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            InTypeDic = this.Db.GetRedisDataDictionaryValue(a.InType ?? 0, DataDictionaryEnum.InvoiceType),

                            //DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                            MakeOutDateTime = a.MakeOutDateTime.ConvertToString(),
                            InCode = a.InCode,
                            InTitle = a.InTitle,//抬头
                            NaShuiHao = a.NaShuiHao,//纳税人识别号
                            InAddress = a.InAddress,//地址
                            InTel = a.InTel,//电话
                            BankName = a.BankName,//银行
                            BankAccount = a.BankAccount,//账号

                        };
            return local.FirstOrDefault();

        }
        #endregion

        #region 询价
        /// <summary>
        /// 询价
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>合同对方审批单对象</returns>
        public InquiryInfo GetInquFlowPdfData(AppInst appInst)
        {
            InquiryInfo Inqu = GetInquiryInfo(appInst.AppObjId);
            Inqu.DicWfData = GetWfOptions(appInst);
            return Inqu;
        }
        /// <summary>
        /// 获取询价信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>合同对方相关信息</returns>
        private InquiryInfo GetInquiryInfo(int compId)
        {
            var query = from a in Db.Set<Inquiry>().AsNoTracking()
                        where a.Id == compId
                        select new
                        {
                            Id = a.Id,
                            Times = a.Times, //项目名称
                            ProjectNumber = a.ProjectNumber,//项目编号
                            CreateUserId = a.CreateUserId, //创建人
                            UsefulLife = a.UsefulLife,//创建时间
                            Sites = a.Sites,//地点
                            Recorder = a.Recorder, //记录人
                            TheWinningConditions = a.TheWinningConditions //中标条件
                        };
            var local = from a in query.AsEnumerable()
                        select new InquiryInfo
                        {
                            Id = a.Id,
                            Times = a.Times, //项目名称
                            ProjectNumber = a.ProjectNumber,//项目编号
                            CreateName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId ?? -2}", "DisplyName"),
                          //  a.CreateUserId, //创建人
                            UsefulLife = a.UsefulLife,//创建时间
                            Sites = a.Sites,//地点
                            RecorderName = this.Db.GetRedisUserFieldValue(a.Recorder ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.Recorder ?? -2}", "DisplyName"),
                            //  a.Recorder, //记录人
                            TheWinningConditions = a.TheWinningConditions //中标条件
                        };
            return local.FirstOrDefault();
        }
        #endregion

        #region 约谈
        /// <summary>
        /// 询价
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>合同对方审批单对象</returns>
        public QuestioningInfo GetQuesFlowPdfData(AppInst appInst)
        {
            QuestioningInfo Inqu = GetQuesInfo(appInst.AppObjId);
            Inqu.DicWfData = GetWfOptions(appInst);
            return Inqu;
        }
        /// <summary>
        /// 获取询价信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>合同对方相关信息</returns>
        private QuestioningInfo GetQuesInfo(int compId)
        {
            var query = from a in Db.Set<Questioning>().AsNoTracking()
                        where a.Id == compId
                        select new
                        {
                            Id = a.Id,
                            Times = a.Times, //项目名称
                            ProjectNumber = a.ProjectNumber,//项目编号
                            CreateUserId = a.CreateUserId, //创建人
                            UsefulLife = a.UsefulLife,//创建时间
                            Sites = a.Sites,//地点
                            Recorder = a.Recorder, //记录人
                            TheWinningConditions = a.TheWinningConditions //中标条件
                        };
            var local = from a in query.AsEnumerable()
                        select new QuestioningInfo
                        {
                            Id = a.Id,
                            Times = a.Times, //项目名称
                            ProjectNumber = a.ProjectNumber,//项目编号
                            CreateName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId ?? -2}", "DisplyName"),
                            //  a.CreateUserId, //创建人
                            UsefulLife = a.UsefulLife,//创建时间
                            Sites = a.Sites,//地点
                            RecorderName = this.Db.GetRedisUserFieldValue(a.Recorder ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.Recorder ?? -2}", "DisplyName"),
                            //  a.Recorder, //记录人
                            TheWinningConditions = a.TheWinningConditions //中标条件
                        };
            return local.FirstOrDefault();
        }
        #endregion
        #region 招标
        /// <summary>
        /// 询价
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>合同对方审批单对象</returns>
        public TeInfo GetTendFlowPdfData(AppInst appInst)
        {
            TeInfo Inqu = GetTeInfo(appInst.AppObjId);
            Inqu.DicWfData = GetWfOptions(appInst);
            return Inqu;
        }
        /// <summary>
        /// 获取询价信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>合同对方相关信息</returns>
        private TeInfo GetTeInfo(int compId)
        {
            var query = from a in Db.Set<TenderInfor>().AsNoTracking()
                        where a.Id == compId
                        select new
                        {
                            Id = a.Id,
                            TenderDate = a.TenderDate, //时间
                            ProjectNo = a.ProjectNo,//项目编号
                            CreateUserId = a.CreateUserId, //创建人
                            TenderExpirationDate = a.TenderExpirationDate,//有效期
                            Iocation = a.Iocation,//地点
                            RecorderId = a.RecorderId, //记录人
                            WinningConditions = a.WinningConditions //中标条件
                        };
            var local = from a in query.AsEnumerable()
                        select new TeInfo
                        {
                            Id = a.Id,
                            TenderDate = a.TenderDate, //时间
                            ProjectNo = a.ProjectNo,//项目编号
                            CreateName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            //  a.CreateUserId, //创建人
                            TenderExpirationDate = a.TenderExpirationDate,//创建时间
                            Iocation = a.Iocation,//地点
                            RecorderName = this.Db.GetRedisUserFieldValue(a.RecorderId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.RecorderId}", "DisplyName"),
                            //  a.Recorder, //记录人
                            WinningConditions = a.WinningConditions //中标条件
                        };
            return local.FirstOrDefault();
        }
        #endregion

        #region 审批意见
        /// <summary>
        /// 审批意见
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>意见信息字典</returns>
        public Dictionary<string, List<WfOption>> GetWfOptions(AppInst appInst)
        {
            var query = from a in Db.Set<AppInstOpin>().AsNoTracking().Where(a => a.InstId == appInst.Id)
                        select new
                        {
                            Id = a.Id,
                            NodeId=a.NodeId,
                            NodeStrId=a.NodeStrId,
                            NodeName =a.Node.Name,
                            CreateDatetime=a.CreateDatetime,
                            CreateUserId=a.CreateUserId,
                            Opinion=a.Opinion

                        };
            var local = from a in query.AsEnumerable()
                        select new WfOption
                        {
                            NodeStrId = a.NodeStrId,
                            NodeId = a.NodeId,
                            NodeName = a.NodeName,
                            AppUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CreateUserId = a.CreateUserId??0,
                            UserEs = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "UserEs"),
                            UserEsTy= this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "UserEsTy"),
                            Option =a.Opinion,
                            AppDate=a.CreateDatetime,
                            ImgSrc = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "UserEs", a.CreateUserId + ".PNG")
                        };

         return local.ToList().GroupBy(a=>a.NodeName).ToDictionary(g=>g.Key,g=>g.ToList());

        }
        #endregion
    }
}
