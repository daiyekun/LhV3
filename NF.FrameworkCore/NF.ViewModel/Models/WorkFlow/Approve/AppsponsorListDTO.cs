using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 已发起对象
    /// </summary>
    public  class AppsponsorListDTO: BaseAppList
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
        /// 最终处理日期
        /// </summary>
        public DateTime? CompleteDateTime { get; set; }

       





    }
}
