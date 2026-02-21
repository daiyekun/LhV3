using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 首页项目列表显示对象
    /// </summary>
   public class ProjectManagerConsoleDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项目收款合同金额
        /// </summary>
        public string XmSkHtJeThod { get; set; }
        /// <summary>
        /// 项目付款合同金额
        /// </summary>
        public string XmFkHtJeThod { get; set; }

        /// <summary>
        /// 项目收款金额
        /// </summary>
        public string XmSkJeThod { get; set; }
        /// <summary>
        /// 项目付款金额
        /// </summary>
        public string XmFkJeThod { get; set; }


    }
}
