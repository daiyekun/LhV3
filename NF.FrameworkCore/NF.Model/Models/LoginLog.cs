using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class LoginLog
    {
        public int Id { get; set; }
        public int? LoginUserId { get; set; }
        public string RequestNetIp { get; set; }
        public string LoginIp { get; set; }
        public byte? Result { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public byte? Status { get; set; }

        public virtual UserInfor LoginUser { get; set; }
    }
}
