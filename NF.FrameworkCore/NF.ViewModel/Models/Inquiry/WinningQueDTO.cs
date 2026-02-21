using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
 public   class WinningQueDTO
    {
        /// <summary>
        /// 中标货物id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 询价id
        /// </summary>
        public int QueId { get; set; }
        /// <summary>
        ///品名
        /// </summary>
        public string WinningName { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string WinningUntiId { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string WinningModel { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal WinningTotalPrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal WinningUitprice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal WinningQuantity { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }
        public string GuidFileName { get; set; }
        public string SourceFileName { get; set; }
    }
}
