using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class InvoiceCheck
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public int? ActualFinanceId { get; set; }
        public decimal? AmountMoney { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public int? ConfirmUserId { get; set; }
        public DateTime? ConfirmDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public DateTime? SettlementDate { get; set; }
        public byte? ChkState { get; set; }
    }
}
