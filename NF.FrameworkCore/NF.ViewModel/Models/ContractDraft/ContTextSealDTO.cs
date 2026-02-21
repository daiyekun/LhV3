using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同文本盖章
    /// </summary>
   public  class ContTextSealDTO
    {
        public int Id { get; set; }
        public int ContTextId { get; set; }
        public int? SealId { get; set; }
        public string SealUser { get; set; }
        public int SealState { get; set; }
        public int? SealNumber { get; set; }
        public int? EachNumber { get; set; }
        public int? SealTotal { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

    }

    /// <summary>
    /// 合同文本盖章显示
    /// </summary>
    public class ContTextSealViewDTO: ContTextSealDTO
    {
        /// <summary>
        /// 签约主体
        /// </summary>
        public string MainDeptName { get; set; }
        /// <summary>
        /// 经办机构
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 用章人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string ContStateDic { get; set; }
        /// <summary>
        /// 印章名称
        /// </summary>
        public string SealName { get; set; }
        /// <summary>
        /// 用章状态
        /// </summary>
        public string SealStateDic { get; set; }

    }
}
