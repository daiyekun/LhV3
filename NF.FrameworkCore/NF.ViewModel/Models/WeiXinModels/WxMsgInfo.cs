using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 消息对象
    /// </summary>
   public class FlowWxMsgInfo
    {
        /// <summary>
        /// 微信账号
        /// </summary>
        public string WxCodes { get; set; }
        /// <summary>
        /// 审批对象ID
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// 名称（合同名称-合同对方名称....）
        /// </summary>
        public string ObjName { get; set; }
        /// <summary>
        /// 消息编号（合同对方编号。。。。）
        /// </summary>
        public string ObjNo { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public string ObjType { get; set; }
        /// <summary>
        /// 金额 千分位。保留2为小数
        /// </summary>
        public string ObjMoney { get; set; }
        /// <summary>
        /// FlowObjEnums来至此枚举值，0：客户。。。。。
        /// </summary>
        public int FlowType { get; set; }
        /// <summary>
        /// 收款/付款
        /// </summary>
        public string FinceType { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string HtDf { get; set; }
        /// <summary>
        /// 经办机构
        /// </summary>

        public string JbJg { get; set; }
        /// <summary>
        /// 消息类型
        /// 审批通过，打回时使用
        /// 6：提醒之前审批人员以及发起人员
        /// </summary>
        public int MsgType { get; set; } = -1;
        /// <summary>
        /// 处理结果
        /// 0：同意
        /// 1：打回
        /// </summary>
        public int AppRest { get; set; } = 0;
        /// <summary>
        /// 处理意见
        /// </summary>
        public string Option { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Node { get; set; }
        /// <summary>
        /// 处理人员
        /// </summary>
        public string AppUser { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string StartUser { get; set; }
    }
}
