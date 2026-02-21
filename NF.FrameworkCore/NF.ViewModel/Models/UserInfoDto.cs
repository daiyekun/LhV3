using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public class UserInfoDTO
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
        public int? Start { get; set; }
        public string Msystem { get; set; }
        public string Minfo { get; set; }
        public string PhName { get; set; }
        public string PhPath { get; set; }
    }
}
