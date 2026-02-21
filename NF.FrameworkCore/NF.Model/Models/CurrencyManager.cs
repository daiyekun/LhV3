using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class CurrencyManager
    {
        public CurrencyManager()
        {
            ContractInfos = new HashSet<ContractInfo>();
            ProjectManagerBudgetCollectCurrencies = new HashSet<ProjectManager>();
            ProjectManagerBudgetPayCurrencies = new HashSet<ProjectManager>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Abbreviation { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

        public virtual ICollection<ContractInfo> ContractInfos { get; set; }
        public virtual ICollection<ProjectManager> ProjectManagerBudgetCollectCurrencies { get; set; }
        public virtual ICollection<ProjectManager> ProjectManagerBudgetPayCurrencies { get; set; }
    }
}
