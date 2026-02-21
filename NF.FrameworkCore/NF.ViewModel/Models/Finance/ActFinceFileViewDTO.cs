using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    /// <summary>
    ///资金附件
    /// </summary>
    public class ActFinceFileDTO: ActFinceFile
    {

    }
    /// <summary>
    /// 实际资金附件
    /// </summary>
    public class ActFinceFileViewDTO: ActFinceFileDTO
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
