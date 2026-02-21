using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContDescription
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public string Citem { get; set; }
        public string Ccontent { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public int IsDelete { get; set; }
    }
}
