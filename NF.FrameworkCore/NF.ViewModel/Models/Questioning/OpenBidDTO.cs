using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class OpenBidDTO: OpenBid
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int? Unit { get; set; }
        public string UnitName { get; set; }
        //public decimal? TotalPrices { get; set; }
        //public decimal? UnitPrice { get; set; }
        //public int? Personnel { get; set; }
        public string PersonneName { get; set; }
        //public int? QuesId { get; set; }
        //public int? IsDelete { get; set; }
        public string TotalPricesthis { get; set; }
        public string UnitPricethis { get; set; }
    }
}
