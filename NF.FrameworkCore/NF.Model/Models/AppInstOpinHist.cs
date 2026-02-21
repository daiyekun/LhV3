using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class AppInstOpinHist
    {
        public int Id { get; set; }
        public int? InstHistId { get; set; }
        public int? NodeHistId { get; set; }
        public string NodeStrId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public string Opinion { get; set; }
        public int? Result { get; set; }
    }
}
