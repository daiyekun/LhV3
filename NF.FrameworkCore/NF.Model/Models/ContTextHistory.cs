using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContTextHistory
    {
        public int Id { get; set; }
        public int? ContTxtId { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public int? TemplateId { get; set; }
        public int? CategoryId { get; set; }
        public string Remark { get; set; }
        public int? DownloadTimes { get; set; }
        public int? ContId { get; set; }
        public int? ContHisId { get; set; }
        public int? Stage { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public byte? IsFromTemp { get; set; }
        public int? Versions { get; set; }
        public string ElectronicVersionPath { get; set; }
        public int? Archivedcopies { get; set; }
        public int? Borrowedcopies { get; set; }
        public string WordPath { get; set; }
        public int? TextLock { get; set; }
        public DateTime? LockTime { get; set; }
        public string GuidFileName { get; set; }
        public string FolderName { get; set; }
        public string ExtenName { get; set; }

        public virtual ContractInfoHistory ContHis { get; set; }
        public virtual ContTxtTemplateHist Template { get; set; }
    }
}
