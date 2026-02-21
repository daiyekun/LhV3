using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 审批权限对象
    /// </summary>
    public class WorFlowPerssion
    {
        /// <summary>
        /// 审批实例
        /// </summary>
        public int InstId { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int NodeId { get; set; }
        /// <summary>
        /// 节点字符串ID
        /// </summary>
        public string NodeStrId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>

        public int Qx { get; set; }

    }
    /// <summary>
    /// 流程节点显示对象
    /// </summary>
    public class WfNodeView
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Nc { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public int Nid { get; set; }
        /// <summary>
        /// 审批意见集合
        /// </summary>
        public IList<OptionMsg> Options { get; set; }
        /// <summary>
        /// 审批状态 0：为审批，1：审批中，2审批通过 3被打回
        /// </summary>
        public int Spst { get; set; }
        /// <summary>
        /// 实例状态 
        /// 1:审批中
        ///2:已通过
        ///3:被否决
        /// </summary>
        public int Insst { get; set; }

    }
    /// <summary>
    /// 审批意见
    /// </summary>
    public class OptionMsg
    {
        /// <summary>
        /// 用户ＩＤ
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Xm { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string Yj { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public string Sj { get; set; }

    }

}
