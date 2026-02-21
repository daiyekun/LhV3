using System;
using System.Collections.Generic;
using System.Text;
using NF.Common.Utility;

namespace NF.ViewModel.Extend.Enums
{
    /// <summary>
    /// 数据字典枚举
    /// </summary>
    [EnumClass(Max = 100, Min = 0, None = -1)]
    public enum DataDictionaryEnum
    {
        /// <summary>
        /// 部门类别：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "机构类别")]
        departmentType = 0,
        /// <summary>
        /// 合同类别：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "合同类别")]
        contractType = 1,
        /// <summary>
        /// 供应商类别：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "供应商类别")]
        suppliersType = 2,
        /// <summary>
        /// 客户类别：3
        /// </summary>
        [EnumItem(Value = 3, Desc = "客户类别")]
        customerType = 3,
        /// <summary>
        /// 其他对方类别：4
        /// </summary>
        [EnumItem(Value = 4, Desc = "其他对方类别")]
        otherType = 4,
        /// <summary>
        /// 客户级别：5
        /// </summary>
        [EnumItem(Value = 5, Desc = "客户级别")]
        customerLevel = 5,
        /// <summary>
        /// 信用等级：6
        /// </summary>
        [EnumItem(Value = 6, Desc = "信用等级")]
        customerCaredit = 6,
        /// <summary>
        /// 公司类型：7
        /// </summary>
        [EnumItem(Value = 7, Desc = "公司类型")]
        companyType = 7,
        /// <summary>
        /// 客户附件类别:8
        /// </summary>
        [EnumItem(Value = 8, Desc = "客户附件类别")]
        customerAttachmentType = 8,
        /// <summary>
        /// 供应商附件类别:9
        /// </summary>
        [EnumItem(Value = 9, Desc = "供应商附件类别")]
        supplierAttachmentType = 9,
        /// <summary>
        /// 其他对方附件类别:9
        /// </summary>
        [EnumItem(Value = 10, Desc = "其他对方附件类别")]
        otherAttachmentType = 10,
        /// <summary>
        /// 供应商级别:11
        /// </summary>
        [EnumItem(Value = 11, Desc = "供应商级别")]
        supplierLevel = 11,
        /// <summary>
        /// 其他对方级别:12
        /// </summary>
        [EnumItem(Value = 12, Desc = "其他对方级别")]
        otherLevel = 12,
        /// <summary>
        /// 项目类别:13
        /// </summary>
        [EnumItem(Value = 13, Desc = "项目类别")]
        projectType = 13,
        /// <summary>
        /// 项目类别:projectFileType:13
        /// </summary>
        [EnumItem(Value = 14, Desc = "项目附件类别")]
        projectFileType = 14,
        /// <summary>
        /// 合同来源:contSource
        /// </summary>
        [EnumItem(Value = 15, Desc = "合同来源")]
        contSource = 15,
        /// <summary>
        /// 文本类别:ContTxtType
        /// </summary>
        [EnumItem(Value = 16, Desc = "文本类别")]
        ContTxtType = 16,
        /// <summary>
        /// 结算方式:SettlModes:17
        /// </summary>
        [EnumItem(Value = 17, Desc = "结算方式")]
        SettlModes = 17,
        /// <summary>
        /// 合同附件类别:SettlModes:18
        /// </summary>
        [EnumItem(Value = 18, Desc = "合同附件类别")]
        ContAttachmentType = 18,
        /// <summary>
        /// 发票类别:InvoiceType:19
        /// </summary>
        [EnumItem(Value = 19, Desc = "发票类别")]
        InvoiceType = 19,
        /// <summary>
        /// 单品附件类别:BcAttachmentType
        /// </summary>
        [EnumItem(Value = 20, Desc = "单品附件类别")]
        BcAttachmentType = 20,
        /// <summary>
        /// 交付方式:DevType
        /// </summary>
        [EnumItem(Value = 30, Desc = "交付方式")]
        DevType = 30,
        /// <summary>
        /// 询价类别
        /// </summary>
        [EnumItem(Value = 31, Desc = "询价类别")]
        InquiryType = 31,
        /// <summary>
        /// 约谈类别
        /// </summary>
        [EnumItem(Value = 32, Desc = "约谈类别")]
        QueType = 32,
        /// <summary>
        /// 招标类别
        /// </summary>
        [EnumItem(Value = 33, Desc = "招标类别")]
        TenderType = 33,
        /// <summary>
        /// 项目来源
        /// </summary>
        [EnumItem(Value = 34, Desc = "项目来源")]
        ProjectSource = 34,
        /// <summary>
        /// 优先级
        /// </summary>
        [EnumItem(Value = 35, Desc = "优先级")]
        PrioritySource = 35,
        /// <summary>
        /// 任务归属
        /// </summary>
        [EnumItem(Value = 36, Desc = "任务归属")]
        ScheduleAttributionSource = 36,
        /// <summary>
        /// 实际资金附件
        /// </summary>
        [EnumItem(Value = 37, Desc = "实际资金附件类别")]
        ActFinceFileType = 37,
        /// <summary>
        /// 发票附件类别
        /// </summary>
        [EnumItem(Value = 38, Desc = "发票附件类别")]
        InvoFileType = 38,
        /// <summary>
        /// 紧急程度
        /// </summary>
        [EnumItem(Value = 39, Desc = "紧急程度")]
         Jjcd= 39,
        /// <summary>
        /// 纠纷类别
        /// </summary>
        [EnumItem(Value = 40, Desc = "纠纷类别")]
        Jflb = 40,
        /// <summary>
        /// 保全类型
        /// </summary>
        [EnumItem(Value = 41, Desc = "保全类型")]
        Bqlx = 41,
    }
    
}
