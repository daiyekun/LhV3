using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 发票附件
    /// </summary>
    public class InvoFileDTO : InvoFile
    {

    }
    /// <summary>
    /// 发票附件
    /// </summary>
    public class InvoFileViewDTO: InvoFileDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 附件类别
        /// </summary>
        public string CategoryName { get; set; }
    }
}
