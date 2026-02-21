using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class InvoDescription
    {
        public int Id { get; set; }
        public int? ContInvoId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Count { get; set; }
        public decimal? Total { get; set; }
        public byte? IsDelete { get; set; }
    }
}
