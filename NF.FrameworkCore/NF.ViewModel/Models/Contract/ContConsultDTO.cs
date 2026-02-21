using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同查阅人
    /// </summary>
    public class ContConsultDTO
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public int? UserId { get; set; }
    }
    /// <summary>
    /// 合同查阅人显示
    /// </summary>
    public class ContConsultViewDTO: ContConsultDTO
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 所属机构
        /// </summary>
        public string DeptName { get; set; }
    }
    }
