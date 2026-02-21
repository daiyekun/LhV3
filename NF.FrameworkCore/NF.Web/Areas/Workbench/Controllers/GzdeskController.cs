using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NF.Web.Areas.Workbench.Controllers
{       [Area("Workbench")]
        [Route("Workbench/[controller]/[action]")]
    public class GzdeskController : NfBaseController
    {
        private IScheduleListService _IScheduleListService;

        public IUserInforService _IUserInforService;
        public IDataDictionaryService _IDataDictionaryService;
        public GzdeskController(

            IScheduleListService IScheduleListService,
            IUserInforService IUserInforService,
            IDataDictionaryService IDataDictionaryService
            )
        {
            _IScheduleListService = IScheduleListService;
            _IUserInforService = IUserInforService;
            _IDataDictionaryService = IDataDictionaryService;

        }
        public IActionResult Index()
        {
            return View();
        }

        private Expression<Func<ScheduleList, bool>> GetQueryExpression(PageInfo<ScheduleList> pageInfo, string keyWord, int? search, List<int?> id)
        {
            var predicateAnd = PredicateBuilder.True<ScheduleList>();
            var predicateOr = PredicateBuilder.False<ScheduleList>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.ScheduleName.Contains(keyWord));
                var rwgs = _IScheduleListService.SJZd(keyWord);
                predicateOr = predicateOr.Or(a => a.ScheduleAttribution == rwgs);
                predicateAnd = predicateAnd.And(predicateOr);
            }
            if (this.SessionCurrUserId == -10000)
            {
               
            }
            else
            {

                if ((search ?? 0) > 0)
                {
                    //  where a.Schedule.State == 1 && (a.Tixing == usid || a.Designee == usid || a.Stalker == usid) && ids.Contains(a.Mystate)
                    predicateAnd = predicateAnd.And(a => a.Stalker == this.SessionCurrUserId);
                }

                else
                {
                    predicateAnd = predicateAnd.And(a => a.Tixing == this.SessionCurrUserId || a.Designee == this.SessionCurrUserId || a.Stalker == this.SessionCurrUserId);
                }
            }
            return predicateAnd;
        }
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<ScheduleList>();
            var predicateAnd = PredicateBuilder.True<ScheduleList>();
            var predicateOr = PredicateBuilder.False<ScheduleList>();
            List<int?> id = new List<int?>() { };
            if (contId == 0)
            {
                id.Add(0);
                id.Add(1);
                id.Add(2);
            }
            else
            {
                id.Add(contId);
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetytAdvQueryWdgzt(pageParam, _IUserInforService, _IDataDictionaryService));
            }
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search, id));

            predicateOr = predicateOr.Or(a => a.IsDelete == 0 && a.ScheduleId > 0);

            predicateAnd = predicateAnd.And(predicateOr);

            var usid =0; //this.SessionCurrUserId;
            if (this.SessionCurrUserId == -10000)
            {
                usid = 1;
            }
            var layPage = _IScheduleListService.GetListdesk(pageInfo, predicateAnd, a => a.Id, false , id ,usid);
            return new CustomResultJson(layPage);
        }
        public IActionResult UpdateMoreField(IList<UpdateFieldInfo> fields)
        {
            var res = _IScheduleListService.UpdateField(fields);
            RequstResult reqInfo = reqInfo = new RequstResult()
            {
                Msg = "修改成功",
                Code = 0,


            };
            if (res <= 0)
            {
                reqInfo.Msg = "修改失败";

            }
            return new CustomResultJson(reqInfo);
        }
        public IActionResult Updstate(int contId, int ty) {
            var res = _IScheduleListService.Updstate(contId, ty );
            RequstResult reqInfo = reqInfo = new RequstResult()
            {
                Msg = "修改成功",
                Code = 0,


            };
            if (res <= 0)
            {
                reqInfo.Msg = "修改失败";

            }
            return new CustomResultJson(reqInfo);
        }


        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo, int contId)
        {

            var pageInfo = new NoPageInfo<ScheduleList>();
            var predicateAnd = PredicateBuilder.True<ScheduleList>();
            PageparamInfo pageParam = new PageparamInfo();
            pageParam.keyWord = exportRequestInfo.KeyWord;
            pageParam.jsonStr = exportRequestInfo.jsonStr;
            //  predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord, exportRequestInfo.search));
            if (exportRequestInfo.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
            }
            else
            {//所有行
                if (!string.IsNullOrEmpty(pageParam.jsonStr))
                {//高级查询
                 // predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryContract(pageParam, _IUserInforService));
                }
            }
            List<int?> id = new List<int?>() { };
            if (contId == 0)
            {
                id.Add(0);
                id.Add(1);
                id.Add(2);
            }
            else
            {
                id.Add(contId);
            }
            
            var layPage = _IScheduleListService.GetListdesk(pageInfo, predicateAnd, a => a.Id, true, id,this.SessionCurrUserId);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "跟踪工作台", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }

    }
}
