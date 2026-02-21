using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models.Schedule
{
    public class ScheduleDetailViewDTO : INfEntityHandle
    {
        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public int ScheduleSer { get; set; }
        public string Description { get; set; }
        public string Pdescription { get; set; }
        public DateTime? PddateTime { get; set; }
        public int? Wancheng { get; set; }
        public byte? State { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public string ScheduleSerName { get; set; }
        public string StateDic { get; set; }
        public string CreateName { get; set; }
        public string ModifyName { get; set; }

        public FieldInfo GetPropValue(string propName)
        {
            var obj = this.GetType().GetProperty(propName);
            return new FieldInfo
            {
                FileType = obj.PropertyType,
                FileValue = obj.GetValue(this, null)
            };

        }
    }
}
