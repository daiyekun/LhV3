using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{

    public class SysFields: SysField { }


    /// <summary>
    /// 合同DTO
    /// </summary>
    public class ContractInfoDTO
    {
        public int? Zbid { get;set;}
        public int? Xjid { get; set; }
        public int? Ytid { get; set; }
        public string ContAmThod { get; set; }
        public string StampTaxThod { get; set; }
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
        /// <summary>
        /// 签约主体简称
        /// </summary>
        public string MainDeptShortName { get; set; }
        /// <summary>
        /// 签约人身份证
        /// </summary>
        public string ContSingNo { get; set; }
        /// <summary>
        /// 自定义字段
        /// </summary>
        public string CustField { get; set; }

        


     

    }

    #region 显示页面显示类
    /// <summary>
    /// 显示View
    /// </summary>
    public class ContractInfoViewDTO: ContractInfoDTO
    {
        /// <summary>
        /// 合同对方类型id
        /// </summary>
        public byte? Ctype { get; set; }
        public string ZbName { get; set; }
        public string XjName { get; set; }
        public string YtName { get; set; }
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
        /// 总包合同名称
        /// </summary>
        public string SumContName { get; set; }
        /// <summary>
        /// 合同完成金额
        /// </summary>
        public string HtWcJeThod { get; set; }
        /// <summary>
        /// 完成比例
        /// </summary>
        public string HtWcBl { get; set; }
        /// <summary>
        /// 票款差
        /// </summary>
        public string PiaoKaunCha { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public string FaPiaoThod { get; set; }
        /// <summary>
        /// 合同对应自定义字段值
        /// </summary>

      public List<string> CustomFields { get; set; }


    }

    #endregion 显示页面显示类

    /// <summary>
    /// 修改和新增时绑定表单View
    /// </summary>
    public class ContractInfoCreateViewDTO: ContractInfoDTO
    {


    }


    #region 列表显示类
    /// <summary>
    /// 合同列表查看
    /// </summary>
    public class ContractInfoListViewDTO : INfEntityHandle
    {
        public string ZbName { get; set; }
        public string XjName { get; set; }
        public string YtName { get; set; }
        /// <summary>
        /// 当前节点审批人
        /// </summary>
        public string Dqjdspr { get; set; }
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 主体ID
        /// </summary>
        public int? MainDeptId { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 对方编号
        /// </summary>
        public string OtherCode { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string ContTypeName { get; set; }

        /// <summary>
        /// 合同来源
        /// </summary>
        public string ContSName { get; set; }
        /// <summary>
        /// 合同对方ID
        /// </summary>
        public int? CompId { get; set; }
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
        ///是否总分包
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
        /// 币种ID
        /// </summary>
        public int? CurrencyId { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrName { get; set; }
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
        /// 印花税千分位
        /// </summary>
        public string StampTax { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 签订日期
        /// </summary>
        public DateTime? SngDate { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EfDate { get; set; }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? PlanDate { get; set; }
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ActDate { get; set; }
        /// <summary>
        /// 经办机构
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string ContStateDic { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int? ContState { get; set; }
        /// <summary>
        /// 签约主体
        /// </summary>
        public string MdeptName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string PrincUserName { get; set; }
        /// <summary>
        /// 备注1
        /// </summary>
        public string Reserve1 { get; set; }
        /// <summary>
        /// 备注2
        /// </summary>
        public string Reserve2 { get; set; }
        /// <summary>
        /// 总包合同ID
        /// </summary>
        public int? SumContId { get; set; }

        /// <summary>
        /// 变更次数
        /// </summary>
        public int ModificationTimes { get; set; } = 0;
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
        /// <summary>
        /// 经办机构
        /// </summary>
        public int? DeptId { get; set; }
        /// <summary>
        /// 合同类别ID
        /// </summary>
        public int? ContTypeId { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal AmountMoney { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string ContSingNo { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string BankAccount { get; set; }
        /// <summary>
        /// 0：收款，1：付款
        /// </summary>
        public byte FinanceType{get;set;}
        /// <summary>
        /// 发票已确认金额
        /// </summary>
        public string CompInAmThod { get; set; }
        /// <summary>
        /// 实际资金已确认金额（完成金额）
        /// </summary>
        public string  CompActAmThod { get; set; }
        /// <summary>
        /// 完成比例，保留2位小数
        /// </summary>
        public string CompRatioStr { get; set; }
        /// <summary>
        /// 发票差额（已完成金额-发票已确认金额）
        /// </summary>
        public string BalaTickThod { get; set; }
        /// <summary>
        /// 折合本币
        /// </summary>
        public decimal ContAmRmb { get; set; }
        /// <summary>
        /// 发票确认金额
        /// </summary>
        public decimal CompInAm { get; set; }
        /// <summary>
        /// 实际资金完成金额
        /// </summary>
        public decimal CompActAm { get; set; }



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

        #endregion 列表显示类

        

    }
    #region 选择合同对象
    /// <summary>
    /// 选择合同实体
    /// </summary>
    public class SelectContractInfoDTO
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
        /// 合同编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 资金性质
        /// </summary>
        public byte FinanceType { get; set; }
        /// <summary>
        /// 资金性质(收款、付款)
        /// </summary>
        public string FinanceTypeDesc { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string ContAmThod { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string ContStateDic { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int? ContState { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 合同列表
        /// </summary>
        public string ContTypeName { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 签约人身份证
        /// </summary>

        public string ContSingNo { get; set; }






    }
    #endregion

    /// <summary>
    /// 合同对方合同信息
    /// </summary>
    public class CompanyContract
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string ContAmThod { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string ContStateDic { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string ContTypeName { get; set; }



    }
    /// <summary>
    ///项目合同信息
    /// </summary>
    public class ProjContract
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 对方ID
        /// </summary>
        public int CompId { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrName { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string ContAmThod { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string ContStateDic { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string ContTypeName { get; set; }
       /// <summary>
        /// 资金性质
        /// </summary>
        public string FinceTypeName { get; set; }



    }



}
