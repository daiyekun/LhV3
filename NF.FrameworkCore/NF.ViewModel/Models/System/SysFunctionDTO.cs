using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 功能点
    /// </summary>
    public class SysFunctionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fcode { get; set; }
        public string Remark { get; set; }
        public byte? IsDelete { get; set; }
        public int? ModeId { get; set; }

       

    }
}
