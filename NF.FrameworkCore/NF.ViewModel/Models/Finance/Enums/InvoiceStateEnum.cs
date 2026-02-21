using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 发票状态
    /// </summary>
    [EnumClass(Max = 6, Min = 0, None = -1)]
    public  enum InvoiceStateEnum
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
        /// 已开出：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "已开出")]
        Invoicing= 2,
        /// <summary>
        /// 已收到：3
        /// </summary>
        [EnumItem(Value = 3, Desc = "已收到")]
        ReceiptInvoice = 3,
        /// <summary>
        /// 被打回：4
        /// </summary>
        [EnumItem(Value = 4, Desc = "被打回")]
        Back = 4,

    }
}
