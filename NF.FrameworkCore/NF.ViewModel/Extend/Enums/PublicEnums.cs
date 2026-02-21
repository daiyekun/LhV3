using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Extend.Enums
{
    /// <summary>
    /// 数据状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum DataState
    {
        /// <summary>
        /// 禁用：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "禁用")]
        Disabled = 0,
        /// <summary>
        /// 启用:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "启用")]
        Enabled = 1,
    }

    /// <summary>
    /// 其他状态值：比如是、否
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum OtherDataState
    {
        /// <summary>
        /// 否：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "否")]
        No = 0,
        /// <summary>
        /// 是:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "是")]
        Yes = 1,
    }
    /// <summary>
    /// 权限枚举
    /// </summary>
    [EnumClass(Max = 10, Min = 0, None = -1)]
    public enum PermissionDicEnum
    {
        /// <summary>
        /// 无权限：0
        /// </summary>
        [EnumItem(Value = -1, Desc = "无权限")]
        None = -1,
        /// <summary>
        /// 有权限：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "有权限")]
        OK = 0,
        /// <summary>
        /// 当前状态不允许操作：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "当前状态不允许此操作")]
        NotState = 2,


    }

    /// <summary>
    /// 数据状态
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum UserState
    {
        /// <summary>
        /// 未启用：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "未启用")]
        Disabled = 0,
        /// <summary>
        /// 启用:1
        /// </summary>
        [EnumItem(Value = 1, Desc = "启用")]
        Enabled = 1,
        /// <summary>
        /// 离职:1
        /// </summary>
        [EnumItem(Value = 2, Desc = "离职")]
        LiZhi = 2,
    }
}
