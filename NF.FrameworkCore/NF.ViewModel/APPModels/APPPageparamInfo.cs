using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.APPModels
{
    public class APPPageparamInfo
    {
        /// <summary>
        /// 当前页条数
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string keyWord { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? beginData { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endData { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string orderField { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 请求类别
        /// 0：分页
        /// 1：不分页
        /// </summary>
        public int requestType { get; set; } = 0;
        /// <summary>
        /// 是不是走选择框模式
        /// </summary>
        public bool selitem { get; set; } = false;
        /// <summary>
        /// 其他intWhere条件
        /// </summary>
        public int otherwh { get; set; } = -1;
        /// <summary>
        /// 资金性质（0：开票、1：收票）
        /// </summary>
        public int fType { get; set; } = -1;
        /// <summary>
        /// 查询类型
        /// </summary>
        public int search { get; set; }

        public int userId { get; set; }

        public string callback { get; set; }
    }
}
