using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Finance.Controllers
{
    /// <summary>
    /// 实际资金
    /// </summary>
    [Area("Finance")]
    [Route("Finance/[controller]/[action]")]
    public class ContActualFinanceController : NfBaseController
    {
        /// <summary>
        /// 实际资金
        /// </summary>
        private IContActualFinanceService _IContActualFinanceService;
        /// <summary>
        /// AutoMapper
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// 计划资金
        /// </summary>
        private IContPlanFinanceService _IContPlanFinanceService;
        /// <summary>
        /// 合同统计字段
        /// </summary>
        private IContStatisticService _IContStatisticService;
        /// <summary>
        /// 发票
        /// </summary>
        private IContInvoiceService _IContInvoiceService;
        /// <summary>
        /// 合同
        /// </summary>
        private IContractInfoService _IContractInfoService;
        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;

        private IProjectManagerService _IProjectManagerService;

        public ContActualFinanceController(IContActualFinanceService IContActualFinanceService,
           IMapper IMapper, ISysPermissionModelService ISysPermissionModelService,
           IContStatisticService IContStatisticService
            , IContPlanFinanceService IContPlanFinanceService
            , IContInvoiceService IContInvoiceService
            , IContractInfoService IContractInfoService
            , IRemindService IRemindService
            , IUserInforService IUserInforService
            , IProjectManagerService IProjectManagerService)
        {
            _IContActualFinanceService = IContActualFinanceService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContStatisticService = IContStatisticService;
            _IContPlanFinanceService = IContPlanFinanceService;
            _IContInvoiceService = IContInvoiceService;
            _IContractInfoService = IContractInfoService;
            _IRemindService = IRemindService;
            _IUserInforService = IUserInforService;
            _IProjectManagerService = IProjectManagerService;
        }

        #region 实际收款
        public IActionResult ActualFinanceCollIndex()
        {
            return View();

        }
        /// <summary>
        /// 建立实际资金页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ActualFinanceCollBuild()
        {
            _IContActualFinanceService.ExecuteSqlCommand($"delete ActFinceFile where ActId={-this.SessionCurrUserId}");
            return View();
        }
        /// <summary>
        /// 查看实际资金页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ActualFinanceCollDetail()
        {

            return View();
        }
        #endregion

        #region 实际付款
        public IActionResult ActualFinancePayIndex()
        {
            return View();

        }
        /// <summary>
        /// 实际付款查看
        /// </summary>
        /// <returns></returns>
        public IActionResult ActualFinancePayDetail()
        {
            return View();
        }

        public IActionResult ActualFinancePayBuild()
        {
            _IContActualFinanceService.ExecuteSqlCommand($"delete ActFinceFile where ActId={-this.SessionCurrUserId}");
            return View();
        }
        #endregion

        #region 公共部分
        /// <summary>
        /// 实际资金
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<ContActualFinance>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.fType, pageParam.search));
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryActFince(pageParam, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryContActualFinance(pageParam, _IUserInforService, _IProjectManagerService, _IContPlanFinanceService));
            }
            if (pageParam.otherwh != null && pageParam.otherwh > 0)
            {
                predicateAnd = predicateAnd.And(a => a.ContId == pageParam.otherwh);
            }
            var layPage = _IContActualFinanceService.GetMainList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContActualFinance, bool>> GetQueryExpression(PageInfo<ContActualFinance> pageInfo, string keyWord, int financeType, int? search)
        {
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            var predicateOr = PredicateBuilder.False<ContActualFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && (a.Cont == null ? false : a.Cont.FinanceType == financeType));
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetActFinanceListPermissionExpression((financeType == 0 ? "querycollcontview" : "querypaycontview"), this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Cont != null && a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont != null && a.Cont.Code.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.ConfirmUser != null && a.ConfirmUser.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.CreateUser != null && a.CreateUser.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => (a.Cont != null && a.Cont.Comp != null) && a.Cont.Comp.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }

            if ((search ?? 0) > 0)
            {
                var content = financeType == 0 ? "待处理的实际收款" : "待处理的实际付款";
                predicateAnd = predicateAnd.And(_IRemindService.GetActualFinanceExpression(content));

            }

            return predicateAnd;

        }
        /// <summary>
        /// 根据合同ID查询计划资金
        /// </summary>
        /// <returns></returns>
        public IActionResult GetPlFinanceByContId(int contId, int actId)
        {
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == contId);
            var pageInfo = new NoPageInfo<ContPlanFinance>();
            var layPage = _IContPlanFinanceService.GetListSecod(pageInfo, predicateAnd, a => a.Id, true, actId);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 计划资金核销
        /// </summary>
        public IActionResult GetPlanCheckList(int contId, int actId)
        {
            var predicateAnd = PredicateBuilder.True<ContPlanFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == contId);
            var pageInfo = new NoPageInfo<ContPlanFinance>();
            var layPage = _IContPlanFinanceService.GetPlanCheckList(pageInfo, predicateAnd, a => a.Id, true, actId);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 根据合同ID查询发票
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInvoiceByContId(int contId, int actId)
        {
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == contId && (a.InState == 2 || a.InState == 3));
            var pageInfo = new NoPageInfo<ContInvoice>();
            var layPage = _IContInvoiceService.GetActInvoiceList(pageInfo, predicateAnd, a => a.Id, true, actId);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建实际资金
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新建实际资金", OptionLogTypeEnum.Add, "新建实际资金", true)]
        public IActionResult Save(ContActualFinanceDTO info, IList<CheckData> chkData)
        {
            var checkType = info.CheckType;
            //return null;
            var saveInfo = _IMapper.Map<ContActualFinance>(info);
            saveInfo.Astate = 0;
            saveInfo.IsDelete = 0;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            var a = 0;
            a = _IContActualFinanceService.Sjzj(saveInfo.AmountMoney ?? 0, saveInfo.ContId ?? 0);

            if (a > 0)
            {
                var currsaveinfo = _IContActualFinanceService.AddSave(saveInfo, checkType, chkData);
                _IContActualFinanceService.ExecuteSqlCommand($"update ActFinceFile set ActId={currsaveinfo.Id} where ActId={-this.SessionCurrUserId}");
                return GetResult();
            }
            else
            {

                var requstResult = new RequstResult()
                {
                    Msg = "实际资金之和不能大于合同金额",
                    Code = 0,
                    RetValue = 1,
                };


                return new CustomResultJson(requstResult);
                //requstResult.RetValue = 1;
                //requstResult.Msg = "实际资金总额不能大于计划资金总额";
            }



        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("修改实际资金", OptionLogTypeEnum.Update, "修改实际资金", true)]
        public IActionResult UpdateSave(ContActualFinanceDTO info, IList<CheckData> chkData)
        {
            var checkType = info.CheckType;
            //return null;

            var updateinfo = _IContActualFinanceService.Find(info.Id);
            //var oldamount = updateinfo.AmountMoney ?? 0;
            var updatedata = _IMapper.Map(info, updateinfo);
            updateinfo.ModifyDateTime = DateTime.Now;
            updateinfo.ModifyUserId = SessionCurrUserId;

            // return GetResult();
            var a = 0;
            a = _IContActualFinanceService.UpdSjzj(updateinfo.AmountMoney ?? 0, updateinfo.ContId ?? 0, updateinfo.Id, this.SessionCurrUserId);

            if (a > 0)
            {
                _IContActualFinanceService.UpdateSave(updateinfo, checkType, chkData);
                return GetResult();
            }
            else
            {

                var requstResult = new RequstResult()
                {
                    Msg = "实际资金之和不能大于合同金额",
                    Code = 0,
                    RetValue = 1,
                };


                return new CustomResultJson(requstResult);
            }

        }

        /// <summary>
        /// 获取当前合同下的核销明细
        /// </summary>
        /// <param name="contId">当前合同ID</param>
        /// <returns></returns>
        public IActionResult GetChkDetail(int contId)
        {
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.ContId == contId);
            var pageInfo = new NoPageInfo<ContActualFinance>();
            var layPage = _IContActualFinanceService.GetChkList(pageInfo, predicateAnd, a => a.Id, true);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {

            var pageInfo = new NoPageInfo<ContActualFinance>();
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            PageparamInfo pageParam = new PageparamInfo();
            pageParam.keyWord = exportRequestInfo.KeyWord;
            pageParam.jsonStr = exportRequestInfo.jsonStr;
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord, exportRequestInfo.FType, exportRequestInfo.search));
            if (exportRequestInfo.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
            }
            else
            {//所有行
                if (!string.IsNullOrEmpty(pageParam.jsonStr))
                {//高级查询
                    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryActFince(pageParam, _IUserInforService));
                }
            }
            var layPage = _IContActualFinanceService.GetMainList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, (exportRequestInfo.FType == 0 ? "实际收款" : "实际付款"), layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
        /// <summary>
        /// 绑定或者修改值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContActualFinanceService.ShowView(Id)


            });
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除实际资金", OptionLogTypeEnum.Del, "删除实际资金", false)]
        public IActionResult Delete(string Ids, int inType)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var permiision = _ISysPermissionModelService.GetDeleteActFinancePermission(inType == 0 ? "deleteInvoice" : "deleteInvoiceOut", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,


            };
            var usname = this.SessionCurrUserId;
            if (usname == -10000)
            {
                var Usid = this.SessionCurrUserId;

                _IContActualFinanceService.Delete(Ids, Usid);

            }
            else
            {
                if (permiision.Code != 0)
                {
                    resinfo.RetValue = permiision.Code;
                    resinfo.Msg = permiision.GetOptionMsg(permiision.Code);
                    resinfo.Data = permiision.noteAllow;
                }
                else
                {
                    var Usid = this.SessionCurrUserId;

                    _IContActualFinanceService.Delete(Ids, Usid);

                }
            }




            return new CustomResultJson(resinfo);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id">修改对象ID</param>
        /// <param name="fieldName">修改字段名称</param>
        /// <param name="fieldVal">修改值，如果不是String后台人为判断</param>
        /// <returns></returns>
        public IActionResult UpdateField(UpdateFieldInfo info, int Id, string fieldName, string fieldVal)
        {
            info.CurrUserId = this.SessionCurrUserId;
            if (string.IsNullOrEmpty(info.FieldName))
            {
                info.FieldName = fieldName;
            }
            if (string.IsNullOrEmpty(info.FieldValue))
            {
                info.FieldValue = fieldVal;
            }
            var res = _IContActualFinanceService.UpdateField(info);
            RequstResult reqInfo = null;
            if (res > 0)
            {
                reqInfo = new RequstResult()
                {
                    Msg = "修改成功",
                    Code = 0,


                };
            }
            else
            {
                reqInfo = new RequstResult()
                {
                    Msg = "修改失败",
                    Code = 0,


                };
            }
            return new CustomResultJson(reqInfo);
        }
        /// <summary>
        /// 合同查看页面
        /// </summary>
        /// <param name="Id">实际资金ID</param>
        /// <returns></returns>
        public IActionResult ShowContView(int Id)
        {
            var actInfo = _IContActualFinanceService.Find(Id);
            var info = _IContractInfoService.ShowView((actInfo == null ? 0 : actInfo.ContId ?? 0));
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info
            });
        }
        /// <summary>
        /// 校验合同金额
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckContAmount(int Id)
        {
            try
            {
                var rest = 0;
                var actInfo = _IContActualFinanceService.Find(Id);
                if (actInfo != null)
                {
                    var contInfo = _IContractInfoService.Find(actInfo.ContId ?? 0);
                    if (actInfo != null)
                    {//标准合同
                     //已经提交金额总数
                        if (contInfo.IsFramework == 0)
                        {
                            var tjjezs = _IContActualFinanceService.GetQueryable(a => a.ContId == contInfo.Id && (a.Astate == 1 || a.Astate == 2)).Sum(a => (decimal?)(a.AmountMoney ?? 0));
                            var tjhjezs = (tjjezs ?? 0) + (actInfo.AmountMoney ?? 0);
                            if (tjhjezs > (contInfo.AmountMoney ?? 0))
                            {
                                rest = 1;

                            }
                        }
                    }
                    else
                    {
                        rest = 1;
                    }

                }
                else
                {
                    rest = 1;
                }

                return new CustomResultJson(new RequstResult()
                {
                    Msg = "suc",
                    Code = 0,
                    RetValue = rest

                });
            }
            catch (Exception ex)
            {
                Log4netHelper.Error(ex.ToString());
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "err",
                    Code = 0,
                    RetValue = -1

                });

            }


        }



        #endregion
    }


}