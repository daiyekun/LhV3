using LhCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.WeiXinApp.Extend;
using NF.WeiXinApp.Utility;
using NF.WeiXinApp.Utility.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NF.WeiXinApp.Utility.Common;
using NF.WeiXin.Lib.Common;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NF.WeiXinApp.Areas.APIData.Controllers
{
    /// <summary>
    /// 流程控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowController : ControllerBase
    {
        /// <summary>
        /// 审批实例
        /// </summary>
        private IAppInstService _IAppInstService;
        /// <summary>
        /// 审批意见
        /// </summary>
        private IAppInstOpinService _IAppInstOpinService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;

        private IContractInfoService _IContractInfoService;
        private readonly ILogger<WorkFlowController> _logger;
        public WorkFlowController(IAppInstService IAppInstService,
             IContractInfoService IContractInfoService,
            IAppInstOpinService IAppInstOpinService,
            IUserInforService IUserInforService,
            ILogger<WorkFlowController> logger
            )
        {
            _IContractInfoService = IContractInfoService;
            _IAppInstService = IAppInstService;
            _IAppInstOpinService = IAppInstOpinService;
            _IUserInforService = IUserInforService;
            _logger = logger;
        }
        // GET api/<WorkFlowController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// 获取流程权限
        /// </summary>
        /// <param name="spId">审批ID</param>
        /// <param name="userId">当前人员ID</param>
        /// <param name="wfItem">审批事项</param>
        /// <param name="spType">审批类型0：客户，1：供应商：3：合同请参看枚举FlowObjEnums</param>
        /// <returns></returns>
        [HttpPost("GetFlowPermission")]
        public string GetFlowPermission([FromBody] FlowPerm flowPerm)
        {

            var info = _IAppInstService.GetFlowPermission(flowPerm);
            return new RequestData(data: info).ToWxJson();
        }

        /// <summary>
        /// 同意提交意见
        /// </summary>
        /// <param name="optionInfo">提交意见</param>
        /// <returns></returns>
        [CustomAction2CommitFilter]
         [HttpPost("SubmitAgreeOption")]
        public async Task<string> SubmitAgreeOption([FromBody] SubOption otion)
        {
            try
            {
                SubmitOptionInfo submitOption = GetSubOption(otion);
                //审批之前的实例信息，主要为了获取当前节点
                var currInst = _IAppInstService.Find(otion.InstId);
                var nodeId = currInst.CurrentNodeId ?? 0;
                _IAppInstOpinService.SubmintOption(submitOption);
                var instInfo = _IAppInstService.Find(otion.InstId);
                //_logger.LogWarning("配置信息:"+ LhLicense.WxKaiQi);
                if (LhLicense.WxKaiQi == 1)
                {
                    //删掉超时提醒
                    //await Task.Factory.StartNew(() =>
                    //{
                    //    if (currInst != null && nodeId > 0)
                    //    {
                    //        _IAppInstService.ClearMsg(instInfo.Id, nodeId, submitOption.SubmitUserId);
                    //    }
                    //});
                    //提醒之前节点
                    await Task.Factory.StartNew(() =>
                    {

                        _IAppInstService.WxmsgPrvNode(instInfo, 0,"");
                    });

                    if (instInfo.AppState == 2)
                    {
                        //通过的时候提醒
                       
                        await Task.Factory.StartNew(() =>
                        {

                            _IAppInstService.WxDhOrTgMsg(instInfo, 0);


                        });
                    }
                    else
                    {
                        //下节点审批
                        await Task.Factory.StartNew(() =>
                        {

                            _IAppInstService.WeiXinFlowNodeMsg(instInfo);


                        });
                    }

                   
                }
                
                return new RequestData().ToWxJson();
            }
            catch (Exception ex)
            {
                Log4netHelper.Error(ex.Message);
                return new RequestData(code: 1).ToWxJson();
            }
        }
        /// <summary>
        /// 获取提交意见
        /// </summary>
        /// <param name="otion"></param>
        /// <returns></returns>
        private SubmitOptionInfo GetSubOption(SubOption otion)
        {
            var userinfo = _IUserInforService.GetQueryable(a => a.WxCode == otion.SubmitWxId).FirstOrDefault();
            SubmitOptionInfo submitOption = new SubmitOptionInfo();
            submitOption.InstId = otion.InstId;
            submitOption.ObjId = otion.ObjId;
            submitOption.ObjMoney = otion.ObjMoney;
            submitOption.ObjType = otion.ObjType;
            submitOption.Option = otion.Option;
            submitOption.OptRes = 1;
            submitOption.SubmitUserId = userinfo == null ? 0 : userinfo.Id;
            return submitOption;
        }

        /// <summary>
        /// 不同意时提交意见
        /// </summary>
        /// <returns></returns>
        [CustomAction2CommitFilter]
        [HttpPost("SubmitDisagreeOption")]
        public async Task<string> SubmitDisagreeOption([FromBody] SubOption otion)
        {
            var jdName = otion.DDs;

            try
            {
                SubmitOptionInfo submitOption = GetSubOption(otion);
                //审批之前的实例信息，主要为了获取当前节点
                var currInst = _IAppInstService.Find(otion.InstId);
                var nodeId = currInst.CurrentNodeId ?? 0;
                _IAppInstOpinService.SubmintDisagreeOption(submitOption);
                var instInfo = _IAppInstService.Find(otion.InstId);
                if (LhLicense.WxKaiQi == 1)
                {
                    //删掉超时提醒
                    //await Task.Factory.StartNew(() =>
                    //{
                    //    if (currInst != null && nodeId > 0)
                    //    {
                    //        _IAppInstService.ClearMsg(instInfo.Id, nodeId, submitOption.SubmitUserId);
                    //    }
                    //});
                    //之前节点
                    await Task.Factory.StartNew(() =>
                    {
                     
                        _IAppInstService.WxmsgPrvNode(instInfo, 1, jdName);
                    });
                    //发起人
                    await Task.Factory.StartNew(() =>
                    {
                        _IAppInstService.WxDhOrTgMsg(instInfo, 1);
 
                    });
                   
                    


                }
                return new RequestData().ToWxJson();
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return new RequestData(code: 1).ToWxJson();
            }
        }
        /// <summary>
        /// 不同意时提交意见
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetFlowOptions")]
        public string GetFlowOptions([FromBody] FlowPerm flowPerm)
        {
            var listopts = _IAppInstService.GetFlowOptions(flowPerm);
            return new RequestData(data: listopts).ToWxJson();
        }

        // POST api/<WorkFlowController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WorkFlowController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WorkFlowController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// 待处理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("WxDcl")]
        public string WxDcl(int page, int limit, string keyWord, string Wxzh)
        {
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo!=null)
            {
                var UsId = usinfo.Id;
                var UsDc = usinfo.DepartmentId;
                var pageInfo = new PageInfo<AppInst>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<AppInst>();
                var predicateOr = PredicateBuilder.False<AppInst>();

                if (!string.IsNullOrEmpty(keyWord))
                {
                    predicateOr = predicateOr.Or(a => a.AppObjName.Contains(keyWord));
                    predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(keyWord));
                    predicateAnd = predicateAnd.And(predicateOr);
                }
                var layPage = _IAppInstService.GetAppWxDclList(pageInfo, UsId, predicateAnd, a => a.StartDateTime, true);
                //  return new CustomResultJson(layPage);
                return layPage.ToWxJson();

            }
            else
            {
                return (new LayPageInfo<ViewModel.WxAppSp>()
                {
                    data = null,
                    count = 0,
                    code = 0


                }).ToWxJson();

            }
            
        }
        /// <summary>
        /// 已处理
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">条数</param>
        /// <param name="keyWord">查询字段</param>
        /// <param name="Wxzh">微信账号</param>
        /// <returns></returns>
        [HttpGet("WxYcl")]
        public string WxYcl(int page, int limit, string keyWord, string Wxzh)
        {
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo != null) { 
            var UsId = usinfo.Id;
            var UsDc = usinfo.DepartmentId;
            var pageInfo = new PageInfo<AppInst>(pageIndex: page, pageSize: limit);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            predicateAnd = predicateAnd.And(
                p => p.AppState == (int)AppInstEnum.AppState1 && p.StartUserId == UsId

            );
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.AppObjName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            //if (pageParam.search > 0)
            //{

            //    predicateAnd = predicateAnd.And(_IRemindService.GetWfIntanceExpression("已通过的审批", this.SessionCurrUserId));

            //}
            var layPage = _IAppInstService.WxYcl(pageInfo, UsId, predicateAnd, a => a.Id, true);
            return layPage.ToWxJson();// return new CustomResultJson(layPage);
            }
             else
            {
                return (new LayPageInfo<ViewModel.WxAppSp>()
                {
                    data = null,
                    count = 0,
                    code = 0


                }).ToWxJson();
            }

        }
        /// <summary>
        /// 我通过
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">条数</param>
        /// <param name="keyWord">查询字段</param>
        /// <param name="Wxzh">微信账号</param>
        /// <returns name="type">0:打回 1:通过 </returns>
        [HttpGet("WxWtg")]
        public string WxWtg(int page, int limit, string keyWord, string Wxzh, int type)
        {
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo != null) { 
            var UsId = usinfo.Id;
            var UsDc = usinfo.DepartmentId;
            var pageInfo = new PageInfo<AppInst>(pageIndex: page, pageSize: limit);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.AppObjName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IAppInstService.GetWxWtgList(pageInfo, UsId, predicateAnd, a => a.Id, true, type);
            return layPage.ToWxJson();// return new CustomResultJson(layPage);
            }
            else
            {
                return (new LayPageInfo<ViewModel.WxAppSp>()
                {
                    data = null,
                    count = 0,
                    code = 0


                }).ToWxJson();
            }
        }
        /// <summary>
        /// 已发起
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">条数</param>
        /// <param name="keyWord">查询字段</param>
        /// <param name="Wxzh">微信账号</param>
        /// <returns></returns>
        [HttpGet("WxYfq")]
        public string WxYfq(int page, int limit, string keyWord, string Wxzh)
        {
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo != null)
            {
                var UsId = usinfo.Id;
                var UsDc = usinfo.DepartmentId;
                var pageInfo = new PageInfo<AppInst>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<AppInst>();
                var predicateOr = PredicateBuilder.False<AppInst>();
                predicateAnd = predicateAnd.And(a => a.StartUserId == UsId);
                if (!string.IsNullOrEmpty(keyWord))
                {
                    predicateOr = predicateOr.Or(a => a.AppObjName.Contains(keyWord));
                    predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(keyWord));
                    predicateAnd = predicateAnd.And(predicateOr);
                }
                var layPage = _IAppInstService.GetYfqList(pageInfo, predicateAnd, a => a.StartDateTime, false);
                return layPage.ToWxJson();//return new CustomResultJson(layPage);
            }
            else
            {
                return (new LayPageInfo<ViewModel.WxAppSp>()
                {
                    data = null,
                    count = 0,
                    code = 0


                }).ToWxJson();
            }
        }

        /// <summary>
        /// 被打回
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">条数</param>
        /// <param name="keyWord">查询字段</param>
        /// <param name="Wxzh">微信账号</param>
        /// <returns></returns>
        [HttpGet("WxBdh")]
        public string WxBdh(int page, int limit, string keyWord, string Wxzh)
        {
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            if (usinfo != null)
            {
                var UsId = usinfo.Id;
                var UsDc = usinfo.DepartmentId;
                var pageInfo = new PageInfo<AppInst>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<AppInst>();
                var predicateOr = PredicateBuilder.False<AppInst>();
                predicateAnd = predicateAnd.And(a => a.StartUserId == UsId && a.AppState == 3 && (a.NewInstId ?? 0) <= 0);
                if (!string.IsNullOrEmpty(keyWord))
                {
                    predicateOr = predicateOr.Or(a => a.AppObjName.Contains(keyWord));
                    predicateOr = predicateOr.Or(a => a.AppObjNo.Contains(keyWord));
                    predicateAnd = predicateAnd.And(predicateOr);
                }
                var layPage = _IAppInstService.GetBdhList(pageInfo, predicateAnd, a => a.StartDateTime, false);
                return layPage.ToWxJson();//return new CustomResultJson(layPage);
            }
            else
            {
                return (new LayPageInfo<ViewModel.WxAppSp>()
                {
                    data = null,
                    count = 0,
                    code = 0


                }).ToWxJson();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instId"></param>
        /// <returns></returns>
        [HttpGet("FlowItem")]
        public string GetFlowTime(int instId)
        {
            var reqdata = new RequestData();
            reqdata.Data = _IAppInstService.GetFlowTime(instId);
           
            return reqdata.ToWxJson(istime:true);
        }

        /// <summary>
        /// 获取时间轴
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetWooFlowTime")]
        public string GetWooFlowTime([FromBody] FlowPerm flowPerm)
        {
           var listopts = _IAppInstService.GetFlowTime(flowPerm);
            return new RequestData(data: listopts).ToWxJson(istime: true);
        }

        /// <summary>
        /// 查询剩余审批信息
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        [HttpGet("GetCurrentAppInfo")]
        public string GetCurrentAppInfo(string wxcode)
        {
            var usinfo = _IContractInfoService.Yhinfo(wxcode);
            var userId = usinfo != null ? usinfo.Id : 0;
            var reqdata = new RequestData();
             reqdata.Data = _IAppInstService.GetDclInfo(userId);

            return reqdata.ToWxJson(istime: true);
        }
    }

}
