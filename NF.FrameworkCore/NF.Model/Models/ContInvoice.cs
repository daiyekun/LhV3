using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContInvoice
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
        public decimal? SubAmount { get; set; }
        public decimal? ConfirmedAmount { get; set; }
        public decimal? SurplusAmount { get; set; }
        public decimal? ActAmountMoney { get; set; }
        public decimal? CheckAmount { get; set; }
        public byte? WfState { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }

        public virtual UserInfor ConfirmUser { get; set; }
        public virtual ContractInfo Cont { get; set; }
        public virtual UserInfor CreateUser { get; set; }
    }
}
