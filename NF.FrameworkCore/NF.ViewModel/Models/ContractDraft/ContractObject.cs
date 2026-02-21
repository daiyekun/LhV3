using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.ContractDraft
{
    public class ContractObject
    {
        /// <summary>
        /// 标的-数量
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 标的-总价
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 本来标的描述
        /// </summary>
        public string BcDesc { get; set; }
        /// <summary>
        /// 标的对应单品类别完整路径/
        /// </summary>
        public string BcInceCateFullName { get; set; }
        /// <summary>
        /// 单品ID
        /// </summary>
        public int? BcInstanceId { get; set; }
        /// <summary>
        /// 单品名称
        /// </summary>
        public string BcName { get; set; }
        /// <summary>
        /// 单品编号
        /// </summary>
        public string BcNo { get; set; }
        /// <summary>
        /// 单品-销售报价
        /// </summary>
        public decimal? BcSalePrice { get; set; }
        /// <summary>
        /// 单品-单位
        /// </summary>
        public string BcUnit { get; set; }
        /// <summary>
        /// 标的-已完成/已交付数量
        /// </summary>
        public decimal? ComplateAmount { get; set; } 
        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; } 
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int? CreateUserId { get; set; }
        /// <summary>
        /// 标的-折扣率
        /// </summary>
        public decimal? DiscountRate{ get; set; }
        /// <summary>
        /// 标的-最终总价
        /// </summary>
        public decimal? FinalAmountMoney { get; set; }
        /// <summary>
        /// 标的-ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标的类型
        /// </summary>
        public byte? IsFromCategory{ get; set; }
        /// <summary>
        /// 名义报价
        /// </summary>
        public decimal? MingYiPrice{ get; set; }
        /// <summary>
        /// 名义折扣率
        /// </summary>
        public decimal? MingYiRate { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 计划交付时间
        /// </summary>
        public DateTime? PlanDateTime { get; set; } 
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? Price{ get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 销售报价
        /// </summary>
        public decimal? SalePrice{ get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 小计
        /// </summary>
        public decimal? SubTotal  { get; set; }
        /// <summary>
        /// 小计折扣率
        /// </summary>
        public decimal? SubTotalRate { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Stype { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit  { get; set; }
        /// <summary>
        ///  =单品管理
        /// </summary>
        public BcInstance BcInstance { get; set; }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="subjectMatter">标的对象</param>
        public void SetValues(ContSubjectMatter subm)
        {
            this.Amount = subm.Amount??0;
            this.AmountMoney = subm.AmountMoney;
            this.BcDesc = "";
            this.BcInceCateFullName = "";
            this.BcInstanceId = subm.BcInstanceId;
            this.BcName = subm.BcInstance == null ? "" : subm.BcInstance.Name;
            this.BcNo= subm.BcInstance == null ? "" : subm.BcInstance.Code;
            this.BcSalePrice= subm.BcInstance == null ? 0 : subm.BcInstance.Price??0;
            this.BcUnit = subm.BcInstance == null ? "": subm.BcInstance.Unit;
            this.ComplateAmount= subm.ComplateAmount??0;
            this.ContId = subm.ContId??0;
            this.CreateDateTime = subm.CreateDateTime;
            this.CreateUserId = subm.CreateUserId;
            this.DiscountRate = subm.DiscountRate;
            this.FinalAmountMoney = 0;
            this.Id = subm.Id;
            this.IsFromCategory = subm.IsFromCategory;
            this.MingYiPrice = 0;
            this.MingYiRate = 0;
            this.Name = subm.Name;
            this.PlanDateTime = subm.PlanDateTime;
            this.Price = subm.Price;
            this.Remark = subm.Remark;
            this.SalePrice = subm.SalePrice;
            this.Spec = subm.Spec;
            this.SubTotal = subm.SubTotal;
            this.SubTotalRate = subm.SubTotalRate;
            this.Stype = subm.Stype;
            this.Unit = subm.Unit;
            this.BcInstance = subm.BcInstance;




        }  




    }
    /// <summary>
    /// 聚合的合同文本数据
    /// </summary>
    public class AggregatedContractObject
    {
       public  ContractObject CtObject;
        public BcInstance BcData;
    }


   
}
    

