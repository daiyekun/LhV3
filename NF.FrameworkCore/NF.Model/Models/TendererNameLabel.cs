using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class TendererNameLabel
    {
        public int Id { get; set; }
        public int? TenderId { get; set; }
        public int? UserId { get; set; }
        public string TeNameLabel { get; set; }
        public string Psition { get; set; }
        public int? TeDartment { get; set; }
        public int IsDelete { get; set; }

        public virtual TenderInfor Tender { get; set; }
    }
}
