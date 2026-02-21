using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class ResponTemp
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TempId { get; set; }
        /// <summary>
        /// 模板历史
        /// </summary>
        public int TempHistId { get; set; }
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public int InstId { get; set; }
    }
}
