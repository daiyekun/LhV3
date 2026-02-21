using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.MobileApp
{
 public   class APPFinance_actual
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ContAmThod { get; set; }
        public string CompName { get; set; }
        public string HtWcJeThod { get; set; }
        public string FaPiaoThod { get; set; }
  
        public UserInfor ConfirmUser { get; set; }
        public ContractInfo Cont { get; set; }
        public UserInfor CreateUser { get; set; }
    }

    public partial class APPContPlanFinance
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public string Name { get; set; }
        public byte? Ftype { get; set; }
        public decimal? AmountMoney { get; set; }
        public int? SettlementModes { get; set; }
        public DateTime? PlanCompleteDateTime { get; set; }
        public byte? Fstate { get; set; }
        public byte? ProgressState { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public decimal? SubAmount { get; set; }
        public decimal? ConfirmedAmount { get; set; }
        public DateTime? ActSettlementDate { get; set; }
        public decimal? SurplusAmount { get; set; }
        public decimal? ActAmountMoney { get; set; }
        public decimal? CheckAmount { get; set; }
        public string      HtWcJeThod { get; set; }
        public string FaPiaoThod  { get; set; }
        public ContractInfo Cont { get; set; }
        public string CreateUserName { get; set; }
        public string AmountMoneyThod { get; set; }
        public string SettlModelName { get; set; }
        public string ConfirmedAmountThod { get; set; }
        public string SubAmountThod { get; set; }
        public string BalanceThod { get; set; }
        public string CompRate { get; set; }
    }
}
