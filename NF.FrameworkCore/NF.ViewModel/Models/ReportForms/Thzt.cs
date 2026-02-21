using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    public class Thzt
    {
        /// <summary>
        /// 未执行合同数
        /// </summary>
        public int Wzx {get;set;}
        /// <summary>
        /// 执行中合同数
        /// </summary>
        public int Zxz { get; set; }
        /// <summary>
        /// 审批通过合同数
        /// </summary>
        public int Sptg { get; set; }
        /// <summary>
        /// 已完成
        /// </summary>
        public int Ywz { get; set; }
        /// <summary>
        /// 已终止
        /// </summary>
        public int Yzz { get; set; }
    }
}
