using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class DisputeManager
    {
        public DisputeManager()
        {
            CaseManagers = new HashSet<CaseManager>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Urgent { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CompId { get; set; }
        public int HandlerId { get; set; }
        public DateTime HandDate { get; set; }
        public int ContId { get; set; }
        public int DisputeType { get; set; }
        public decimal Amount { get; set; }
        public string CompInfo { get; set; }
        public string Remark { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public int? IsDelete { get; set; }
        public int? Dstate { get; set; }

        public virtual Company Comp { get; set; }
        public virtual ContractInfo Cont { get; set; }
        public virtual UserInfor CreateUser { get; set; }
        public virtual DataDictionary DisputeTypeNavigation { get; set; }
        public virtual UserInfor Handler { get; set; }
        public virtual DataDictionary UrgentNavigation { get; set; }
        public virtual ICollection<CaseManager> CaseManagers { get; set; }
    }
}
