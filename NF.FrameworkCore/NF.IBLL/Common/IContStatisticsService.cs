using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 合同统计字段
    /// </summary>
   public partial interface IContStatisticService
    {
        /// <summary>
        /// 添加统计字段
        /// </summary>
        /// <param name="info">统计字段对象</param>
        /// <returns></returns>
        bool AddData(ContStatistic info);
    }
}
