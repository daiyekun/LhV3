using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 变量结构
    /// </summary>
    public class ContractVariable
    {
        public String VarName { get; set; }

        public String VarLabel { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public String VarValue { get; set; }
    }
    /// <summary>
    /// 变量值
    /// </summary>
    public class ContVarValueInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 签约主体ID
        /// </summary>
        public int MainDeptId { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal AmountMoney { get; set; }
        ///// <summary>
        ///// 合同金额千分位
        ///// </summary>
        //public string AmountThod { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 签订日期
        /// </summary>
        public string SigDate { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public string EffectDate { get; set; }
        /// <summary>
        /// 计划完成日期
        /// </summary>
        public string PlanDate { get; set; }
     
        /// <summary>
        /// 签约主体
        /// </summary>
        public string MainDept { get; set; }
       /// <summary>
       /// 签约主体信息
       /// </summary>
        public HtDeptMain HtDeptMain { get; set; }

        /// <summary>
        /// 合同对方
        /// </summary>
        public string HtDf { get; set; }
        /// <summary>
        /// 对方代表
        /// </summary>
        public string DfDb { get; set; }
        /// <summary>
        /// 对方电话
        /// </summary>
        public string DfTel { get; set; }
        /// <summary>
        /// 对方地址
        /// </summary>
        public string DfDz { get; set; }
        /// <summary>
        /// 对方邮编
        /// </summary>
        public string DfYb { get; set; }
        /// <summary>
        /// 对方传真
        /// </summary>
        public string DfCz { get; set; }
        /// <summary>
        /// 客户发票名称
        /// </summary>
        public string KfInvoiceName { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string KfDz { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        public string KfDh { get; set; }
        /// <summary>
        /// 客户银行
        /// </summary>
        public string KfYh { get; set; }
        /// <summary>
        /// 客户账号
        /// </summary>
        public string KfZh { get; set; }
        /// <summary>
        /// 客户登记号
        /// </summary>
        public string KfDzh { get; set; }
        /// <summary>
        /// 供应商账号
        /// </summary>
        public string GysZh { get; set; }
        /// <summary>
        /// 供应商银行
        /// </summary>
        public string GysYh { get; set; }
        /// <summary>
        /// 当前日期
        /// </summary>
        public string CurrDate { get; set; }
    }
    /// <summary>
    /// 签约主体信息
    /// </summary>
    public class HtDeptMain
    {
        /// <summary>
        /// 签约主体代表
        /// </summary>
        public string LawPerson { get; set; }
        /// <summary>
        /// 签约主体税务登记号
        /// </summary>
        public string TaxId { get; set; }
        /// <summary>
        /// 签约主体银行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 签约主体账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 签约主体地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 签约主体邮编
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 签约主体电话
        /// </summary>
        public string TelePhone { get; set; }
        /// <summary>
        /// 签约主体发票名称
        /// </summary>
        public string InvoiceName { get; set; }
        /// <summary>
        /// 签约主体传真
        /// </summary>
        public string Fax { get; set; }

    }

}
