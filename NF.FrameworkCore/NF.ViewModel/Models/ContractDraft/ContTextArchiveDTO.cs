using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 归档主表DTO
    /// </summary>
   public class ContTextArchiveDTO
    {
        public int Id { get; set; }
        public int? ContTextId { get; set; }
        public string ArcCode { get; set; }
        public string ArcCabCode { get; set; }
        public int? ArcSumNumber { get; set; }
        public int? BorrSumNumber { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
    /// <summary>
    /// 归档信息
    /// </summary>
    public class ContTextArchiveViewDTO: ContTextArchiveDTO
    {
        /// <summary>
        /// 剩余份数
        /// </summary>
        public int ResidueNumber { get; set; }

    }
    /// <summary>
    /// 归档明细DTO
    /// </summary>
    public class ContTextArchiveItemDTO
    {
        public int Id { get; set; }
        public int? ArchiveId { get; set; }
        public int? ContTextId { get; set; }
        public int? ArcNumber { get; set; }
        public string ArcRemark { get; set; }
        public string ArcCode { get; set; }
        public string ArcCabCode { get; set; }
        public string SubUser { get; set; }
        public DateTime? SubDateTime { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string GuidFileName { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

    }

    /// <summary>
    /// 归档明细DTO
    /// </summary>
    public class ContTextArchiveItemViewDTO: ContTextArchiveItemDTO
    {
        /// <summary>
        /// 创建人（经办人）
        /// </summary>
        public string CreateUserName { get; set; }

    }
  }
