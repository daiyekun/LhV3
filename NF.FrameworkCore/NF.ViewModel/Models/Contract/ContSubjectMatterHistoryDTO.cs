using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 标的历史
    /// </summary>
   public class ContSubjectMatterHistoryDTO
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Stype { get; set; }
        public string Unit { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Price { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public int? BcInstanceId { get; set; }
        public byte? IsFromCategory { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? SubTotalRate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ComplateAmount { get; set; }
        public DateTime? PlanDateTime { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? AmountMoney { get; set; }
        public decimal? NominalQuote { get; set; }
        public decimal? NominalRate { get; set; }
    }
    /// <summary>
    /// 合同标的显示类
    /// </summary>
    public class ContSubjectMatterHistoryViewDTO : ContSubjectMatterHistoryDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 单价千分位
        /// </summary>
        public string PriceThod { get; set; }
        /// <summary>
        /// 小计千分位
        /// </summary>
        public string SubTotalThod { get; set; }
        /// <summary>
        /// 销售报价千分位
        /// </summary>
        public string SalePriceThod { get; set; }

    }
}
