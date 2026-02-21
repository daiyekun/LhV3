using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 模板历史
    /// </summary>
    public class ContTxtTemplateHistListDTO
    {
      
        public int Id { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TempId { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Vesion { get; set; }
        /// <summary>
        /// 是否使用此版本
        /// </summary>
        public int UseVersion { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDateTime { get; set; }


    }
}
