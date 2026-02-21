using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class GroupInfo
    {
        public GroupInfo()
        {
            FlowTempNodeInfoHists = new HashSet<FlowTempNodeInfoHist>();
            FlowTempNodeInfos = new HashSet<FlowTempNodeInfo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public byte Gstate { get; set; }
        public byte IsDelete { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }

        public virtual ICollection<FlowTempNodeInfoHist> FlowTempNodeInfoHists { get; set; }
        public virtual ICollection<FlowTempNodeInfo> FlowTempNodeInfos { get; set; }
    }
}
