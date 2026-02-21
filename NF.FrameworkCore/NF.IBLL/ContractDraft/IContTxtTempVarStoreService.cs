using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
   
    /// <summary>
    /// 合同文本变量库
    /// </summary>
   public partial interface IContTxtTempVarStoreService
    {
        /// <summary>
        /// 获取所有的合同文本合同系统变量
        /// </summary>
        /// <param name="cttextid">合同文本id</param>
        /// <param name="locale">语言版本</param>
        /// <returns>合同系统变量列表</returns>
        IList<ContractVariable> GetAllContractVariables(int cttextid, String locale);
        /// <summary>
        /// 获取文本系统变量
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="locale">语言版本</param>
        /// <returns></returns>
        IList<ContractVariable> GetContractVariables(Int32 cttextid, String locale);
        /// <summary>
        /// 合同文本自定义变量
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <returns>合同文本自定义变量列表</returns>
        IList<ContractVariable> GetCustomVariables(Int32 cttextid);
    }
}
