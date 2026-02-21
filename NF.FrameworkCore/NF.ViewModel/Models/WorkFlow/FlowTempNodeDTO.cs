using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 节点
    /// </summary>
    public  class FlowTempNodeDTO
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public int? TempId { get; set; }
        public string Name { get; set; }
        public int? Left { get; set; }
        public int? Top { get; set; }
        public int? Type { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public byte? Alt { get; set; }
        public byte? Marked { get; set; }
       
    }
    /// <summary>
    /// 显示节点实体
    /// </summary>
    public class FlowTempNodeViewDTO
    {
       
        public string strid { get; set; }
        public string name { get; set; }
        public int? left { get; set; }
        public int? top { get; set; }
        public string type { get; set; }
        public int? height { get; set; }
        public int? width { get; set; }
        public bool alt { get; set; }
        public bool marked { get; set; }
    }


}
