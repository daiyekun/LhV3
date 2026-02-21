using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class OpenTenderCondition
    {
        public OpenTenderCondition()
        {
            TheWinningUnits = new HashSet<TheWinningUnit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Unit { get; set; }
        public decimal? TotalPrices { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Personnel { get; set; }
        public int? LnquiryId { get; set; }
        public int? IsDelete { get; set; }
        public int? OpenId { get; set; }
        public string Lxr { get; set; }
        public string Lxfs { get; set; }

        public virtual Inquiry Lnquiry { get; set; }
        public virtual Company UnitNavigation { get; set; }
        public virtual ICollection<TheWinningUnit> TheWinningUnits { get; set; }
    }
}
