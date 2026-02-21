using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Department
    {
        public Department()
        {
            ContractInfoDepts = new HashSet<ContractInfo>();
            ContractInfoHistoryDepts = new HashSet<ContractInfoHistory>();
            ContractInfoHistoryMainDepts = new HashSet<ContractInfoHistory>();
            ContractInfoMainDepts = new HashSet<ContractInfo>();
            DeptMains = new HashSet<DeptMain>();
            Inquiries = new HashSet<Inquiry>();
            Questionings = new HashSet<Questioning>();
            Roles = new HashSet<Role>();
            UserInfors = new HashSet<UserInfor>();
        }

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

        public virtual DataDictionary Category { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoDepts { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryDepts { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryMainDepts { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoMainDepts { get; set; }
        public virtual ICollection<DeptMain> DeptMains { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<Questioning> Questionings { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserInfor> UserInfors { get; set; }
    }
}
