using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 实际资金
    /// </summary>
    public class ContActualFinanceDTO
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public int? SettlementMethod { get; set; }
        public byte? FinceType { get; set; }
        public decimal? AmountMoney { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CurrencyRate { get; set; }
        public DateTime? ActualSettlementDate { get; set; }
        public string VoucherNo { get; set; }
        public byte? Astate { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public int? ConfirmUserId { get; set; }
        public DateTime? ConfirmDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        /// <summary>
        /// 计划资金核销0，发票核销1
        /// </summary>
        public int CheckType { get; set; }
       

    }
    /// <summary>
    /// 实际资金查看
    /// </summary>
    public class ContActualFinanceViewDTO
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int? ContId { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public int? SettlementMethod { get; set; }
       
        /// <summary>
        /// 资金类型
        /// </summary>
        public byte? FinceType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 币种ID
        /// </summary>
        public int? CurrencyId { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal? CurrencyRate { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime? ActualSettlementDate { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string VoucherNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Astate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
       /// <summary>
       /// 确认人ID
       /// </summary>
        public int? ConfirmUserId { get; set; }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmDateTime { get; set; }
        /// <summary>
        /// 备用1
        /// </summary>
        public string Reserve1 { get; set; }
        /// <summary>
        /// 备用2
        /// </summary>
        public string Reserve2 { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethodDic { get; set; }
        /// <summary>
        /// 金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        ///实际资金状态
        /// </summary>
        public string AstateDic { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public byte WfState { get; set; }


    }
    /// <summary>
    /// 显示列表
    /// </summary>
    public class ContActualFinanceListViewDTO : ContActualFinanceDTO, INfEntityHandle
    {
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
        /// 合同对方类别
        /// </summary>
        public string CompCategoryName { get; set; }
        /// <summary>
        /// 合同类别ID
        /// </summary>
        public int? ContTypeId { get; set; }
        /// <summary>
        /// 合同经办机构
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 合同部门ID
        /// </summary>
        public int? DeptId { get; set; }
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
        /// 结算方式
        /// </summary>
        public string SettlementMethodDic { get; set; }
        /// <summary>
        /// 发票金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        ///实际资金状态
        /// </summary>
        public string AstateDic { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>                  
        public byte? WfState { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public string WfCurrNodeName { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public int? WfItem { get; set; }
        /// <summary>
        /// 审批事项描述
        /// </summary>
        public string WfItemDic { get; set; }
        /// <summary>
        /// 流程状态描述
        /// </summary>
        public string WfStateDic { get; set; }


    }
    /// <summary>
    /// 核销明细
    /// </summary>
    public class ContActualFinanceChkListViewDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 发票金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethodDic { get; set; }
        /// <summary>
        /// 结算日期
        /// </summary>
        public DateTime? ActualSettlementDate { get; set; }
        /// <summary>
        ///实际资金状态
        /// </summary>
        public string AstateDic { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmDateTime { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public string WfStateDic { get; set; }

    }

    /// <summary>
    /// 核销数据
    /// </summary>
    public class CheckData
    {
        /// <summary>
        /// 核销ID 
        /// </summary>
        public int ChkId { get; set; }
        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal ChkMonery { get; set; }
        ///// <summary>
        ///// 当前核销对象的金额
        ///// </summary>
        //public decimal AmountMoney { get; set; }

    }
    /// <summary>
    /// 合同下发票列表
    /// </summary>
    public class ContractActualFinance
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmDateTime { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethodDic { get; set; }
        /// <summary>
        /// 金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        ///实际资金状态
        /// </summary>
        public string AstateDic { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string VoucherNo { get; set; }
        /// <summary>
        /// 结算日期
        /// </summary>
        public DateTime? ActualSettlementDate { get; set; }



    }
}
