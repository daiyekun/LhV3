using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
  public  class RoleDTO
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public int Rstate { get; set; }
        public int? DepartmentId { get; set; }
        public string Remark { get; set; }
        public byte? IsDelete { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
            /// <summary>
            /// 状态描述
            /// </summary>
        public string RstateDic { get; set; }
    }
    /// <summary>
    /// 角色简单信息
    /// </summary>
    public class RoleSimpDTO
    {
        /// <summary>
        /// ID 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string value { get; set; }
    }
}
