using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class AppInstNodeInfo
    {
        public AppInstNodeInfo()
        {
            AppGroupUsers = new HashSet<AppGroupUser>();
        }

        public int Id { get; set; }
        public int? InstId { get; set; }
        public int? InstNodeId { get; set; }
        public string NodeStrId { get; set; }
        public int? Nrule { get; set; }
        public int? ReviseText { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public int? IsMin { get; set; }
        public int? IsMax { get; set; }
        public int? NodeState { get; set; }
        public int? SubmitMsg { get; set; }
        public int? Isktz { get; set; }
        public string UsIds { get; set; }

        public virtual ICollection<AppGroupUser> AppGroupUsers { get; set; }
    }
}
