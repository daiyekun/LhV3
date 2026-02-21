using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class CaseManager
    {
        public CaseManager()
        {
            ActFinceFiles = new HashSet<ActFinceFile>();
            AjglAttachments = new HashSet<AjglAttachment>();
            AjglSpwsAttachments = new HashSet<AjglSpwsAttachment>();
            AjglSsBqAttachments = new HashSet<AjglSsBqAttachment>();
            AjglXgLxes = new HashSet<AjglXgLx>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Urgent { get; set; }
        public string DisName { get; set; }
        public string DisCode { get; set; }
        public int DisId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime HandDate { get; set; }
        public int ContId { get; set; }
        public int? HandlerId { get; set; }
        public string WoDw { get; set; }
        public string Remark { get; set; }
        public int? DisputeType { get; set; }
        public string Pawn { get; set; }
        public decimal? Amount { get; set; }
        public string AmoutDisc { get; set; }
        public string DisDic { get; set; }
        public string CaseRemark { get; set; }
        public string CaseBrief { get; set; }
        public int? CaseId { get; set; }
        public string CaseFileName { get; set; }
        public string CaseFilePath { get; set; }
        public string CourtName { get; set; }
        public DateTime? BeginDate { get; set; }
        public string SpResult { get; set; }
        public string SpFileName { get; set; }
        public string SpFilePath { get; set; }
        public string LawyerName { get; set; }
        public string HandRemark { get; set; }
        public int? PreType { get; set; }
        public string PreFileName { get; set; }
        public string PreFilePath { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public int? IsDelete { get; set; }
        public int? Dstate { get; set; }

        public virtual UserInfor CreateUser { get; set; }
        public virtual DisputeManager Dis { get; set; }
        public virtual DataDictionary DisputeTypeNavigation { get; set; }
        public virtual DataDictionary PreTypeNavigation { get; set; }
        public virtual DataDictionary UrgentNavigation { get; set; }
        public virtual ICollection<ActFinceFile> ActFinceFiles { get; set; }
        public virtual ICollection<AjglAttachment> AjglAttachments { get; set; }
        public virtual ICollection<AjglSpwsAttachment> AjglSpwsAttachments { get; set; }
        public virtual ICollection<AjglSsBqAttachment> AjglSsBqAttachments { get; set; }
        public virtual ICollection<AjglXgLx> AjglXgLxes { get; set; }
    }
}
