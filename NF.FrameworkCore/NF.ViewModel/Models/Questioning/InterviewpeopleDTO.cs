using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public class InterviewpeopleDTO
    {
        public int Id { get; set; }
        public int? Inquirer { get; set; }
        public string InquirerName { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? Department { get; set; }
        public string DepartmentName { get; set; }
        public int? QuesId { get; set; }
        public int? IsDelete { get; set; }
    }
}
