using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class FlowTemp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Version { get; set; }
        public byte? IsValid { get; set; }
        public int? ObjType { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public string DeptIds { get; set; }
        public string CategoryIds { get; set; }
        public string FlowItems { get; set; }

        public virtual UserInfor CreateUser { get; set; }
    }
}
