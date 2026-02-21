using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.Common
{
    /// <summary>
    /// 权限请求实体
    /// </summary>
    public class RequestPermissionInfo
    {
        /// <summary>
        /// 功能标识字符串
        /// </summary>
        public string FuncCode { get; set; }
        /// <summary>
        /// 对象ID
        /// </summary>
        public int ObjId { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
         public int ObjHtId { get; set; }
        /// <summary>
        /// 对象ID集合
        /// </summary>
        public IList<int> ObjIds { get; set; }

    }
}
