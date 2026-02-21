using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Inquiry
    {
        public Inquiry()
        {
            ContractInfos = new HashSet<ContractInfo>();
            Inquirers = new HashSet<Inquirer>();
            InquiryAttachments = new HashSet<InquiryAttachment>();
            OpenTenderConditions = new HashSet<OpenTenderCondition>();
            OpeningSituationInfors = new HashSet<OpeningSituationInfor>();
            TheWinningUnits = new HashSet<TheWinningUnit>();
            WinningInqs = new HashSet<WinningInq>();
        }

        public int Id { get; set; }
        public byte? Inquirer { get; set; }
        public int? ProjectName { get; set; }
        public string ProjectNumber { get; set; }
        public string Sites { get; set; }
        public DateTime? Times { get; set; }
        public int? ContractExecuteBranch { get; set; }
        public string TheWinningConditions { get; set; }
        public byte? Recorder { get; set; }
        public DateTime? UsefulLife { get; set; }
        public byte? IsDelete { get; set; }
        public byte? InState { get; set; }
        public byte? WfState { get; set; }
        public int? CreateUserId { get; set; }
        public int? ModifyUserId { get; set; }
        public int? InquiryType { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }
        public int? Zbdw { get; set; }
        public decimal? Zje { get; set; }

        public virtual Department ContractExecuteBranchNavigation { get; set; }
        public virtual UserInfor CreateUser { get; set; }
        public virtual ProjectManager ProjectNameNavigation { get; set; }
        public virtual Company ZbdwNavigation { get; set; }
        public virtual ICollection<ContractInfo> ContractInfos { get; set; }
        public virtual ICollection<Inquirer> Inquirers { get; set; }
        public virtual ICollection<InquiryAttachment> InquiryAttachments { get; set; }
        public virtual ICollection<OpenTenderCondition> OpenTenderConditions { get; set; }
        public virtual ICollection<OpeningSituationInfor> OpeningSituationInfors { get; set; }
        public virtual ICollection<TheWinningUnit> TheWinningUnits { get; set; }
        public virtual ICollection<WinningInq> WinningInqs { get; set; }
    }
}
