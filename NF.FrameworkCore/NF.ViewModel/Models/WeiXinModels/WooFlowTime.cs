using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models.WeiXinModels
{

    /// <summary>
    /// 流程时间轴
    /// </summary>
    public class WooFlowTime
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public IList<Option> Options{get;set;}
        /// <summary>
        /// 状态
        /// </summary>
        public int Tstate { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>

        public string Sta { get; set; }
    }

    public class Option
    {
        /// <summary>
        /// 审批人员
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string YuJian { get; set; } = "";
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OpTime { get; set; }

    }


}
