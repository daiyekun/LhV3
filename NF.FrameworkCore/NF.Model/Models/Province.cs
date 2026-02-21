using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Province
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public byte? IsEnable { get; set; }
    }
}
