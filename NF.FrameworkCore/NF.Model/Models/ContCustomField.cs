using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContCustomField
    {
        public int Id { get; set; }
        public int? FieldId { get; set; }
        public string FieldVale { get; set; }
        public int? ContId { get; set; }
    }
}
