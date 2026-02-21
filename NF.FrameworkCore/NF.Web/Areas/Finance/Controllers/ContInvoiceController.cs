using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    /// 发票
    /// </summary>
    [Area("Finance")]
    [Route("Finance/[controller]/[action]")]
    public class ContInvoiceController : NfBaseController
    {
        /// <summary>
        /// 发票操作
        /// </summary>
        private IContInvoiceService _IContInvoiceService;
        /// <summary>
        /// AutoMapper
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// 合同统计字段
        /// </summary>
        private IContStatisticService _IContStatisticService;

        /// <summary>
        /// 合同操作
        /// </summary>
        private IContractInfoService _IContractInfoService;
        //发票明细
        private IInvoDescriptionService _IInvoDescriptionService;
        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        private IProjectManagerService _IProjectManagerService;
      

        private IContPlanFinanceService _IContPlanFinanceService;
        public ContInvoiceController(IContInvoiceService IContInvoiceService,
              IMapper IMapper, ISysPermissionModelService ISysPermissionModelService
            , IContractInfoService IContractInfoService
            , IContStatisticService IContStatisticService
            , IInvoDescriptionService IInvoDescriptionService
            , IRemindService IRemindService
            , IUserInforService IUserInforService
            , IProjectManagerService IProjectManagerService
            , IContPlanFinanceService IContPlanFinanceService
           )

        {
            _IProjectManagerService = IProjectManagerService;
            _IContInvoiceService = IContInvoiceService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContractInfoService = IContractInfoService;
            _IContStatisticService = IContStatisticService;
            _IInvoDescriptionService = IInvoDescriptionService;
            _IRemindService = IRemindService;
            _IUserInforService = IUserInforService;
            _IContPlanFinanceService = IContPlanFinanceService;
        }
        #region 收票
        /// <summary>
        /// 收票
        /// </summary>
        /// <returns></returns>
        public IActionResult InvoiceIndex()
        {
            return View();
        }
        /// <summary>
        /// 新建收票
        /// </summary>
        /// <returns></returns>
        public IActionResult BuildInvoice()
        {
            _IContInvoiceService.ClearJunkItemData(this.SessionCurrUserId);
            return View();
        }
        /// <summary>
        /// 收票查看
        /// </summary>
        /// <returns></returns>
        public IActionResult DetailInvoice()
        {
            return View();
        }

        #endregion

        #region 开票
        /// <summary>
        /// 开票
        /// </summary>
        /// <returns></returns>
        public IActionResult InvoiceOutIndex()
        {
            return View();
        }
        /// <summary>
        /// 新建开票
        /// </summary>
        /// <returns></returns>
        public IActionResult BuildInvoiceOut()
        {
            _IContInvoiceService.ClearJunkItemData(this.SessionCurrUserId);
            return View();
        }
        /// <summary>
        /// 收票查看
        /// </summary>
        /// <returns></returns>
        public IActionResult DetailInvoiceOut()
        {
            return View();
        }

        #endregion
        #region 公用
        /// <summary>
        /// 发票大列列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<ContInvoice>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.fType, pageParam.search));
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryInvoice(pageParam, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryContInvoice(pageParam, _IUserInforService, _IProjectManagerService, _IContPlanFinanceService));
            }
            if (pageParam.otherwh != null && pageParam.otherwh > 0)
            {
                predicateAnd = predicateAnd.And(a => a.ContId == pageParam.otherwh);
            }
            var layPage = _IContInvoiceService.GetMainList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContInvoice, bool>> GetQueryExpression(PageInfo<ContInvoice> pageInfo, string keyWord, int financeType, int? search)
        {
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
            var predicateOr = PredicateBuilder.False<ContInvoice>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.Cont.FinanceType == financeType);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetInvoiceListPermissionExpression((financeType == 0 ? "querycollcontview" : "querypaycontview"), this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Cont!=null&& a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont != null&& a.Cont.Code.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.CreateUser!=null&&a.CreateUser.DisplyName.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.ConfirmUser!=null&& a.ConfirmUser.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => !string.IsNullOrEmpty(a.InCode)&& a.InCode.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            if ((search ?? 0) > 0)
            {//提醒列表
                var content = financeType == 0 ? "待处理的开票" : "待处理的收票";
                predicateAnd = predicateAnd.And(_IRemindService.GetInvoiceExpression(content));

            }


            return predicateAnd;

        }


        /// <summary>
        /// 保存发票
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新建发票", OptionLogTypeEnum.Add, "新建发票", true)]
        public IActionResult Save(ContInvoiceDTO info)
        {
            var saveInfo = _IMapper.Map<ContInvoice>(info);
            if (info.IsOutInvoice)
            {
                var chkdesc = CheckInvoiceDesc(saveInfo);
                if (Convert.ToInt32(chkdesc.RetValue) > 0)
                {
                    return GetResult(chkdesc);
                }
            }


            var chkres = CheckContInvoiceSave(saveInfo);
            if (Convert.ToInt32(chkres.RetValue) > 0)
            {
                return GetResult(chkres);
            }
            else
            {
                saveInfo.InState = 0;
                saveInfo.CreateDateTime = DateTime.Now;
                saveInfo.ModifyDateTime = DateTime.Now;
                saveInfo.CreateUserId = this.SessionCurrUserId;
                saveInfo.ModifyUserId = this.SessionCurrUserId;
                _IContInvoiceService.AddSave(saveInfo);
                // AddInfoAddData(saveInfo.ContId, saveInfo.AmountMoney);
                return GetResult();
            }
        }

        /// <summary>
        /// 添加发票时添加合同统计字段
        /// </summary>
        /// <param name="contId"></param>
        /// <param name="amountMoney">当前金额</param>
        private void AddInfoAddData(int? contId, decimal? amountMoney)
        {
            ContStatistic statistics = new ContStatistic();
            statistics.InvoiceAmount = amountMoney;
            statistics.ModifyDateTime = DateTime.Now;
            statistics.ModifyUserId = SessionCurrUserId;
            statistics.ContId = contId;
            statistics.ActualAmount = 0;
            statistics.CompInAm = 0;
            statistics.CompActAm = 0;
            //double iamount = 0;
            //var amount=RedisHelper.HashGet($"{StaticData.ContStat}:{contId}", "InvoiceAmount").TryParse(out iamount);
            //if (iamount > 0)
            //{
            //    RedisHelper.HashDelete($"{StaticData.ContStat}:{contId}", "InvoiceAmount");
            //    var dcamount = Convert.ToDecimal(iamount);
            //    RedisHelper.HashSet($"{StaticData.ContStat}:{contId}", "InvoiceAmount", (dcamount + amountMoney).ToString());
            //}
            //else
            //{
            //    RedisHelper.HashSet($"{StaticData.ContStat}:{contId}", "InvoiceAmount", amountMoney.ToString());
            //}
            _IContStatisticService.AddData(statistics);


        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("修改发票", OptionLogTypeEnum.Update, "修改发票", true)]
        public IActionResult UpdateSave(ContInvoiceDTO info)
        {


            if (info.Id > 0)
            {

                var updateinfo = _IContInvoiceService.Find(info.Id);
                var oldamount = updateinfo.AmountMoney ?? 0;//原来的发票金额
                var updatedata = _IMapper.Map(info, updateinfo);
                //if (info.IsOutInvoice)
                //{
                    var chkdesc = CheckInvoiceDesc(updatedata);
                    if (Convert.ToInt32(chkdesc.RetValue) > 0)
                    {
                        return GetResult(chkdesc);
                    }
               //}

                var chkres = CheckContInvoiceSave(updatedata);
                if (Convert.ToInt32(chkres.RetValue) > 0)
                {
                    return GetResult(chkres);
                }
                else if (this.SessionCurrUserId==-10000)
                {
                    updatedata.InState = info.InState;
                    updatedata.ModifyUserId = this.SessionCurrUserId;
                    updatedata.ModifyDateTime = DateTime.Now;
                    _IContInvoiceService.Update(updatedata);
                    _IContInvoiceService.Xiug(updatedata.ContId ?? 0);
                }
                //发票明细校验
                //var chkfpmx = CheckContInvoiceMxSave(updatedata);
                //if (Convert.ToInt32(chkfpmx.RetValue) > 0)
                //{
                //    return GetResult(chkfpmx);
                //}

                else
                {

                    updatedata.InState = 0;
                    updatedata.ModifyUserId = this.SessionCurrUserId;
                    updatedata.ModifyDateTime = DateTime.Now;
                    _IContInvoiceService.Update(updatedata);
                  //  _IContInvoiceService.Xiug(updatedata.ContId ?? 0);
                    //AddInfoAddData(updatedata.ContId,((updatedata.AmountMoney??0)- oldamount));
                }
            }
            return GetResult();
        }
        /// <summary>
        /// 判断与合同金额判断
        /// </summary>
        /// <param name="saveInfo"></param>
        /// <returns></returns>
        private RequstResult CheckContInvoiceSave(ContInvoice saveInfo)
        {
            var queryplance = _IContInvoiceService.GetQueryable(a => (a.ContId == -this.SessionCurrUserId || a.ContId == saveInfo.ContId) && a.IsDelete == 0 && a.Id != saveInfo.Id);
            var contInfo = _IContractInfoService.Find(saveInfo.ContId ?? 0);
            decimal suminvoice = queryplance.Sum(a => (a.AmountMoney ?? 0));

            var requstResult = new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,
                RetValue = 0,
            };
            if (contInfo != null && suminvoice <= contInfo.AmountMoney)
            {

            }
            else
            {
                requstResult.RetValue = 1;
                requstResult.Msg = "发票金额之和不能大于合同金额！";
            }
            return requstResult;



        }

       
        /// <summary>
        /// 校验合同合同金额
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckContJe(ContInvoice saveInfo)
        {
            try
            {
                var res = 0;
                var fpzj = _IContInvoiceService.GetQueryable(a => a.ContId == saveInfo.ContId && a.IsDelete != 1).Sum(a => (decimal?)a.AmountMoney ?? 0);
                decimal dqzj = 0;
                
                if (saveInfo.Id != 0)
                {
                    dqzj= _IContInvoiceService.GetQueryable(a => a.Id == saveInfo.Id && a.IsDelete != 1).FirstOrDefault().AmountMoney??0;
                }
                    
                var contInfo = _IContractInfoService.Find(saveInfo.ContId ?? 0);
                if (contInfo != null)
                {
                    var zje = fpzj- dqzj + (saveInfo.AmountMoney ?? 0);
                    if (zje > contInfo.AmountMoney)
                    {
                        res = 1;

                    }
                }
                else
                {
                    res = 1;
                }
                var requstResult = new RequstResult()
                {
                    Msg = "succ",
                    Code = 0,
                    RetValue = res,
                };
                return GetResult(requstResult);
            }
            catch (Exception ex)
            {
                Log4netHelper.Error(ex.ToString());
                var requstResult = new RequstResult()
                {
                    Msg = "err",
                    Code = 0,
                    RetValue = -1,
                };
                return GetResult(requstResult);
            }
            //return GetResult(CheckContInvoiceSave(saveInfo));
        }
        /// <summary>
        /// 判断发票明细金额之和与发票金额比较
        /// </summary>
        /// <param name="saveInfo"></param>
        /// <returns></returns>
        private RequstResult CheckInvoiceDesc(ContInvoice saveInfo)
        {
            var queryincedesc = _IInvoDescriptionService.GetQueryable(a => a.IsDelete!=1&&(a.ContInvoId == -this.SessionCurrUserId || a.ContInvoId == saveInfo.Id));
            var sumdesc = queryincedesc.Sum(a => a.Total ?? 0);
            var requstResult = new RequstResult() { Code = 0 };
            if (queryincedesc.Any() && saveInfo.AmountMoney != sumdesc)
            {
                requstResult.RetValue = 1;
                requstResult.Msg = "发票明细金额之和必须等于发票金额";
            }

            return requstResult;
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除发票", OptionLogTypeEnum.Del, "删除发票", false)]
        public IActionResult Delete(string Ids, int inType)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var permiision = _ISysPermissionModelService.GetDeleteInvoicePermission(inType == 0 ? "deleteInvoice" : "deleteInvoiceOut", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,


            };

            var usname = this.SessionCurrUserId;
            if (usname == -10000)
            {
                var usid = this.SessionCurrUserId;
                _IContInvoiceService.Delete(Ids, usid);
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
                    //var query0 = _IContInvoiceService.GetQueryable(a => listIds.Contains(a.Id));
                    //var sumamount = query0.Sum(a => a.AmountMoney ?? 0);
                    var usid = this.SessionCurrUserId;
                    _IContInvoiceService.Delete(Ids, usid);
                    // AddInfoAddData(query0.First().ContId, -sumamount);
                }
            }


              


            return new CustomResultJson(resinfo);
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContInvoiceService.ShowView(Id)


            });
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id">修改对象ID</param>
        /// <param name="fieldName">修改字段名称</param>
        /// <param name="fieldVal">修改值，如果不是String后台人为判断</param>
        /// <returns></returns>
        [NfCustomActionFilter("修改字段", OptionLogTypeEnum.Update, "修改字段", false)]
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
            var res = _IContInvoiceService.UpdateField(info);
            RequstResult reqInfo = new RequstResult()
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
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {

            var pageInfo = new NoPageInfo<ContInvoice>();
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
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
                    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryInvoice(pageParam, _IUserInforService));
                }
            }
            var layPage = _IContInvoiceService.GetMainList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, (exportRequestInfo.FType == 0 ? "开票" : "收票"), layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }


        #endregion

        #region 发票内容
        /// <summary>
        /// 获取发票明细列表
        /// </summary>
        /// <param name="InvId">发票ID</param>
        /// <returns>发票明细列表</returns>
        public IActionResult GetInvoiceDescList(int InvId)
        {
            var pageInfo = new NoPageInfo<InvoDescription>();
            var predicateAnd = PredicateBuilder.True<InvoDescription>();
            predicateAnd = predicateAnd.And(a => (a.ContInvoId == InvId || a.ContInvoId == -this.SessionCurrUserId) && a.IsDelete == 0);
            _IInvoDescriptionService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(_IInvoDescriptionService.GetList(pageInfo, predicateAnd, a => a.Id, false));
        }
        /// <summary>
        /// 添加发票明细
        /// </summary>
        /// <returns></returns>
        public IActionResult AddInvoiceDesc(int InvId)
        {
            _IInvoDescriptionService.Add(new InvoDescription()
            {
                ContInvoId = InvId > 0 ? InvId : -this.SessionCurrUserId,
                Name = "名称",
                Price = 0,
                Count = 0,
                Total = 0,
                IsDelete = 0
            });
            return new CustomResultJson(new RequstResult()
            {
                Msg = "success",
                Code = 0,
            });
        }
        /// <summary>
        /// 修改发票明细
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="field"></param>
        /// <param name="fdv"></param>
        /// <returns></returns>
        public IActionResult UpdateInvoiceDesc(int Id, string field, string fdv)
        {
            var res = _IInvoDescriptionService.UpdateDesc(Id, field, fdv);
            if (res)
            {
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "success",
                    Code = 0,
                });
            }
            else
            {
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "no",
                    Code = 0,
                });

            }
        }
        /// <summary>
        /// 删除发票明细
        /// </summary>
        /// <returns></returns>
        public IActionResult DeleteDesc(string Ids)
        {
            _IInvoDescriptionService.Delete(Ids);
            return new CustomResultJson(new RequstResult() { Code = 0, Msg = "删除成功" });

        }
        #endregion




    }
}