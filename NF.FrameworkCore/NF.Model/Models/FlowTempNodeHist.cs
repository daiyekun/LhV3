using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class FlowTempNodeHist
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public int? TempHistId { get; set; }
        public int? TempId { get; set; }
        public string Name { get; set; }
        public int? Left { get; set; }
        public int? Top { get; set; }
        public int? Type { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public byte? Alt { get; set; }
        public byte? Marked { get; set; }
    }
}
