using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Utility;

namespace NF.Web.Areas.System.Controllers
{
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class RemindController : Controller
    {
        private IRemindService _IRemindService;
       
        public RemindController(IRemindService IRemindService)
        {
            _IRemindService = IRemindService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 提醒列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Remind>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Remind>();
            predicateAnd = predicateAnd.And(a => a.IsDelete==0);
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateAnd = predicateAnd.And(a=>a.Name.Contains(pageParam.keyWord)
                ||a.CustomName.Contains(pageParam.keyWord));
            }
            var layPage = _IRemindService.GetList(pageInfo, predicateAnd, a=>a.Id, true);
             return new CustomResultJson(layPage);
            
        }

        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            _IRemindService.Delete(Ids);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,


            };
            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(IEnumerable<Remind> data)
        {
            var ispd = 0;
            try
            {
                _IRemindService.Update(data);
                ispd = 1;
            }
            catch (Exception)
            {

                ispd = 2;
            }


          
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "保存成功！",
                    Code = 0,
                    RetValue= ispd

                });

            //else
            //{
            //    return new CustomResultJson(new RequstResult()
            //    {
            //        Msg = "保存失败提前天数和延后天数请分开保存！",
            //        Code = 0,
                   

            //    });
            //}
           
        }
    }
}