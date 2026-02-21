using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 模块-菜单信息
    /// </summary>
   public class SysModelDTO
    {
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

        public int id { get; set; }
        public int pid { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public string IsShowDic { get; set; }
        /// <summary>
        /// 上级名称
        /// </summary>
        public string PName { get; set; }


    }

   
}
