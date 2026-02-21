using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 审批
    /// </summary>
    public class WfBase
    {
        /// <summary>
        /// 审批意见数据对象
        /// </summary>
        public Dictionary<string,List<WfOption>> DicWfData=new Dictionary<string, List<WfOption>>();
    }

    /// <summary>
    /// 审批意见
    /// </summary>
    public class WfOption
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string NodeStrId { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int? NodeId { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string AppUserName { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string Option { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? AppDate { get; set; }

        public int CreateUserId { get; set; }

        public string UserEs { get; set; }
        public string UserEsTy { get; set; }

        public string ImgSrc { get; set; }
    }
}
