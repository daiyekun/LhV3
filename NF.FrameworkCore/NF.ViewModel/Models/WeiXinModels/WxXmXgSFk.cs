using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel
{/// <summary>
 /// 微信项目相关收款合同
 /// </summary>
    public class WxXmXgSk
    {   /// <summary>
        /// 合同名称
        /// </summary>
        public string HtSName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HtSCode { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string HtSContAmThod { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string HtSCompName { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string HtSCurrName { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string HtSContTypeName { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string HtSContStateDic { get; set; }
        /// <summary>
        /// 资金性质
        /// </summary>
        public string HtSFinceTypeName { get; set; }
        /// <summary>
        /// 合同id
        /// </summary>
        public int HtSId { get; set; }

        public int HtSCompId { get; set; }
    }
    /// <summary>
    /// 微信项目相关付款合同
    /// </summary>
    public class WxXmXgFk
    {   /// <summary>
        /// 合同名称
        /// </summary>
        public string HtFName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string HtFCode { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string HtFContAmThod { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string HtFCompName { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string HtFCurrName { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public string HtFContTypeName { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string HtFContStateDic { get; set; }
        /// <summary>
        /// 资金性质
        /// </summary>
        public string HtFFinceTypeName { get; set; }
        /// <summary>
        /// 合同id
        /// </summary>
        public string HtFId { get; set; }
    }
}
