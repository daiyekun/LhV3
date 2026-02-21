using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同文本
    /// </summary>
   public class ContTextDTO
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
    public class ContTextViewDTO : ContTextDTO
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
    /// <summary>
    /// 合同文本大列表
    /// </summary>
    public class ContTextListViewDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件类型名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 文本来源
        /// </summary>
        public string IsFromTxt { get; set; }
        /// <summary>
        /// 文件全名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件说明
        /// </summary>
        public string Remark { get; set; }
       
        /// <summary>
        /// 扩展名称
        /// </summary>
        public string ExtenName { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int? ContId { get; set; }
        /// <summary>
        ///  合同名称
        /// </summary>
        public string ContName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContCode { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string ContStateDic { get; set; }
        /// <summary>
        /// 合同状态标识
        /// </summary>
        public int? ContState { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string PrincipalUserName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 合同对方ID
        /// </summary>
        public int? CompId { get; set; }
        /// <summary>
        /// 用章状态
        /// </summary>
        public int SealState { get; set; }
        /// <summary>
        /// 用章状态描述
        /// </summary>
        public string SealStateDic { get; set; }
        /// <summary>
        /// 用章名称
        /// </summary>
        public string SealName { get; set; }
        /// <summary>
        /// 用章日期
        /// </summary>
        public DateTime? SealDate { get; set; }
        /// <summary>
        /// 归档状态
        /// </summary>
        public int ArchiveState { get; set; }
        /// <summary>
        /// 归档状态描述
        /// </summary>
        public string ArchiveStateDic { get; set; }
        /// <summary>
        /// 归档号
        /// </summary>
        public string ArchiveCode { get; set; }
        /// <summary>
        /// 归档号
        /// </summary>
        public string ArchiveCabCode { get; set; }
        /// <summary>
        /// 归档电子版文件
        /// </summary>
        public string ArchiveEleFile { get; set; }
        /// <summary>
        /// 归档份数
        /// </summary>
        public int ArchiveNumber { get; set; }
        /// <summary>
        /// 借阅份数
        /// </summary>
        public int BorrowNumber { get; set; }
        /// <summary>
        /// 剩余份数
        /// </summary>
        public int Surplus { get; set; }
        /// <summary>
        /// 合同文本来源
        /// </summary>
        public byte IsFromTemp { get; set; }
        /// <summary>
        /// 是否生成PDF
        /// </summary>
        public bool IsPdf { get; set; }

    }



}
