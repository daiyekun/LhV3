using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContTxtTempAndSubField
    {
        public int Id { get; set; }
        public int? SubFieldId { get; set; }
        public int? TempHistId { get; set; }
        public string Sval { get; set; }
        public byte? IsTotal { get; set; }
        public int? FieldType { get; set; }
        public int? BcId { get; set; }
        public int? SortFd { get; set; }
    }
}
