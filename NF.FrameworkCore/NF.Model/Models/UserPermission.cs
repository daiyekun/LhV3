using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class UserPermission
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FuncId { get; set; }
        public string FuncCode { get; set; }
        public byte? FuncType { get; set; }
        public string DeptIds { get; set; }
        public int? ModeId { get; set; }
    }
}
