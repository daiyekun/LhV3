using System;
using System.Collections.Generic;

#nullable disable

namespace NF.Model.Models
{
    public partial class AppMsg
    {
        public int Id { get; set; }
        public int MsgType { get; set; }
        public string MsgRemark { get; set; }
        public int? InstId { get; set; }
        public int? NodeId { get; set; }
        public int? UserId { get; set; }
        public DateTime? MsgDate { get; set; }
        public int MsgState { get; set; }
        public int TxSum { get; set; }
    }
}
