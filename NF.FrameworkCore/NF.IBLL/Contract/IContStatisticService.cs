using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{
    /// <summary>
    /// 合同统计表计算
    /// </summary>
    public partial interface IContStatisticService
    {
        /// <summary>
        /// 设置统计
        /// </summary>
        /// <param name="No">合同编号</param>
        void SetContractTongJi(string No = "");
    }
}
