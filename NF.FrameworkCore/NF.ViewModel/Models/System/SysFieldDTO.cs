using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel
{
   public class SysFieldDTO: SysField
    {
        public string FieldTypeName { get; set; }
        public string RequiredName { get; set; }
        public string IsListName { get; set; }
        public string TagName { get; set; }
       
    }
}
