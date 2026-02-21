using NF.Common.Utility;
using NF.ViewModel.Extend.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace NF.ViewModel.Models
{
    public class ProjectManagerViewDTO : ProjectManagerDTO, INfEntityHandle
    {
        /// <summary>
        /// 项目类别
        /// </summary>
        public string ProjTypeName { get; set; }
        /// <summary>
        /// 创建人显示名称
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 赋值人显示名称
        /// </summary>
        public string PriUserName { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public string PstateDic { get; set; }

        /// <summary>
        /// 计划收款千分位
        /// </summary>
      
        public string BugetCollectAmountMoneyThod { get; set; }
        /// <summary>
        /// 计划付款千分位
        /// </summary>
      
        public string BudgetPayAmountMoneyThod{ get; set; }
        /// <summary>
        /// 收款币种
        /// </summary>
        public string BudgetCollectCurrencyName { get; set; }
        /// <summary>
        /// 付款币种
        /// </summary>
        public string BudgetPayCurrencyName { get; set; }
        /// <summary>
        /// 创建人部门ID
        /// </summary>
        public int UserDeptId { get; set; }
       
       
        /// <summary>
        /// 流程状态
        /// </summary>
        public byte? WfState { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public string WfCurrNodeName { get; set; }
        /// <summary>
        /// 审批事项描述
        /// </summary>
        public string WfItemDic { get; set; }
        /// <summary>
        /// 流程状态描述
        /// </summary>
        public string WfStateDic { get; set; }
        /// <summary>
        /// 项目来源
        /// </summary>
        public string ProjectSourceName { get; set; }
        public int? ProjectSource { get; set; }
        public int? WfItem { get; set; }

        private string GetStateDic(int ?psate)
        {
            return EmunUtility.GetDesc(typeof(ProjStateEnum), psate ?? 0);
        }

        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <returns></returns>
        public FieldInfo GetPropValue(string propName)
        {
            var obj = this.GetType().GetProperty(propName);
            return new FieldInfo {
                FileType = obj.PropertyType,
                FileValue = obj.Name== "Pstate"? GetStateDic((int?)obj.GetValue(this, null)) : obj.GetValue(this, null)
            };

        }
    }

    /// <summary>
    /// 项目资金统计
    /// </summary>
    public class ProjFundCalcul
    {
        /// <summary>
        /// 收款合同金额
        /// </summary>
        public string SkHtJeThod { get; set; } = "0.00";
        /// <summary>
        /// 已收款金额
        /// </summary>
        public string SkCompAtmThod { get; set; } = "0.00";
        /// <summary>
        /// 未收金额
        /// </summary>
        public string NoSkCompAtmThod { get; set; } = "0.00";

        /// <summary>
        /// 已开金额
        /// </summary>
        public string SkCompInThod { get; set; } = "0.00";
        /// <summary>
        /// 未开金额
        /// </summary>
        public string NoSkCompInThod { get; set; } = "0.00";
        /// <summary>
        /// 财务应收
        /// </summary>
        public string SkCaiYsThod { get; set; } = "0.00";
        /// <summary>
        /// 财务预收
        /// </summary>
        public string SKCaiYjThod { get; set; } = "0.00";

        /// <summary>
        /// 付款合同金额
        /// </summary>
        public string FkHtJeThod { get; set; } = "0.00";
        /// <summary>
        /// 已付款金额
        /// </summary>
        public string FkCompAtmThod { get; set; } = "0.00";
        /// <summary>
        /// 未付金额
        /// </summary>
        public string NoFkCompAtmThod { get; set; } = "0.00";

        /// <summary>
        /// 已收金额
        /// </summary>
        public string FkCompInThod { get; set; } = "0.00";
        /// <summary>
        /// 未收金额
        /// </summary>
        public string NoFkCompInThod { get; set; } = "0.00";
        /// <summary>
        /// 财务应付
        /// </summary>
        public string FkCaiYsThod { get; set; } = "0.00";
        /// <summary>
        /// 财务预付
        /// </summary>
        public string FKCaiYjThod { get; set; } = "0.00";

    }
}
