using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Questioning.Controllers
{
    [Area("Questioning")]
    [Route("Questioning/[controller]/[action]")]
 
    public class WinningController : NfBaseController
    {
        private IWinningQueService _IWinningQueService;
        private IWinningInqService _IWinningInqService;
        public WinningController(IWinningQueService IWinningQueService)
        {
            _IWinningQueService = IWinningQueService;
        }

        public IActionResult Build()
        {
            return View();
        }
        public IActionResult GetActListByContId(int contId)
        {
            var pageInfo = new NoPageInfo<WinningQue>();
            var predicateAnd = PredicateBuilder.True<WinningQue>();
            predicateAnd = predicateAnd.And(a => a.QueId== contId && a.IsDelete == 0);
            var layPage = _IWinningQueService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }

    
        public IActionResult ShowView1()
        {
            var Id = this.SessionCurrUserId;
            //   var info = _IWinningInqService.ShowView(-Id);
            var pageInfo = new NoPageInfo<WinningQue>();
            var predicateAnd = PredicateBuilder.True<WinningQue>();
            predicateAnd = predicateAnd.And(a => a.QueId == -Id && a.IsDelete == 0);
            var layPage = _IWinningQueService.GetListView(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        [NfCustomActionFilter("删除合同附件", OptionLogTypeEnum.Del, "删除合同附件", false)]
        public IActionResult Delete(string Ids)
        {
            _IWinningQueService.Delete(Ids);
            return GetResult();
        }
        public IActionResult SaveData(int contId, IList<WinningQueDTO> subs)
        {

            _IWinningQueService.AddSave(subs, contId);
            return GetResult();
        }
    }
}