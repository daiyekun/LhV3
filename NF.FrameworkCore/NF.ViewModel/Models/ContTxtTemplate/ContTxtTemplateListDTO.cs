using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public  class ContTxtTemplateListDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模板类别
        /// </summary>
        public int? TepType { get; set; }
        /// <summary>
        /// 模板类别
        /// </summary>
        public string TepTypeDic { get; set; }
        /// <summary>
        /// 文本类别
        /// </summary>
        public int? TextType { get; set; }
        /// <summary>
        /// 文本类别
        /// </summary>
        public string TextTypeDic { get; set; }
        /// <summary>
        /// 部门IDs
        /// </summary>
        public string DeptIds { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptNames { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreateUserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int? ModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDateTime { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Vesion { get; set; }
        /// <summary>
        /// 状态0：未启用。1启用（默认）
        /// </summary>
        public int? Tstate { get; set; }
        /// <summary>
        /// 是否允许编辑;0:否、1：是
        /// </summary>
        public byte? WordEdit { get; set; }
        /// <summary>
        /// 当前使用的模板历史ID
        /// </summary>
        public int UseHistId { get; set; }


    }
}
