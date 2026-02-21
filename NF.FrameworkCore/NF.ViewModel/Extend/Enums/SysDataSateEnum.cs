using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Extend.Enums
{
   [EnumClass(Max = 15, Min = 0, None = -1)] 
    /// <summary>
    /// 系统数据状态
    /// </summary>
    public enum SysDataSateEnum
    {
        /// <summary>
        ///未审核
        /// </summary>
        [EnumItem(Value = 0, Desc = "未审核")]
        Unreviewed = 0,
        /// <summary>
        ///审核通过
        /// </summary>
        [EnumItem(Value = 1, Desc = "审核通过")]
        Verified = 1,

    }
}
