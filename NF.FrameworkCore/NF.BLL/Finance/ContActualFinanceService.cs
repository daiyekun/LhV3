using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.Common.Extend;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Models.Finance.Enums;
using NF.ViewModel.Extend.Enums;
using Microsoft.EntityFrameworkCore;
using NF.ViewModel;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 实际资金
    /// </summary>
    public partial class ContActualFinanceService
    {
        /// <summary>
        /// 查询实际资金大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContActualFinanceListViewDTO> GetMainList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda, Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContActualFinance>().Include(a => a.Cont).ThenInclude(a => a.Comp)
                .AsTracking().Where(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContActualFinance>))
                tempquery = tempquery.Skip<ContActualFinance>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContActualFinance>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,

                            ContId = a.ContId,
                            ContName = a.Cont == null ? "" : a.Cont.Name,
                            ContCode = a.Cont == null ? "" : a.Cont.Code,
                            CompName = (a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "",
                            CompClassId = (a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.CompClassId : -1,
                            CompId = a.Cont == null ? -1 : a.Cont.CompId,
                            DeptId = a.Cont == null ? -1 : a.Cont.DeptId,
                            ContTypeId = a.Cont == null ? -1 : a.Cont.ContTypeId,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            VoucherNo = a.VoucherNo,
                            AmountMoney = a.AmountMoney,
                            Astate = a.Astate,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            Remark = a.Remark,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            SettlementMethod = a.SettlementMethod,
                            FinceType = a.FinceType,
                            ActualSettlementDate = a.ActualSettlementDate,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItem = a.WfItem


                        };
            var local = from a in query.AsEnumerable()
                        select new ContActualFinanceListViewDTO
                        {

                            Id = a.Id,
                            ContId = a.ContId,
                            ContName = a.ContName,
                            ContCode = a.ContCode,
                            CompName = a.CompName,
                            CompId = a.CompId,
                            DeptId = a.DeptId,
                            DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.DeptId ?? -2),//经办机构a.DeptId,
                            ContCategoryName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别 
                            ContTypeId = a.ContTypeId,
                            CompCategoryName = this.Db.GetRedisDataDictionaryValue(a.CompClassId ?? 0, (a.FinceType == 0 ? DataDictionaryEnum.customerType : DataDictionaryEnum.suppliersType)),
                           // DataDicUtility.GetDicValueToRedis(a.CompClassId, (a.FinceType == 0 ? DataDictionaryEnum.customerType : DataDictionaryEnum.suppliersType)),//对方类别 
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            VoucherNo = a.VoucherNo,
                            AmountMoney = a.AmountMoney,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            Astate = a.Astate,
                            AstateDic = EmunUtility.GetDesc(typeof(ActFinanceStateEnum), a.Astate ?? -1),
                            ConfirmUserId = a.ConfirmUserId,
                            SettlementMethodDic = this.Db.GetRedisDataDictionaryValue(a.SettlementMethod ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementMethod, DataDictionaryEnum.SettlModes),//结算方式 
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                          //  RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                            ConfirmDateTime = a.ConfirmDateTime,
                            Remark = a.Remark,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            ActualSettlementDate = a.ActualSettlementDate,
                            WfState = a.WfState ?? 0,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                        };
            return new LayPageInfo<ContActualFinanceListViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }



        public int Sjzj(decimal je, int id)
        {
            //var result =1 ;
            //合同金额
            var contInfo = Db.Set<ContractInfo>().Where(a => a.Id == id && a.IsDelete == 0).FirstOrDefault();
            if (contInfo != null && contInfo.IsFramework == (int)ContractProperty.Standard)
            {
                var Zjejh = contInfo == null ? 0 : contInfo.AmountMoney;
                //实际资金总金额
                var Zje = Db.Set<ContActualFinance>().Where(a => a.ContId == id && a.IsDelete == 0).Sum(a => a.AmountMoney);
                //实际资金总金额+本次新加的实际资金
                var zjeSj = Zje + je;

                if (zjeSj <= Zjejh)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 1;
            }


        }
        public int UpdSjzj(decimal je, int id, int Zid, int usid)
        {
            //var a = 1;
            //合同金额
            var contInfo = Db.Set<ContractInfo>().Where(a => a.Id == id && a.IsDelete == 0).FirstOrDefault();
            if (contInfo != null && contInfo.IsFramework == (int)ContractProperty.Standard)
            {
                var Zjejh = contInfo == null ? 0 : contInfo.AmountMoney;
                //实际资金总金额
                var Zje = Db.Set<ContActualFinance>().Where(a => a.ContId == id && a.Id != Zid && a.IsDelete == 0).Sum(a => a.AmountMoney);
                //要修改的金额
                // var Xje = Db.Set<ContActualFinance>().Where(a => a.Id==Zid && a.IsDelete == 0).FirstOrDefault().AmountMoney;
                //实际资金总金额-修改前的金额+修改后的金
                var zjeSj = Zje + je;
                if (zjeSj <= Zjejh)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 1;
            }
        }


        /// <summary>
        /// 核销明细
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContActualFinanceChkListViewDTO> GetChkList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda, Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            AmountMoney = a.AmountMoney,
                            ContId = a.ContId,
                            Astate = a.Astate,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            ActualSettlementDate = a.ActualSettlementDate,
                            SettlementMethod = a.SettlementMethod



                        };
            var local = from a in query.AsEnumerable()
                        select new ContActualFinanceChkListViewDTO
                        {

                            Id = a.Id,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            AstateDic = EmunUtility.GetDesc(typeof(ActFinanceStateEnum), a.Astate ?? -1),
                            SettlementMethodDic = this.Db.GetRedisDataDictionaryValue(a.SettlementMethod ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementMethod, DataDictionaryEnum.SettlModes),
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                            ConfirmDateTime = a.ConfirmDateTime,
                            ActualSettlementDate = a.ActualSettlementDate,
                            WfStateDic = "",
                            AmountMoney = a.AmountMoney,

                        };
            return new LayPageInfo<ContActualFinanceChkListViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }

        #region 保存实际资金

        /// <summary>
        /// 保存实际资金
        /// </summary>
        /// <param name="contActual"></param>
        /// <param name="CheckType">核销类型0：计划资金，1：发票</param>
        /// <param name="chkData">核销数据</param>
        /// <returns></returns>
        public ContActualFinance AddSave(ContActualFinance contActual, int CheckType, IList<CheckData> chkData)
        {
            Add(contActual);
            if (CheckType == 0)
            {//计划资金核销
                CheckPlanFinance(contActual, chkData);
            }
            else
            {//发票核销
                CheckInvoice(contActual, chkData);
            }
            // IsFrameUpdateHt(contActual);
            //修改附件

            return contActual;


        }
        /// <summary>
        /// 框架合同修改合同金额
        /// </summary>
        //private void IsFrameUpdateHt(ContActualFinance contActual)
        //{
        //    var contInfo = this.Db.Set<ContractInfo>().Where(a => a.Id == contActual.ContId).FirstOrDefault();
        //    if (contInfo != null && contInfo.IsFramework == 1)
        //    {

        //        string sqlstr = $"update ContractInfo set AmountMoney=AmountMoney+{(contActual.AmountMoney ?? 0)} where Id={contInfo.Id};";
        //        ExecuteSqlCommand(sqlstr);
        //    }
        //}


        #region 发票核销

        /// <summary>
        /// 发票核销
        /// </summary>
        private bool CheckInvoice(ContActualFinance contActual, IList<CheckData> chkData)
        {
            IList<InvoiceCheck> invoiceChecks = new List<InvoiceCheck>();
            Dictionary<int, decimal> dicinvoice = new Dictionary<int, decimal>();
            // StringBuilder plansql = new StringBuilder();
            foreach (var item in chkData)
            {
                InvoiceCheck finnCheck = new InvoiceCheck();
                finnCheck.IsDelete = 0;
                finnCheck.ChkState = 0;
                finnCheck.CreateDateTime = System.DateTime.Now;
                finnCheck.CreateUserId = contActual.CreateUserId;
                finnCheck.ModifyDateTime = System.DateTime.Now;
                finnCheck.ModifyUserId = contActual.CreateUserId;
                finnCheck.InvoiceId = item.ChkId;
                finnCheck.AmountMoney = item.ChkMonery;
                finnCheck.ActualFinanceId = contActual.Id;
                invoiceChecks.Add(finnCheck);
                dicinvoice.Add(item.ChkId, item.ChkMonery);

            }
            // CreateActualUpdateInvoice(dicinvoice);
            // CreateActualToContractStatic(contActual);
            //核销表数据添加
            this.Db.Set<InvoiceCheck>().AddRange(invoiceChecks);
            this.SaveChanges();
            return true;

        }

        /// <summary>
        /// 添加实际资金修改发票
        /// </summary>
        /// <param name="dicplan">当前发票</param>
        //private void CreateActualUpdateInvoice(Dictionary<int, decimal> dicinvoice)
        //{
        //    var arrayIds = dicinvoice.Keys.ToArray();
        //    var listplans = this.Db.Set<ContInvoice>().Where(a => arrayIds.Contains(a.Id)).ToList();
        //    foreach (var plan in listplans)
        //    {
        //        plan.CheckAmount = dicinvoice[plan.Id];
        //        plan.ActAmountMoney = (plan.ActAmountMoney ?? 0) + dicinvoice[plan.Id];
        //        //plan.SurplusAmount = (plan.AmountMoney ?? 0) - (plan.ActAmountMoney ?? 0);
        //        //发票修改
        //        this.Db.Set<ContInvoice>().Attach(plan);
        //    }
        //}
        #endregion 

        #region 计划资金核销相关

        /// <summary>
        /// 计划资金核销
        /// </summary>
        private bool CheckPlanFinance(ContActualFinance contActual, IList<CheckData> chkData)
        {
            IList<PlanFinnCheck> planFinnChecks = new List<PlanFinnCheck>();
            Dictionary<int, decimal> dicplan = new Dictionary<int, decimal>();
            // StringBuilder plansql = new StringBuilder();
            foreach (var item in chkData)
            {
                PlanFinnCheck finnCheck = new PlanFinnCheck();
                finnCheck.IsDelete = 0;
                finnCheck.ChkState = 0;
                finnCheck.CreateDateTime = System.DateTime.Now;
                finnCheck.CreateUserId = contActual.CreateUserId;
                finnCheck.ModifyDateTime = System.DateTime.Now;
                finnCheck.ModifyUserId = contActual.CreateUserId;
                finnCheck.PlanFinanceId = item.ChkId;
                finnCheck.AmountMoney = item.ChkMonery;
                finnCheck.ActualFinanceId = contActual.Id;
                planFinnChecks.Add(finnCheck);
                dicplan.Add(item.ChkId, item.ChkMonery);

            }
            //CreateActualUpdatePlanFinance(dicplan);
            //CreateActualToContractStatic(contActual);
            //核销表数据添加
            this.Db.Set<PlanFinnCheck>().AddRange(planFinnChecks);
            this.SaveChanges();
            return true;

        }
        ///// <summary>
        ///// 添加实际资金修改计划资金
        ///// </summary>
        ///// <param name="dicplan">当前计划资金</param>
        //private void CreateActualUpdatePlanFinance(Dictionary<int, decimal> dicplan)
        //{
        //    var arrayIds = dicplan.Keys.ToArray();
        //    var listplans = this.Db.Set<ContPlanFinance>().Where(a => arrayIds.Contains(a.Id)).ToList();
        //    foreach (var plan in listplans)
        //    {
        //        plan.CheckAmount = 0;
        //        plan.ActAmountMoney = (plan.ActAmountMoney ?? 0) + dicplan[plan.Id];
        //        //plan.SurplusAmount = (plan.AmountMoney ?? 0) - (plan.ActAmountMoney ?? 0);
        //        ////计划资金修改
        //        this.Db.Set<ContPlanFinance>().Attach(plan);
        //    }
        //}

        /// <summary>
        /// 添加实际资金时修改合同统计字段
        /// </summary>
        /// <param name="contActual">实际资金对象</param>
        private void CreateActualToContractStatic(ContActualFinance contActual)
        {
            var contstatic = this.Db.Set<ContStatistic>().FirstOrDefault(a => a.ContId == contActual.ContId);
            if (contstatic != null)
            {  //修改合同统计字段
                contstatic.ActualAmount = (contstatic.ActualAmount ?? 0) + (contActual.AmountMoney ?? 0);
                //contstatic.BalaTick= (contstatic.CompInAm??0)-(contstatic)
                this.Db.Set<ContStatistic>().Attach(contstatic);
            }
            else
            {//添加合同统计字段
                ContStatistic contStatistics = new ContStatistic();
                contStatistics.ActualAmount = contActual.AmountMoney ?? 0;
                contStatistics.ContId = contActual.ContId;
                contStatistics.ModifyUserId = contActual.ModifyUserId;
                contStatistics.ModifyDateTime = DateTime.Now;
                this.Db.Set<ContStatistic>().Add(contStatistics);
            }
        }
        #endregion

        #endregion 

        #region 修改计划资金
        /// <summary>
        /// 修改实际资金
        /// </summary>
        /// <param name="contActual">实际资金对象</param>
        /// <param name="CheckType">核销类型0：计划资金，1：发票</param>
        /// <param name="chkData">核销数据</param>
        /// <returns></returns>
        public ContActualFinance UpdateSave(ContActualFinance contActual, int CheckType, IList<CheckData> chkData)
        {
            Update(contActual);
            Xiug(contActual);
            if (CheckType == 0)
            {//计划资金核销
                UpdateCheckPlanFinance(contActual, chkData);
            }
            else
            {//发票核销
                UpdateCheckInvoice(contActual, chkData);
            }
            return contActual;

        }

        public int Xiug(ContActualFinance contActual)
        {
            var je = Db.Set<ContActualFinance>().Where(a => a.ContId == contActual.ContId && a.Astate == 2&&a.IsDelete==0).Sum(a => a.AmountMoney);
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append($"update ContStatistic set CompActAm='{je}' where ContId ={contActual.ContId};");
            var Htzt = Db.Set<ContractInfo>().Where(a => a.IsDelete == 0 && a.Id == contActual.ContId).FirstOrDefault().IsFramework;
            if (Htzt==1)
            {
                sqlstr.Append($"update ContractInfo set AmountMoney='{je}' where Id ={contActual.ContId};");
            }
          
            return ExecuteSqlCommand(sqlstr.ToString());
        }
        #region 修改计划资金核销操作
        /// <summary>
        /// 计划资金核销
        /// </summary>
        private bool UpdateCheckPlanFinance(ContActualFinance contActual, IList<CheckData> chkData)
        {


            //根据实际资金查询之前核销
            var listIds = this.Db.Set<PlanFinnCheck>().Where(a => a.ActualFinanceId == contActual.Id).Select(a => a.Id).ToList();
            var chkIds = "0";
            if (listIds.Count() > 0)
            {
                chkIds = StringHelper.ArrayInt2String(listIds);
            }

            //删掉之前的
            var res = ExecuteSqlCommand($"delete PlanFinnCheck where Id in({chkIds})");
            if (res > 0)
            {
                IList<PlanFinnCheck> planFinnChecks = new List<PlanFinnCheck>();
                // Dictionary<int, decimal> dicplan = new Dictionary<int, decimal>();
                foreach (var item in chkData)
                {
                    PlanFinnCheck finnCheck = new PlanFinnCheck();
                    finnCheck.IsDelete = 0;
                    finnCheck.ChkState = 0;
                    finnCheck.CreateDateTime = System.DateTime.Now;
                    finnCheck.CreateUserId = contActual.CreateUserId;
                    finnCheck.ModifyDateTime = System.DateTime.Now;
                    finnCheck.ModifyUserId = contActual.CreateUserId;
                    finnCheck.PlanFinanceId = item.ChkId;
                    finnCheck.AmountMoney = item.ChkMonery;
                    finnCheck.ActualFinanceId = contActual.Id;
                    planFinnChecks.Add(finnCheck);
                    // dicplan.Add(item.ChkId, item.ChkMonery);

                }

                this.Db.Set<PlanFinnCheck>().AddRange(planFinnChecks);
            }
            this.SaveChanges();
            return true;



        }
        /// <summary>
        /// 实际资金修改发票核销修改
        /// </summary>
        /// <param name="contActual">实际资金</param>
        /// <param name="chkData">核销数据</param>
        public void UpdateCheckInvoice(ContActualFinance contActual, IList<CheckData> chkData)
        {
            //根据实际资金查询之前核销
            var listIds = this.Db.Set<InvoiceCheck>().Where(a => a.ActualFinanceId == contActual.Id&&a.IsDelete==0).Select(a => a.Id).ToList();
            if (listIds.Count() >0)
            {
                var chkIds = StringHelper.ArrayInt2String(listIds);
           
                //删掉之前的
                var res = ExecuteSqlCommand($"delete InvoiceCheck where Id in({chkIds})");
                if (res > 0)
                {
                    IList<InvoiceCheck> invoiceChecks = new List<InvoiceCheck>();

                    foreach (var item in chkData)
                    {
                        InvoiceCheck finnCheck = new InvoiceCheck();
                        finnCheck.IsDelete = 0;
                        finnCheck.ChkState = 0;
                        finnCheck.CreateDateTime = System.DateTime.Now;
                        finnCheck.CreateUserId = contActual.CreateUserId;
                        finnCheck.ModifyDateTime = System.DateTime.Now;
                        finnCheck.ModifyUserId = contActual.CreateUserId;
                        finnCheck.InvoiceId = item.ChkId;
                        finnCheck.AmountMoney = item.ChkMonery;
                        finnCheck.ActualFinanceId = contActual.Id;
                        invoiceChecks.Add(finnCheck);


                    }

                    this.Db.Set<InvoiceCheck>().AddRange(invoiceChecks);
                    this.SaveChanges();

                }
            }
        }


        /// <summary>
        /// 合同统计字段
        /// </summary>
        public void UpdateActualToContractStatic(ContActualFinance contActual, ContActualFinance oldContActual)
        {
            var contstatic = this.Db.Set<ContStatistic>().FirstOrDefault(a => a.ContId == contActual.ContId);
            if (contstatic != null)
            {  //修改合同统计字段
                contstatic.ActualAmount = (contstatic.ActualAmount ?? 0) + ((contActual.AmountMoney ?? 0) - (oldContActual.AmountMoney ?? 0));
                this.Db.Set<ContStatistic>().Attach(contstatic);
            }
        }
        ///// <summary>
        ///// 添加实际资金修改计划资金
        ///// </summary>
        ///// <param name="dicplan">当前计划资金</param>
        //private void UpdateActualUpdatePlanFinance(Dictionary<int, decimal> dicplan,Dictionary<int,decimal> olddicplan)
        //{

        //    var arrayoldIds = olddicplan.Keys.ToList();

        //    ////新老合并
        //    var arrayIds = dicplan.Keys.ToList().Concat(arrayoldIds).Distinct().ToArray();
        //    var listplans = this.Db.Set<ContPlanFinance>().Where(a => arrayIds.Contains(a.Id)).ToList();
        //    foreach (var plan in listplans)
        //    {
        //        if (arrayoldIds.Contains(plan.Id))
        //        {//标识之前添加
        //            plan.ActAmountMoney = (plan.ActAmountMoney ?? 0) - olddicplan[plan.Id];
        //        }
        //        if (dicplan.ContainsKey(plan.Id))
        //        {
        //            plan.ActAmountMoney = (plan.ActAmountMoney ?? 0) + olddicplan[plan.Id];
        //        }
        //        plan.CheckAmount = 0;

        //        this.Db.Set<ContPlanFinance>().Attach(plan);
        //    }

        //}


        #endregion

        #endregion
        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContActualFinanceViewDTO ShowView(int Id)
        {
            var query = from a in _ContActualFinanceSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            FinceType = a.FinceType,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            SettlementMethod = a.SettlementMethod,
                            AmountMoney = a.AmountMoney,
                            CurrencyId = a.CurrencyId,
                            CurrencyRate = a.CurrencyRate,
                            ActualSettlementDate = a.ActualSettlementDate,
                            VoucherNo = a.VoucherNo,
                            Astate = a.Astate,
                            Remark = a.Remark,
                            Bank = a.Bank,
                            Account = a.Account,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            ContId = a.ContId,
                            WfState = a.WfState,//流程状态
                        };
            var local = from a in query.AsEnumerable()
                        select new ContActualFinanceViewDTO
                        {
                            Id = a.Id,
                            FinceType = a.FinceType,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            SettlementMethod = a.SettlementMethod,
                            AmountMoney = a.AmountMoney,
                            CurrencyId = a.CurrencyId,
                            CurrencyRate = a.CurrencyRate,
                            ActualSettlementDate = a.ActualSettlementDate,
                            VoucherNo = a.VoucherNo,
                            Astate = a.Astate,
                            Remark = a.Remark,
                            Bank = a.Bank,
                            Account = a.Account,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            ContId = a.ContId,
                            SettlementMethodDic = this.Db.GetRedisDataDictionaryValue(a.SettlementMethod ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementMethod, DataDictionaryEnum.SettlModes),
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            AstateDic = EmunUtility.GetDesc(typeof(ActFinanceStateEnum), a.Astate ?? -1),
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                            WfState = a.WfState ?? 0,
                        };

            return local.FirstOrDefault();

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids, int usid)
        {
            StringBuilder sqlstr = new StringBuilder();
            if (usid == -10000)
            {
                var listIds = StringHelper.String2ArrayInt(Ids);
                foreach (var item in listIds)
                {
                    var zjinfo = Db.Set<ContActualFinance>().Where(a => a.Id == item && a.Astate == 2&&a.IsDelete==0).ToList();
                    if (zjinfo.Count()>0)
                    {
                        var je = zjinfo.FirstOrDefault().AmountMoney;
                        var Htid = zjinfo.FirstOrDefault().ContId;
                        var de = Db.Set<ContStatistic>().Where(a => a.ContId == Htid).FirstOrDefault().CompActAm;
                        var jes = de - je;
                        sqlstr.Append($"update ContStatistic set CompActAm='{jes}' where ContId={Htid};");
                    }
                }
            }
            sqlstr.Append($"update ContActualFinance set IsDelete=1 where Id in({Ids});");
            sqlstr.Append($"update PlanFinnCheck set IsDelete=1 where ActualFinanceId in({Ids});");
            sqlstr.Append($"update InvoiceCheck set IsDelete=1 where ActualFinanceId in({Ids});");
            return ExecuteSqlCommand(sqlstr.ToString());
            //    sqlstr.Append($"update ContActualFinance set IsDelete=1 where Id in({Ids});");
            //    sqlstr.Append($"update PlanFinnCheck set IsDelete=1 where ActualFinanceId in({Ids});");
            //    sqlstr.Append($"update InvoiceCheck set IsDelete=1 where ActualFinanceId in({Ids});");
            //    var du = "";
            //    return ExecuteSqlCommand(du);
            //    //return ExecuteSqlCommand(sqlstr.ToString());
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            StringBuilder strsql = new StringBuilder();
            switch (info.FieldName)
            {
                case "Astate"://状态
                    UpdateState(info, strsql);
                    break;
                case "Reserve1":
                case "Reserve2":
                case "ActualSettlementDate"://结算日期
                    strsql.Append($"update  ContActualFinance set {info.FieldName}='{info.FieldValue}',ModifyUserId={info.CurrUserId},ModifyDateTime='{DateTime.Now}' where Id={info.Id}");
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(strsql.ToString()))
                return ExecuteSqlCommand(strsql.ToString());
            return 0;

        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="info"></param>
        /// <param name="strsql"></param>
        private void UpdateState(UpdateFieldInfo info, StringBuilder strsql)
        {
            var state = Convert.ToByte(info.FieldValue);
            if (state == (int)ActFinanceStateEnum.Confirmed)
            {
                //var UpdateMoney = Convert.ToDecimal(info.UpdateMoney);
                var continfo = this.Db.Set<ContractInfo>().Where(a => a.Id == info.OtherId).FirstOrDefault();
                if (continfo != null && continfo.IsFramework == (int)ContractProperty.Framework)
                {//框架合同的合同金额等于实际资金金额已确认之和
                    strsql.Append($"update  ContractInfo set AmountMoney=AmountMoney+{(info.UpdateMoney)} where Id={info.OtherId}");
                }

                strsql.Append($"update  ContActualFinance set Astate={state},ConfirmUserId={info.CurrUserId},ConfirmDateTIme='{DateTime.Now}' where Id={info.Id};");
                UpdateContStaticCompActAm(info, continfo);
                strsql.Append($"update  ContractInfo set ContStaticId=(select Id from ContStatistic where ContId={info.OtherId} ) where Id={info.OtherId};");
                // strsql.Append($"update  ContStatistics set CompActAm=CompActAm+{UpdateMoney} where ContId={info.OtherId};");
                if (this.Db.Set<PlanFinnCheck>().Where(a => a.ActualFinanceId == info.Id).Any())
                {
                    UpdatePlanComfirAmont(info);

                }
                else
                {
                    UpdateInvoiceComfirAmount(info);
                }
            }
            else
            {
                strsql.Append($"update  ContActualFinance set Astate={state},ModifyUserId={info.CurrUserId},ModifyDateTime='{DateTime.Now}' where Id={info.Id}");
            }
        }
        /// <summary>
        /// 修改合同统计字段-已确认资金
        /// </summary>
        /// <param name="info"></param>
        /// <param name="UpdateMoney">已确认金额</param>
        private void UpdateContStaticCompActAm(UpdateFieldInfo info, ContractInfo continfo)
        {
            var contstatic = this.Db.Set<ContStatistic>().Where(a => a.ContId == info.OtherId).FirstOrDefault();
            if (contstatic == null)
            {
                ContStatistic contStatistics = new ContStatistic();
                contStatistics.CompActAm = info.UpdateMoney;
                contStatistics.ContId = info.OtherId;//合同ID
                contStatistics.ModifyDateTime = DateTime.Now;
                contStatistics.ModifyUserId = info.CurrUserId;
                contStatistics.CompInAm = 0;
                contStatistics.BalaTick = (contStatistics.CompInAm ?? 0) - (contStatistics.CompActAm ?? 0);//票款差额
                GetComRation(contStatistics, continfo);//完成比例
                this.Db.Set<ContStatistic>().Add(contStatistics);
            }
            else
            {
                contstatic.CompActAm = (contstatic.CompActAm ?? 0) + info.UpdateMoney;


                GetComRation(contstatic, continfo);
                contstatic.BalaTick = (contstatic.CompInAm ?? 0) - (contstatic.CompActAm ?? 0);//票款差额
                contstatic.ModifyDateTime = DateTime.Now;
                contstatic.ModifyUserId = info.CurrUserId;
                this.Db.Set<ContStatistic>().Attach(contstatic);
            }
        }
        /// <summary>
        /// 计算完成比例
        /// </summary>
        /// <param name="contstatic">统计合同对象</param>
        private void GetComRation(ContStatistic contstatic, ContractInfo contInfo)
        {
            //var contInfo = this.Db.Set<ContractInfo>().Where(a => a.Id == contstatic.ContId).FirstOrDefault();
            if (contInfo.IsFramework == (int)(int)ContractProperty.Standard)
            {
                var htje = contInfo == null ? 0 : (contInfo.AmountMoney ?? 0);
                if (htje == 0 || contstatic.CompActAm == 0)
                {
                    contstatic.CompRatio = 0;

                }
                else
                {

                    var tempval = PublicMethod.DivisionTWoDec((contstatic.CompActAm ?? 0), htje);//保留四舍五入两位小数
                    contstatic.CompRatio = tempval > 1 ? 1 : tempval;//最高封顶100%
                }
            }
            else
            {
                contstatic.CompRatio = 1;
            }
        }

        /// <summary>
        /// 更新发票的已确认金额
        /// </summary>
        /// <param name="info"></param>
        private void UpdateInvoiceComfirAmount(UpdateFieldInfo info)
        {
            var listchks = this.Db.Set<InvoiceCheck>().Where(a => a.ActualFinanceId == info.Id).ToList();
            foreach (var chk in listchks)
            {
                chk.ChkState = 1;
                chk.ConfirmDateTime = DateTime.Now;
                chk.ConfirmUserId = info.CurrUserId;
                this.Db.Set<InvoiceCheck>().Attach(chk);
            }
            var dicchks = listchks.ToDictionary(a => a.InvoiceId, a => a.AmountMoney);
            var lispinvoices = this.Db.Set<ContInvoice>().Where(a => dicchks.Keys.Contains(a.Id)).ToList();
            foreach (var item in lispinvoices)
            {
                item.ConfirmedAmount = (item.ConfirmedAmount ?? 0) + (dicchks[item.Id] ?? 0);
                this.Db.Set<ContInvoice>().Attach(item);
            }
            this.Db.SaveChanges();
        }

        /// <summary>
        /// 更新计划资金的已确认金额
        /// </summary>
        /// <param name="info">更新对象</param>
        private void UpdatePlanComfirAmont(UpdateFieldInfo info)
        {
            var listchks = this.Db.Set<PlanFinnCheck>().Where(a => a.ActualFinanceId == info.Id).ToList();
            foreach (var chk in listchks)
            {
                chk.ChkState = 1;
                chk.ConfirmDateTime = DateTime.Now;
                chk.ConfirmUserId = info.CurrUserId;
                this.Db.Set<PlanFinnCheck>().Attach(chk);
            }
            var dicchks = listchks.ToDictionary(a => a.PlanFinanceId, a => a.AmountMoney);
            var lisplans = this.Db.Set<ContPlanFinance>().Where(a => dicchks.Keys.Contains(a.Id)).ToList();
            foreach (var item in lisplans)
            {

                if ((item.ConfirmedAmount ?? 0) + (dicchks[item.Id] ?? 0) >= item.AmountMoney)
                {
                    item.Fstate = 1;//已完成
                }
                item.ConfirmedAmount = (item.ConfirmedAmount ?? 0) + (dicchks[item.Id] ?? 0);
                this.Db.Set<ContPlanFinance>().Attach(item);
            }
            this.Db.SaveChanges();
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContractActualFinance> GetList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda, Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContActualFinance>().AsTracking().Where<ContActualFinance>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContActualFinance>))
                tempquery = tempquery.Skip<ContActualFinance>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContActualFinance>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            VoucherNo = a.VoucherNo,
                            AmountMoney = a.AmountMoney,
                            Astate = a.Astate,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            SettlementMethod = a.SettlementMethod,
                            ActualSettlementDate = a.ActualSettlementDate,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContractActualFinance
                        {
                            Id = a.Id,
                            CreateDateTime = a.CreateDateTime,
                            VoucherNo = a.VoucherNo,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            AstateDic = EmunUtility.GetDesc(typeof(ActFinanceStateEnum), a.Astate ?? -1),
                            SettlementMethodDic = this.Db.GetRedisDataDictionaryValue(a.SettlementMethod ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementMethod, DataDictionaryEnum.SettlModes),//发票类型 
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                            ConfirmDateTime = a.ConfirmDateTime,
                            ActualSettlementDate = a.ActualSettlementDate
                        };
            return new LayPageInfo<ContractActualFinance>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }


    }
}
