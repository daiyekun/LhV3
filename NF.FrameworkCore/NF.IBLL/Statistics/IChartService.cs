using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 图表接口
    /// </summary>
    public partial interface IChartService:IBaseService<ContractInfo>
    {
        /// <summary>
        /// 按年统计合同情况
        /// </summary>
        /// <param name="Year">年份</param>
        /// <returns></returns>
        HtZhiXingTongji GetHtZxTj(int Year = 0);
        /// <summary>
        /// 合同类别饼图
        /// </summary>
        /// <returns></returns>
        HtLbTjInfo GetHtLbPie();
    }
}
