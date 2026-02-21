using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 计划资金
    /// </summary>
   public class ContPlanFinanceDTO
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public string Name { get; set; }
        public byte? Ftype { get; set; }
        public decimal? AmountMoney { get; set; }
        public int? SettlementModes { get; set; }
        public DateTime? PlanCompleteDateTime { get; set; }
        public byte? Fstate { get; set; }
        public byte? ProgressState { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        /// <summary>
        /// 额外新增，用于判断是否是框架合同执行时新增计划资金
        /// </summary>
        public int IsFramework { get; set; }
    }
    /// <summary>
    /// 计划资金显示
    /// </summary>
    public class ContPlanFinanceViewDTO: ContPlanFinanceDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 计划金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlModelName { get; set; }
        public string SumHtje { get; set; }

    }
    /// <summary>
    /// 一般用于根据ID查询的列表-比如实际资金页面
    /// </summary>
    public class ContPlanFinanceViewSecoundDTO
    {
        /// <summary>
        /// 计划资金ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 已确认（已完成）
        /// </summary>
        public decimal? ConfirmedAmount { get; set; }
        /// <summary>
        /// 已完成金额千分位
        /// </summary>
        public string ConfirmedAmountThod { get; set; }
        /// <summary>
        /// 余额（未完成）
        /// </summary>
        public string BalanceThod { get; set; }
        /// <summary>
        /// 已提交未确认金额
        /// </summary>
        public decimal? SubAmount { get; set; }
        /// <summary>
        /// 已提交未确认金额千分位
        /// </summary>
        public string  SubAmountThod { get; set; }
        /// <summary>
        /// 可核销
        /// </summary>
        public decimal? SurplusAmount { get; set; }
        /// <summary>
        /// 可核销=（金额-建立的实际资金金额）
        /// </summary>
        public string SurplusAmountThod { get; set; }
        /// <summary>
        /// 计划完成日期
        /// </summary>
        public DateTime? PlanCompleteDateTime { get; set; }
        /// <summary>
        /// 完成比例
        /// </summary>
        public string CompRate { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlModelName { get; set; }
        /// <summary>
        /// 本次核销金额
        /// </summary>
        public decimal? CheckAmount { get; set; }
        public string  CheckAmountThod { get; set; }
        /// <summary>
        /// 剩余计划资金
        /// </summary>
        public string SyPlanAmountThod { get; set; }
             


    }

    /// <summary>
    /// 计划资金查询
    /// </summary>
    public class ContPlanFinanceListViewDTO: ContPlanFinanceViewDTO, INfEntityHandle
    {
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContCode{ get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string ContCategoryName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string PrincipalUserName { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public string BalanceThod { get; set; }
        /// <summary>
        /// 公司名称（客户、供应商名称）
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 公司（客户，供应商）类别
        /// </summary>
        public string CompTypeName { get; set; }
        /// <summary>
        /// 合同经办机构
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 实际完成日期
        /// </summary>
        public DateTime? ActSettlementDate { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 已完成金额
        /// </summary>
        public string CompAmountThod { get; set; }
        /// <summary>
        /// 完成比例
        /// </summary>
        public string ContActBl { get; set; }
        /// <summary>
        /// 合同对方ID
        /// </summary>
        public int? CompId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjId { get; set; }
        /// <summary>
        /// 完成金额
        /// </summary>
        public decimal CompAmount { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>

        public decimal Balance { get; set; }

        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <returns></returns>
        public FieldInfo GetPropValue(string propName)
        {
            var obj = this.GetType().GetProperty(propName);
            return new FieldInfo
            {
                FileType = obj.PropertyType,
                FileValue = obj.GetValue(this, null)
            };

        }
    }
}
