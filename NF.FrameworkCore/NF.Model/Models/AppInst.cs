using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class AppInst
    {
        public AppInst()
        {
            AppGroupUsers = new HashSet<AppGroupUser>();
        }

        public int Id { get; set; }
        public int? TempHistId { get; set; }
        public int? TempId { get; set; }
        public int? Version { get; set; }
        public int ObjType { get; set; }
        public int AppObjId { get; set; }
        public string AppObjName { get; set; }
        public string AppObjNo { get; set; }
        public decimal? AppObjAmount { get; set; }
        public int? AppObjCateId { get; set; }
        public int AppState { get; set; }
        public int? Mission { get; set; }
        public int? StartUserId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int? CurrentNodeId { get; set; }
        public string CurrentNodeStrId { get; set; }
        public string CurrentNodeName { get; set; }
        public DateTime? CompleteDateTime { get; set; }
        public int? NewInstId { get; set; }
        public byte? FinceType { get; set; }
        public int? AppSecObjId { get; set; }

        public virtual ICollection<AppGroupUser> AppGroupUsers { get; set; }
    }
}
