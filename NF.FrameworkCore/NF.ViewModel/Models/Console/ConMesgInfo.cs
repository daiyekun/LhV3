using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class ConMesgInfo
    {
        /// <summary>
        /// 待处理数
        /// </summary>
        public string PedingNum { get; set; } = "0";
        /// <summary>
        /// 被打回数
        /// </summary>
        public string BeiDahuiNum { get; set; } = "0";
        /// <summary>
        /// 到期计划收款
        /// </summary>
        public string DqjhskNum { get; set; } = "0";
        /// <summary>
        /// 到期计划付款
        /// </summary>
        public string DqjhfkNum { get; set; } = "0";
        /// <summary>
        /// 待处理实际收款
        /// </summary>
        public string DclsjskNum { get; set; } = "0";
        /// <summary>
        /// 待处理实际付款
        /// </summary>
        public string DclsjfkNum { get; set; } = "0";
        /// <summary>
        /// 待处理收票
        /// </summary>
        public string DclspNum { get; set; } = "0";
        /// <summary>
        /// 待处理开票
        /// </summary>
        public string DclkpNum { get; set; } = "0";
        /// <summary>
        /// 到期收款合同
        /// </summary>
        public string DqSkHtNum { get; set; } = "0";
        /// <summary>
        /// 到期付款合同
        /// </summary>
        public string DqFkHtNum { get; set; } = "0";
        /// <summary>
        /// 到期收款合同
        /// </summary>
        public string XqySkHtNum { get; set; } = "0";
        /// <summary>
        /// 到期付款合同
        /// </summary>
        public string XqyFkHtNum { get; set; } = "0";
        /// <summary>
        /// 已通过
        /// </summary>
        public string YiTongGuo { get; set; } = "0";
        /// <summary>
        /// 我的工作台
        /// </summary>
        public string Mydesc { get; set; } = "0";
        /// <summary>
        /// 跟踪工作台
        /// </summary>
        public string Gzdesc { get; set; } = "0";
        /// <summary>
        /// 进度评定提醒
        /// </summary>
        public string Jdpdtx { get; set; } = "0";
    }
}
