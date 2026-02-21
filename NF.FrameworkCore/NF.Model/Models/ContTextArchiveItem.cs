using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContTextArchiveItem
    {
        public int Id { get; set; }
        public int? ArchiveId { get; set; }
        public int? ContTextId { get; set; }
        public int? ArcNumber { get; set; }
        public string ArcRemark { get; set; }
        public string ArcCode { get; set; }
        public string ArcCabCode { get; set; }
        public string SubUser { get; set; }
        public DateTime? SubDateTime { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string GuidFileName { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
}
