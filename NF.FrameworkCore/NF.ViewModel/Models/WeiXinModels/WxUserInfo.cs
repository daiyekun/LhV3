using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models.WeiXinModels
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public class WxUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string UdisName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string UdepName { get; set; }
        /// <summary>
        /// 用户电话
        /// </summary>
        public string Utel { get; set; }
        /// <summary>
        /// 用户移动电话
        /// </summary>
        public string Umobile { get; set; }
        /// <summary>
        /// 用户Email
        /// </summary>
        public string Uemail { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
    }
}
