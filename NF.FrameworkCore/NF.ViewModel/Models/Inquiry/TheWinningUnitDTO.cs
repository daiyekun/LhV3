using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class TheWinningUnitDTO : TheWinningUnit
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int? WinningUnit { get; set; }
        public string WinningUnitName { get; set; }
        //public decimal? BidPrices { get; set; }
        //public decimal? BidPrice { get; set; }
        //public int BidUser { get; set; }
        public string BidUserName { get; set; }
        //public int? LnquiryId { get; set; }

        public string BidPricesthis{ get; set;}
        public string BidPricethis { get; set;}

}
}
