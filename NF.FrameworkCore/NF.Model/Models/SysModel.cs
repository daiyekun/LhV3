using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class SysModel
    {
        public SysModel()
        {
            SysFunctions = new HashSet<SysFunction>();
        }

        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }
        public string No { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int? Sort { get; set; }
        public string RequestUrl { get; set; }
        public string Remark { get; set; }
        public byte? IsShow { get; set; }
        public string AreaName { get; set; }
        public byte? IsDelete { get; set; }
        public string Mpath { get; set; }
        public int? Leaf { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDatetime { get; set; }
        public string Ico { get; set; }

        public virtual UserInfor CreateUser { get; set; }
        public virtual UserInfor ModifyUser { get; set; }
        public virtual ICollection<SysFunction> SysFunctions { get; set; }
    }
}
