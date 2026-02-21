using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 查看修改显示
    /// </summary>
    public class ContTxtTemplateViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public int? TepType { get; set; }
        public int? TextType { get; set; }
        public string DeptIds { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public int? Vesion { get; set; }
        public int? Tstate { get; set; }
        public int? FieldType { get; set; }
        public byte? WordEdit { get; set; }
        public int? ShowType { get; set; }
        public int? ShowTypeNumber { get; set; }
        public string MingXiTitle { get; set; }
        public int? SubcompId { get; set; }


        /// <summary>
        /// 模板类别
        /// </summary>
        public string TepTypeDic { get; set; }
        /// <summary>
        /// 文本类别
        /// </summary>
        public string TextTypeDic { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string DeptNames { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string TstateDic { get; set; }
        /// <summary>
        /// 组织机构数组
        /// </summary>
        public IList<int> DeptIdsArray { get; set; }
        /// <summary>
        /// 当前历史模板ID
        /// </summary>
        public int HistId { get; set; }
        /// <summary>
        /// 是否显示标的
        /// </summary>
        public int ShowSub { get; set; }
        /// <summary>
        /// 是否显示标的复选框
        /// </summary>
        public bool ShowSubMatter { get; set; }
        /// <summary>
        /// 显示格式，0:统一格式
        /// </summary>
        public int FieldTypeVal { get; set; }
        /// <summary>
        /// 合同文本类别
        /// </summary>
        public string TepTypes { get; set; }

        /// <summary>
        /// 合同文本类别数组
        /// </summary>
        public IList<int> TepTypesArray { get; set; }



    }
}
