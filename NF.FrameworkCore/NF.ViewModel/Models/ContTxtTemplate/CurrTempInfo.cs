using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 当前模板信息
    /// </summary>
   public  class CurrTempInfo
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TempId { get; set; }
        /// <summary>
        /// 模板历史ID
        /// </summary>
        public int TempHistId { get; set; }
    }
}
