using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Tender.Controllers
{/// <summary>
/// 招标导入数据
/// </summary>
    [Area("Tender")]
    [Route("Tender/[controller]/[action]")]
    public class WinningItemController : NfBaseController
    {
        private IWinningItemService _IWinningItemService;
        public WinningItemController(IWinningItemService IWinningItemService)
        {
            _IWinningItemService = IWinningItemService;
        }

        public IActionResult Build()
        {
            return View();
        }
        public IActionResult GetActListByContId(int contId)
        {
            var pageInfo = new NoPageInfo<WinningItem>();
            var predicateAnd = PredicateBuilder.True<WinningItem>();
            predicateAnd = predicateAnd.And(a => a.TenderId == contId && a.IsDelete == 0);
            var layPage = _IWinningItemService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }


        public IActionResult ShowView1()
        {
            var Id = this.SessionCurrUserId;
            //   var info = _IWinningInqService.ShowView(-Id);
            var pageInfo = new NoPageInfo<WinningItem>();
            var predicateAnd = PredicateBuilder.True<WinningItem>();
            predicateAnd = predicateAnd.And(a => a.TenderId == -Id && a.IsDelete == 0);
            var layPage = _IWinningItemService.GetListView(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        [NfCustomActionFilter("删除合同附件", OptionLogTypeEnum.Del, "删除合同附件", false)]
        public IActionResult Delete(string Ids)
        {
            _IWinningItemService.Delete(Ids);
            return GetResult();
        }
    }
}