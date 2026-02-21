using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class SuccessfulBidderLable
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public string SuccessName { get; set; }
        public int? SuccessUntiId { get; set; }
        public decimal SuccTotalPrice { get; set; }
        public decimal SuccUitprice { get; set; }
        public int? SuccId { get; set; }
        public int? IsDelete { get; set; }
        public int? Zbdwid { get; set; }

        public virtual OpeningSituationInfor SuccessUnti { get; set; }
        public virtual TenderInfor Tender { get; set; }
    }
}
