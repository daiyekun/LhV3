using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.IBLL;
using NF.Web.Utility;

namespace NF.Web.Areas.NfCommon.Controllers
{
    /// <summary>
    /// 城市，省份查询
    /// </summary>
    [Area("NfCommon")]
    [Route("NfCommon/[controller]/[action]")]
    public class ProvinceCityController : Controller
    {
        private IProvinceService _IProvinceService;
        public ProvinceCityController(IProvinceService IProvinceService)
        {
            _IProvinceService = IProvinceService;
        }
        public IActionResult GetProvinces()
        {
           
            var requstResult = new RequstResult()
            {
                Msg = "登录超时,请重新登录！",
                Code = 0,
                Data = _IProvinceService.GetAll()
            };

            return new CustomResultJson(requstResult);
        }
    }
}