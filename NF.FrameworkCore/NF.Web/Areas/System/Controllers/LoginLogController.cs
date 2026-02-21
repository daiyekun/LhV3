using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.System.Controllers
{
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class LoginLogController : NfBaseController//Controller
    {
        private ILoginLogService _ILoginLogService;
        private IMapper _mapper { get; set; }
       
        public LoginLogController(ILoginLogService ILoginLogService, IMapper mapper)
        {
            _ILoginLogService = ILoginLogService;
            _mapper = mapper;
           

        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询登录日志
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(int? limit, int? page, string loginName, string loginStratTime, string loginEndTime)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<LoginLog>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<LoginLog>();

            var stratTime = loginStratTime;
            var endTime = loginEndTime;
            DateTime? sTime = null;
            DateTime? eTime = null;
            if (!string.IsNullOrEmpty(stratTime))
            {
                sTime = Convert.ToDateTime(stratTime);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                eTime = Convert.ToDateTime(endTime);
            }
            if (!string.IsNullOrEmpty(loginName))
            {
                predicateAnd = predicateAnd.And(p => p.LoginUser!=null&& p.LoginUser.Name.Contains(loginName));
            }
            if (sTime != null)
            {
                predicateAnd = predicateAnd.And(p => p.CreateDatetime >= sTime);
            }
            if (eTime != null)
            {
                predicateAnd = predicateAnd.And(p => p.CreateDatetime <= eTime);
            }

            var layPage = _ILoginLogService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage,true);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _ILoginLogService.ExecuteDelSqlCommandByIds(Ids);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0,


            });
        }
    }
}