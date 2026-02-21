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
using System.Collections.Generic;

namespace NF.Web.Areas
{
    [Area("Questioning")]
    [Route("Questioning/[controller]/[action]")]
    public class InterviewpeopleController : NfBaseController
    {
        public IMapper _IMapper;
        public IInterviewpeopleService _IInterviewpeopleService;

        public InterviewpeopleController(IMapper IMapper, IInterviewpeopleService IInterviewpeopleService)
        {
            _IMapper = IMapper;
            _IInterviewpeopleService = IInterviewpeopleService;
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立开标人", OptionLogTypeEnum.Add, "建立开标人", true)]
        public IActionResult SaveKbr(int contId, InterviewpeopleDTO Interviewpeople)
        {
            var saveInfo = _IMapper.Map<Interviewpeople>(Interviewpeople);
            saveInfo.Name = "名称";
            saveInfo.Position = "职位";
            saveInfo.Inquirer = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Department = 0;
            saveInfo.QuesId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
            _IInterviewpeopleService.Add(saveInfo);
            return GetResult();

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<Interviewpeople>();
            var predicateAnd = PredicateBuilder.True<Interviewpeople>();
            var predicateOr = PredicateBuilder.False<Interviewpeople>();
            predicateOr = predicateOr.Or(a => a.QuesId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.QuesId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IInterviewpeopleService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _IInterviewpeopleService.Delete(Ids);
            return GetResult();
        }
        /// <summary>
        /// 保存招标人
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(int contId, IList<InterviewpeopleDTO> subs)
        {

            _IInterviewpeopleService.AddSave(subs, contId, _IMapper);
            return GetResult();
        }
    }
}