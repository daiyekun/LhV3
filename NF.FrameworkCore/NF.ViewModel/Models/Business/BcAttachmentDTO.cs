using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 单品附件
    /// </summary>
    public class BcAttachmentDTO
    {
        public int Id { get; set; }
        public string FolderName { get; set; }
        public string Path { get; set; }
        public string GuidFileName { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string Remark { get; set; }
        public int? DownloadTimes { get; set; }
        public int? BcId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }

    /// <summary>
    /// 附件-显示类
    /// </summary>
    public class BcAttachmentViewDTO : BcAttachmentDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 附件类别
        /// </summary>
        public string CategoryName { get; set; }
    }
}
