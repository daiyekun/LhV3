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
using System.Threading.Tasks;

namespace NF.BLL
{
    public partial class ContInvoiceService
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
        public LayPageInfo<ContInvoiceListViewDTO> GetWxMainList<s>(PageInfo<ContInvoice> pageInfo, Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc)
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
                                WfItem = a.WfItem,



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
                                DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                                //RedisValueUtility.GetDeptName(a.DeptId ?? -2),//经办机构a.DeptId,
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
                                WfItem = a.WfItem ?? -2,
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
                    data = null,
                    count = 0,
                    code = 0
                };
            }
        }


        /// <summary>
        /// 查看和修改绑定
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>对象</returns>
        public WxContInvoice WxShowView(int Id)
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
                            MakeOutDateTime = a.MakeOutDateTime,
                            ContId = a.ContId,
                            WfState = a.WfState,//流程状态
                            InvoiceTitle = a.InTitle,
                            TaxIdentification = a.TaxpayerIdentification,
                            InvoiceAddress = a.InAddress,
                            InvoiceTel = a.InTel,
                            BankName = a.BankName,
                            BankAccount = a.BankAccount,
                            WfItem = a.WfItem,
                        };
            var local = from a in query.AsEnumerable()
                        select new WxContInvoice
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
                            //DataDicUtility.GetDicValueToRedis(a.InType, DataDictionaryEnum.InvoiceType),//发票类别
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            InStateDic = EmunUtility.GetDesc(typeof(InvoiceStateEnum), a.InState ?? -1),
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            ConfirmUserName = this.Db.GetRedisUserFieldValue(a.ConfirmUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.ConfirmUserId ?? 0), //确认人
                            WfState = a.WfState ?? 0,//流程状态
                            InvoiceTitle = a.InvoiceTitle,
                            TaxIdentification = a.TaxIdentification,
                            InvoiceAddress = a.InvoiceAddress,
                            InvoiceTel = a.InvoiceTel,
                            BankName = a.BankName,
                            BankAccount = a.BankAccount,
                            WfItem = a.WfItem ?? -2,
                        };

            return local.FirstOrDefault();
        }
    }
}
