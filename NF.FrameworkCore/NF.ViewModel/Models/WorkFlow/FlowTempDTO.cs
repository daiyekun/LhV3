using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 基础
    /// </summary>
    public class FlowTempBaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Version { get; set; }
        public byte? IsValid { get; set; }
        public int? ObjType { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public string DeptIds { get; set; }
        public string CategoryIds { get; set; }
        public string FlowItems { get; set; }
    }
    /// <summary>
    /// 流程模板
    /// </summary>
    public class FlowTempDTO: FlowTempBaseDTO
    {
       
        /// <summary>
        /// 字典类别数组
        /// </summary>
        public IList<int> CategoryIdsArray { get; set; }
        /// <summary>
        /// 组织机构数组
        /// </summary>
        public IList<int> DeptIdsArray { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public IList<int> FlowItemsArray { get; set; }


    }
    /// <summary>
    /// 显示类实体模板（列表）
    /// </summary>
    public class FlowTempViewDTO: FlowTempBaseDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 审批对象描述
        /// </summary>
        public string ObjTypeDic { get; set; }
        /// <summary>
        /// 审批对象类别
        /// </summary>
        public string CategorysName { get; set; }
        /// <summary>
        /// 所属经办机构
        /// </summary>
        public string DeptsName { get; set; }
        /// <summary>
        /// 流程审批事项
        /// </summary>
        public string FlowItemsDic { get; set; }
    }
}
