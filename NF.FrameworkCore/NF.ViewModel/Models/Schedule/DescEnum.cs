using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models.Schedule
{ /// <summary>
  /// 数据状态
  /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
  public  enum DescEnum
    {
        /// <summary>
        /// 等待：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "等待")]
        State0 = 0,
        /// <summary>
        /// 已提醒:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "已提醒")]
        State1 = 1,
        /// <summary>
        /// 未执行：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "未执行")]
        State2 = 2,
        /// <summary>
        /// 已完成：3
        /// </summary>
        [EnumItem(Value = 3, Desc = "已完成")]
        State3 = 3,
        /// <summary>
        /// 已关闭:4
        /// </summary>
        [EnumItem(Value = 4, Desc = "已关闭")]
        State4 = 4,

    }

}
