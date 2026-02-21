using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同来源统计
    /// </summary>
   public class HtLyTj
    {
        /// <summary>
        /// 招标合同-收
        /// </summary>
        public int Szbht { get; set; }
        /// <summary>
        /// 询价合同-收
        /// </summary>
        public int Sxjht { get; set; }
        /// <summary>
        /// 指定合同-收
        /// </summary>
        public int Szdht { get; set; }
        /// <summary>
        /// 招标合同-付
        /// </summary>
        public int Fzbht { get; set; }
        /// <summary>
        /// 询价合同-付
        /// </summary>
        public int Fxjht { get; set; }
        /// <summary>
        /// 指定合同-付
        /// </summary>
        public int Fzdht { get; set; }

    }
}
