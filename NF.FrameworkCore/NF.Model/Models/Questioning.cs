using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Questioning
    {
        public Questioning()
        {
            Bidlabels = new HashSet<Bidlabel>();
            ContractInfos = new HashSet<ContractInfo>();
            Interviewpeople = new HashSet<Interviewpeople>();
            OpenBids = new HashSet<OpenBid>();
            QuestioningAttachments = new HashSet<QuestioningAttachment>();
            WinningQues = new HashSet<WinningQue>();
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
        public int? QueType { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }
        public int? Zbdw { get; set; }
        public decimal? Zje { get; set; }

        public virtual Department ContractExecuteBranchNavigation { get; set; }
        public virtual UserInfor CreateUser { get; set; }
        public virtual ProjectManager ProjectNameNavigation { get; set; }
        public virtual Company ZbdwNavigation { get; set; }
        public virtual ICollection<Bidlabel> Bidlabels { get; set; }
        public virtual ICollection<ContractInfo> ContractInfos { get; set; }
        public virtual ICollection<Interviewpeople> Interviewpeople { get; set; }
        public virtual ICollection<OpenBid> OpenBids { get; set; }
        public virtual ICollection<QuestioningAttachment> QuestioningAttachments { get; set; }
        public virtual ICollection<WinningQue> WinningQues { get; set; }
    }
}
