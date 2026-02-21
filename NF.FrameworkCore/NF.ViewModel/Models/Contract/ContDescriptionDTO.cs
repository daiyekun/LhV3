using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同备忘
    /// </summary>
    public class ContDescriptionDTO
    {
        public int Id { get; set; }
        public int? ContId { get; set; }
        public string Citem { get; set; }
        public string Ccontent { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public int IsDelete { get; set; }
    }
    /// <summary>
    /// 备忘显示
    /// </summary>
    public class ContDescriptionViewDTO: ContDescriptionDTO
    {
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateUserName { get; set; }

    }

}
