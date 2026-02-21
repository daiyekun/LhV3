using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
   public class HtjeTj
    {
        /// <summary>
        /// 年份list
        /// </summary>
        public List<string> time { get; set; }
        /// <summary>
        /// 签约金额list
        /// </summary>
        public List<int> Qyje { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public List<int> Ysje { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        public List<int> Ssje { get; set; }

    }
}
