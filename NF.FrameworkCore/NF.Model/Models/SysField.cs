using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class SysField
    {
        public int Id { get; set; }
        public string Lable { get; set; }
        public int? FieldType { get; set; }
        public string SelData { get; set; }
        public int? Required { get; set; }
        public int? IsList { get; set; }
        public int? Tag { get; set; }
        public int? Isqy { get; set; }
        public int? Zbpx { get; set; }
        public string Fname { get; set; }
    }
}
