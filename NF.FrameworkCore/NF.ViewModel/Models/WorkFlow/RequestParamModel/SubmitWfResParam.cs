using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 提交流程获取流程图的参数
    /// </summary>
    public class SubmitWfResParam
    {
        /// <summary>
        /// 流程模板
        /// </summary>
        public int TempId { get; set; }
        /// <summary>
        /// 审批金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
