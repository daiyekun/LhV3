using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class AppInstOpin
    {
        public int Id { get; set; }
        public int? InstId { get; set; }
        public int? NodeId { get; set; }
        public string NodeStrId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public string Opinion { get; set; }
        public int? Result { get; set; }

        public virtual AppInstNode Node { get; set; }
    }
}
