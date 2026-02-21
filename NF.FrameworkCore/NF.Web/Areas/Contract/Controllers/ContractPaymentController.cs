using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using LhCode;
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

namespace NF.Web.Areas.Contract.Controllers
{
    /// <summary>
    /// 付款合同
    /// </summary>
    [Area("Contract")]
    [Route("Contract/[controller]/[action]")]
    public class ContractPaymentController : NfBaseController
    { /// <summary>
      /// 合同操作
      /// </summary>
        private IContractInfoService _IContractInfoService;
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
        /// 标的
        /// </summary>
        private IContSubjectMatterService _IContSubjectMatterService;
        /// <summary>
        /// 实际资金
        /// </summary>
        private IContActualFinanceService _IContActualFinanceService;
          /// <summary>
          /// 发票
          /// </summary>
        private IContInvoiceService _IContInvoiceService;
        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        /// <summary>
        /// 自动生成编号
        /// </summary>
        private INoHipler _INoHipler;
        /// <summary>
        /// 数据字典
        /// </summary>
        private IDataDictionaryService _IDataDictionaryService;
        public ContractPaymentController(IContractInfoService IContractInfoService, IMapper IMapper,
            ISysPermissionModelService ISysPermissionModelService, IContPlanFinanceService IContPlanFinanceService,
            IContSubjectMatterService IContSubjectMatterService
            , IContInvoiceService IContInvoiceService
            , IContActualFinanceService IContActualFinanceService
            , IRemindService IRemindService
             , IUserInforService IUserInforService
            , INoHipler INoHipler,
            IDataDictionaryService IDataDictionaryService)
        {
            _IContractInfoService = IContractInfoService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContPlanFinanceService = IContPlanFinanceService;
            _IContSubjectMatterService = IContSubjectMatterService;
            _IContInvoiceService = IContInvoiceService;
            _IContActualFinanceService = IContActualFinanceService;
            _IRemindService = IRemindService;
            _IUserInforService = IUserInforService;
            _INoHipler = INoHipler;
            //_IContractInfoHistoryService = IContractInfoHistoryService;
            _IDataDictionaryService = IDataDictionaryService;

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            _IContractInfoService.ClearJunkItemData(this.SessionCurrUserId);
            return View();
        }
        /// <summary>
        /// 变更
        /// </summary>
        /// <returns></returns>
        public IActionResult Change()
        {
            return View();
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public IActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 开关判断合同编号自动生成问题
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public IActionResult CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            if (LhLicense.NewCoreStyle != 1)
            {


                return new CustomResultJson(new RequstResult()
                {
                    Msg = "",
                    Code = 0,
                    RetValue = _IContractInfoService.CheckInputValExist(fieldInfo)

                });
            }
            else
            {
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "",
                    Code = 0,
                    RetValue = false //_IContractInfoService.CheckInputValExist(fieldInfo)

                }); ;

            }
        }



        /// <summary>
        /// 条件判断
        /// </summary>
        /// <param name="saveInfo"></param>
        /// <returns></returns>
        private RequstResult CheckContSave(ContractInfo saveInfo)
        {
            var queryplance = _IContPlanFinanceService.GetQueryable(a => a.ContId == -this.SessionCurrUserId || a.ContId == saveInfo.Id && a.IsDelete == 0);
            var querysub = _IContSubjectMatterService.GetQueryable(a => a.ContId == -this.SessionCurrUserId || a.ContId == saveInfo.Id && a.IsDelete == 0);
            decimal sumplance = queryplance.Sum(a => (a.AmountMoney ?? 0));
            decimal sumsub = querysub.Sum(a => (a.SubTotal ?? 0));

            var requstResult = new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,
                RetValue = 0,
            };
            if (saveInfo.IsFramework == 0)
            { //标准合同
                if (queryplance.Any() && sumplance != saveInfo.AmountMoney)
                {
                    requstResult.RetValue = 1;
                    requstResult.Msg = "计划资金金额之和不能大于合同金额！";

                }
                else if (querysub.Any() && sumsub != saveInfo.AmountMoney)
                {
                    requstResult.RetValue = 2;
                    requstResult.Msg = "标的小计之和不能大于合同金额！";

                }
            }
            else
            {
                if (queryplance.Any() && sumplance != saveInfo.AdvanceAmount)
                {
                    requstResult.RetValue = 1;
                    requstResult.Msg = "计划资金金额之和需要等于预收金额！";

                }
                else if (querysub.Any() && sumsub != saveInfo.AdvanceAmount)
                {
                    requstResult.RetValue = 2;
                    requstResult.Msg = "标的小计之和不能大于预收金额！";

                }
            }

            return requstResult;



        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("新建付款合同", OptionLogTypeEnum.Add, "新建付款合同", true)]
        public IActionResult Save(ContractInfoDTO info)
        {
            var temphtzt = info.ContState ?? 0;
            info.AmountMoney = ParseThousandthString(info.ContAmThod);
            info.StampTax = ParseThousandthString(info.StampTaxThod);
            // 开关自动生成编码
            if (LhCode.LhLicense.NewCoreStyle == 1)
            {
                info.Code = _INoHipler.ContractNo(info.DeptId ?? 0, info.ContTypeId ?? 0, info.MainDeptShortName);
            }
            var saveInfo = _IMapper.Map<ContractInfo>(info);
            var chkres = CheckContSave(saveInfo);
            if (Convert.ToInt32(chkres.RetValue) > 0)
            {
                return GetResult(chkres);
            }
            else
            {
                saveInfo.CreateDateTime = DateTime.Now;
                saveInfo.ModifyDateTime = DateTime.Now;
                saveInfo.CreateUserId = this.SessionCurrUserId;
                saveInfo.ModifyUserId = this.SessionCurrUserId;
                saveInfo.ContState = (byte)(temphtzt == 100 ? temphtzt : 0); ;
                saveInfo.IsDelete = 0;
                saveInfo.FinanceType = 1;//付款
                //if (saveInfo.IsFramework == 1)
                //{//框架合同,合同金额=预收金额
                //    saveInfo.AmountMoney = saveInfo.AdvanceAmount;
                //}
                var saveInfoHis = _IMapper.Map<ContractInfoHistory>(saveInfo);
                var dic = _IContractInfoService.AddSave(saveInfo, saveInfoHis, _IMapper);
                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功",
                    Data = dic

                });
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("新建付款合同", OptionLogTypeEnum.Add, "新建付款合同", true)]
        public IActionResult Save02(ContractInfoDTO info)
        {
            var temphtzt = info.ContState ?? 0;
            info.AmountMoney = ParseThousandthString(info.ContAmThod);
            info.StampTax = ParseThousandthString(info.StampTaxThod);
            // 开关自动生成编码
            if (LhCode.LhLicense.NewCoreStyle == 1)
            {
                info.Code = _INoHipler.ContractNo(info.DeptId ?? 0, info.ContTypeId ?? 0, info.MainDeptShortName);
            }
            var saveInfo = _IMapper.Map<ContractInfo>(info);
            var chkres = CheckContSave(saveInfo);
            if (Convert.ToInt32(chkres.RetValue) > 0)
            {
                return GetResult(chkres);
            }
            else
            {
                saveInfo.CreateDateTime = DateTime.Now;
                saveInfo.ModifyDateTime = DateTime.Now;
                saveInfo.CreateUserId = this.SessionCurrUserId;
                saveInfo.ModifyUserId = this.SessionCurrUserId;
                saveInfo.ContState = (byte)(temphtzt == 100 ? temphtzt : 0); ;
                saveInfo.IsDelete = 0;
                saveInfo.FinanceType = 1;//付款
                //if (saveInfo.IsFramework == 1)
                //{//框架合同,合同金额=预收金额
                //    saveInfo.AmountMoney = saveInfo.AdvanceAmount;
                //}
                var saveInfoHis = _IMapper.Map<ContractInfoHistory>(saveInfo);
                var dic = _IContractInfoService.AddSave02(saveInfo, saveInfoHis, _IMapper);
                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功",
                    Data = dic

                });
            }
        }
        /// <summary>
        ///千分位转数字
        /// </summary>
        /// <param name="thousandthStr"></param>
        /// <returns></returns>
        public decimal ParseThousandthString(string thousandthStr)
        {
            decimal _value = -1;
            if (!string.IsNullOrEmpty(thousandthStr))
            {
                try
                {
                    _value = decimal.Parse(thousandthStr, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                }
                catch (Exception ex)
                {
                    _value = -1;
                    // Debug.WriteLine(string.Format("将千分位字符串{0}转换成数字异常，原因:{0}", thousandthStr, ex.Message));
                }
            }
            return _value;
        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("修改付款合同", OptionLogTypeEnum.Update, "修改付款合同", true)]
        public IActionResult UpdateSave(ContractInfoDTO info)
        {
            var temphtzt = info.ContState ?? 0;
            info.AmountMoney = ParseThousandthString(info.ContAmThod);
            info.StampTax = ParseThousandthString(info.StampTaxThod);
            if (info.Id > 0)
            {
                var updateinfo = _IContractInfoService.Find(info.Id);
              //  info.Code = updateinfo.Code;
                var updatedata = _IMapper.Map(info, updateinfo);
                var chkres = CheckContSave(updatedata);
                if (Convert.ToInt32(chkres.RetValue) > 0)
                {
                    return GetResult(chkres);
                }
                else
                {

                    var hisContInfo = _IMapper.Map<ContractInfoHistory>(updatedata);
                    updatedata.ModifyUserId = this.SessionCurrUserId;
                    updatedata.ModifyDateTime = DateTime.Now;
                    updatedata.FinanceType = 1;//付款
                    updatedata.ContState = (byte)(temphtzt == 100 ? temphtzt : 0);
                    var dic= _IContractInfoService.UpdateSave(updatedata, hisContInfo, _IMapper);
                    return GetResult(new RequstResult
                    {
                        Code = 0,
                        Msg = "操作成功",
                        Data = dic

                    });
                }
            }
            return GetResult();
        }


        /// <summary>
        /// 修改页提交流程
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public IActionResult UpdateSave01(ContractInfoDTO info)
        {
            var temphtzt = info.ContState ?? 0;
            info.AmountMoney = ParseThousandthString(info.ContAmThod);
            info.StampTax = ParseThousandthString(info.StampTaxThod);
            if (info.Id > 0)
            {
                var updateinfo = _IContractInfoService.Find(info.Id);
                //  info.Code = updateinfo.Code;
                var updatedata = _IMapper.Map(info, updateinfo);
                var chkres = CheckContSave(updatedata);
                if (Convert.ToInt32(chkres.RetValue) > 0)
                {
                    return GetResult(chkres);
                }
                else
                {

                    var hisContInfo = _IMapper.Map<ContractInfoHistory>(updatedata);
                    updatedata.ModifyUserId = this.SessionCurrUserId;
                    updatedata.ModifyDateTime = DateTime.Now;
                    updatedata.FinanceType = 1;//付款
                    updatedata.ContState = (byte)(temphtzt == 100 ? temphtzt : 0);
                    var dic = _IContractInfoService.UpdateSave01(updatedata, hisContInfo, _IMapper);
                    return GetResult(new RequstResult
                    {
                        Code = 0,
                        Msg = "操作成功",
                        Data = dic

                    });
                }
            }
            return GetResult();



            //info.AmountMoney = ParseThousandthString(info.ContAmThod);
            //info.StampTax = ParseThousandthString(info.StampTaxThod);

            //if (info.Id > 0)
            //{
            //    var updateinfo = _IContractInfoService.Find(info.Id);
            //    //info.Code = updateinfo.Code;
            //    var updatedata = _IMapper.Map(info, updateinfo);
            //    var chkres = CheckContSave(updatedata);
            //    if (Convert.ToInt32(chkres.RetValue) > 0)
            //    {
            //        return GetResult(chkres);
            //    }
            //    else
            //    {
            //        var hisContInfo = _IMapper.Map<ContractInfoHistory>(updatedata);
            //        updatedata.CountField = info.CustField;
            //        updatedata.ModifyUserId = this.SessionCurrUserId;
            //        updatedata.ModifyDateTime = DateTime.Now;
            //        updatedata.FinanceType = 1;//付款
            //        var dic = _IContractInfoService.UpdateSave01(updatedata, hisContInfo);
            //        foreach (var item in dic)
            //        {
            //            _IContractInfoService.CyzdUpd(item.Id, info.CustField);
            //            var rt = item.Id;
            //        }
            //        return GetResult(new RequstResult
            //        {
            //            Code = 0,
            //            Msg = "操作成功",
            //            Data = dic

            //        });
            //    }
            //}
          //  return GetResult();
        }
        /// <summary>
        /// 合同变更
        /// </summary>
        /// <param name="info">变更对象</param>
        /// <returns></returns>
        [NfCustomActionFilter("变更付款合同", OptionLogTypeEnum.Change, "变更付款合同", true)]
        public IActionResult ChangeSave(ContractInfoDTO info)
        {
            info.AmountMoney = ParseThousandthString(info.ContAmThod);
            info.StampTax = ParseThousandthString(info.StampTaxThod);
            if (info.Id > 0)
            {
                var updateinfo = _IContractInfoService.Find(info.Id);
                info.Code = updateinfo.Code;
                var temptimes = updateinfo.ModificationTimes ?? 0;
                //var changetimes = (updateinfo.ModificationTimes ?? 0) + 1;
                var updatedata = _IMapper.Map(info, updateinfo);
                var chkres = CheckContSave(updatedata);
                if (Convert.ToInt32(chkres.RetValue) > 0)
                {
                    return GetResult(chkres);
                }
                else
                {
                    if (updateinfo.ContState == 1)
                    { //变更

                        var hisContInfo = _IMapper.Map<ContractInfoHistory>(updatedata);
                        updatedata.ModifyUserId = this.SessionCurrUserId;
                        updatedata.ModifyDateTime = DateTime.Now;
                        updatedata.ModificationTimes = temptimes + 1;//变更次数
                        updatedata.FinanceType = 1;//付款
                        _IContractInfoService.ChangeSave(updatedata, hisContInfo, _IMapper);
                    }
                    else
                    {//变更后修改
                        var hisContInfo = _IMapper.Map<ContractInfoHistory>(updatedata);
                        updatedata.ModifyUserId = this.SessionCurrUserId;
                        updatedata.ModifyDateTime = DateTime.Now;
                        updatedata.FinanceType = 1;//付款
                        updatedata.ModificationTimes = temptimes;//保持原有变更次数不变
                        _IContractInfoService.UpdateSave(updatedata, hisContInfo, _IMapper);

                    }
                }
            }
            return GetResult();

        }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContractInfo, bool>> GetQueryExpression(PageInfo<ContractInfo> pageInfo, string keyWord,int? search)
        {
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            var predicateOr = PredicateBuilder.False<ContractInfo>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0&&a.FinanceType==1&&a.ContState!=100);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractListPermissionExpression("querypaycontlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.DisplyName.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            if ((search ?? 0) > 0)
            {

                predicateAnd = predicateAnd.And(_IRemindService.GetContractExpression("到期付款合同"));

            }

            return predicateAnd;
        }
        /// <summary>
        /// 列表-
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<ContractInfo>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search));
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryContract(pageParam, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryContract(pageParam, _IDataDictionaryService));
            }
            Expression<Func<ContractInfo, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Name":
                    orderbyLambda = a => a.Name;

                    break;
                case "Code":
                    orderbyLambda = a => a.Code;
                    break;
                case "ContAmThon"://合同金额
                    orderbyLambda = a => a.AmountMoney;
                    break;
                case "CreateDateTime"://建立时间
                    orderbyLambda = a => a.CreateDateTime;
                    break;
                case "SngDate"://签订时间
                    orderbyLambda = a => a.SngnDateTime;
                    break;
                case "EfDate"://生效时间
                    orderbyLambda = a => a.EffectiveDateTime;
                    break;
                case "PlanDate"://计划完成时间
                    orderbyLambda = a => a.PlanCompleteDateTime;
                    break;
                case "ActDate"://实际完成时间
                    orderbyLambda = a => a.ActualCompleteDateTime;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IContractInfoService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除付款合同", OptionLogTypeEnum.Del, "删除付款合同", false)]
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var permiision = _ISysPermissionModelService.GetContractDeletePermission("delpaycont", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };
            var usname = this.SessionCurrUserId;
            if (usname == -10000)
            {
                _IContractInfoService.Delete(Ids, 1);
                resinfo = new RequstResult()
                {
                    RetValue =2,
                Msg = "超级管理员已删除合同及和合同相关的资金，附件，发票等信息",
                   
                };
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
                    _IContractInfoService.Delete(Ids, 0);
                }
            }
            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _IContractInfoService.ShowView(Id);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info


            });
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id">修改对象ID</param>
        /// <param name="fieldName">修改字段名称</param>
        /// <param name="fieldVal">修改值，如果不是String后台人为判断</param>
        /// <returns></returns>
        public IActionResult UpdateField(int Id, string fieldName, string fieldVal)
        {
            var res = _IContractInfoService.UpdateField(new UpdateFieldInfo()
            {
                Id = Id,
                FieldName = fieldName,
                FieldValue = fieldVal


            });
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
        /// 修改多字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateMoreField(IList<UpdateFieldInfo> fields)
        {
            var res = _IContractInfoService.UpdateField(fields);
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
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {

            var pageInfo = new NoPageInfo<ContractInfo>();
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord, exportRequestInfo.search));
            PageparamInfo pageParam = new PageparamInfo();
            pageParam.keyWord = exportRequestInfo.KeyWord;
            pageParam.jsonStr = exportRequestInfo.jsonStr;
            if (exportRequestInfo.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
            }
            else
            {//所有行
                if (!string.IsNullOrEmpty(pageParam.jsonStr))
                {//高级查询
                    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryContract(pageParam, _IUserInforService));
                }
            }
            var layPage = _IContractInfoService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "付款合同", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }

        /// <summary>
        /// 选择列表
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetSelectList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<ContractInfo>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search));
            var layPage = _IContractInfoService.GetSelectList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 根据合同ID查询实际资金
        /// </summary>
        /// <returns></returns>
        public IActionResult GetActListByContId(int contId)
        {
            var pageInfo = new NoPageInfo<ContActualFinance>();
            var predicateAnd = PredicateBuilder.True<ContActualFinance>();
            predicateAnd = predicateAnd.And(a => a.ContId == contId && a.IsDelete == 0);
            var layPage = _IContActualFinanceService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 根据合同ID查询发票
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInvoiceListByContId(int contId)
        {
            var pageInfo = new NoPageInfo<ContInvoice>();
            var predicateAnd = PredicateBuilder.True<ContInvoice>();
            predicateAnd = predicateAnd.And(a => a.ContId == contId && a.IsDelete == 0);
            var layPage = _IContInvoiceService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 资金统计
        /// </summary>
        public IActionResult ContractStatic(int Id)
        {

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContractInfoService.GetContractStatic(Id)


            });
        }

    }
}