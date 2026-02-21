using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Extend.Enums;
using NF.Common.Extend;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 发票
    /// </summary>
    public partial  class ContInvoiceService
    {
        /// <summary>
        /// 查询发票大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContInvoiceListViewDTO> GetMainList<s>(PageInfo<ContInvoice> pageInfo, Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            try
            {
                var tempquery = Db.Set<ContInvoice>()
                       .Include(a => a.Cont)
                        .Include(a => a.CreateUser)
                         .Include(a => a.ConfirmUser)
                       .Include(a => a.Cont).ThenInclude(a => a.Comp)
                        .Include(a => a.Cont).ThenInclude(a => a.CreateUser)
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
                if (!(pageInfo is NoPageInfo<ContInvoice>))
                    tempquery = tempquery.Skip<ContInvoice>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContInvoice>(pageInfo.PageSize);
                var query = from a in tempquery
                            select new
                            {
                                Id = a.Id,
                                InType = a.InType,
                                ContId = a.ContId,
                                ContName = a.Cont != null ? a.Cont.Name : "",
                                ContCode = a.Cont != null ? a.Cont.Code : "",
                                CompName = (a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Name : "",
                                CompId = a.Cont != null ? a.Cont.CompId : -1,
                                DeptId = a.Cont != null ? a.Cont.DeptId : -1,
                                ContTypeId = a.Cont != null ? a.Cont.ContTypeId : -1,
                                CreateUserId = a.CreateUserId,
                                CreateDateTime = a.CreateDateTime,
                                InCode = a.InCode,
                                AmountMoney = a.AmountMoney,
                                InState = a.InState,
                                ConfirmUserId = a.ConfirmUserId,
                                ConfirmDateTime = a.ConfirmDateTime,
                                Remark = a.Remark,
                                Reserve1 = a.Reserve1,
                                Reserve2 = a.Reserve2,
                                FinanceType = a.Cont.FinanceType,
                                WfState = a.WfState,
                                WfCurrNodeName = a.WfCurrNodeName,
                                WfItem = a.WfItem



                            };
                var local = from a in query.AsEnumerable()
                            select new ContInvoiceListViewDTO
                            {

                                Id = a.Id,
                                InType = a.InType,
                                ContId = a.ContId,
                                ContName = a.ContName,
                                ContCode = a.ContCode,
                                CompName = a.CompName,
                                CompId = a.CompId,
                                DeptId = a.DeptId,
                                DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                               // RedisValueUtility.GetDeptName(a.DeptId ?? -2),//经办机构a.DeptId,
                                ContTypeId = a.ContTypeId,
                                ContCategoryName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                                //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别 
                                CreateUserId = a.CreateUserId,
                                CreateDateTime = a.CreateDateTime,
                                InCode = a.InCode,
                                AmountMoney = a.AmountMoney,
                                AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                                InState = a.InState,
                                InStateDic = EmunUtility.GetDesc(typeof(InvoiceStateEnum), a.InState ?? -1),
                                ConfirmUserId = a.ConfirmUserId,
                                InTypeName = this.Db.GetRedisDataDictionaryValue(a.InType ?? 0, DataDictionaryEnum.InvoiceType),
                                //DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                                CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                                //RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                                ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                                //RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                                ConfirmDateTime = a.ConfirmDateTime,
                                Remark = a.Remark,
                                Reserve1 = a.Reserve1,
                                Reserve2 = a.Reserve2,
                                FinanceType = a.FinanceType,
                                WfState = a.WfState ?? 0,
                                WfCurrNodeName = a.WfCurrNodeName,
                                WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                                WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                            };
                return new LayPageInfo<ContInvoiceListViewDTO>()
                {
                    data = local.ToList(),
                    count = pageInfo.TotalCount,
                    code = 0


                };
            }
            catch (Exception ex)
            {
                Log4netHelper.Error(ex.ToString());
                return new LayPageInfo<ContInvoiceListViewDTO>()
                {
                    data =null,
                    count =0,
                    code = 0


                };

               
            }


        }

        /// <summary>
        /// 实际资金核销发票列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContInvoiceActViewDTO> GetActInvoiceList<s>(PageInfo<ContInvoice> pageInfo, Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc,int actId)
        {
            var chkinvoices = this.Db.Set<InvoiceCheck>().AsNoTracking().Where(a => a.ActualFinanceId == actId&&a.IsDelete==0).ToList();
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            AmountMoney=a.AmountMoney,
                            ConfirmedAmount = a.ConfirmedAmount,
                            //CheckAmount=a.CheckAmount



                        };
            var local = from a in query.AsEnumerable()
                        select new ContInvoiceActViewDTO
                        {

                            Id = a.Id,
                            //发票金额
                            AmountMoney = a.AmountMoney,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            //已收已付
                            ConfirmedAmount = a.ConfirmedAmount,
                            ConfirmedAmountThod = a.ConfirmedAmount.ThousandsSeparator(),
                            //票款差
                            FareWorse=(a.AmountMoney??0)-(a.ConfirmedAmount ?? 0),
                            FareWorseThod= ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)).ThousandsSeparator(),
                            //应收应付
                            Accounts= (a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0),
                            AccountsThod =((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)).ThousandsSeparator(), 
                            //本次金额
                            CheckAmount = chkinvoices.Where(p => p.InvoiceId == a.Id).Any() ? chkinvoices.Where(p => p.InvoiceId == a.Id).FirstOrDefault().AmountMoney : 0,
                            CheckAmountThod=(chkinvoices.Where(p => p.InvoiceId == a.Id).Any() ? chkinvoices.Where(p => p.InvoiceId == a.Id).FirstOrDefault().AmountMoney : 0).ThousandsSeparator(),
                           
                        };
            return new LayPageInfo<ContInvoiceActViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 合同下发票列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="actId"></param>
        /// <returns></returns>
        public LayPageInfo<ContractInvoice> GetList<s>(PageInfo<ContInvoice> pageInfo, Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContInvoice>().AsTracking().Where<ContInvoice>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContInvoice>))
                tempquery = tempquery.Skip<ContInvoice>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContInvoice>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            InType = a.InType,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            InCode = a.InCode,
                            AmountMoney = a.AmountMoney,
                            InState = a.InState,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,

                        };
            var local = from a in query.AsEnumerable()
                        select new ContractInvoice
                        {

                            Id = a.Id,
                            InCode = a.InCode,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            InState = a.InState,
                            InStateDic = EmunUtility.GetDesc(typeof(InvoiceStateEnum), a.InState ?? -1),
                            InTypeName = this.Db.GetRedisDataDictionaryValue(a.InType ?? 0, DataDictionaryEnum.InvoiceType),
                            //DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类型 
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                         //   RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                           // RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                            ConfirmDateTime = a.ConfirmDateTime,
                            CreateDateTime=a.CreateDateTime,

                        };
            return new LayPageInfo<ContractInvoice>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="contInvoice">保存发票</param>
        /// <returns></returns>
        public ContInvoice AddSave(ContInvoice contInvoice)
        {
            var inof = Add(contInvoice);
            EventUtility eventUtility = new EventUtility();
            eventUtility.ContInvoiceEvent += UpdateItems;
            //eventUtility.ContInvoiceEvent += AddContStatic;
            eventUtility.ExceContInvoiceEvent(inof);
           // UpdateItems(inof);
            return inof;

        }
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        public int ClearJunkItemData(int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"delete InvoFile  where InvoId={-currUserId};");//
            strsql.Append($"delete InvoDescription  where ContInvoId={-currUserId};");//清除垃圾数据
           
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
        }
        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <param name="HisId">合同历史ID</param>
        public void UpdateItems(ContInvoice contInvoice)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"update InvoDescription set ContInvoId={contInvoice.Id} where ContInvoId={-contInvoice.ModifyUserId};");
            strsql.Append($"update InvoFile set InvoId={contInvoice.Id} where InvoId={-contInvoice.ModifyUserId};");//发票附件
            ExecuteSqlCommand(strsql.ToString());

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids,int usid)
        {
            StringBuilder sqlstr = new StringBuilder();
            if (usid == -10000)
            {
                var listIds = StringHelper.String2ArrayInt(Ids);
                foreach (var item in listIds)
                {
                    var zjinfo = Db.Set<ContInvoice>().Where(a => a.Id == item && a.InState == 2 && a.IsDelete == 0).ToList();
                    if (zjinfo.Count() > 0)
                    {
                        var je = zjinfo.FirstOrDefault().AmountMoney;
                        var Htid = zjinfo.FirstOrDefault().ContId;
                        var de = Db.Set<ContStatistic>().Where(a => a.ContId == Htid).FirstOrDefault().CompInAm;
                        var jes = de - je;
                        sqlstr.Append($"update ContStatistic set CompInAm='{jes}' where ContId={Htid};");
                    }
                }
            }
            sqlstr.Append($"update ContInvoice set IsDelete=1 where Id  in({Ids});");
            // sqlstr = "update ContInvoice set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr.ToString());
           // return ExecuteSqlCommand(sd);
            //return ExecuteSqlCommand(sqlstr);
        }

        public int ZJtj(string  id) {
            var listIds = StringHelper.String2ArrayInt(id);
            string sqlstr = "";
            foreach (var item in listIds)
            {
                var Fp = Db.Set<ContInvoice>().Where(a => a.Id == item&&a.InState==2).ToList();
                var Htid = Fp.Select(c => c.ContId);

                var je = Fp.Select(e => e.AmountMoney);


               // var sd= Db.Set<ContStatistic>().Where(a => a.ContId==);
            }

        
            return ExecuteSqlCommand(sqlstr);

        }
        /// <summary>
        /// 查看和修改绑定
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>对象</returns>
        public ContInvoiceViewDTO ShowView(int Id)
        {
            var query = from a in _ContInvoiceSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            InType = a.InType,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            InCode = a.InCode,
                            AmountMoney = a.AmountMoney,
                            InState = a.InState,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            Remark = a.Remark,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            MakeOutDateTime=a.MakeOutDateTime,
                            ContId=a.ContId,
                            WfState=a.WfState,//流程状态
                            InvoiceTitle=a.InTitle,
                            TaxIdentification=a.TaxpayerIdentification,
                            InvoiceAddress=a.InAddress,
                            InvoiceTel=a.InTel,
                            BankName=a.BankName,
                            BankAccount=a.BankAccount,

                        };
            var local = from a in query.AsEnumerable()
                        select new ContInvoiceViewDTO
                        {
                            Id = a.Id,
                            InType = a.InType,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            InCode = a.InCode,
                            AmountMoney = a.AmountMoney,
                            InState = a.InState,
                            ConfirmUserId = a.ConfirmUserId,
                            ConfirmDateTime = a.ConfirmDateTime,
                            Remark = a.Remark,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            MakeOutDateTime = a.MakeOutDateTime,
                            ContId = a.ContId,
                            //发票类别
                            InTypeName = this.Db.GetRedisDataDictionaryValue(a.InType ?? 0, DataDictionaryEnum.InvoiceType),
                         //   DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类别
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            InStateDic = EmunUtility.GetDesc(typeof(InvoiceStateEnum), a.InState ?? -1),
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                           // RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                            WfState=a.WfState??0,//流程状态
                            InvoiceTitle = a.InvoiceTitle,
                            TaxIdentification = a.TaxIdentification,
                            InvoiceAddress = a.InvoiceAddress,
                            InvoiceTel = a.InvoiceTel,
                            BankName = a.BankName,
                            BankAccount = a.BankAccount,
                        };

            return local.FirstOrDefault();

        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            StringBuilder sqlstr = new StringBuilder();
            switch (info.FieldName)
            {

                case "InCode"://发票号
                case "Reserve1":
                case "Reserve2":
                    sqlstr.Append( $"update  ContInvoice set {info.FieldName}='{info.FieldValue}' where Id={info.Id}");
                    break;
                
                case "MakeOutDateTime"://实际履行日期
                    sqlstr.Append($"update  ContInvoice set MakeOutDateTime='{info.FieldValue}' where Id={info.Id}");
                    break;
                case "InState"://状态
                    var tmpstate = Convert.ToInt32(info.FieldValue);
                    switch ((InvoiceStateEnum)tmpstate)
                    {
                        case InvoiceStateEnum.Invoicing:
                        case InvoiceStateEnum.ReceiptInvoice:
                            sqlstr.Append($"update  ContInvoice set InState={info.FieldValue},ConfirmUserId={info.CurrUserId},ConfirmDateTIme='{DateTime.Now}' where Id={info.Id};");
                           // var invoiceInfo = Find(info.Id);
                            //更新相关合同统计字段（已确认发票）
                            //sqlstr.Append($"update  ContStatistics set CompInAm=CompInAm+{(invoiceInfo.AmountMoney ?? 0)} where ContId={invoiceInfo.ContId};");
                            UpdateContStaticCompInAm(info);
                            sqlstr.Append($"update  ContractInfo set ContStaticId=(select Id from ContStatistic where ContId={info.OtherId} ) where Id={info.OtherId};");
                            break;
                        default:
                            sqlstr.Append($"update  ContInvoice set InState={info.FieldValue} where Id={info.Id}");
                            break;

                    }

                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr.ToString()))
                return ExecuteSqlCommand(sqlstr.ToString());
            return 0;

        }

        /// <summary>
        /// 修改合同统计字段-已确认发票
        /// </summary>
        /// <param name="info"></param>
        private void UpdateContStaticCompInAm(UpdateFieldInfo info)
        {
            var contstatic = this.Db.Set<ContStatistic>().Where(a => a.ContId == info.OtherId).FirstOrDefault();
            if (contstatic == null)
            {
                
                   ContStatistic contStatistics = new ContStatistic();
                contStatistics.CompInAm = info.UpdateMoney;
                contStatistics.ContId = info.OtherId;//合同ID
                contStatistics.ModifyDateTime = DateTime.Now;
                contStatistics.ModifyUserId = info.CurrUserId;
                contStatistics.BalaTick = info.UpdateMoney;//票款差额=当前发票-0（实际资金没有）
                contStatistics.CompRatio = 0;//完成比例
                contStatistics.CompActAm = 0;//完成金额
                this.Db.Set<ContStatistic>().Add(contStatistics);
            }
            else
            {
                contstatic.CompInAm = (contstatic.CompInAm ?? 0) + info.UpdateMoney;
                contstatic.ModifyDateTime = DateTime.Now;
                contstatic.ModifyUserId = info.CurrUserId;
                contstatic.BalaTick = (contstatic.CompInAm ?? 0) - (contstatic.CompActAm ?? 0);//票款差额
                this.Db.Set<ContStatistic>().Attach(contstatic);
            }

            this.Db.SaveChanges();
        }



        public int Xiug(int ContId)
        {
            var je = Db.Set<ContInvoice>().Where(a => a.ContId == ContId && a.InState == 2).Sum(a => a.AmountMoney);
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append($"update ContStatistic set CompInAm='{je}' where ContId ={ContId};");
            //sqlstr.Append($"update ContractInfo set AmountMoney='{je}' where Id ={contActual.ContId};");
            return ExecuteSqlCommand(sqlstr.ToString());
        }

    }
}
