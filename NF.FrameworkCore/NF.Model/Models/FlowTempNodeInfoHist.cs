using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class FlowTempNodeInfoHist
    {
        public int Id { get; set; }
        public int? TempHistId { get; set; }
        public int? TempId { get; set; }
        public string NodeStrId { get; set; }
        public int? Nrule { get; set; }
        public int? ReviseText { get; set; }
        public int? GroupId { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public int? IsMin { get; set; }
        public int? IsMax { get; set; }

        public virtual GroupInfo Group { get; set; }
    }
}
