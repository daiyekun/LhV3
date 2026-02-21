using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContSubDelivery
    {
        public int Id { get; set; }
        public int? SubId { get; set; }
        public DateTime? PlanDateTime { get; set; }
        public DateTime? ActualDateTime { get; set; }
        public decimal? ThedeliveryAmount { get; set; }
        public int? Dstate { get; set; }
        public decimal? AmountMoney { get; set; }
        public int? Num { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public int? DevState { get; set; }

        public virtual ContSubjectMatter Sub { get; set; }
    }
}
