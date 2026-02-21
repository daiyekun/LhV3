using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LhCode;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
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
    public class AppInstController : NfBaseController
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
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;

        public AppInstController(IMapper IMapper, IAppInstService IAppInstService
            , IAppInstNodeService IAppInstNodeService
            , IRemindService IRemindService)
        {
            _IMapper = IMapper;
            _IAppInstService = IAppInstService;
            _IAppInstNodeService = IAppInstNodeService;
            _IRemindService = IRemindService;

        }
        /// <summary>
        /// 显示流程
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowFlow()
        {
            return View();
        }
        /// <summary>
        /// 待处理
        /// </summary>
        /// <returns></returns>
        public IActionResult AppPendingList()
        {
            return View();

        }
        /// <summary>
        /// 已处理
        /// </summary>
        /// <returns></returns>
        public IActionResult AppProcessedList()
        {
            return View();
        }
        /// <summary>
        /// 已发起
        /// </summary>
        /// <returns></returns>
        public IActionResult AppSponsorList()
        {
            return View();
        }
        /// <summary>
        /// 被打回
        /// </summary>
        /// <returns></returns>
        public IActionResult BeBackList()
        {
            return View();
        }

        /// <summary>
        /// 保存审批实例
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SubmitWorkFlow(AppInstDTO appInstDTO)
        {
            AppInst instInfo = null;
          
                var saveInfo = _IMapper.Map<AppInst>(appInstDTO);
                saveInfo.StartUserId = this.SessionCurrUserId;
                saveInfo.StartDateTime = DateTime.Now;
                saveInfo.CreateUserId = this.SessionCurrUserId;
                saveInfo.CreateDateTime = DateTime.Now;
                saveInfo.AppState = 0;
                 instInfo = _IAppInstService.SubmitWorkFlow(saveInfo, _IMapper);
                  //_IAppInstService.SubmitWfUpdateObjWfInfo(instInfo);

         await   Task.Factory.StartNew(() =>
            {

                _IAppInstService.SubmitWfUpdateObjWfInfo(instInfo);
            });
            if (LhLicense.WxKaiQi==1)
            {
                //添加微信消息
                await Task.Factory.StartNew(() =>
                {

                    _IAppInstService.WeiXinFlowNodeMsg(instInfo);
                });
            }
           

            return  GetResult();
        }


        /// <summary>
        /// 已发起列表数据
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAppSponsorList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<AppInst>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            predicateAnd = predicateAnd.And(a=>a.StartUserId==this.SessionCurrUserId);
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.AppObjName.Contains(pageParam.keyWord));
                predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IAppInstService.GetAppSponsorList(pageInfo, predicateAnd, a => a.StartDateTime, false);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 待处理
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAppPendingList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<AppInst>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
           
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.AppObjName.Contains(pageParam.keyWord));
                predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IAppInstService.GetAppPendingList(pageInfo,this.SessionCurrUserId, predicateAnd, a => a.StartDateTime, true);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 已处理
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAppProcessedList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<AppInst>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            predicateAnd = predicateAnd.And(
                p => p.AppState == (int)AppInstEnum.AppState1 && p.StartUserId == this.SessionCurrUserId
                
            );
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.AppObjName.Contains(pageParam.keyWord));
                predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            if (pageParam.search  > 0)
            {

                predicateAnd = predicateAnd.And(_IRemindService.GetWfIntanceExpression("已通过的审批",this.SessionCurrUserId));

            }
            var layPage = _IAppInstService.GetAppProcessedList(pageInfo, this.SessionCurrUserId, predicateAnd, a => a.CompleteDateTime, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 被打回列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAppBeBackList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<AppInst>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            predicateAnd = predicateAnd.And(a => a.StartUserId == this.SessionCurrUserId&&a.AppState==3&&(a.NewInstId??0)<=0);
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.AppObjName.Contains(pageParam.keyWord));
                predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IAppInstService.GetAppBeBackList(pageInfo, predicateAnd, a => a.StartDateTime, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 审批历史
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAppHistList(int appObjId,int objType)
        {
            var pageInfo = new NoPageInfo<AppInst>();
            var predicateAnd = PredicateBuilder.True<AppInst>();
            //var predicateOr = PredicateBuilder.False<AppInst>();
            predicateAnd = predicateAnd.And(a=>a.ObjType== objType&&a.AppObjId== appObjId);

            var layPage = _IAppInstService.GetAppHistList(pageInfo, this.SessionCurrUserId, predicateAnd, a => a.Id, true);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 查看页面查看流程
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewFlow()
        {
            return View();

        }
        /// <summary>
        /// 查看页面查看流程信息
        /// </summary>
        /// <param name="instId">实例Id</param>
        /// <returns></returns>
        public IActionResult ViewFlowChart(int instId)
        {
            var data = _IAppInstNodeService.LoadFlowChart(instId, _IMapper);
            return new CustomResultJson(data);
            
        }

        /// <summary>
        /// 根据节点ID查询节点信息
        /// </summary>
        /// <returns></returns>
        public IActionResult GetNodeInfoView(string nodeStr, int instId)
        {
            var info = _IAppInstNodeService.GetNodeInfoByStrId(nodeStr, instId);
            if (info == null)
            {
                info = new AppInstNodeInfoViewDTO()
                {
                    Id = 0,
                    NodeStrId = nodeStr,
                    InstId = instId,
                    UserNames = "",
                    GroupName = ""

                };
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info


            });
        }
        /// <summary>
        /// 审批页
        /// </summary>
        /// <returns></returns>
        public IActionResult ApprovePage()
        {
            return View();
        }
        /// <summary>
        /// 查看所有意见
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowAllOpinion()
        {
            return View();
        }
        /// <summary>
        /// 我审批-打回
        /// </summary>
        /// <returns></returns>
        public IActionResult WoShenPiDh()
        {
            return View();
        }
        /// <summary>
        /// 我审批-通过
        /// </summary>
        /// <returns></returns>
        public IActionResult WoShenPiTg()
        {
            return View();
        }

        public IActionResult GetWoShenPi(PageparamInfo pageParam,int wosp)
        {
            var pageInfo = new PageInfo<AppInst>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.AppObjName.Contains(pageParam.keyWord));
                predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            //if (pageParam.search > 0)
            //{

            //    predicateAnd = predicateAnd.And(_IRemindService.GetWfIntanceExpression("已通过的审批", this.SessionCurrUserId));

            //}
            var layPage = _IAppInstService.GetWoShenPiList(pageInfo, this.SessionCurrUserId, predicateAnd, a => a.Id, false, wosp);
            return new CustomResultJson(layPage);
        }





    }
}