using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 节点类型枚举
    /// </summary>
    [EnumClass(Max = 8, Min = 0, None = -1)]
    public enum NodeTypeEnum
    {
        /// <summary>
        /// 0:开始节点（start round mix）
        /// </summary>
        [EnumItem(Value = 0, Desc = "start round mix")]
         NType0 = 0,
        /// <summary>
        /// 1:结束节点（end round mix）
        /// </summary>
        [EnumItem(Value = 1, Desc = "end round mix")]
        NType1 = 1,
        /// <summary>
        /// 2:任务节点（task）
        /// </summary>
        [EnumItem(Value = 2, Desc = "task")]
        NType2 = 2,
        /// <summary>
        /// 3:自动节点（node）
        /// </summary>
        [EnumItem(Value = 3, Desc = "node")]
        NType3 = 3
    }
    /// <summary>
    /// 节点连线类型
    /// </summary>
    [EnumClass(Max = 4, Min = 0, None = -1)]
    public enum NodeLineTypeEnum
    {
        /// <summary>
        /// 0:普通直线链接线（sl）
        /// </summary>
        [EnumItem(Value = 0, Desc = "sl")]
        lType0 = 0,
        /// <summary>
        /// 1:中段可上下移动的折线（ tb）
        /// </summary>
        [EnumItem(Value = 1, Desc = "tb")]
        lType1 = 1,
        /// <summary>
        /// 2:中段可左右移动的折线（ lr）
        /// </summary>
        [EnumItem(Value = 2, Desc = "lr")]
        lType2 = 2
        
    }
    /// <summary>
    /// 区域颜色（泳道）
    /// </summary>
    [EnumClass(Max = 4, Min = 0, None = -1)]
    public enum ArearColorEnum
    {
        /// <summary>
        /// 0:红色red
        /// </summary>
        [EnumItem(Value = 0, Desc = "red")]
        Red = 0,
        /// <summary>
        /// 1:yellow
        /// </summary>
        [EnumItem(Value = 1, Desc = "yellow")]
        Yellow = 1,
        /// <summary>
        /// 2:blue
        /// </summary>
        [EnumItem(Value = 2, Desc = "blue")]
        Blue = 2,
        /// <summary>
        /// 3:green
        /// </summary>
        [EnumItem(Value = 3, Desc = "green")]
        Green = 3

    }
    /// <summary>
    /// 流程节点状态
    /// </summary>
    [EnumClass(Max = 6, Min = 0, None = -1)]
    public enum NodeStateEnum
    {
        /// <summary>
        /// 0:未审批
        /// </summary>
        [EnumItem(Value = 0, Desc = "未审批")]
        WeiShenHe = 0,
        /// <summary>
        /// 1:审批中
        /// </summary>
        [EnumItem(Value = 1, Desc = "审批中")]
        ShenHeZhong = 1,
        /// <summary>
        /// 2:已通过
        /// </summary>
        [EnumItem(Value = 2, Desc = "已通过")]
        YiTongGuo = 2,
        /// <summary>
        /// 3:已打回
        /// </summary>
        [EnumItem(Value = 3, Desc = "已打回")]
        BeiDaHui = 3,
        /// <summary>
        /// 4:被跳过
        /// </summary>
        [EnumItem(Value = 4, Desc = "被跳过")]
        BeiTiaoGuo = 4,
       
    }

   
}
