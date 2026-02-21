using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同对方-备忘录
    /// </summary>
   public class CompDescriptionDTO
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string Item { get; set; }
        public string ContentText { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
    /// <summary>
    /// 备忘录视图类
    /// </summary>
    public class CompDescriptionViewDTO: CompDescriptionDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserDisplyName { get; set; }
    }
}
