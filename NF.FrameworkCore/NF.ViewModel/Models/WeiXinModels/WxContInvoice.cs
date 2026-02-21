using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
  public   class WxContInvoice
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public int? InType { get; set; }
        public string InTitle { get; set; }
        public string TaxpayerIdentification { get; set; }
        public string InAddress { get; set; }
        public string InTel { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public decimal? AmountMoney { get; set; }
        public DateTime? MakeOutDateTime { get; set; }
        public string InCode { get; set; }
        public byte? InState { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string InContent { get; set; }
        public string Remark { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public int? ConfirmUserId { get; set; }
        public DateTime? ConfirmDateTime { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        /// <summary>
        /// 是不是开票
        /// </summary>
        public bool IsOutInvoice { get; set; } = false;

        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string InTypeName { get; set; }
        /// <summary>
        /// 发票金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 发票状态
        /// </summary>
        public string InStateDic { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public byte WfState { get; set; }
        /// <summary>
        /// 发票标题
        /// </summary>

        public string InvoiceTitle { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        public string TaxIdentification { get; set; }
        /// <summary>
        /// 发票地址
        /// </summary>
        public string InvoiceAddress { get; set; }
        /// <summary>
        /// 发票电话
        /// </summary>
        public string InvoiceTel { get; set; }
        public int? WfItem { get; set; }

    }
}
