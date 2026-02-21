using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
   public  class WxProjAttachmen
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public string ProjFileType { get; set; }
    }
}
