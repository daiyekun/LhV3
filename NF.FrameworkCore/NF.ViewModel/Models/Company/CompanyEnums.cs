using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同对方状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public  enum CompStateEnum
    {
        /// <summary>
        /// 未审核：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未审核")]
        Unreviewed = 0,
        /// <summary>
        /// 审核通过
        /// </summary>
        [EnumItem(Value = 1, Desc = "审核通过")]
        Audited = 1,
        /// <summary>
        /// 已终止
        /// </summary>
        [EnumItem(Value = 2, Desc = "已终止")]
        Terminated = 2,

    }
}
