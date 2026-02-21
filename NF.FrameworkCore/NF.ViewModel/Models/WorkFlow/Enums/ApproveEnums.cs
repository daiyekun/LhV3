using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 审批实例状态（流程状态）
    /// </summary>
    [EnumClass(Max = 4, Min = 0, None = -1)]
    public enum AppInstEnum
    {
        /// <summary>
        /// 1:审批中
        /// </summary>
        [EnumItem(Value = 1, Desc = "审批中")]
        AppState0 = 1,
        /// <summary>
        /// 2:审批通过
        /// </summary>
        [EnumItem(Value = 2, Desc = "审批通过")]
        AppState1 = 2,
        /// <summary>
        /// 3:被打回
        /// </summary>
        [EnumItem(Value = 3, Desc = "被打回")]
        AppState2 = 3,
        
    }
    /// <summary>
    /// 流程节点审批规则
    /// </summary>
    [EnumClass(Max = 4, Min = 0, None = -1)]
    public enum NodeNruleEnum
    {
        /// <summary>
        /// 0:全部通过
        /// </summary>
        [EnumItem(Value = 0, Desc = "全部通过")]
        All = 0,
        /// <summary>
        /// 1:任意通过
        /// </summary>
        [EnumItem(Value = 1, Desc = "任意通过")]
        AtWill = 1,
    }
    /// <summary>
    /// 意见审批结果
    /// </summary>
    [EnumClass(Max = 4, Min = 0, None = -1)]
    public enum OptionResultEnum
    {
        /// <summary>
        /// 1:审批中
        /// </summary>
        [EnumItem(Value = 1, Desc = "审批中")]
        Approval = 1,
        /// <summary>
        /// 2:已通过
        /// </summary>
        [EnumItem(Value = 2, Desc = "已通过")]
        Approved = 2,
        /// <summary>
        /// 3:一票通过
        /// </summary>
        [EnumItem(Value = 3, Desc = "一票通过")]
        AVoteBy = 3,
        /// <summary>
        /// 4:已打回
        /// </summary>
        [EnumItem(Value = 4, Desc = "已打回")]
        Back = 4,
        
    }

    /// <summary>
    /// 数据流程状态
    /// </summary>
    [EnumClass(Max = 4, Min = 0, None = -1)]
    public enum WfStateEnum
    {
        /// <summary>
        /// -1:空
        /// </summary>
        [EnumItem(Value = -1, Desc = "")]
         Null = 0,
        /// <summary>
        /// 0:未提交
        /// </summary>
        [EnumItem(Value = 0, Desc = "未提交")]
        WeiTiJiao = 0,
        /// <summary>
        /// 1:审批中
        /// </summary>
        [EnumItem(Value = 1, Desc = "审批中")]
        ShenPiZhong = 1,
        /// <summary>
        /// 2:审批通过
        /// </summary>
        [EnumItem(Value = 2, Desc = "审批通过")]
        ShenPiTongGuo = 2,
        /// <summary>
        /// 3:被打回
        /// </summary>
        [EnumItem(Value = 3, Desc = "被打回")]
        BeiDaHui = 3,
        /// <summary>
        /// 3:被打回
        /// </summary>
        [EnumItem(Value = 4, Desc = "已撤回")]
        BeiDaHusi = 4,

    }
}
