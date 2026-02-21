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
        public LayPageInfo<WxActualFinance> GetWxMainList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda, Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContActualFinance>().Include(a => a.Cont)
               .Where(whereLambda.Compile()).AsQueryable();
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
                            ContName = a.Cont == null ? "" : a.Cont.Name,
                            AmountMoney = a.AmountMoney,
                            Astate = a.Astate,
                            Htid = a.ContId,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItem = a.WfItem,



                        };
            var local = from a in query.AsEnumerable()
                        select new WxActualFinance
                        {
                            Htid = a.Htid,
                            Id = a.Id,//id 
                            HtName = a.ContName,//合同名称
                            Sjzj = a.AmountMoney.ThousandsSeparator(),//实际资金金额
                            ZjSata = EmunUtility.GetDesc(typeof(ActFinanceStateEnum), a.Astate ?? -1),
                            WfItem = a.WfItem ?? -2,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),


                        };
            return new LayPageInfo<WxActualFinance>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public WxContActualFinance WxShowView(int Id)
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
                            WfItem = a.WfItem,
                        };
            var local = from a in query.AsEnumerable()
                        select new WxContActualFinance
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
                            WfItem = a.WfItem ?? -2,


                        };

            return local.FirstOrDefault();

        }


    }
}
