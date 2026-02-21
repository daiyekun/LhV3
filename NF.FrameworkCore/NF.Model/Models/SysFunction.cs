using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class SysFunction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fcode { get; set; }
        public string Remark { get; set; }
        public byte? IsDelete { get; set; }
        public int? ModeId { get; set; }

        public virtual SysModel Mode { get; set; }
    }
}
