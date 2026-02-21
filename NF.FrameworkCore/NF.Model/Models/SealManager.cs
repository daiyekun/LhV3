using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class SealManager
    {
        public SealManager()
        {
            ContTextSeals = new HashSet<ContTextSeal>();
        }

        public int Id { get; set; }
        public int? MainDeptId { get; set; }
        public string SealName { get; set; }
        public string SealCode { get; set; }
        public int? UserId { get; set; }
        public int? DeptId { get; set; }
        public DateTime EnabledDate { get; set; }
        public byte SealState { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

        public virtual ICollection<ContTextSeal> ContTextSeals { get; set; }
    }
}
