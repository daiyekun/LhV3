using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class AppGroupUser
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public int? InstId { get; set; }
        public string NodeStrId { get; set; }
        public int? NodeId { get; set; }
        public int? NinfoId { get; set; }
        public int? UserIsSp { get; set; }

        public virtual AppInst Inst { get; set; }
        public virtual AppInstNodeInfo Ninfo { get; set; }
        public virtual AppInstNode Node { get; set; }
    }
}
