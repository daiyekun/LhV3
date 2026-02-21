using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 单品属性
    /// </summary>
    [EnumClass(Max = 4, Min = 0, None = -1)]
    public  enum BcPropertyEnums
    {
        /// <summary>
        /// 销售：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "销售")]
        XiaoShou = 0,
        /// <summary>
        /// 采购：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "采购")]
        CaiGou = 1,
        /// <summary>
        /// 采购&采购：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "销售采购")]
        XiaoShouAndCaiGou = 2,
    }
}
