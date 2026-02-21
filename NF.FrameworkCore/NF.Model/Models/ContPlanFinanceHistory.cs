using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContPlanFinanceHistory
    {
        public int Id { get; set; }
        public int? PlanFinanceId { get; set; }
        public int? ContId { get; set; }
        public int? ContHisId { get; set; }
        public string Name { get; set; }
        public byte? Ftype { get; set; }
        public decimal? AmountMoney { get; set; }
        public int? SettlementModes { get; set; }
        public DateTime? PlanCompleteDateTime { get; set; }
        public byte? Fstate { get; set; }
        public byte? ProgressState { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public decimal? SubAmount { get; set; }
        public decimal? ActAmountMoney { get; set; }
        public decimal? ConfirmedAmount { get; set; }
        public DateTime? ActSettlementDate { get; set; }
        public decimal? SurplusAmount { get; set; }
        public decimal? CheckAmount { get; set; }
        public int? IsWxMsg { get; set; }
        public DateTime? IsWxmsgDate { get; set; }

        public virtual ContractInfoHistory ContHis { get; set; }
    }
}
