using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class FlowTempNodeCondHist
    {
        public int Id { get; set; }
        public int? NodeId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ObjType { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public int? IsMin { get; set; }
        public int? IsMax { get; set; }
        public decimal? Min1 { get; set; }
        public decimal? Max1 { get; set; }
        public int? IsMin1 { get; set; }
        public int? IsMax1 { get; set; }
        public decimal? Min2 { get; set; }
        public decimal? Max2 { get; set; }
        public int? IsMin2 { get; set; }
        public int? IsMax2 { get; set; }
        public string CategroyIds { get; set; }
        public string CategoryIds1 { get; set; }
        public string CategoryIds2 { get; set; }
        public string FlowIte { get; set; }
    }
}
