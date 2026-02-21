using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Inquirer
    {
        public int Id { get; set; }
        public int? InqId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? Department { get; set; }
        public int? InquiryId { get; set; }
        public int? IsDelete { get; set; }

        public virtual Inquiry Inquiry { get; set; }
    }
}
