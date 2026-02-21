using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 交付列表显示字段
    /// </summary>
    public class JiaoFuListInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContName { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int? ContId { get; set; }
        /// <summary>
        /// 标的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 单价千分位
        /// </summary>
        public string PriceThod { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? Price{ get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// 已交付数量
        /// </summary>
        public decimal? ComplateAmount { get; set; }
        /// <summary>
        /// 本次交付数量
        /// </summary>
        public decimal? CurrDelNum { get; set; }
        /// <summary>
        /// 本次交付金额
        /// </summary>
        public string  CurrDelmoneyThod { get; set; }
        /// <summary>
        /// 本次交付金额
        /// </summary>
        public decimal? CurrDelmoney { get; set; }
        /// <summary>
        /// 未交付数量
        /// </summary>
        public decimal? NotDelNum { get; set; }
        /// <summary>
        /// 原始未交付数量
        /// </summary>
        public decimal? NotOldDelNum { get; set; }
        /// <summary>
        /// 交付比例
        /// </summary>
        public string JfBl { get; set; }
        /// <summary>
        /// 小计
        /// </summary>
        public string TotalThod { get; set; }
        /// <summary>
        /// 计划交付日期
        /// </summary>
        public DateTime? PlanDateTime { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 交付总额
        /// </summary>
        //public string DevSumMThod { get; set; }

        public int CompId { get; set; }
    }
}
