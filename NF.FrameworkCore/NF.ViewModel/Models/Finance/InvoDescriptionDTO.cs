using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 发票明细
    /// </summary>
    public  class InvoDescriptionDTO
    {
        public int Id { get; set; }
        public int? ContInvoId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Count { get; set; }
        public decimal? Total { get; set; }

    }

    /// <summary>
    /// 发票明细
    /// </summary>
    public class InvoDescriptionViewDTO: InvoDescriptionDTO
    {
        /// <summary>
        /// 金额
        /// </summary>
        public string TotalThod { get; set; }

    }
}
