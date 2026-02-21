using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel
{
    public class Woocontract
    {
        /// <summary>
        /// //0-收款；1-付款
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 页
        /// </summary>
        public string start { get; set; }
        /// <summary>
        /// 行
        /// </summary>
        public string limit { get; set; }
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
        /// 资金性质（0：收、1：付）
        /// </summary>
        public int fType { get; set; } = -1;

        public string username { get; set; }

        public string userId { get; set; }

        public string callback { get; set; }
    }
}
