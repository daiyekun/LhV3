using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class BusinessCategory
    {
        public BusinessCategory()
        {
            BcInstances = new HashSet<BcInstance>();
        }

        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte? IsDelete { get; set; }
        public int? SubCompId { get; set; }

        public virtual ICollection<BcInstance> BcInstances { get; set; }
    }
}
