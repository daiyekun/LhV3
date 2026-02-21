using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel
{
   public class WxCountText
    {
       public int  Id { get; set; }
       public string Name { get; set; }
    /// <summary>
    /// 文本类型
    /// </summary>
    public string ContTxtType { get; set; }
        /// <summary>
        /// 扩展名称
        /// </summary>
        public string ExtenName { get; set; }
    }
}
