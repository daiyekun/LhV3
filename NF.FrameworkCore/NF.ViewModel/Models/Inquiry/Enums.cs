using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 数据状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum ZXYStateEnums
    {
        /// <summary>
        /// 未执行：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未执行")]
        State0 = 0,
        /// <summary>
        /// 执行中:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "执行中")]
        State1 = 1,
    }
}
