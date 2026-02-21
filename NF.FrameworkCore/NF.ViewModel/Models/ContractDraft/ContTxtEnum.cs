using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    ///文本来源
    /// </summary>
    [EnumClass(Max = 8, Min = 0, None = -1)]
    public enum SourceTxtEnum
    {
        /// <summary>
        /// 未知：-1
        /// </summary>
        [EnumItem(Value = -1, Desc = "未知")]
        None = -1,
        /// <summary>
        /// 本地上传：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "本地上传")]
        Upload = 0,
        /// <summary>
        /// 模板起草：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "模板起草")]
        TempDraft = 1,
        /// <summary>
        /// 自由起草：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "自由起草")]
        FreeDraft = 2,

    }
    /// <summary>
    ///阶段
    /// </summary>
    [EnumClass(Max = 5, Min = 0, None = -1)]
    public enum StageTxtEnum
    {
        /// <summary>
        /// 未知：-1
        /// </summary>
        [EnumItem(Value = -1, Desc = "未知")]
        None = -1,
        /// <summary>
        /// 原始：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "原始")]
        Raw = 0,


    }
    /// <summary>
    /// 当前合同文本编辑状态
    /// </summary>
    [EnumClass(Max = 3, Min = 0, None = -1)]
    public enum CreatTypeEnum
    {
        /// <summary>
        /// 创建
        /// </summary>
        [EnumItem(Value = 0, Desc = "创建")]
        Create = 0,
        /// <summary>
        /// 修改
        /// </summary>
        [EnumItem(Value = 1, Desc = "修改")]
        Update = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [EnumItem(Value = 2, Desc = "变更")]
        Change = 1,

    }
    /// <summary>
    /// 合同用章状态
    /// </summary>
    [EnumClass(Max = 2, Min = 0, None = -1)]
    public enum SealStateEnum
    {
        /// <summary>
        /// 我方已用章对方未用章
        /// </summary>
        [EnumItem(Value = 0, Desc = "我方已用章对方未用章")]
        SimpleSeal = 0,
        /// <summary>
        /// 双方均用章
        /// </summary>
        [EnumItem(Value = 1, Desc = "双方均用章")]
        BothSeal = 1,
       

    }
    /// <summary>
    /// 归档状态
    /// </summary>
    [EnumClass(Max = 2, Min = 0, None = -1)]
    public enum ArchiveStateEnum
    {
        /// <summary>
        /// 未归档
        /// </summary>
        [EnumItem(Value = 0, Desc = "未归档")]
        NotArch = 0,
        /// <summary>
        /// 已归档
        /// </summary>
        [EnumItem(Value = 1, Desc = "已归档")]
        Arch = 1,


    }

}
