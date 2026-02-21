using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
   public class ScheduleManagementViewDTO : INfEntityHandle
    {
        public int Id { get; set; }
        public string ScheduleName { get; set; }
        public string ScheduleSer { get; set; }
        public int Priority { get; set; }
        public int ScheduleAttribution { get; set; }
        public int? ScheduleDuixiang { get; set; }
        public string Description { get; set; }
        public int Designee { get; set; }
        public int Stalker { get; set; }
        public DateTime? JhCreateDateTime { get; set; }
        public DateTime? JhCompleteDateTime { get; set; }
        public DateTime? SjCreateDateTime { get; set; }
        public DateTime? SjCompleteDateTime { get; set; }
        public byte? State { get; set; }
        public int? Wancheng { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public byte? IsDelete { get; set; }
        public DateTime? Myjdtime { get; set; }
        public int? Mystate { get; set; }
        public DateTime? Gzdatetime { get; set; }
        public int? Gzstate { get; set; }
        //优先级数据字典
        public string PriorityDic { get; set; }

        //任务归属数据字典
        public string ScheduleAttributionDic { get; set; }
        //对象
        public string ScheduleDuixiangName { get; set; }
        //指派给 
        public string DesigneeName { get; set; }
        //跟踪者
        public string StalkerName { get; set; }
        //创建人
        public string CreateName { get; set; }
        //修改人
        public string ModifyName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StateDic { get; set; }
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
