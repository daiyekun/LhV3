using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// Session 存储的User对象
    /// </summary>
   public class SessionUserInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplyName { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNo { get; set; }
        /// <summary>
        /// 部门
        /// </summary>

        public int? DepartmentId { get; set; }
    }
}
