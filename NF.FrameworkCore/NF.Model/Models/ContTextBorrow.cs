using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContTextBorrow
    {
        public int Id { get; set; }
        public int? ContTextId { get; set; }
        public string BorrUser { get; set; }
        public DateTime? BorrDateTime { get; set; }
        public string BorrDeptName { get; set; }
        public string BorrRemark { get; set; }
        public int? BorrHandlerUser { get; set; }
        public int? BorrNumber { get; set; }
        public int? RepayNumber { get; set; }
        public int? RepayHandlerUser { get; set; }
        public DateTime? RepayDateTime { get; set; }
        public string RepayUser { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
}
