using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.ContTxtTemplate
{
    /// <summary>
    /// 合同标的统一格式 字段名
    /// </summary>    
    [EnumClass(Min = 1, Max = 13, None = 0)]
    public enum ObjectFieldEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 1	报价
        /// </summary>
        [EnumItem(Value = 1, Desc = "报价")]
        Field1 = 1,
        /// <summary>
        /// 2	标的名称
        /// </summary>
        [EnumItem(Value = 2, Desc = "标的名称")]
        Field2 = 2,
        /// <summary>
        /// 3	单价
        /// </summary>
        [EnumItem(Value = 3, Desc = "单价")]
        Field3 = 3,
        /// <summary>
        /// 4	单品编号
        /// </summary>
        [EnumItem(Value = 4, Desc = "单品编号")]
        Field4 = 4,
        /// <summary>
        /// 5	单品名称
        /// </summary>
        [EnumItem(Value = 5, Desc = "单品名称")]
        Field5 = 5,
        /// <summary>
        /// 6	单位
        /// </summary>
        [EnumItem(Value = 6, Desc = "单位")]
        Field6 = 6,
        /// <summary>
        /// 7	计划完成日期
        /// </summary>
        [EnumItem(Value = 7, Desc = "计划完成日期")]
        Field7 = 7,
        ///// <summary>
        ///// 8	名义报价
        ///// </summary>
        //[EnumItem(Value = 8, Desc = "名义报价")]
        //Field8 = 8,
        ///// <summary>
        ///// 9	名义折扣率
        ///// </summary>
        //[EnumItem(Value = 9, Desc = "名义折扣率")]
        //Field9 = 9,
        /// <summary>
        /// 10	数量
        /// </summary>
        [EnumItem(Value = 10, Desc = "数量")]
        Field10 = 10,
        /// <summary>
        /// 11	小计
        /// </summary>
        [EnumItem(Value = 11, Desc = "小计")]
        Field11 = 11,
        /// <summary>
        /// 12	折扣率
        /// </summary>
        [EnumItem(Value = 12, Desc = "折扣率")]
        Field12 = 12,
        /// <summary>
        /// 13	备注
        /// </summary>
        [EnumItem(Value = 13, Desc = "备注")]
        Field13 = 13,
    }
    /// <summary>
    /// 按业务品类设置格式 非业务类字段名
    /// </summary>    
    [EnumClass(Min = 1, Max = 11, None = 0)]
    public enum ObjectField2Enum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 1:标的名称
        /// </summary>
        [EnumItem(Value = 1, Desc = "标的名称")]
        Field1 = 1,
        /// <summary>
        /// 2:规格
        /// </summary>
        [EnumItem(Value = 2, Desc = "规格")]
        Field2 = 2,
        /// <summary>
        /// 3:型号
        /// </summary>
        [EnumItem(Value = 3, Desc = "型号")]
        Field3 = 3,
        /// <summary>
        /// 4:单位
        /// </summary>
        [EnumItem(Value = 4, Desc = "单位")]
        Field4 = 4,
        /// <summary>
        /// 5:单价
        /// </summary>
        [EnumItem(Value = 5, Desc = "单价")]
        Field5 = 5,
        /// <summary>
        /// 6:数量
        /// </summary>
        [EnumItem(Value = 6, Desc = "数量")]
        Field6 = 6,
        /// <summary>
        /// 7:小计
        /// </summary>
        [EnumItem(Value = 7, Desc = "小计")]
        Field7 = 7,
        /// <summary>
        /// 8:报价
        /// </summary>
        [EnumItem(Value = 8, Desc = "报价")]
        Field8 = 8,
        /// <summary>
        /// 9:折扣率
        /// </summary>
        [EnumItem(Value = 9, Desc = "折扣率")]
        Field9 = 9,
        /// <summary>
        /// 10:计划交付日期
        /// </summary>
        [EnumItem(Value = 10, Desc = "计划交付日期")]
        Field10 = 10,
        /// <summary>
        /// 11:备注
        /// </summary>
        [EnumItem(Value = 11, Desc = "备注")]
        Field11 = 11,
    }
    /// <summary>
    /// 按业务品类设置格式 业务类字段名
    /// </summary>    
    [EnumClass(Min = 13, Max = -1, None = 0)]
    public enum ObjectField3Enum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// -1:标的名称
        /// </summary>
        [EnumItem(Value = 1, Desc = "标的名称")]
        Field1 = 1,
        /// <summary>
        /// -2:单品名称
        /// </summary>
        [EnumItem(Value = 2, Desc = "单品名称")]
        Field2 = 2,
        /// <summary>
        /// -3:单品编号
        /// </summary>
        [EnumItem(Value = 3, Desc = "单品编号")]
        Field3 = 3,
        /// <summary>
        /// -4:单位
        /// </summary>
        [EnumItem(Value = 4, Desc = "单位")]
        Field4 = 4,
        /// <summary>
        /// -5:单价
        /// </summary>
        [EnumItem(Value = 5, Desc = "单价")]
        Field5 = 5,
        /// <summary>
        /// -6:数量
        /// </summary>
        [EnumItem(Value = 6, Desc = "数量")]
        Field6 = 6,
        /// <summary>
        /// -7:报价
        /// </summary>
        [EnumItem(Value = 7, Desc = "报价")]
        Field7 = 7,
        /// <summary>
        /// -8:折扣率
        /// </summary>
        [EnumItem(Value = 8, Desc = "折扣率")]
        Field8 = 8,
        ///// <summary>
        ///// -9:名义报价
        ///// </summary>
        //[EnumItem(Value = -9, Desc = "名义报价")]
        //Field9 = -9,
        ///// <summary>
        ///// -10:名义折扣率
        ///// </summary>
        //[EnumItem(Value = -10, Desc = "名义折扣率")]
        //Field10 = -10,
        /// <summary>
        /// -11:小计
        /// </summary>
        [EnumItem(Value = 11, Desc = "小计")]
        Field11 = 11,
        /// <summary>
        /// -12:计划交付日期
        /// </summary>
        [EnumItem(Value = 12, Desc = "计划交付日期")]
        Field12 = 12,
        /// <summary>
        /// -13:备注
        /// </summary>
        [EnumItem(Value = 13, Desc = "备注")]
        Field13 = 13,
    }

    /// <summary>
    /// 字段类型
    /// </summary>
    [EnumClass(Default = -1, Min = -1, Max = 1, None = -2)]
    public enum FieldTypeEnum
    {
        /// <summary>
        /// -2:无
        /// </summary>
        None = -2,
        /// <summary>
        /// -1:没有选择
        /// </summary>
        [EnumItem(Value = 0, Desc = "没有选择")]
        NotSelect = -1,
        /// <summary>
        /// 0:合同标的统一格式
        /// </summary>
        [EnumItem(Value = 0, Desc = "合同标的统一格式")]
        Unity = 0,
        /// <summary>
        /// 1:按业务品类设置格式
        /// </summary>
        [EnumItem(Value = 1, Desc = "按业务品类设置格式")]
        BusinessCategory = 1,
    }

}
