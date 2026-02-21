using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class CompContact
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

        public virtual UserInfor CreateUser { get; set; }
        public virtual UserInfor ModifyUser { get; set; }
    }
}
