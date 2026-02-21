using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class AppInstOpinDTO
    {
        /// <summary>
        /// 意见ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string AddUserName { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? AddDatetime { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string Opinion { get; set; }
        /// <summary>
        /// 节点收到日期
        /// </summary>
        public DateTime? ReceDateTime { get; set; }
       
    }
}
