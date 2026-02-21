using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 部门
    /// </summary>
  public  class DepartmentDTO
    {
        public int Id { get; set; }
        public int Pid { get; set; }
        public string Name { get; set; }
        public string No { get; set; }
        public int? CategoryId { get; set; }
        public int? Dsort { get; set; }
        public string Remark { get; set; }
        public byte? IsMain { get; set; }
        public string ShortName { get; set; }
        public int? IsSubCompany { get; set; }
        public byte? IsDelete { get; set; }
        public int? Dstatus { get; set; }
        public string Dpath { get; set; }
        public int? Leaf { get; set; }
        /// <summary>
        /// 部门类别
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 是否是签约主体
        /// </summary>
        public string IsMainDic { get; set; }
        /// <summary>
        /// 子公司
        /// </summary>
        public string IsSubCompanyDic { get; set; }
    }
    /// <summary>
    /// 查看页面包含签约主体信息
    /// </summary>
    public class DepartmentViewDTO: DepartmentDTO
    {
        public string LawPerson { get; set; }
        public string TaxId { get; set; }
        public string BankName { get; set; }
        public string Account { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Fax { get; set; }
        public string TelePhone { get; set; }
        public string InvoiceName { get; set; }

    }
    /// <summary>
    /// 存储Redis对象
    /// </summary>
    public class RedisDept: IEntityDTO
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }

    }



}
