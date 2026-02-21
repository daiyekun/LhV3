using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContActualFinance
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public int? SettlementMethod { get; set; }
        public byte? FinceType { get; set; }
        public decimal? AmountMoney { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public DateTime? ActualSettlementDate { get; set; }
        public string VoucherNo { get; set; }
        public byte? Astate { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public int? ConfirmUserId { get; set; }
        public DateTime? ConfirmDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public byte? WfState { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }
        public string TuisongPz { get; set; }

        public virtual UserInfor ConfirmUser { get; set; }
        public virtual ContractInfo Cont { get; set; }
        public virtual UserInfor CreateUser { get; set; }
    }
}
