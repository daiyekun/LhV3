using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class GroupUser
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
    }
}
