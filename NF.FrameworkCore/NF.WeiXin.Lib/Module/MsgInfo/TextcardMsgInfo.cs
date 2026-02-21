using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.WeiXin.Lib.Module.MsgInfo
{
    /// <summary>
    /// 文本卡片消息
    /// </summary>
    public class TextcardMsgInfo: WxAppMsgInfo
    {
        /// <summary>
        /// 表示是否开启id转译，0表示否，1表示是，默认0
        /// </summary>
        public int enable_id_trans { get; set; } = 0;
        /// <summary>
        /// 表示是否开启重复消息检查，0表示否，1表示是，默认0
        /// </summary>
        public int enable_duplicate_check { get; set; } = 0;
        /// <summary>
        /// 表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时
        /// </summary>
        public int duplicate_check_interval { get; set; } = 1800;

        /// <summary>
        /// 消息主体
        /// </summary>
        public TextcardMain textcard { get; set; }

    }
}
