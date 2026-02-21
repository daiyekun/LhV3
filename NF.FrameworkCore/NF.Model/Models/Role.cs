using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDatetime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public int Rstate { get; set; }
        public int? DepartmentId { get; set; }
        public string Remark { get; set; }
        public byte? IsDelete { get; set; }

        public virtual UserInfor CreateUser { get; set; }
        public virtual Department Department { get; set; }
        public virtual UserInfor ModifyUser { get; set; }
    }
}
