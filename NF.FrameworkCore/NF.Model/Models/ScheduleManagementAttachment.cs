using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ScheduleManagementAttachment
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string Remark { get; set; }
        public int? DownloadTimes { get; set; }
        public int? SchedulemId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public string FolderName { get; set; }
        public string GuidFileName { get; set; }

        public virtual ScheduleManagement Schedulem { get; set; }
    }
}
