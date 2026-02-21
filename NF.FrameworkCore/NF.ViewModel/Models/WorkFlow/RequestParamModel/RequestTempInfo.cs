using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 请求模板信息
    /// </summary>
    public  class RequestTempInfo
    {
        /// <summary>
        /// 审批事项
        /// </summary>
        public int FlowItem { get; set; }
        /// <summary>
        /// 经办机构
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 审批对象
        /// </summary>
        public int ObjType { get; set; }
        /// <summary>
        /// 对象类别ID
        /// </summary>
        public int ObjCateId { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int ObjId { get; set; }

    }
}
