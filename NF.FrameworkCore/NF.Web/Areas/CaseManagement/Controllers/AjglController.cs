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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NF.Web.Areas.CaseManagement.Controllers
{
    [Area("CaseManagement")]
    [Route("CaseManagement/[controller]/[action]")]
    public class AjglController : NfBaseController
    {
        private IDisputeManagerService _IDisputeManagerService;
        private ICompanyService _ICompanyService;
        private IDataDictionaryService _IDataDictionaryService;
        /// <summary>
        /// 映射 AutoMapper
        /// </summary>
        private IMapper _IMapper { get; set; }
        private IProjectManagerService _IProjectManagerService;
        private ICityService _ICityService;
        private IProvinceService _IProvinceService;
        private ICountryService _ICountryService;
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// 合同
        /// </summary>
        private IContractInfoService _IContractInfoService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        /// <summary>
        /// 编号自动生成
        /// </summary>
        private INoHipler _INoHipler;
        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;

        private ICaseManagerService _ICaseManagerService;

        public AjglController(ICompanyService ICompanyService, ICompContactService ICompContactService, IRemindService IRemindService,
            IMapper IMapper, ICityService ICityService, ICountryService ICountryService,
            IProvinceService IProvinceService, ISysPermissionModelService ISysPermissionModelService
            , IContractInfoService IContractInfoService
            , IUserInforService IUserInforService
            , INoHipler INoHipler
            ,IDataDictionaryService IDataDictionaryService
            , ICaseManagerService ICaseManagerService
           , IDisputeManagerService IDisputeManagerService
            , IProjectManagerService IProjectManagerService)
        {
            _IDataDictionaryService = IDataDictionaryService;
            _ICaseManagerService = ICaseManagerService;
            _IDisputeManagerService = IDisputeManagerService;
            _IRemindService = IRemindService;
            _ICompanyService = ICompanyService;
            _IMapper = IMapper;
            _ICityService = ICityService;
            _ICountryService = ICountryService;
            _IProvinceService = IProvinceService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContractInfoService = IContractInfoService;
            _IUserInforService = IUserInforService;
            _INoHipler = INoHipler;
            _IProjectManagerService = IProjectManagerService;

        }
        /// <summary>
        /// 客户与供应商
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {

            var pageInfo = new PageInfo<Model.Models.Company>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();

            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam));
            if (pageParam.selitem)
            {//选择框
                predicateAnd = predicateAnd.And(a => a.Cstate == (int)CompStateEnum.Audited);
            }
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryCompany(pageParam, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryCompany(pageParam, _IUserInforService, _IProjectManagerService));
            }
            Expression<Func<Model.Models.Company, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Name":
                    orderbyLambda = a => a.Name;

                    break;
                case "Code":
                    orderbyLambda = a => a.Code;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _ICompanyService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<Model.Models.Company, bool>> GetQueryExpression(PageInfo<Model.Models.Company> pageInfo, PageparamInfo pageParam)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            var predicateOr = PredicateBuilder.False<Model.Models.Company>();
            predicateAnd = predicateAnd.And(a => (a.Ctype == 0|| a.Ctype ==1) && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetCmpListPermissionExpression("querycustomerlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(pageParam.keyWord) && pageParam.keyWord.ToLower() != "undefined")
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(pageParam.keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(pageParam.keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.DisplyName.Contains(pageParam.keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.Name.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }


            return predicateAnd;
        }
        /// <summary>
        /// 选择收款合同与付款合同
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
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContractInfo, bool>> GetQueryExpression(PageInfo<ContractInfo> pageInfo, string keyWord, int? search)
        {
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            var predicateOr = PredicateBuilder.False<ContractInfo>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && (a.FinanceType == 1 || a.FinanceType == 0));
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractListPermissionExpression("querypaycontlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.DisplyName.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            
            return predicateAnd;
        }



        /// <summary>
        /// 选择案件明细
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetAjmx(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<DisputeManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<DisputeManager>();
            predicateAnd = predicateAnd.And(GetQueryExpressionAjmx(pageInfo, pageParam.keyWord, pageParam.search));
            var layPage = _IDisputeManagerService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);

        }

        /// <summary>
        /// 获取案件明细查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<DisputeManager, bool>> GetQueryExpressionAjmx(PageInfo<DisputeManager> pageInfo, string keyWord, int? search)
        {
            var predicateAnd = PredicateBuilder.True<DisputeManager>();
            var predicateOr = PredicateBuilder.False<DisputeManager>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);
           // predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractListPermissionExpression("querypaycontlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.DisplyName.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }

            return predicateAnd;
        }


        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Build(int id)
        {
            _ICaseManagerService.ClearJunkItemData(this.SessionCurrUserId);
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

        public IActionResult Save(AjjfDTO info)
        {
            var saveInfo = _IMapper.Map<CaseManager>(info);
            saveInfo.CreateUserId = this.SessionCurrUserId; //  获取当前用户id
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.IsDelete = 0;
            saveInfo.Dstate = 0; //  获取当前用户id
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.ModifyUserId = this.SessionCurrUserId;

            var dcs = _ICaseManagerService.AddSave(saveInfo);

            return GetResult(new RequstResult
            {
                Code = 0,
                Msg = "操作成功",
                // Data = dic

            });

        }
        /// <summary>
        /// 列表-
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList1(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<CaseManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<CaseManager>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search));
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
               predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryAjgls(pageParam, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryAjgls(pageParam, _IDataDictionaryService));
            }
            Expression<Func<CaseManager, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Name":
                    orderbyLambda = a => a.Name;

                    break;
                case "Code":
                    orderbyLambda = a => a.Code;
                    break;
                case "Amount"://合同金额
                    orderbyLambda = a => a.Amount;
                    break;

                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _ICaseManagerService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<CaseManager, bool>> GetQueryExpression(PageInfo<CaseManager> pageInfo, string keyWord, int? search)
        {
            var predicateAnd = PredicateBuilder.True<CaseManager>();
            var predicateOr = PredicateBuilder.False<CaseManager>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);

              predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetAjglListPermissionExpression("Ajgllist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.DisplyName.Contains(keyWord));
                //predicateOr = predicateOr.Or(a => a.PrincipalUser.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }



            return predicateAnd;
        }

        /// <summary>
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _ICaseManagerService.ShowView(Id);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info
            });
        }

        public IActionResult UpdateSave(AjjfDTO info)
        {


            if (info.Id > 0)
            {
                var updateinfo = _ICaseManagerService.Find(info.Id);
                //   info.Code = updateinfo.Code;
                var updatedata = _IMapper.Map(info, updateinfo);




                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                var dic = _ICaseManagerService.UpdateSave(updatedata);


                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功",
                    Data = dic

                });


            }
            return GetResult();

        }


        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>

        public IActionResult Delete1(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            //  var permiision = _ISysPermissionModelService.GetContractDeletePermission("delcollcont", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };


            _ICaseManagerService.Delete(Ids, 0);


            return new CustomResultJson(resinfo);
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除收款合同", OptionLogTypeEnum.Del, "删除收款合同", false)]
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var permiision = _ISysPermissionModelService.GetAjglDeletePermission("AjgDelete", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };

            var usname = this.SessionCurrUserId;

            if (permiision.Code != 0)
            {
                resinfo.RetValue = permiision.Code;
                resinfo.Msg = permiision.GetOptionMsg(permiision.Code);
                resinfo.Data = permiision.noteAllow;
            }
            else
            {
                _ICaseManagerService.Delete(Ids, 0);
            }

            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {

            var pageInfo = new NoPageInfo<CaseManager>();
            var predicateAnd = PredicateBuilder.True<CaseManager>();
            PageparamInfo pageParam = new PageparamInfo();
            pageParam.keyWord = exportRequestInfo.KeyWord;
            pageParam.jsonStr = exportRequestInfo.jsonStr;
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord, exportRequestInfo.search));
            if (exportRequestInfo.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
            }
            else
            {//所有行
                if (!string.IsNullOrEmpty(pageParam.jsonStr))
                {//高级查询
                 // predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryAjgl(pageParam, _IUserInforService));
                }
            }
            var layPage = _ICaseManagerService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "纠纷上报", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }


        /// <summary>
        /// 修改多字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateMoreField(IList<UpdateFieldInfo> fields)
        {
            var res = _ICaseManagerService.UpdateField(fields);
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

    }
}
