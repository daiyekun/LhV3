using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 节点连线
    /// </summary>
    public class TempNodeLineDTO
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public byte? Dash { get; set; }
        public byte? Marked { get; set; }
        public byte? Alt { get; set; }
    }
    /// <summary>
    /// 节点连线显示类
    /// </summary>
    public class TempNodeLineViewDTO
    {
      
        public string strid { get; set; }
       
        public string name { get; set; }
       
        public string type { get; set; }
        public double M { get; set; }
        public string from { get; set; }
       
        public string to { get; set; }
       
        public bool dash { get; set; }
       
        public bool marked { get; set; }
       
        public bool alt { get; set; }
       
       
    }
}
