using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   /// <summary>
   /// 修改字段信息
   /// </summary>
   public class UpdateFieldInfo
    {
        /// <summary>
        /// 修改数据ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 其他ID
        /// </summary>
        public int OtherId { get; set; }
        /// <summary>
        /// 操作当前的金额
        /// </summary>
        public decimal UpdateMoney { get; set; } = 0;
        /// <summary>
        /// 修改数据字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 修改字段值
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// 当前操作人员ID
        /// </summary>
        public int CurrUserId { get; set; } = 0;
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType { get; set; } = "string";
    }
}
