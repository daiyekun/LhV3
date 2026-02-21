using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Web.Controllers;

namespace NF.Web.Areas.Others.Controllers
{
    [Area("Others")]
    [Route("Others/[controller]/[action]")]
    public class CountryController : NfBaseController
    {
        private ICountryService _ICountryService;
       public CountryController(ICountryService ICountryService)
        {
            _ICountryService = ICountryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 返回3级联动数据
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAddress()
        {
           
            if (RedisHelper.KeyExists("AddressString"))
            {
                return Content(RedisHelper.StringGet("AddressString"), "application/json");
            }
            else
            {
                var list = _ICountryService.GetAddress();
                var strdata= JsonUtility.SerializeObject(list).ToLower();
                RedisHelper.StringSetAsync("AddressString", strdata);
                return Content(strdata, "application/json");
            }
        }
    }
}