
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.Common
{
  public  class APPwork
    {
        /// <summary>
        /// 当前页条数
        /// </summary>
        public int? start { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int? limit { get; set; } 
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 查询类型
        /// </summary>
        public string wftype { get; set; }

        public int ftype { get; set; } = 1;
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

        public int userId { get; set; }
        public string callback { get; set; }
    }
}
