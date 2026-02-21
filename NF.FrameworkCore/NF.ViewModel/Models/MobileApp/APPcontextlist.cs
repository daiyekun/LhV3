using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// APP合同文本
    /// </summary>
    public class APPContTextListViewDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件类型名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 文件说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }

    }

    /// <summary>
    /// APP合同附件
    /// </summary>
    public class APPContFUjianListViewDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 附件类型名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 附件说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }

    }
    /// <summary>
    /// APP合同实际资金
    /// </summary>
    public class APPContfINCTListViewDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 计划资金名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 计划金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? PlanCompleteDateTime { get; set; }
        /// <summary>
        /// 附件说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }


    }

    /// <summary>
    /// APP合同计划资金
    /// </summary>
    public class APPContSHIJIfINCTListViewDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime? ActualSettlementDate { get; set; }

        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethod { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserId { get; set; }


    }

    /// <summary>
    /// APP资金统计
    /// </summary>
    public class APPContractStatic
    {
        /// <summary>
        /// 财务实际收款（状态已确认）
        /// </summary>
        public string ActMoneryThod { get; set; }
        /// <summary>
        /// 未收款=合同金额-ActMoneryThod
        /// </summary>
        public string ActNoMoneryThod { get; set; }
        /// <summary>
        /// 开发票金额（发票状态是已确认）
        /// </summary>
        public string InvoiceMoneryThod { get; set; }
        /// <summary>
        /// 未开发票金额=合同金额-InvoiceMoneryThod
        /// </summary>
        public string InvoiceNoMoneryThod { get; set; }
        /// <summary>
        /// 财务应收=开票金额-实际收款金额
        /// </summary>
        public string ReceivableThod { get; set; }
        /// <summary>
        /// 财务预收=实际收款-开票金额
        /// </summary>
        public string AdvanceThod { get; set; }



    }

    /// <summary>
    /// APP
    /// </summary>
    public class APPContDescription
    {

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 事项
        /// </summary>
        public string Citem { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Ccontent { get; set; }
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

    }
    /// <summary>
    /// APP审批记录
    /// </summary>
    public class APPAppInst
    {

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public string MissionDic { get; set; }
        /// <summary>
        /// 当前审批节点
        /// </summary>
        public string CurrentNodeName { get; set; }
        /// <summary>
        /// 当前审批流程状态
        /// </summary>
        public string AppStateDic { get; set; }

        /// <summary>
        /// 审批发起日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 审批结束日期
        /// </summary>
        public DateTime? CompleteDateTime { get; set; }

    }

    /// <summary>
    /// APP发票列表
    /// </summary>
    public class APPContInvoice
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int? ContractnId { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractnName { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string contractNO { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CompayName { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string FapiaoType { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string FapiaoNO { get; set; }
        /// <summary>
        /// 金额
        /// </summary>

        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string FapiaoState { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }

        public DateTime? MakeOutDateTime { get; set; }
        public DateTime? ConfirmDateTIme { get; set; }
        
        public string CreateUserName { get; set; }



    }
    /// <summary>
    /// APPh合同查看详情审批单
    /// </summary>
    public class APPcontractSPD
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string CurrentNodeName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string CuName { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Opin { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? copnData { get; set; }
    }

}
