using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class TheWinningUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? WinningUnit { get; set; }
        public decimal? BidPrices { get; set; }
        public decimal? BidPrice { get; set; }
        public int? BidUser { get; set; }
        public int? LnquiryId { get; set; }
        public int? IsDelete { get; set; }
        public int? Zbdwid { get; set; }
        public string Lxr { get; set; }
        public string Lxfs { get; set; }

        public virtual Inquiry Lnquiry { get; set; }
        public virtual OpenTenderCondition WinningUnitNavigation { get; set; }
    }
}
