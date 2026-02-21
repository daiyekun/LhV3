using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   /// <summary>
   /// 项目事项
   /// </summary>
   public class ProjDescriptionDTO
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Pitem { get; set; }
        public string ProjContent { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
    /// <summary>
    /// 项目时间列表显示类
    /// </summary>
    public class ProjDescriptionViewDTO: ProjDescriptionDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }

    }
}
