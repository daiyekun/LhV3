using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContractInfoHistory
    {
        public ContractInfoHistory()
        {
            ContPlanFinanceHistories = new HashSet<ContPlanFinanceHistory>();
            ContTextHistories = new HashSet<ContTextHistory>();
        }

        public int Id { get; set; }
        public int? ContId { get; set; }
        public string Code { get; set; }
        public string OtherCode { get; set; }
        public string Name { get; set; }
        public int? ContTypeId { get; set; }
        public byte FinanceType { get; set; }
        public decimal? AmountMoney { get; set; }
        public decimal? StampTax { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public byte ContState { get; set; }
        public int? DeptId { get; set; }
        public int? CompId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? SngnDateTime { get; set; }
        public DateTime? EffectiveDateTime { get; set; }
        public DateTime? PlanCompleteDateTime { get; set; }
        public DateTime? ActualCompleteDateTime { get; set; }
        public int? PrincipalUserId { get; set; }
        public string FinanceTerms { get; set; }
        public int? ModificationTimes { get; set; }
        public string ModificationRemark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public int? MainDeptId { get; set; }
        public byte? ContDivision { get; set; }
        public int? SumContId { get; set; }
        public int? CompId3 { get; set; }
        public int? CompId4 { get; set; }
        public byte? IsFramework { get; set; }
        public DateTime? PerformanceDateTime { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? EstimateAmount { get; set; }
        public int? ContSourceId { get; set; }
        public byte? WfState { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }
        public string ContSingNo { get; set; }

        public virtual Company Comp { get; set; }
        public virtual Company CompId3Navigation { get; set; }
        public virtual Company CompId4Navigation { get; set; }
        public virtual DataDictionary ContSource { get; set; }
        public virtual DataDictionary ContType { get; set; }
        public virtual UserInfor CreateUser { get; set; }
        public virtual Department Dept { get; set; }
        public virtual Department MainDept { get; set; }
        public virtual UserInfor PrincipalUser { get; set; }
        public virtual ProjectManager Project { get; set; }
        public virtual ICollection<ContPlanFinanceHistory> ContPlanFinanceHistories { get; set; }
        public virtual ICollection<ContTextHistory> ContTextHistories { get; set; }
    }
}
