using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Inquiry.Controllers
{
    [Area("Inquiry")]
    [Route("Inquiry/[controller]/[action]")]
    public class WinningController : NfBaseController
    {
        private IWinningInqService _IWinningInqService;
     public   WinningController(IWinningInqService IWinningInqService) {
            _IWinningInqService = IWinningInqService;
        }
        //根据id查询中标货物清单
        public IActionResult GetActListByContId(int contId)
        {
            var pageInfo = new NoPageInfo<WinningInq>();
            var predicateAnd = PredicateBuilder.True<WinningInq>();
            predicateAnd = predicateAnd.And(a => a.Inqid == contId && a.IsDelete == 0);
            var layPage = _IWinningInqService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        public IActionResult ShowView1()
        {
            var Id = this.SessionCurrUserId;
            //   var info = _IWinningInqService.ShowView(-Id);
            var pageInfo = new NoPageInfo<WinningInq>();
            var predicateAnd = PredicateBuilder.True<WinningInq>();
            predicateAnd = predicateAnd.And(a => a.Inqid == -Id && a.IsDelete == 0);
            var layPage = _IWinningInqService.GetListView(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        [NfCustomActionFilter("删除合同附件", OptionLogTypeEnum.Del, "删除合同附件", false)]
        public IActionResult Delete(string Ids)
        {
            _IWinningInqService.Delete(Ids);
            return GetResult();
        }
        public IActionResult SaveData(int contId, IList<WinningInqDTO> subs)
        {

            _IWinningInqService.AddSave(subs, contId);
            return GetResult();
        }
    }
}