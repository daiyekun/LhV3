using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
  public class FlowPerm
    {
        public int SpId { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public int WfItem { get; set; }
        /// <summary>
        /// 微信ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 审批对象
        /// </summary>
        public int SpType { get; set; }
    }
    /// <summary>
    /// 审批意见
    /// </summary>
    public class SubOption
    {
        /// <summary>
        /// 审批实例ID
        /// </summary>
        public int InstId { get; set; }
        /// <summary>
        /// 审批对象类别（客户，供应商、合同）
        /// </summary>
        public int ObjType { get; set; }
        /// <summary>
        /// 对象金额
        /// </summary>
        public decimal?ObjMoney { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// 审批对象意见
        /// </summary>
        public string Option { get; set; }
        /// <summary>
        /// 审批对象结果1:同意，2:不同意
        /// </summary>
        public int OptRes { get; set; }
        /// <summary>
        /// 提交微信意见人ID
        /// </summary>
        public string SubmitWxId { get; set; }

        public string DDs { get; set; }
    }
}
