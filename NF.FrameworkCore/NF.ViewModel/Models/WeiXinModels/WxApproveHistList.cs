using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
   public  class WxApproveHistList
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public int? Mission { get; set; }
        /// <summary>
        /// 审批事项描述
        /// </summary>
        public string MissionDic { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public int? AppState { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string AppStateDic { get; set; }
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string CurrentNodeName { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string StartUserName { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime? StartDateTime { get; set; }

        public DateTime? CompleteDateTime { get; set; }

    }
}
