using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ProjectManager
    {
        public ProjectManager()
        {
            ContractInfoHistories = new HashSet<ContractInfoHistory>();
            ContractInfos = new HashSet<ContractInfo>();
            Inquiries = new HashSet<Inquiry>();
            Questionings = new HashSet<Questioning>();
            TenderInfors = new HashSet<TenderInfor>();
        }

        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }
        public DateTime? PlanBeginDateTime { get; set; }
        public DateTime? PlanCompleteDateTime { get; set; }
        public DateTime? ActualBeginDateTime { get; set; }
        public DateTime? ActualCompleteDateTime { get; set; }
        public decimal? BugetCollectAmountMoney { get; set; }
        public int? BudgetCollectCurrencyId { get; set; }
        public decimal? BudgetPayAmountMoney { get; set; }
        public int? BudgetPayCurrencyId { get; set; }
        public int? PrincipalUserId { get; set; }
        public int? Pstate { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public byte? WfState { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }
        public int? ProjectSource { get; set; }

        public virtual CurrencyManager BudgetCollectCurrency { get; set; }
        public virtual CurrencyManager BudgetPayCurrency { get; set; }
        public virtual DataDictionary Category { get; set; }
        public virtual UserInfor CreateUser { get; set; }
        public virtual UserInfor ModifyUser { get; set; }
        public virtual UserInfor PrincipalUser { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistories { get; set; }
        public virtual ICollection<ContractInfo> ContractInfos { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<Questioning> Questionings { get; set; }
        public virtual ICollection<TenderInfor> TenderInfors { get; set; }
    }
}
