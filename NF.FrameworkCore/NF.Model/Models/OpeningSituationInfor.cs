using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class OpeningSituationInfor
    {
        public OpeningSituationInfor()
        {
            SuccessfulBidderLables = new HashSet<SuccessfulBidderLable>();
        }

        public int Id { get; set; }
        public int TenderId { get; set; }
        public string OpenSituationName { get; set; }
        public int? Unit { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Uitprice { get; set; }
        public int UserId { get; set; }
        public int IsDelete { get; set; }
        public int? LnquiryId { get; set; }
        public int? OpenId { get; set; }

        public virtual Inquiry Lnquiry { get; set; }
        public virtual TenderInfor Tender { get; set; }
        public virtual Company UnitNavigation { get; set; }
        public virtual ICollection<SuccessfulBidderLable> SuccessfulBidderLables { get; set; }
    }
}
