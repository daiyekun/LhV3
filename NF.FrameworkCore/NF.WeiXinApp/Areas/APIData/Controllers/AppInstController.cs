using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.WeiXinApp.Extend;
using NF.WeiXinApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Areas.APIData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppInstController : ControllerBase
    {
        private IContractInfoService _IContractInfoService;
        private IAppInstService _IAppInstService;
        public AppInstController(IContractInfoService IContractInfoService
            , IAppInstService IAppInstService)
        {
            _IAppInstService = IAppInstService;
            _IContractInfoService = IContractInfoService;
        }


        /// <summary>
        /// 审批历史
        /// </summary>
        /// <returns></returns>
        public string GetAppHistList(int appObjId, int objType, string UsName)
        {
            var usinfo = _IContractInfoService.Yhinfo(UsName);
            var pageInfo = new NoPageInfo<AppInst>();
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            predicateAnd = predicateAnd.And(a => a.ObjType == objType && a.AppObjId == appObjId);

            var layPage = _IAppInstService.GetWXAppHistList(pageInfo, usinfo.Id, predicateAnd, a => a.Id, true);
            // return new CustomResultJson(layPage);
            // return null;
            return layPage.ToWxJson();
        }
    }
}
