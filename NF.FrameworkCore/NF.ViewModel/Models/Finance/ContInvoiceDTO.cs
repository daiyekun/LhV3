using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 发票DTO
    /// </summary>
    public class ContInvoiceDTO
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public int? InType { get; set; }
        public string InTitle { get; set; }
        public string TaxpayerIdentification { get; set; }
        public string InAddress { get; set; }
        public string InTel { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public decimal? AmountMoney { get; set; }
        public DateTime? MakeOutDateTime { get; set; }
        public string InCode { get; set; }
        public byte? InState { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string InContent { get; set; }
        public string Remark { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public int? ConfirmUserId { get; set; }
        public DateTime? ConfirmDateTime { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        /// <summary>
        /// 是不是开票
        /// </summary>
        public bool IsOutInvoice { get; set; } = false;



    }
    /// <summary>
    /// 显示DTO
    /// </summary>
    public class ContInvoiceViewDTO: ContInvoiceDTO
    {
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string InTypeName { get; set; }
        /// <summary>
        /// 发票金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 发票状态
        /// </summary>
        public string InStateDic { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public byte WfState { get; set; }
        /// <summary>
        /// 发票标题
        /// </summary>

        public string InvoiceTitle { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        public string TaxIdentification { get; set; }
        /// <summary>
        /// 发票地址
        /// </summary>
        public string InvoiceAddress { get; set; }
        /// <summary>
        /// 发票电话
        /// </summary>
        public string InvoiceTel { get; set; }


    }
    /// <summary>
    /// 显示DTO
    /// </summary>
    public class ContInvoiceListViewDTO : ContInvoiceDTO, INfEntityHandle
    {
        public int? DeptId { get; set; }
        public int? WfItem { get; set; }
        /// <summary>
        /// 对应合同资金性质0：收款、1：付款
        /// </summary>
        public byte FinanceType { get; set; }
       
        public FieldInfo GetPropValue(string propName)
        {
            var obj = this.GetType().GetProperty(propName);
            return new FieldInfo
            {
                FileType = obj.PropertyType,
                FileValue = obj.GetValue(this, null)
            };
        }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContCode { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string ContCategoryName { get; set; }

        /// <summary>
        /// 公司名称（客户、供应商名称）
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 合同经办机构
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 合同对方ID
        /// </summary>
        public int? CompId { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string InTypeName { get; set; }
        /// <summary>
        /// 发票金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 发票状态
        /// </summary>
        public string InStateDic { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public byte WfState { get; set; }
        /// <summary>
        /// 流程状态描述
        /// </summary>
        public string WfStateDic { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public string WfCurrNodeName { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public string WfItemDic { get; set; }
        /// <summary>
        /// 合同类型Id
        /// </summary>
        public int? ContTypeId { get; set; }
    }
    /// <summary>
    /// 类似实际资金页面的列表
    /// </summary>
    public class ContInvoiceActViewDTO
    {
      
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 发票金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 已收已付金额（已确认实际资金）
        /// </summary>
        public decimal? ConfirmedAmount { get; set; }
        /// <summary>
        /// 已收已付金额千分位（已确认实际资金）
        /// </summary>
        public string ConfirmedAmountThod { get; set; }
        /// <summary>
        /// 票款差千分位 已开票金额-已收款金额
        /// </summary>
        public string FareWorseThod { get; set; }
        /// <summary>
        /// 票款差 已开票金额-已收款金额
        /// </summary>
        public decimal FareWorse { get; set; }
        /// <summary>
        /// 应（收款/付款）千分位 已开票金额-已收款金额
        /// </summary>
        public string AccountsThod { get; set; }
        /// <summary>
        /// 应（收款/付款）已开票金额-已收款金额
        /// </summary>
        public decimal Accounts { get; set; }
        /// <summary>
        /// 本次核销（本次收付款）
        /// </summary>
        public string CheckAmountThod { get; set; }
        /// <summary>
        /// 本次核销金额
        /// </summary>
        public decimal? CheckAmount { get; set; }



    }
    /// <summary>
    /// 合同发票
    /// </summary>
    public class ContractInvoice
    { 
    
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 发票金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string InTypeName { get; set; }
        /// <summary>
        /// 发票状态
        /// </summary>
        public string InStateDic { get; set; }
        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime? MakeOutDateTime { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
         public string InCode { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmDateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        public byte? InState { get; set; }

    }




}
