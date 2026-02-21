using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ScheduleList
    {
        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public int ScheduleAttribution { get; set; }
        public int? ScheduleDuixiang { get; set; }
        public string Description { get; set; }
        public string Descriptionms { get; set; }
        public int Tixing { get; set; }
        public int Designee { get; set; }
        public int Stalker { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public int? ScheduleId { get; set; }
        public byte? IsDelete { get; set; }
        public DateTime? Myjdtime { get; set; }
        public int? Mystate { get; set; }

        public virtual ScheduleManagement Schedule { get; set; }
    }
}
