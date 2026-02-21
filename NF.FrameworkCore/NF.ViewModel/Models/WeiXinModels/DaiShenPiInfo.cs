using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{

    /// <summary>
    /// 待审批信息
    /// </summary>
    public class DaiShenPiInfo
    {
        /// <summary>
        /// 待处理总数
        /// </summary>
        public int RowCount { get; set; } = 0;
        /// <summary>
        /// 下一条ID
        /// </summary>
        public int NextId { get; set; } = 0;

    }
}
