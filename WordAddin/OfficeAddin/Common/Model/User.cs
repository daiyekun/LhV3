using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 状态标识
        /// </summary>
        public string ReadOnly { get; set; }
    }
}
