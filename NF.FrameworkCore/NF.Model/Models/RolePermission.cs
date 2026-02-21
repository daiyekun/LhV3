using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? FuncId { get; set; }
        public string FuncCode { get; set; }
        public byte? FuncType { get; set; }
        public string DeptIds { get; set; }
        public int? ModeId { get; set; }
    }
}
