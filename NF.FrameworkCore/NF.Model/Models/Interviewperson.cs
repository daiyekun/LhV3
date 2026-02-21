using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Interviewperson
    {
        public int Id { get; set; }
        public int? Inquirer { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? Department { get; set; }
        public int? QuesId { get; set; }
        public int? IsDelete { get; set; }
        public string Kij { get; set; }

        public virtual Questioning Ques { get; set; }
    }
}
