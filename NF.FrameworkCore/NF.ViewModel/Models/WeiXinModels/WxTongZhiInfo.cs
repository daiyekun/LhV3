using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{

    /// <summary>
    /// 审批通知条数
    /// </summary>
    public class WxTongZhiInfo
    {
        /// <summary>
        /// 微信账号
        /// </summary>
        public string WxCode { get; set; }
        /// <summary>
        /// 提醒条数
        /// </summary>
        public int Rows { get; set; }
    }
}
