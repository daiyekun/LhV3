using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContTxtTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public int? TepType { get; set; }
        public int? TextType { get; set; }
        public string DeptIds { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public int? Vesion { get; set; }
        public int? Tstate { get; set; }
        public int? FieldType { get; set; }
        public byte? WordEdit { get; set; }
        public int? ShowType { get; set; }
        public int? ShowTypeNumber { get; set; }
        public string MingXiTitle { get; set; }
        public int? SubcompId { get; set; }
        public int? ShowSub { get; set; }
        public string TepTypes { get; set; }

        public virtual UserInfor CreateUser { get; set; }
        public virtual DataDictionary TepTypeNavigation { get; set; }
        public virtual DataDictionary TextTypeNavigation { get; set; }
    }
}
