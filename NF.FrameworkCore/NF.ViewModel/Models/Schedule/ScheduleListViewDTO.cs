using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models.Schedule
{
   public class ScheduleListViewDTO: INfEntityHandle
    {

        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public int ScheduleAttribution { get; set; }
        public int? ScheduleDuixiang { get; set; }
        public string Description { get; set; }
        public string Descriptionms { get; set; }
        public int Tixing { get; set; }
        public int Designee { get; set; }
        public int Stalker { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public int? ScheduleId { get; set; }
        public byte? IsDelete { get; set; }
        public DateTime? Myjdtime { get; set; }
        public int? Mystate { get; set; }


        public string ScheduleAttributionDic { get; set; }
        public string ScheduleDuixiangName { get; set; }
        public string TixingName { get; set; }
        public string DesigneeName { get; set; }
        public string StalkerName { get; set; }
        public string CreateUserName { get; set; }
        /// <summary>
        /// 进度名称
        /// </summary>
       public string Jdname { get; set; }
        /// <summary>
        /// 进度时间
        /// </summary>
        public DateTime? JdtataTime { get; set; }
      
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 状态名字
        /// </summary>
        public string MystateName { get; set; }

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
