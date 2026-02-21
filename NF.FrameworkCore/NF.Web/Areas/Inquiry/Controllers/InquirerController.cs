using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Inquiry
{
    /// <summary>
    /// 询价人
    /// </summary>
    [Area("Inquiry")]
    [Route("Inquiry/[controller]/[action]")]
    public class InquirerController : NfBaseController
    {
        public IMapper _IMapper;
        public IInquirerService _IInquirerService;

        public InquirerController(IMapper IMapper, IInquirerService IInquirerService)
        {
            _IMapper = IMapper;
            _IInquirerService = IInquirerService;
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立开标人", OptionLogTypeEnum.Add, "建立开标人", true)]
        public IActionResult SaveKbr(int contId, InquirerDTO Inquirer)
        {
            var saveInfo = _IMapper.Map<Inquirer>(Inquirer);
            saveInfo.Name = "名称";
            saveInfo.Position = "职位";
            saveInfo.InqId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Department = 0;
            saveInfo.InquiryId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
            _IInquirerService.Add(saveInfo);
            return GetResult();

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<Inquirer>();
            var predicateAnd = PredicateBuilder.True<Inquirer>();
            var predicateOr = PredicateBuilder.False<Inquirer>();
            predicateOr = predicateOr.Or(a => a.InquiryId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.InquiryId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IInquirerService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _IInquirerService.Delete(Ids);
            return GetResult();
        }
        /// <summary>
        /// 保存招标人
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(int contId, IList<InquirerDTO> subs)
        {

            _IInquirerService.AddSave(subs, contId);
            return GetResult();
        }
    }
}