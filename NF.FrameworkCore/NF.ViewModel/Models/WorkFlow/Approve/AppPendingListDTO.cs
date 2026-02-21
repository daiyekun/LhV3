using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public class AppPendingListDTO: BaseAppList
    {
        public int? CurrentNodeId { get; set; }
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string CurrentNodeName { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string StartUserName { get; set; }
    }
}
