using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 标的大列表
    /// </summary>
    public class ContSubjectMatterListDTO: INfEntityHandle
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int? ContId { get; set; }
        /// <summary>
        /// 所属类别
        /// </summary>
        public string CatePath { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContNo { get; set; }
        /// <summary>
        /// 标的名称
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 单价千分位
        /// </summary>
        public string PriceThod { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Amountstr { get; set; }

      
        /// <summary>
        /// 小计千分位
        /// </summary>
        public string TotalThod { get; set; }
        /// <summary>
        /// 合同折让
        /// </summary>
        public string HtZr { get; set; }
        /// <summary>
        /// 报价
        /// </summary>
        public string SalePriceThod { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public string Zkl { get; set; }
       /// <summary>
       /// 备注
       /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 计划交付日期
        /// </summary>
        public DateTime ? PlanDateTime { get; set; }
        /// <summary>
        /// 已交付数量
        /// </summary>
        public decimal ComplateAmount { get; set; }
        /// <summary>
        /// 未交付数量
        /// </summary>
        public decimal NotDelNum { get; set; }
        /// <summary>
        /// 交付比例
        /// </summary>
        public string JfBl { get; set; }
        /// <summary>
        /// 实际交付日期
        /// </summary>
        public DateTime? SjJfRq { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string ContStateDic { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public int ContState { get; set; }
        /// <summary>
        /// 交付状态
        /// </summary>
        public byte? SubState { get; set; }
        //单品名称
        public string BcName { get; set; }
        /// <summary>
        /// 单品编号
        /// </summary>
        public string BcCode { get; set; }
        /// <summary>
        /// 合同对方id
        /// </summary>
        public int CompId { get; set; }

        public FieldInfo GetPropValue(string propName)
        {
            var fieldinfo = new FieldInfo();
            var obj = this.GetType().GetProperty(propName);
            fieldinfo.FileType = obj.PropertyType;
            fieldinfo.FileValue = obj.GetValue(this, null);
            //if (propName == "Pro")
            //{
            //    try
            //    {
            //        fieldinfo.FileValue = EmunUtility.GetDesc(typeof(BcPropertyEnums), Convert.ToInt32(this.GetType().GetProperty(propName).GetValue(this, null)));
            //    }
            //    catch (Exception)
            //    {

            //        return fieldinfo;
            //    }

            //}
            //else
            //{
            //    var obj = this.GetType().GetProperty(propName);
            //    fieldinfo.FileType = obj.PropertyType;
            //    fieldinfo.FileValue = obj.GetValue(this, null);
            //}
            return fieldinfo;
        }
    }
}
