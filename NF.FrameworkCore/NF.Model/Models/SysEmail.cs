using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class SysEmail
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public int? ServicePort { get; set; }
        public string SenderMail { get; set; }
        public string SendNickname { get; set; }
        public string MailPwd { get; set; }
    }
}
