using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{ 
    /// <summary>
    /// 实例节点信息
    /// </summary>
   public  class AppInstNodeInfoDTO
    {

        public int Id { get; set; }
        public int? InstId { get; set; }
        public string NodeStrId { get; set; }
        public int? Nrule { get; set; }
        public int? ReviseText { get; set; }
        public int? GroupId { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public int? IsMin { get; set; }
        public int? IsMax { get; set; }

    }

    /// <summary>
    /// 实例节点信息
    /// </summary>
    public class AppInstNodeInfoViewDTO : AppInstNodeInfoDTO
    {


        /// <summary>
        /// 组用户名称
        /// </summary>
        public string UserNames { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 节点状态
        /// </summary>
        public string StateDic { get; set; }

    }
}
