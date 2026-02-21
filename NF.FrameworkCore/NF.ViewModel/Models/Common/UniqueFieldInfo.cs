using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.Common
{
    /// <summary>
    /// 校验字段值是否唯一实体类
    /// </summary>
   public class UniqueFieldInfo
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// 当前对象ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 0:客户1：供应商2：其他对方
        /// </summary>
        public int ObjType { get; set; }

      

    }
}
