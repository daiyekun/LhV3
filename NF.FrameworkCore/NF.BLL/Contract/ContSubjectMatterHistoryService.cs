using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NF.Model.Models;
using System.Linq.Expressions;
using NF.Common.Utility;
using NF.Common.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 标的历史
    /// </summary>
   public partial class ContSubjectMatterHistoryService
    {
        /// <summary>
        /// 查询标的历史
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContSubjectMatterHistoryViewDTO> GetList<s>(PageInfo<ContSubjectMatterHistory> pageInfo, Expression<Func<ContSubjectMatterHistory, bool>> whereLambda, Expression<Func<ContSubjectMatterHistory, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Spec = a.Spec,
                            Stype = a.Stype,
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
                            NominalQuote = a.NominalQuote,
                            NominalRate = a.NominalRate,
                            PlanDateTime = a.PlanDateTime


                        };
            var local = from a in query.AsEnumerable()
                        select new ContSubjectMatterHistoryViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Spec = a.Spec,//规格
                            Stype = a.Stype,//型号
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Unit = a.Unit,//单位
                            Amount = a.Amount,
                            Price = a.Price,
                            Remark = a.Remark,
                            DiscountRate = a.DiscountRate,
                            SubTotalRate = a.SubTotalRate,
                            SubTotal = a.SubTotal,
                            SalePrice = a.SalePrice,
                            AmountMoney = a.AmountMoney,
                            NominalQuote = a.NominalQuote,
                            PlanDateTime = a.PlanDateTime,
                            NominalRate = a.NominalRate,
                            PriceThod = a.Price.ThousandsSeparator(),
                            SubTotalThod = a.SubTotal.ThousandsSeparator(),
                            SalePriceThod = a.SalePrice.ThousandsSeparator(),


                        };
            return new LayPageInfo<ContSubjectMatterHistoryViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
    }
}
