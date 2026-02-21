using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class OpenBid
    {
        public OpenBid()
        {
            Bidlabels = new HashSet<Bidlabel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Unit { get; set; }
        public decimal? TotalPrices { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Personnel { get; set; }
        public int? QuesId { get; set; }
        public int? IsDelete { get; set; }
        public int? OpenId { get; set; }

        public virtual Questioning Ques { get; set; }
        public virtual Company UnitNavigation { get; set; }
        public virtual ICollection<Bidlabel> Bidlabels { get; set; }
    }
}
