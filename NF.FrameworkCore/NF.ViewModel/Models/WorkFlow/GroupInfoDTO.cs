using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 组
    /// </summary>
    public  class GroupInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public byte Gstate { get; set; }
        public byte IsDelete { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
    }
    /// <summary>
    /// 组显示类
    /// </summary>
    public class GroupInfoViewDTO: GroupInfoDTO
    {
       

    }
    /// <summary>
    /// 选择组实体类
    /// </summary>
    public class SelectGroupList
    { 
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte Gstate { get; set; }
        /// <summary>
        /// 组用户
        /// </summary>
        public string UserNames { get; set; }

    }

    /// <summary>
    /// 选择组实体类
    /// </summary>
    public class SelectGroupusinfoList
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte Gstate { get; set; }
        /// <summary>
        /// 组名字
        /// </summary>
        public string GroupNames { get; set; }
        /// <summary>
        /// 组Id
        /// </summary>
        public int  GroupId { get; set; }

    }

}
