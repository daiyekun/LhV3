using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 项目计划时间
    /// </summary>
   public  class ProjScheduleDTO
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Pitem { get; set; }
        public DateTime? PlanBeginDateTime { get; set; }
        public DateTime? PlanCompleteDateTime { get; set; }
        public DateTime? ActualBeginDateTime { get; set; }
        public DateTime? ActualCompleteDateTime { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }

    }
    /// <summary>
    /// 项目时间列表
    /// </summary>
    public class ProjScheduleViewDTO: ProjScheduleDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }

    }
}
