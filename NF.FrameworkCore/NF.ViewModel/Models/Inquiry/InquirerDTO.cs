using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public class InquirerDTO
    {
        public int Id { get; set; }
        public int? InqId { get; set; }
        public string InqName { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? Department { get; set; }
        public string DepartmentName { get; set; }
        public int? InquiryId { get; set; }
        public int? IsDelete { get; set; }
    }
}
