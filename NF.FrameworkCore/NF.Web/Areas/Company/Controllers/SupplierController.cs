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
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Company.Controllers
{
    [Area("Company")]
    [Route("Company/[controller]/[action]")]
    public class SupplierController : NfBaseController
    {
        private ICompanyService _ICompanyService;

        /// <summary>
        /// 映射 AutoMapper
        /// </summary>
        private IMapper _IMapper { get; set; }
        private IProjectManagerService _IProjectManagerService;
        private ICityService _ICityService;
        private IProvinceService _IProvinceService;
        private ICountryService _ICountryService;
        private ISysPermissionModelService _ISysPermissionModelService;
        private IContractInfoService _IContractInfoService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        private INoHipler _INoHipler;


        public SupplierController(ICompanyService ICompanyService, ICompContactService ICompContactService,
            IMapper IMapper, ICityService ICityService, ICountryService ICountryService,
            IProvinceService IProvinceService, ISysPermissionModelService ISysPermissionModelService
            , IContractInfoService IContractInfoService
            , IUserInforService IUserInforService
            , INoHipler INoHipler
            , IProjectManagerService IProjectManagerService)
        {
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
        /// 列表页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新建页
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            _ICompanyService.ClearJunkItemData(this.SessionCurrUserId);
            return View();
        }
        /// <summary>
        /// 查看页
        /// </summary>
        /// <returns></returns>
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            fieldInfo.ObjType = 1;//供应商
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = _ICompanyService.CheckInputValExist(fieldInfo)

            });
        }

        [NfCustomActionFilter("新增供应商", OptionLogTypeEnum.Add, "新增供应商", true)]
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult Save(CompanyDTO info)
        {


            var saveInfo = _IMapper.Map<Model.Models.Company>(info);
            if (LhCode.LhLicense.SupplierCoreStyle == 1)
            {
                saveInfo.Code = _INoHipler.CustrNo(saveInfo.CompClassId??0,1);
            }
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.Cstate = 0;
            saveInfo.IsDelete = 0;
            saveInfo.Ctype = 1;//供应商
            _ICompanyService.Add(saveInfo);
            //修改标签信息
            _ICompanyService.UpdateItems(saveInfo.Id, this.SessionCurrUserId);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        [NfCustomActionFilter("修改供应商", OptionLogTypeEnum.Update, "修改供应商", true)]
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult UpdateSave(CompanyDTO info)
        {

            if (info.Id > 0)
            {
                var updateinfo = _ICompanyService.Find(info.Id);
                var updatedata = _IMapper.Map(info, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _ICompanyService.Update(updatedata);
            }

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<Model.Models.Company, bool>> GetQueryExpression(PageInfo<Model.Models.Company> pageInfo, string keyWord)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            var predicateOr = PredicateBuilder.False<Model.Models.Company>();
            predicateAnd = predicateAnd.And(a => a.Ctype == 1 && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetCmpListPermissionExpression("querysupplierlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord)&& keyWord.ToLower() != "undefined")
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
        /// 列表-
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Model.Models.Company>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord));
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

        [NfCustomActionFilter("删除供应商", OptionLogTypeEnum.Del, "删除供应商", false)]
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };
            var usname = this.SessionCurrUserId;
            if (usname == -10000)
            {
                var ISdelete = _ICompanyService.GetIsHt(Ids);
                if (ISdelete == 0)
                {
                   
                        _ICompanyService.Delete(Ids);
                   
                }
                else
                {
                    resinfo.Msg = "选中的供应商还有合同存在，请删除对应的合同后在尝试删除";
                    resinfo.Data = Ids;
                    resinfo.RetValue = 1;
                }
          
            }
            else
            {
                var permiision = _ISysPermissionModelService.GetCmpDeletePermission("deletesupplier", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
                if (permiision.Code != 0)
                {
                    resinfo.RetValue = permiision.Code;
                    resinfo.Msg = permiision.GetOptionMsg(permiision.Code);
                    resinfo.Data = permiision.noteAllow;
                }
                else
                {
                    _ICompanyService.Delete(Ids);
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
            var info = _ICompanyService.ShowView(Id);
            //info.CountryName = _ICountryService.GetAll().Where(a => a.Id == info.CountryId).Any() ? _ICountryService.GetAll().Where(a => a.Id == info.CountryId).FirstOrDefault().Name : "";
            //info.ProvinceName = _IProvinceService.GetAll().Where(a => a.Id == info.CountryId).Any() ? _IProvinceService.GetAll().Where(a => a.Id == info.CountryId).FirstOrDefault().Name : "";
            //info.CityName = _ICityService.GetAll().Where(a => a.Id == info.CountryId).Any() ? _ICityService.GetAll().Where(a => a.Id == info.CountryId).FirstOrDefault().Name : "";
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
            var res = _ICompanyService.UpdateField(new UpdateFieldInfo()
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
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {

            var pageInfo = new NoPageInfo<Model.Models.Company>();
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            PageparamInfo pageParam = new PageparamInfo();
            pageParam.keyWord = exportRequestInfo.KeyWord;
            pageParam.jsonStr = exportRequestInfo.jsonStr;
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord));
            if (exportRequestInfo.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
            }
            else
            {//所有行
                if (!string.IsNullOrEmpty(pageParam.jsonStr))
                {//高级查询
                    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryCompany(pageParam, _IUserInforService));
                }
            }
            var layPage = _ICompanyService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "供应商", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }

        /// <summary>
        /// 根据合同对方查询合同列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetContsByComId(int companyId)
        {
            var pageInfo = new NoPageInfo<ContractInfo>();
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            predicateAnd = predicateAnd.And(a => a.CompId == companyId && a.IsDelete == 0);
            var layPage = _IContractInfoService.GetContsByCompId(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 获取资金统计
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFundStatistics(int compId)
        {

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,
                Data = _ICompanyService.GetFundStatistics(compId)


            });
        }
        /// <summary>
        /// 查询项目列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public IActionResult GetProjList(PageparamInfo pageParam)
        {
            var projIds = _IContractInfoService.GetQueryable(a => a.CompId == pageParam.otherwh &&
            (a.ContState == (int)ContractState.Execution
            || a.ContState == (int)ContractState.Terminated
            || a.ContState == (int)ContractState.Completed)).Select(a => a.ProjectId ?? 0).ToList();
            var pageInfo = new NoPageInfo<ProjectManager>();
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
            predicateAnd = predicateAnd.And(p => projIds.Any(t => p.Id == t));
            Expression<Func<Model.Models.ProjectManager, object>> orderbyLambda = null;
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
            var layPage = _ICompanyService.GetProjList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }

        /// <summary>
        /// 查询标的列表
        /// </summary>
        /// <param name="pageParam"></param>
        /// <returns></returns>
        public IActionResult GetSubmitList(PageparamInfo pageParam)
        {
            var contIds = _IContractInfoService.GetQueryable(a => a.CompId == pageParam.otherwh &&
            (a.ContState == (int)ContractState.Execution
            || a.ContState == (int)ContractState.Terminated
            || a.ContState == (int)ContractState.Completed)).Select(a => a.Id).ToList();
            var pageInfo = new PageInfo<Model.Models.ContSubjectMatter>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.ContSubjectMatter>();
            predicateAnd = predicateAnd.And(p => contIds.Any(t => p.ContId == t));
            Expression<Func<Model.Models.ContSubjectMatter, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Name":
                    orderbyLambda = a => a.Name;

                    break;

                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _ICompanyService.GetSubmitList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }


    }
}