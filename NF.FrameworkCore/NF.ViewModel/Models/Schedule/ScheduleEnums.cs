using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 数据状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum ScheduleEnums
    {
        /// <summary>
        /// 未执行：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未完成")]
        State0 = 0,
        /// <summary>
        /// 执行中:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "已完成")]
        State1 = 1,
    }
}
