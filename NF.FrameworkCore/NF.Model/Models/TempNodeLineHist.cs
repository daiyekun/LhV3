using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class TempNodeLineHist
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public int? TempHistId { get; set; }
        public int? TempId { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public byte? Dash { get; set; }
        public byte? Marked { get; set; }
        public byte? Alt { get; set; }
        public double? M { get; set; }
    }
}
