using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.Contract.Controllers
{
    /// <summary>
    /// 合同查阅人
    /// </summary>
    [Area("Contract")]
    [Route("Contract/[controller]/[action]")]
    public class ContConsultController : NfBaseController
    {
        private IContConsultService _IContConsultService;
        private IMapper _IMapper;
        public ContConsultController(IContConsultService IContConsultService, IMapper IMapper)
        {
            _IContConsultService = IContConsultService;
            _IMapper = IMapper;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new PageInfo<ContConsult>();
            var predicateAnd = PredicateBuilder.True<ContConsult>();
            var predicateOr = PredicateBuilder.False<ContConsult>();
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ContId == contId );
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IContConsultService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            return View();

        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        public IActionResult Save(int contId,string userIds)
        {
            IList<ContConsult> consults = new List<ContConsult>();
           var listUserIds= StringHelper.String2ArrayInt(userIds);
            foreach (var uid in listUserIds)
            {
                ContConsult info = new ContConsult();
                info.ContId = contId;
                info.UserId = uid;
                consults.Add(info);
            }
            _IContConsultService.Add(consults);

            return GetResult();

        }
        

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IContConsultService.Delete(Ids);
            return GetResult();
        }
    }
}