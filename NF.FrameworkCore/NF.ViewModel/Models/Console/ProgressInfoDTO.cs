using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 首页进度
    /// </summary>
    public class ProgressInfoDTO
    {
        /// <summary>
        /// 收款合同完成比例
        /// </summary>
        public string SkHtWcBl { get; set; } = "0%";
        /// <summary>
        /// 付款合同完成比例
        /// </summary>
        public string FkHtWcBl { get; set; } = "0%";
        /// <summary>
        /// 收票完成比例
        /// </summary>
        public string SpWcBl { get; set; } = "0%";
        /// <summary>
        /// 开票完成比例
        /// </summary>
        public string KpWcBl { get; set; } = "0%";
    }
}
