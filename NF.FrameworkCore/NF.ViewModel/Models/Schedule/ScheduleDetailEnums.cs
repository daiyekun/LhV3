using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models.Schedule
{
    /// <summary>
    /// 数据状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
   public enum ScheduleDetailEnums
    {
        /// <summary>
        /// 未确认：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未确认")]
        State0 = 0,
        /// <summary>
        /// 已确认:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "已确认")]
        State1 = 1,
        /// <summary>
        /// 已确认:2
        /// </summary>
        [EnumItem(Value = 2, Desc = "已通过")]
        State2 = 2,
        /// <summary>
        /// 已确认:3
        /// </summary>
        [EnumItem(Value = 3, Desc = "未通过")]
        State3 = 3,
    }
}
