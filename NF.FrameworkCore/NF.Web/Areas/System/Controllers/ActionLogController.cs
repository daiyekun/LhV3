using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ActionLogController : NfBaseController//Controller
    {
        private IOptionLogService _IOptionLogService;
        public ActionLogController(IOptionLogService IOptionLogService)
        {
            _IOptionLogService = IOptionLogService;
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
            var pageInfo = new PageInfo<OptionLog>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<OptionLog>();
            var predicateOr = PredicateBuilder.False<OptionLog>();
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
                predicateOr = predicateOr.Or(p => p.User==null?false:p.User.Name.Contains(loginName));
                predicateOr = predicateOr.Or(p => p.ActionTitle.Contains(loginName));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            if (sTime != null)
            {
                predicateAnd = predicateAnd.And(p => p.CreateDatetime >= sTime);
            }
            if (eTime != null)
            {
                predicateAnd = predicateAnd.And(p => p.CreateDatetime <= eTime);
            }
            //状态
            predicateAnd = predicateAnd.And(a => a.Status == 0);
            var layPage = _IOptionLogService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage,true);

        }
        /// <summary>
        /// 删除-软删除（修改状态）
        /// </summary>
        /// <param name="Ids">修改集合</param>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IOptionLogService.UpdateState(Ids);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0,


            });
        }
    }
}