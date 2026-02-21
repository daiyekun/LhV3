using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LhCode;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.WorkFlow.Controllers
{
    [Area("WorkFlow")]
    [Route("WorkFlow/[controller]/[action]")]
    public class AppInstOptionController : NfBaseController
    {
        /// <summary>
        /// 映射
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 实例服务
        /// </summary>
        private IAppInstService _IAppInstService;
        /// <summary>
        /// 实例节点
        /// </summary>
        private IAppInstNodeService _IAppInstNodeService;
        /// <summary>
        /// 意见
        /// </summary>
        private IAppInstOpinService _IAppInstOpinService;

        public AppInstOptionController(
            IMapper IMapper,
            IAppInstService IAppInstService
            ,IAppInstNodeService IAppInstNodeService
            , IAppInstOpinService IAppInstOpinService)
        {
            _IMapper = IMapper;
            _IAppInstService = IAppInstService;
            _IAppInstNodeService = IAppInstNodeService;
            _IAppInstOpinService = IAppInstOpinService;

        }
        /// <summary>
        /// 同意时提交意见
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SubmitAgreeOption(SubmitOptionInfo optionInfo)
        {
             optionInfo.SubmitUserId = this.SessionCurrUserId;
            _IAppInstOpinService.SubmintOption(optionInfo);
            if (LhLicense.WxKaiQi == 1)
            {
                //添加微信消息
                await Task.Factory.StartNew(() =>
                {
                    var instInfo = _IAppInstService.Find(optionInfo.InstId);
                    _IAppInstService.WeiXinFlowNodeMsg(instInfo);
                });
            }
            return GetResult();
        }

        /// <summary>
        /// 不同意时提交意见
        /// </summary>
        /// <returns></returns>
        public IActionResult SubmitDisagreeOption(SubmitOptionInfo optionInfo)
        {
            optionInfo.SubmitUserId = this.SessionCurrUserId;
            _IAppInstOpinService.SubmintDisagreeOption(optionInfo);
            return GetResult();
        }
        /// <summary>
        /// 审批意见列表
        /// </summary>
        /// <param name="instId">审批实例ID</param>
        /// <param name="nodestrId">节点ID</param>
        /// <returns></returns>
        public IActionResult GetWfOptinions(PageparamInfo pageParam,int instId,string nodestrId)
        {
            var pageInfo = new PageInfo<AppInstOpin>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<AppInstOpin>();
            predicateAnd = predicateAnd.And(a=>a.InstId== instId);
            if (!string.IsNullOrEmpty(nodestrId))
            {
                predicateAnd = predicateAnd.And(a => a.NodeStrId == nodestrId);
            }
            var layPage = _IAppInstOpinService.GetOptinionList(pageInfo, predicateAnd, a => a.Id, true);
            return new CustomResultJson(layPage,true);
        }
    }
}