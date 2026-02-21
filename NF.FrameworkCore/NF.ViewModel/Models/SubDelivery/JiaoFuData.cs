using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 交付数据
    /// </summary>
    public class JiaoFuData
    {
        /// <summary>
        /// 标的ID
        /// </summary>
        public int SubId { get; set; }
        /// <summary>
        /// 交付之前的剩余数量
        /// </summary>
        public decimal? YanShiNum { get; set; }
        /// <summary>
        /// 当前交付数量
        /// </summary>
        public decimal? CurrNumber { get; set; }
        /// <summary>
        /// 此次交付完毕以后剩余数量
        /// </summary>
        public decimal? NotNumber { get; set; }
    }
}
