using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class OptionLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Remark { get; set; }
        public string RequestUrl { get; set; }
        public byte? RequestMethod { get; set; }
        public string RequestData { get; set; }
        public string RequestIp { get; set; }
        public string RequestNetIp { get; set; }
        public double? TotalTime { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public byte? Status { get; set; }
        public string ActionTitle { get; set; }
        public string ExecResult { get; set; }
        public byte? OptionType { get; set; }

        public virtual UserInfor User { get; set; }
    }
}
