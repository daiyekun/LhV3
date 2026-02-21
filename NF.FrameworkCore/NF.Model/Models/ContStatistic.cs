using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContStatistic
    {
        public ContStatistic()
        {
            ContractInfos = new HashSet<ContractInfo>();
        }

        public int Id { get; set; }
        public int? ContId { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? CompInAm { get; set; }
        public decimal? CompActAm { get; set; }
        public decimal? CompRatio { get; set; }
        public decimal? BalaTick { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }

        public virtual ICollection<ContractInfo> ContractInfos { get; set; }
    }
}
