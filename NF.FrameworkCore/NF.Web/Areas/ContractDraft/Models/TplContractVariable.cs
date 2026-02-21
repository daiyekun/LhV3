using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.ContractDraft.Models
{
    /// <summary>
    /// 模板变量
    /// </summary>
    public class TplContractVariable
    {
        /// <summary>
        /// ID
        /// </summary>
        public String VarName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public String VarLabel { get; set; }
    }
}
