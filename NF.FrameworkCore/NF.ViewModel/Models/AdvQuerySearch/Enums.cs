using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.AdvQuerySearch
{
    /// <summary>
    /// 取值范围equal，like,between,start,end,unequal,empty 或者为null（如果为null则等价equal）
    /// </summary>
    /// 
    [EnumClass(Max = 10, Min = 0, None = -1)]
    public enum ConditionOptionValEnum
    {
        /// <summary>
        /// equal：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "equal")]
        Equal = 0,
        /// <summary>
        /// like
        /// </summary>
        [EnumItem(Value = 1, Desc = "like")]
        Like = 1,
        /// <summary>
        /// between
        /// </summary>
        [EnumItem(Value = 2, Desc = "between")]
        Between = 2,
        /// <summary>
        /// start
        /// </summary>
        [EnumItem(Value = 3, Desc = "start")]
        Start = 3,
        /// <summary>
        /// end
        /// </summary>
        [EnumItem(Value = 4, Desc = "end")]
        End = 4,
        /// <summary>
        /// unequal
        /// </summary>
        [EnumItem(Value = 5, Desc = "unequal")]
        Unequal = 5,
        /// <summary>
        /// empty
        /// </summary>
        [EnumItem(Value = 6, Desc = "empty")]
        Empty = 6,
    }
}
