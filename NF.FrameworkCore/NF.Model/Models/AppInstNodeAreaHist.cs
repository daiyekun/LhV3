using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class AppInstNodeAreaHist
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public int? InstHistId { get; set; }
        public string Name { get; set; }
        public int? Left { get; set; }
        public int? Top { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Color { get; set; }
        public byte? Alt { get; set; }
    }
}
