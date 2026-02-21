using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
  public  class TendererNameLabelDTO
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string TeNameLabel { get; set; }
        public string Psition { get; set; }
        public int? TeDartment { get; set; }
        public string TeDartmentName { get; set; }

        public int IsDelete { get; set; }
    }
}
