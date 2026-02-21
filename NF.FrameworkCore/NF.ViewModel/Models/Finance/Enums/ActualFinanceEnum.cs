using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.Finance.Enums
{
    /// <summary>
    /// 实际资金状态枚举
    /// </summary>
    [EnumClass(Max = 6, Min = 0, None = -1)]
    public  enum ActFinanceStateEnum
    {
        /// <summary>
        /// 未提交：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未提交")]
        Uncommitted = 0,
        /// <summary>
        /// 已提交：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "已提交")]
        Submitted = 1,
        /// <summary>
        /// 已确认：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "已确认")]
        Confirmed = 2,
        /// <summary>
        /// 被打回：3
        /// </summary>
        [EnumItem(Value = 3, Desc = "被打回")]
        BeBack =3,
        /// <summary>
        /// 审批通过
        /// </summary>
        [EnumItem(Value = 6, Desc = "审批通过")]
        Approved = 6,
    }
    /// <summary>
    /// 资金属性（收款/付款）
    /// </summary>
    [EnumClass(Max = 6, Min = 0, None = -1)]
    public enum FinanceTypeEnum
    {
        /// <summary>
        /// 收款：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "收款")]
        Sk = 0,
        /// <summary>
        /// 已提交：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "付款")]
        Fk = 1,
       
    }
}
