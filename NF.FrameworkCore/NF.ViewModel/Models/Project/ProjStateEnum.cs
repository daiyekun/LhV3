using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{

    /// <summary>
    /// 项目状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum ProjStateEnum
    {
        /// <summary>
        ///未执行
        /// </summary>
        [EnumItem(Value = 0, Desc = "未执行")]
        Unexecuted = 0,
        /// <summary>
        ///执行中
        /// </summary>
        [EnumItem(Value = 1, Desc = "执行中")]
        Execution = 1,
        /// <summary>
        ///已完成
        /// </summary>
        [EnumItem(Value = 2, Desc = "已完成")]
        Completed = 2,
        /// <summary>
        ///已终止
        /// </summary>
        [EnumItem(Value = 3, Desc = "已终止")]
        Terminated = 3,
        /// <summary>
        ///已终止
        /// </summary>
        [EnumItem(Value = 4, Desc = "已作废")]
        Dozee = 4,
        /// <summary>
        ///审批通过
        /// </summary>
        [EnumItem(Value = 5, Desc = "审批通过")]
        Approve = 5,
    }
}
