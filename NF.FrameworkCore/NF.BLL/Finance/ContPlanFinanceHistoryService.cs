using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.ViewModel.Models.Utility;
using NF.Common.Extend;
using NF.ViewModel.Extend.Enums;
using NF.BLL.Extend;
namespace NF.BLL
{
    /// <summary>
    /// 合同历史
    /// </summary>
   public partial class ContPlanFinanceHistoryService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContPlanFinanceHistoryViewDTO> GetList<s>(PageInfo<ContPlanFinanceHistory> pageInfo, Expression<Func<ContPlanFinanceHistory, bool>> whereLambda, Expression<Func<ContPlanFinanceHistory, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            Fstate = a.Fstate,
                            Remark = a.Remark,
                            SettlementModes = a.SettlementModes,//结算方式
                            //CreateUserName = a.CreateUser.DisplyName


                        };
            var local = from a in query.AsEnumerable()
                        select new ContPlanFinanceHistoryViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            Fstate = a.Fstate,
                            Remark = a.Remark,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            SettlModelName = this.Db.GetRedisDataDictionaryValue(a.SettlementModes ?? 0, DataDictionaryEnum.SettlModes)
                         //   DataDicUtility.GetDicValueToRedis(a.SettlementModes, DataDictionaryEnum.SettlModes)
                        };
            return new LayPageInfo<ContPlanFinanceHistoryViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
    }
}
