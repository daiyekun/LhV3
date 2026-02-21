using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class SignInFromWordUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否自读
        /// </summary>
        public string ReadOnly { get; set; } = "false";
    }
}
