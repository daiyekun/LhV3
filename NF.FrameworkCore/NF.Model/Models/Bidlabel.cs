using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Bidlabel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? WinningUnit { get; set; }
        public decimal? BidPrices { get; set; }
        public decimal? BidPrice { get; set; }
        public int? BidUser { get; set; }
        public int? QuesId { get; set; }
        public int? Bidlabel1 { get; set; }
        public int? Zbdwid { get; set; }

        public virtual Questioning Ques { get; set; }
        public virtual OpenBid WinningUnitNavigation { get; set; }
    }
}
