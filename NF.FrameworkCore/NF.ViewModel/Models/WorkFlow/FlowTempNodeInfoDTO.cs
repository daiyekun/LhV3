using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 节点信息
    /// </summary>
    public class FlowTempNodeInfoDTO
    {
        public int Id { get; set; }
        public int? TempId { get; set; }
        public string NodeStrId { get; set; }
        public int? Nrule { get; set; }
        public int? ReviseText { get; set; }
        public int? GroupId { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public int? IsMin { get; set; }
        public int? IsMax { get; set; }
        public int? Isktz { get; set; }
        public string UsIds { get; set; }
    }
    /// <summary>
    /// 流程节点信息
    /// </summary>
    public class FlowTempNodeInfoViewDTO: FlowTempNodeInfoDTO
    {
       
      
        /// <summary>
        /// 组用户名称
        /// </summary>
        public string UserNames { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }

    }
}
