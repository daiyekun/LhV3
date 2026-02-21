using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 计划资金历史
    /// </summary>
   public class ContPlanFinanceHistoryDTO
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
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
    /// <summary>
    /// 计划资金显示
    /// </summary>
    public class ContPlanFinanceHistoryViewDTO : ContPlanFinanceHistoryDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 计划金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlModelName { get; set; }

    }
}
