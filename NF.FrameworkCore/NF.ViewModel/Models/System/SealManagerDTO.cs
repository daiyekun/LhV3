using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 印章管理
    /// </summary>
    public  class SealManagerDTO
    {
        public int Id { get; set; }
        public string SealName { get; set; }
        public string SealCode { get; set; }
        public int? MainDeptId { get; set; }
        public int? UserId { get; set; }
        public int? DeptId { get; set; }
        public DateTime EnabledDate { get; set; }
        public byte SealState { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
    /// <summary>
    ///  显示
    /// </summary>
    public class SealManagerViewDTO: SealManagerDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 保管人
        /// </summary>
        public string KeeperUserName { get; set; }
        /// <summary>
        /// 保管部门
        /// </summary>
        public string DeptName { get; set; }

    }
    /// <summary>
    /// 选择印章下拉框
    /// </summary>
    public class SelectSealViewDTO
    {
        /// <summary>
        /// 印章ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 印章名称
        /// </summary>
        public string Name { get; set; }

    }
}
