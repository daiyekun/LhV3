using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 首页合同列表数据对象
    /// </summary>
   public class ConsoleContractInfoDTO
    {
        /// <summary>
        /// 合同ID
        /// </summary>
        public  int Id { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 合同金额千分位
        /// </summary>
        public string HtJeThond { get; set; }
        /// <summary>
        /// 合同完成比例
        /// </summary>
        public string HtWcBl { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public string FpJeThod { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string ContSingNo { get; set; }
    }
}
