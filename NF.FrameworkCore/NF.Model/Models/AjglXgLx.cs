using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class AjglXgLx
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fznr { get; set; }
        public int? Ajglid { get; set; }

        public virtual CaseManager Ajgl { get; set; }
    }
}
