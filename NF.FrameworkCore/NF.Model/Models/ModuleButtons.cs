using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ModuleButtons
    {
        public int Id { get; set; }
        public int? ModuleId { get; set; }
        public int? ButtonId { get; set; }
    }
}
