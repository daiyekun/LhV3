using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class AppInstNodeHist
    {
        public int Id { get; set; }
        public int? InstHistId { get; set; }
        public int? TempHistId { get; set; }
        public string NodeStrId { get; set; }
        public string Name { get; set; }
        public int? Left { get; set; }
        public int? Top { get; set; }
        public int? Type { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public byte? Alt { get; set; }
        public byte? Marked { get; set; }
        public int? Norder { get; set; }
        public int NodeState { get; set; }
        public DateTime? ReceDateTime { get; set; }
        public DateTime? CompDateTime { get; set; }
    }
}
