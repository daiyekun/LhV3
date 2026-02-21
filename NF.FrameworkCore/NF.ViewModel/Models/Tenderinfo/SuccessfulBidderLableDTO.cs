using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class SuccessfulBidderLableDTO : SuccessfulBidderLable
    {
        //public int Id { get; set; }
        //public int TenderId { get; set; }
        //public string SuccessName { get; set; }
        //public int SuccessUntiId { get; set; }
        public string SuccessUntiName { get; set; }
        //public decimal SuccTotalPrice { get; set; }
        //public decimal SuccUitprice { get; set; }
        //public int? SuccId { get; set; }
        public string SuccName { get; set; }
        // public int? IsDelete { get; set; }
        public string SuccTotalPricethis { get; set; }
        public string SuccUitpricethis { get; set; }

      public int  SuccessUntiIds{get;set;}
    }
}
