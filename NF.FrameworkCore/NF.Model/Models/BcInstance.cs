using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class BcInstance
    {
        public BcInstance()
        {
            ContSubjectMatters = new HashSet<ContSubjectMatter>();
        }

        public int Id { get; set; }
        public int? LbId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public decimal? Price { get; set; }
        public byte? Pro { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

        public virtual UserInfor CreateUser { get; set; }
        public virtual BusinessCategory Lb { get; set; }
        public virtual UserInfor ModifyUser { get; set; }
        public virtual ICollection<ContSubjectMatter> ContSubjectMatters { get; set; }
    }
}
