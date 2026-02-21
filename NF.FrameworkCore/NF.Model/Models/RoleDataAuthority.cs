using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class RoleDataAuthority
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? AuthorityType { get; set; }
    }
}
