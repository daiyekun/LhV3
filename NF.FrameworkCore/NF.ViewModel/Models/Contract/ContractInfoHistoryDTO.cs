using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同历史DTO
    /// </summary>
   public class ContractInfoHistoryDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 对方编号
        /// </summary>
        public string OtherCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public int? ContTypeId { get; set; }
        /// <summary>
        /// 资金性质
        /// </summary>
        public byte FinanceType { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 印花税
        /// </summary>
        public decimal? StampTax { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public int? CurrencyId { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal? CurrencyRate { get; set; }
        /// <summary>
        /// 合同状态,默认0：未执行
        /// </summary>
        public byte? ContState { get; set; } = 0;
        /// <summary>
        /// 经办机构
        /// </summary>
        public int? DeptId { get; set; }
        /// <summary>
        /// 合同对方ID
        /// </summary>
        public int? CompId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }
        /// <summary>
        /// 签约时间
        /// </summary>
        public DateTime? SngnDateTime { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? EffectiveDateTime { get; set; }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? PlanCompleteDateTime { get; set; }
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ActualCompleteDateTime { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public int? PrincipalUserId { get; set; }
        /// <summary>
        /// 资金条款
        /// </summary>
        public string FinanceTerms { get; set; }
        /// <summary>
        /// 修改次数
        /// </summary>
        public int? ModificationTimes { get; set; }
        /// <summary>
        /// 变更说明
        /// </summary>
        public string ModificationRemark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyDateTime { get; set; }
        /// <summary>
        /// 是否删除默认0
        /// </summary>
        public byte IsDelete { get; set; } = 0;
        /// <summary>
        /// 备注1
        /// </summary>
        public string Reserve1 { get; set; }
        /// <summary>
        /// 备注2
        /// </summary>
        public string Reserve2 { get; set; }
        /// <summary>
        /// 签约主体
        /// </summary>
        public int? MainDeptId { get; set; }
        /// <summary>
        /// 总分包：1：总包、2：分包
        /// </summary>
        public string ContDivision { get; set; }
        /// <summary>
        /// 分包合同是选择的总包合同
        /// </summary>
        public int? SumContId { get; set; }
        /// <summary>
        /// 第三方
        /// </summary>
        public int? CompId3 { get; set; }
        /// <summary>
        /// 第四方
        /// </summary>
        public int? CompId4 { get; set; }
        /// <summary>
        /// 合同属性：0：标准合同、1：框合同
        /// </summary>
        public byte? IsFramework { get; set; }
        /// <summary>
        /// 实际履行时间
        /// </summary>
        public DateTime? PerformanceDateTime { get; set; }
        /// <summary>
        /// 预收、预付金额
        /// </summary>
        public decimal? AdvanceAmount { get; set; }
        /// <summary>
        /// 预估金额
        /// </summary>
        public decimal? EstimateAmount { get; set; }
        /// <summary>
        /// 合同来源
        /// </summary>
        public int? ContSourceId { get; set; }
    }
    /// <summary>
    /// 合同历史显示页
    /// </summary>
    public class ContractInfoHistoryViewDTO: ContractInfoHistoryDTO
    {
        /// <summary>
        /// 合同类别
        /// </summary>
        public string ContTypeName { get; set; }

        /// <summary>
        /// 合同来源
        /// </summary>
        public string ContSName { get; set; }
        /// <summary>
        /// 合同对方名称
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjName { get; set; }
        /// <summary>
        /// 合同属性
        /// </summary>
        public string ContPro { get; set; }
        /// <summary>
        /// 总包
        /// </summary>
        public string ContSum { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string ContAmThod { get; set; }
        /// <summary>
        /// 折合本币
        /// </summary>
        public string ContAmRmbThod { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 预估千分位
        /// </summary>
        public string EsAmountThod { get; set; }
        /// <summary>
        /// 预收预付千分位
        /// </summary>
        public string AdvAmountThod { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 经办机构
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StateDic { get; set; }

        /// <summary>
        /// 签约主体
        /// </summary>
        public string MdeptName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string PrincUserName { get; set; }
        /// <summary>
        /// 印花税千分位
        /// </summary>
        public string StampTaxThod { get; set; }
        /// <summary>
        /// 第三方
        /// </summary>
        public string Comp3Name { get; set; }
        /// <summary>
        /// 第4方
        /// </summary>
        public string Comp4Name { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string PrincipalUserName { get; set; }
        /// <summary>
        /// 变更说明
        /// </summary>
        public string ChangeDesc { get; set; }
        /// <summary>
        /// 资金性质
        /// </summary>
         new public string FinanceTerms { get; set; }
    }


    /// <summary>
    /// 合同变更对象
    /// </summary>
    public class ContChangeInfo
    {
        /// <summary>
        /// 合同ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 变更人
        /// </summary>
        public string ChangePerson { get; set; }
        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime? ChangeDate { get; set; }
        /// <summary>
        /// 变更报告
        /// </summary>
        public string ChageDesc { get; set; }
        /// <summary>
        /// 变更版本
        /// </summary>
        public string ChangeVersions { get; set; }


    }
}
