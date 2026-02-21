using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContTextSeal
    {
        public int Id { get; set; }
        public int ContTextId { get; set; }
        public int? SealId { get; set; }
        public string SealUser { get; set; }
        public int SealState { get; set; }
        public int? SealNumber { get; set; }
        public int? EachNumber { get; set; }
        public int? SealTotal { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

        public virtual ContText ContText { get; set; }
        public virtual SealManager Seal { get; set; }
    }
}
