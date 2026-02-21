using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ProjSchedule
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Pitem { get; set; }
        public DateTime? PlanBeginDateTime { get; set; }
        public DateTime? PlanCompleteDateTime { get; set; }
        public DateTime? ActualBeginDateTime { get; set; }
        public DateTime? ActualCompleteDateTime { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

        public virtual UserInfor CreateUser { get; set; }
    }
}
