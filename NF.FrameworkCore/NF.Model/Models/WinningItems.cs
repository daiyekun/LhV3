using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class WinningItems
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public string WinningName { get; set; }
        public string WinningUntiId { get; set; }
        public decimal WinningTotalPrice { get; set; }
        public decimal WinningUitprice { get; set; }
        public decimal WinningQuantity { get; set; }
        public string WinningModel { get; set; }
        public int IsDelete { get; set; }
        public int? SessionCurrUserId { get; set; }
        public int? ModifyUserId { get; set; }
        public string GuidFileName { get; set; }
        public string SourceFileName { get; set; }
    }
}
