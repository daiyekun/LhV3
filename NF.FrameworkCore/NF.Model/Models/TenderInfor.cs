using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class TenderInfor
    {
        public TenderInfor()
        {
            ContractInfos = new HashSet<ContractInfo>();
            OpeningSituationInfors = new HashSet<OpeningSituationInfor>();
            SuccessfulBidderLables = new HashSet<SuccessfulBidderLable>();
            TenderAttachments = new HashSet<TenderAttachment>();
            TendererNameLabels = new HashSet<TendererNameLabel>();
        }

        public int Id { get; set; }
        public int TenderUserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectNo { get; set; }
        public string Iocation { get; set; }
        public DateTime TenderDate { get; set; }
        public int ContractEnforcementDepId { get; set; }
        public string WinningConditions { get; set; }
        public int RecorderId { get; set; }
        public DateTime TenderExpirationDate { get; set; }
        public int TenderStatus { get; set; }
        public int IsDelete { get; set; }
        public byte? WfState { get; set; }
        public int CreateUserId { get; set; }
        public int? TenderType { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }
        public int? Zbdw { get; set; }
        public decimal? Zje { get; set; }

        public virtual UserInfor CreateUser { get; set; }
        public virtual ProjectManager Project { get; set; }
        public virtual Company ZbdwNavigation { get; set; }
        public virtual ICollection<ContractInfo> ContractInfos { get; set; }
        public virtual ICollection<OpeningSituationInfor> OpeningSituationInfors { get; set; }
        public virtual ICollection<SuccessfulBidderLable> SuccessfulBidderLables { get; set; }
        public virtual ICollection<TenderAttachment> TenderAttachments { get; set; }
        public virtual ICollection<TendererNameLabel> TendererNameLabels { get; set; }
    }
}
