using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class UserModule
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ModuleId { get; set; }
    }
}
