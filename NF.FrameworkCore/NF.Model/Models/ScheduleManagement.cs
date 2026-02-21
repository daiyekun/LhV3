using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ScheduleManagement
    {
        public ScheduleManagement()
        {
            ScheduleDetails = new HashSet<ScheduleDetail>();
            ScheduleLists = new HashSet<ScheduleList>();
            ScheduleManagementAttachments = new HashSet<ScheduleManagementAttachment>();
        }

        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public string ScheduleSer { get; set; }
        public int Priority { get; set; }
        public int ScheduleAttribution { get; set; }
        public int? ScheduleDuixiang { get; set; }
        public string Description { get; set; }
        public int Designee { get; set; }
        public int Stalker { get; set; }
        public DateTime? JhCreateDateTime { get; set; }
        public DateTime? JhCompleteDateTime { get; set; }
        public DateTime? SjCreateDateTime { get; set; }
        public DateTime? SjCompleteDateTime { get; set; }
        public byte? State { get; set; }
        public int? Wancheng { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public DateTime? Myjdtime { get; set; }
        public int? Mystate { get; set; }
        public DateTime? Gzdatetime { get; set; }
        public int? Gzstate { get; set; }

        public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; }
        public virtual ICollection<ScheduleList> ScheduleLists { get; set; }
        public virtual ICollection<ScheduleManagementAttachment> ScheduleManagementAttachments { get; set; }
    }
}
