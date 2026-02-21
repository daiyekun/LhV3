using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContTxtTempVarStore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte? IsCustomer { get; set; }
        public byte? Isdelete { get; set; }
        public int? TempHistId { get; set; }
        public int? StoreType { get; set; }
        public int? OriginalId { get; set; }
    }
}
