using NF.Model.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 客户联系人
    /// </summary>
    public class APPCompContact
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public string DeptName { get; set; }
        public string Position { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Im { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
    public class APPCompAttachmentDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string Remark { get; set; }
        public int? DownloadTimes { get; set; }
        public int? CompanyId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public string FolderName { get; set; }
        public string GuidFileName { get; set; }
        public RedisValue CreateUserDisplyName { get; set; }
        public string CategoryName { get; set; }
    }
    public partial class APPCompAttachment
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
        public int? CompanyId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

        public DataDictionary Category { get; set; }
        public UserInfor CreateUser { get; set; }
    }

      public class APPCompAttachmentViewDTO: APPCompAttachmentDTO
    {
       
        
    }
}
