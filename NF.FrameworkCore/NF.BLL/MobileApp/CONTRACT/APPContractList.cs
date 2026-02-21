using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.IBLL;
using NF.IBLL.MobileApp.CONTRACT;
using NF.Model.Models;
using NF.ViewModel;
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
    public partial class APPContractList : BaseService<ContractInfo>, IAAPPContractList
    {
        private DbSet<ContractInfo> _ContractInfoSet = null;
        public APPContractList(DbContext dbContext)
           : base(dbContext)
        {
            _ContractInfoSet = base.Db.Set<ContractInfo>();
        }
        public APPContractList() { }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public APPLayPageInfo<ContractInfoListViewDTO> GetList<s>(Expression<Func<ContractInfo, bool>> whereLambda,
             Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc,int type, int start, int limit)
        {
           // var tempquery = _ContractInfoSet.AsTracking().Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
            var tempquery = _ContractInfoSet.Include(a => a.Comp).AsTracking().Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            //if (!(pageInfo is NoPageInfo<ContractInfo>))
            //{ //分页
            //    tempquery = tempquery.Skip<ContractInfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContractInfo>(pageInfo.PageSize);
            //}

            var query = from a in tempquery where a.FinanceType==type
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
                            //ProjName = a.Project.Name,
                            AmountMoney = a.AmountMoney,
                            ContState = a.ContState
                        };
            var local = from a in query.AsEnumerable()
                        select new ContractInfoListViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            CompName = a.CompName,//合同对方
                            //AmountMoney = a.AmountMoney??0,//合同金额
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                           // DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别                            CompId = a.CompId,
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            //ContSum = a.ContDivision > 0 ? "是" : "否",
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位

                        };
            int totalCount = local.Count(); //共有记录数
            int pageNum = start / limit;//共有页数 
            var result = local.Skip(limit * pageNum).Take(limit).ToList();
            //var result = query.Skip(limit * pageNum).Take(limit).ToList();
            return new APPLayPageInfo<ContractInfoListViewDTO>()
            {
                totalCount = totalCount,
                items = result,
              
        };

        }
        /// <summary>
        /// 根据合同ID查询合同基本信息
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        public ContractInfoHistoryViewDTO GetIDView(int Id)
        {
            var tempquery = _ContractInfoSet.Include(a => a.Comp).AsTracking().AsQueryable();
            var query = from a in tempquery
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Code=a.Code,
                            Name = a.Name,
                            AmountMoney = a.AmountMoney,
                            OtherCode = a.OtherCode,
                            ContTypeId=a.ContTypeId,
                            CompName = a.Comp.Name,
                            MainDeptId =a.MainDeptId,
                            CreateUserId = a.CreateUserId,
                            DeptId=a.DeptId,
                            ContState = a.ContState,
                            CreateUser=a.CreateUser,
                            PrincipalUserId=a.PrincipalUserId
                        };
            var local = from a in query.AsEnumerable()
                        select new ContractInfoHistoryViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//合同名称
                            Code = a.Code,//合同编号
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),

                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额
                            CompName =a.CompName,//合同对方7
                            MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.MainDeptId ?? -2),//签约主体
                            DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                           // RedisValueUtility.GetDeptName(a.DeptId ?? -2),//经办机构
                            StateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),//状态
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                          //  RedisValueUtility.GetUserShowName(a.CreateUserId),//创建人
                            PrincipalUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? 0),//负责人
                        };
            return local.FirstOrDefault();
        }

        /// <summary>
        /// 根据合同ID查询合同文本
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        public List<APPContTextListViewDTO> AppcontractDetailFile1(int Id)
        {
            var query = from a in Db.Set<ContText>().AsNoTracking()
                        where a.ContId == Id
                        select new
                        {
                            Id = a.Id,
                            Name=a.Name,//名称
                            Path = a.Path,
                            CategoryId = a.CategoryId,//类别
                            Remark =a.Remark,//说明
                        };
            var local = from a in query.AsEnumerable()
                        select new APPContTextListViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Path = a.Path.Replace("~/", ""),
                            CategoryName = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ContTxtType),
                           // DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContTxtType),
                            Remark =a.Remark == null ? "":a.Remark,//说明
                        };  
            return local.ToList();
        }

        /// <summary>
        /// 根据合同ID查询合同文附件
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        public List<APPContFUjianListViewDTO> AppcontractDetailFile2(int Id)
        {
            var query = from a in Db.Set<ContAttachment>().AsNoTracking()
                        where a.ContId == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            CategoryId = a.CategoryId,//类别
                            Remark = a.Remark,//说明
                            Path = a.Path,
                        };
            var local = from a in query.AsEnumerable()
                        select new APPContFUjianListViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            CategoryName = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ContAttachmentType),
                            //DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContAttachmentType),
                            Remark = a.Remark == null ? "" : a.Remark,//说明
                            Path = a.Path.Replace("~/", ""),
                        };
            return local.ToList();
        }

        /// <summary>
        /// APP根据合同ID查询计划资金
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        public List<APPContfINCTListViewDTO> AppcontractDetailAFinance(int Id)
        {
            var query = from a in Db.Set<ContPlanFinance>().AsNoTracking()
                        where a.ContId == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            AmountMoney = a.AmountMoney,
                            PlanCompleteDateTime=a.PlanCompleteDateTime,
                            Remark = a.Remark,//说明
                        };
            var local = from a in query.AsEnumerable()
                        select new APPContfINCTListViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name == null ? "" : a.Name,//名称
                            AmountMoney = a.AmountMoney,//金额
                            PlanCompleteDateTime = a.PlanCompleteDateTime,//完成时间
                            Remark = a.Remark == null ? "" : a.Remark,//说明,//说明
                        };
            return local.ToList();
        }

        /// <summary>
        /// APP根据合同ID查询实际资金
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        public List<APPContSHIJIfINCTListViewDTO> AppfinanceActualList(int Id)
        {
            var query = from a in Db.Set<ContActualFinance>().AsNoTracking()
                        where a.ContId == Id
                        select new
                        {
                            Id = a.Id,
                            AmountMoney = a.AmountMoney,
                            ActualSettlementDate = a.ActualSettlementDate,
                            SettlementMethod =a.SettlementMethod,
                            ConfirmUserId=a.ConfirmUserId,
                        };
            var local = from a in query.AsEnumerable()
                        select new APPContSHIJIfINCTListViewDTO
                        {
                            Id = a.Id,
                            AmountMoney = a.AmountMoney,
                            ActualSettlementDate = a.ActualSettlementDate,
                            SettlementMethod = this.Db.GetRedisDataDictionaryValue(a.SettlementMethod ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementMethod, DataDictionaryEnum.SettlModes),
                            ConfirmUserId = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0),
                        };
            return local.ToList();
        }
        /// <summary>
        ///根据app资金统计
        /// </summary>
        public APPContractStatic GetContractStatic(int ContId)
        {
            var info = Db.Set<ContStatistic>().AsNoTracking().FirstOrDefault(a => a.ContId == ContId);
            APPContractStatic contractStatic = new APPContractStatic();
            if (info != null)
            {
                var continfo = Db.Set<ContractInfo>().AsNoTracking().FirstOrDefault(a => a.Id == ContId);
                contractStatic.ActMoneryThod = info.CompActAm.ThousandsSeparator();
                contractStatic.InvoiceMoneryThod = info.CompInAm.ThousandsSeparator();
                //应收=实际开票-实际资金
                contractStatic.ReceivableThod = ((info.CompInAm ?? 0) - (info.CompActAm ?? 0)).ThousandsSeparator();
                //预收=实际资金-实际开票
                contractStatic.AdvanceThod = ((info.CompActAm ?? 0) - (info.CompInAm ?? 0)).ThousandsSeparator();
                if (continfo != null)
                {
                    contractStatic.ActNoMoneryThod = ((continfo.AmountMoney ?? 0) - (info.CompActAm ?? 0)).ThousandsSeparator();
                    contractStatic.InvoiceNoMoneryThod = ((continfo.AmountMoney ?? 0) - (info.CompInAm ?? 0)).ThousandsSeparator();
                }

            }
            else
            {
                contractStatic.ActMoneryThod = "0.00";
                contractStatic.InvoiceMoneryThod = "0.00";
                //应收=实际开票-实际资金
                contractStatic.ReceivableThod = "0.00";
                //预收=实际资金-实际开票
                contractStatic.AdvanceThod = "0.00";
                contractStatic.ActNoMoneryThod = "0.00";
                contractStatic.InvoiceNoMoneryThod = "0.00";
            }

            return contractStatic;

        }

        /// <summary>
        /// APP根据合同ID查询合同备忘
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        public List<APPContDescription> AppcontractDetailRemark(int Id)
        {
            var query = from a in Db.Set<ContDescription>().AsNoTracking()
                        where a.ContId == Id
                        select new
                        {
                            Id = a.Id,
                            Citem=a.Citem,
                            Ccontent = a.Ccontent,
                            CreateDateTime = a.CreateDateTime,


                        };
            var local = from a in query.AsEnumerable()
                        select new APPContDescription
                        {
                            Id = a.Id,
                            Citem = a.Citem == null ? "" : a.Citem,
                            Ccontent = a.Ccontent==null?"": a.Ccontent,
                            CreateDateTime = a.CreateDateTime,
                        };
            return local.ToList();
        }

        /// <summary>
        /// APP根据合同ID查询审批记录
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        public List<APPAppInst> AppcontractDetailWorkflow(int Id)
        {
            var query = from a in Db.Set<AppInst>().AsNoTracking()
                        where a.ObjType ==3 && a.AppObjId==Id
                        select new
                        {
                            Id = a.Id,
                            Mission = a.Mission,
                            CurrentNodeId = a.CurrentNodeId,
                            CurrentNodeName = a.CurrentNodeName,
                            AppState= a.AppState,
                            CreateDateTime = a.CreateDateTime,
                            CompleteDateTime = a.CompleteDateTime,
                        };
            var local = from a in query.AsEnumerable()
                        select new APPAppInst
                        {
                            Id = a.Id,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, 3),//审批事项
                            CurrentNodeName = a.CurrentNodeName == null ? "" : a.CurrentNodeName,
                            AppStateDic = EmunUtility.GetDesc(typeof(AppInstEnum), a.AppState),
                            CreateDateTime = a.CreateDateTime,
                            CompleteDateTime = a.CompleteDateTime,
                        };
            return local.ToList();
        }
    }
}
