using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 节点区域
    /// </summary>
    public class TempNodeAreaDTO
    {
        public int Id { get; set; }
        public string StrId { get; set; }
        public int? TempId { get; set; }
        public string Name { get; set; }
        public int? Left { get; set; }
        public int? Top { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Color { get; set; }
        public byte? Alt { get; set; }

    }

    /// <summary>
    /// 节点区域
    /// </summary>
    public class TempNodeAreaViewDTO
    {
         
        public string strid { get; set; }
        public string name { get; set; }
       
        public int? left { get; set; }
       
        public int? top { get; set; }
        
        public int? width { get; set; }
       
        public int? height { get; set; }
        
        public string color { get; set; }
       
        public bool alt { get; set; }

    }

}
