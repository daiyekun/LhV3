using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 审批类别父类
    /// </summary>
   public  class BaseAppList
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 审批对象
        /// </summary>
        public int ObjType { get; set; }
        /// <summary>
        /// 审批对象描述
        /// </summary>
        public string ObjTypeDic { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int AppObjId { get; set; }
        /// <summary>
        /// 审批对象名称
        /// </summary>
        public string AppObjName { get; set; }
        /// <summary>
        /// 审批对象编号
        /// </summary>
        public string AppObjNo { get; set; }
        /// <summary>
        /// 审批对象金额
        /// </summary>
        public decimal AppObjAmount { get; set; }
        /// <summary>
        /// 千分位
        /// </summary>
        public string AppObjAmountThod { get; set; }
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
        /// 资金性质
        /// </summary>
        public byte? FinceType { get; set; }
        /// <summary>
        /// 次要ID，比如提交实际资金时的合同ID
        /// </summary>
        public int? AppSecObjId { get; set; }
    }
}
