using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserInforDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DisplyName { get; set; }
        public int? Sex { get; set; }
        public int? Age { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? EntryDatetime { get; set; }
        public string IdNo { get; set; }
        public string Address { get; set; }
        public int? DepartmentId { get; set; }
        public int? Sort { get; set; }
        public int? State { get; set; }
        public string Remark { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDatetime { get; set; }
        public int? IsDelete { get; set; }
        public int? Ustart { get; set; }
        public string Msystem { get; set; }
        public string Minfo { get; set; }
        public string PhName { get; set; }
        public string PhPath { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string SexDic { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string UstartDic { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string Repass { get; set; }
        /// <summary>
        /// 用户状态描述
        /// </summary>

        public string StateDic { get; set; }
        /// <summary>
        /// 电子签章状态
        /// </summary>
        public int? UserEsTy { get; set; }
        /// <summary>
        /// 微信账号
        /// </summary>
        public string WxCode { get; set; }

    }
    /// <summary>
    /// Redis存储信息
    /// </summary>
    public class RedisUser: IEntityDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名-登录名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplyName { get; set; }
        /// <summary>
        /// Emal
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int? DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? State { get; set; }

        public int? UserEs { get; set; }
        public int? UserEsTy { get; set; }
        /// <summary>
        /// 微信账号
        /// </summary>
        public string WxCode { get; set; }

    }
    /// <summary>
    /// 用户零时存储对象
    /// </summary>
    public class UserTemp
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplyName { get; set; }

    }
}
