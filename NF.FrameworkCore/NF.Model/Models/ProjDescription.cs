using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ProjDescription
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Pitem { get; set; }
        public string ProjContent { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

        public virtual UserInfor CreateUser { get; set; }
    }
}
