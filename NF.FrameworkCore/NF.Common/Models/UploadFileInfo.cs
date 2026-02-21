using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NF.Common.Models
{
    /// <summary>
    /// 上传文件相关信息
    /// </summary>
    public class UploadFileInfo
    {
        /// <summary>
        /// 原始文件名称
        /// </summary>
        public string SourceFileName { get; set; }
        /// <summary>
        /// Guid后文件名称
        /// </summary>
        public string GuidFileName { get; set; }
        /// <summary>
        /// 存储文件名称
        /// </summary>
        public string FolderName { get; set; }
        /// <summary>
        /// 没有扩展的文件名
        /// </summary>
        public string NotExtenFileName { get; set; }
        /// <summary>
        /// 文件扩展
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 是否使用Guid文件名称
        /// </summary>
        public bool RemGuidName { get; set; } = true;


    }
    /// <summary>
    /// 下载对象
    /// </summary>
    public class DownLoadInfo
    {
        /// <summary>
        /// 文件流
        /// </summary>
        public FileStream NfFileStream { get; set; }
        public string Memi { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

    }
   
    /// <summary>
    /// 下载请求对象
    /// </summary>
    public class DownLoadAndUploadRequestInfo
    {
        /// <summary>
        /// 下载ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 下载文件夹枚举值：UploadAndDownloadFoldersEnum
        /// </summary>
        public int folderIndex { get; set; }
        /// <summary>
        /// 特殊标识，比如合同历史文本下载
        /// </summary>
        public int Dtype { get; set; } = 0;
        /// <summary>
        /// 下载类型（主要用于合同文本下载）
        /// 0：默认值
        /// 1：下载Word
        /// 2:下载PDF
        /// </summary>
        public int DownType { get; set; } = 0;
    }


         
}
