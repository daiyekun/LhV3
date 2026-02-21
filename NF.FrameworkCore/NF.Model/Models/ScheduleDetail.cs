using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ScheduleDetail
    {
        public ScheduleDetail()
        {
            ScheduleDetailAttachments = new HashSet<ScheduleDetailAttachment>();
        }

        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public int ScheduleSer { get; set; }
        public string Description { get; set; }
        public string Pdescription { get; set; }
        public DateTime? PddateTime { get; set; }
        public int? Wancheng { get; set; }
        public byte? State { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public byte? IsDelete { get; set; }

        public virtual ScheduleManagement ScheduleSerNavigation { get; set; }
        public virtual ICollection<ScheduleDetailAttachment> ScheduleDetailAttachments { get; set; }
    }
}
