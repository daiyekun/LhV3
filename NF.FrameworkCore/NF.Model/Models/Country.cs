using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public byte? IsEnable { get; set; }
    }
}
