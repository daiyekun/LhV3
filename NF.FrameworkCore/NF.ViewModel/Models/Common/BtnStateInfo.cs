using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 按钮权限对象
    /// </summary>
    public class BtnStateInfo
    {
        /// <summary>
        /// 更新
        /// </summary>
        public bool Update { get; set; } = false;
        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete { get; set; } = false;
        /// <summary>
        /// 提交
        /// </summary>
        public bool Submit { get; set; } = false;
        /// <summary>
        /// 确认
        /// </summary>
        public bool Confirm { get; set; } = false;
        /// <summary>
        /// 打回
        /// </summary>
        public bool Back { get; set; } = false;

    }
}
