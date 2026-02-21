using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同文本
    /// </summary>
   public class ContTextHistoryDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public int? TemplateId { get; set; }
        public int? CategoryId { get; set; }
        public string Remark { get; set; }
        public int? DownloadTimes { get; set; }
        public int? ContId { get; set; }
        public int? ContHisId { get; set; }
        public byte? Stage { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public byte? IsFromTemp { get; set; }
        public int? Versions { get; set; }
        public string ElectronicVersionPath { get; set; }
        public int? Archivedcopies { get; set; }
        public int? Borrowedcopies { get; set; }
        public string WordPath { get; set; }
        public int? TextLock { get; set; }
        public DateTime? LockTime { get; set; }
        public string GuidFileName { get; set; }
        public string ExtenName { get; set; }
        public string FolderName { get; set; }
    }
    /// <summary>
    /// 合同文本显示类
    /// </summary>
    public class ContTextHistoryViewDTO : ContTextHistoryDTO
    {
        /// <summary>
        /// 创建名称
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 模板起草-本地上传-自由起草
        /// </summary>
        public string IsFromTxt { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public string Stagetxt { get; set; }
        /// <summary>
        /// 文本类型
        /// </summary>
        public string ContTxtType { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TempName { get; set; }

    }


}
