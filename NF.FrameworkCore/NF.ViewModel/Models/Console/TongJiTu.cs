using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 图表1
    /// </summary>
    public class HtZhiXingTongji
    {
        /// <summary>
        /// 收款合同金额
        /// </summary>
        public IList<decimal> SkhtJe { get; set; }
        /// <summary>
        /// 付款合同金额
        /// </summary>
        public IList<decimal> FkHtJe { get; set; }
        /// <summary>
        /// 实际收款金额
        /// </summary>
        public IList<decimal> SjSkJe { get; set; }
        /// <summary>
        /// 实际付款金额
        /// </summary>
        public IList<decimal> SjFkJe { get; set; }
        /// <summary>
        /// 按月份计算合同条数
        /// </summary>
        public IList<long> HtCount { get; set; }
    }
    /// <summary>
    /// 合同类别统计
    /// </summary>
    public class HtLbTjInfo
    {
        /// <summary>
        /// 合同类别
        /// </summary>
        public IList<string> HtLbs = new List<string>();
        /// <summary>
        /// 类别金额数组
        /// </summary>
        public IList<HtLbJeInfo> HtLbJes = new List<HtLbJeInfo>();

    }
    


    /// <summary>
    /// 合同类别金额对象
    /// </summary>
    public class HtLbJeInfo
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal value { get; set; }
    }

}
