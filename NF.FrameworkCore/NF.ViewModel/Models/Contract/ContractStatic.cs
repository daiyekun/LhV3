using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 资金统计
    /// </summary>
    public  class ContractStatic
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



        //20210702 新加字段 
        /// <summary>
        /// 已签订分包金额
        /// </summary>
        public string ZByqdje { get; set; }
        /// <summary>
        /// 剩余合同金额
        /// </summary>
        public string ZBsyje { get; set; }
        /// <summary>
        /// 分包实际付款
        /// </summary>
        public string FbSjfk { get; set; }
        /// <summary>
        /// 分包未付款
        /// </summary>
        public string Fbwfk { get; set; }
        /// <summary>
        /// 分包已收票
        /// </summary>
        public string Fbysp { get; set; }
        /// <summary>
        /// 分包未收票
        /// </summary>
        public string Fbwsp { get; set; }



    }
}
