using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class AppInstDTO
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TempId { get; set; }
        /// <summary>
        /// 模板历史ID
        /// </summary>
        public int TempHistId { get; set; }

        /// <summary>
        /// 对象类型(0:客户)
        /// </summary>
        public int ObjType { get; set; }
        /// <summary>
        /// 对象Id
        /// </summary>
        public int AppObjId { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        public string AppObjName { get; set; }
        /// <summary>
        /// 对象编号
        /// </summary>
        public string AppObjNo { get; set; }
        /// <summary>
        /// 对象金额
        /// </summary>
        public decimal AppObjAmount { get; set; }
        /// <summary>
        /// 对象类别ID
        /// </summary>
        public int AppObjCateId { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public int Mission { get; set; }
        /// <summary>
        /// 资金类型（0:收款。1:付款）
        /// </summary>
        public byte? FinceType { get; set; }
        /// <summary>
        /// 次要ID，比如提交实际资金时合同ID
        /// </summary>
        public int? AppSecObjId { get; set; }


    }
}
