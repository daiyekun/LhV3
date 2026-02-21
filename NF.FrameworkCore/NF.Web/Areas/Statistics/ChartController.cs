using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.IBLL;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.Statistics
{
    /// <summary>
    /// 统计图
    /// </summary>
    [Area("Statistics")]
    [Route("Statistics/[controller]/[action]")]
    
    public class ChartController : NfBaseController
    {
        private IChartService _IChartService;
        public ChartController(IChartService IChartService)
        {
            _IChartService = IChartService;
        }
        /// <summary>
        /// 合同执行情况统计图
        /// </summary>
        /// <returns></returns>
        public IActionResult GetHtQingKuantongji()
        {
            
            return new CustomResultJson(new RequstResult()
            {
               
                Code = 0,
                Data= _IChartService.GetHtZxTj()


            });
        }
        /// <summary>
        /// 合同类别统计饼图
        /// </summary>
        /// <returns></returns>
        public IActionResult GetHtLbTjPie()
        {

            return new CustomResultJson(new RequstResult()
            {

                Code = 0,
                Data = _IChartService.GetHtLbPie()


            });
        }
    }
}