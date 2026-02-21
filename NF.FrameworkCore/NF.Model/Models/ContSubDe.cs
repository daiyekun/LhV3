using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContSubDe
    {
        public int Id { get; set; }
        public int? SubDeliverId { get; set; }
        public int? DeliverType { get; set; }
        public string DeliverLocation { get; set; }
        public int? DeliverUserId { get; set; }
        public DateTime? ActualDateTime { get; set; }
        public string GuidFileName { get; set; }
        public string FolderName { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public int? SubId { get; set; }
        public decimal? CurrDevNumber { get; set; }
        public decimal? NotDevNumber { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte? Dstate { get; set; }
        public byte? IsDelete { get; set; }

        public virtual UserInfor CreateUser { get; set; }
        public virtual DataDictionary DeliverTypeNavigation { get; set; }
        public virtual UserInfor DeliverUser { get; set; }
        public virtual ContSubjectMatter Sub { get; set; }
    }
}
