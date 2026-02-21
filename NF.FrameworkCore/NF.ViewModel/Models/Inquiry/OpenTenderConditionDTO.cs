using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public class OpenTenderConditionDTO: OpenTenderCondition
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int? Unit { get; set; }
        //public decimal? TotalPrices { get; set; }
        //public decimal? UnitPrice { get; set; }
        //public int? Personnel { get; set; }
        //public int? LnquiryId { get; set; }
        //public int? IsDelete { get; set; }
        public string UnitName { get; set; }
        /// <summary>
        /// 合同对方id
        /// </summary>
        public int UnitId { get; set; }
        public string TotalPricesthis { get; set; }
        public string UnitPricethis { get; set; }
        public string PersonneName { get; set; }
    }
}
