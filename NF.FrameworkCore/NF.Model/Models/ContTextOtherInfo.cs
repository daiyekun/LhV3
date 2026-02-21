using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContTextOtherInfo
    {
        public int Id { get; set; }
        public int? ContTextId { get; set; }
        public int? ArcSumNumber { get; set; }
        public int? BorrSumNumber { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
}
