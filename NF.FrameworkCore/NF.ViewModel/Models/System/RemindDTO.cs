using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 提醒
    /// </summary>
   public class RemindDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///事项
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        /// 事项名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 事项描述（可修改）
        /// </summary>
        public string CustomName { get; set; }
        /// <summary>
        /// 提前天数
        /// </summary>
        public int AheadDays { get; set; }
        /// <summary>
        /// 延后天数
        /// </summary>
        public int DelayDays { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? IsDelete { get; set; }
    }
}
