using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 盖章
    /// </summary>
   public partial interface IContTextSealService
    {
        /// <summary>
        /// 盖章信息显示
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="Id">当前合同文本ID</param>
        /// <returns></returns>
        ContTextSealViewDTO ShowView(int userId, int Id);
    }
}
