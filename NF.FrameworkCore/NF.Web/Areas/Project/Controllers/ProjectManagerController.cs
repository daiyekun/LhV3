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
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Project.Controllers
{
    [Area("Project")]
    [Route("Project/[controller]/[action]")]
    public class ProjectManagerController : NfBaseController
    {
        private IProjectManagerService _IProjectManagerService;
        private IMapper _IMapper;
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        /// <summary>
        /// 自动生成编号
        /// </summary>
        private INoHipler _INoHipler;
        public ProjectManagerController(IProjectManagerService IProjectManagerService, IMapper IMapper, 
            ISysPermissionModelService ISysPermissionModelService
            , IUserInforService IUserInforService
            , INoHipler INoHipler)
        {
            _IProjectManagerService = IProjectManagerService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IUserInforService = IUserInforService;
            _INoHipler = INoHipler;
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
            _IProjectManagerService.ClearJunkItemData(this.SessionCurrUserId);
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
        /// <summary>
        /// 判断字符值是否存在
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public IActionResult CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = _IProjectManagerService.CheckInputValExist(fieldInfo)

            });
        }

        [NfCustomActionFilter("新增项目", OptionLogTypeEnum.Add, "新增项目", true)]
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult Save(ProjectManagerViewDTO info)
        {

            info.BugetCollectAmountMoney = ParseThousandthString(info.BugetCollectAmountMoneyThod);
            info.BudgetPayAmountMoney = ParseThousandthString(info.BudgetPayAmountMoneyThod);
            var saveInfo = _IMapper.Map<Model.Models.ProjectManager>(info);
          
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.Pstate = 0;
            saveInfo.IsDelete = 0;
            saveInfo.ProjectSource = info.ProjectSource;
            if (LhLicense.ProjCodeZd == 1) { 
              saveInfo.Code = _INoHipler.ProjectNo();
            }
            _IProjectManagerService.Add(saveInfo);
            //修改标签信息
            _IProjectManagerService.UpdateItems(saveInfo.Id, this.SessionCurrUserId);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        //千分位转数字
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
        [NfCustomActionFilter("修改项目", OptionLogTypeEnum.Update, "修改项目", true)]
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult UpdateSave(ProjectManagerViewDTO info)
        {
            info.BugetCollectAmountMoney = ParseThousandthString(info.BugetCollectAmountMoneyThod);
            info.BudgetPayAmountMoney = ParseThousandthString(info.BudgetPayAmountMoneyThod);
            if (info.Id > 0)
            {
                var updateinfo = _IProjectManagerService.Find(info.Id);
                var updatedata = _IMapper.Map(info, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                updatedata.ProjectSource = info.ProjectSource;
                _IProjectManagerService.Update(updatedata);
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
        private Expression<Func<Model.Models.ProjectManager, bool>> GetQueryExpression(PageInfo<Model.Models.ProjectManager> pageInfo, string keyWord)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
            var predicateOr = PredicateBuilder.False<Model.Models.ProjectManager>();
            predicateAnd = predicateAnd.And(a =>  a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetProjectListPermissionExpression("queryprojectlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
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
        /// 列表-
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Model.Models.ProjectManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord));
            if (pageParam.selitem)
            {//选择框
                predicateAnd = predicateAnd.And(a => a.Pstate == (int)ProjStateEnum.Execution);
            }
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryProject(pageParam, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryProjectManager(pageParam, _IUserInforService, _IProjectManagerService));
            }
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
                case "BugetCollectAmountMoneyThod"://计划收款
                    orderbyLambda = a => a.BugetCollectAmountMoney;
                    break;
                case "BudgetPayAmountMoneyThod"://计划付款
                    orderbyLambda = a => a.BudgetPayAmountMoney;
                    break;
                case "PlanBeginDateTime"://计划开始时间
                    orderbyLambda = a => a.PlanBeginDateTime;
                    break;
                case "PlanCompleteDateTime"://计划结束时间
                    orderbyLambda = a => a.PlanCompleteDateTime;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IProjectManagerService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }
        public int Xmlb(string name) {


            return 0;
        }
        [NfCustomActionFilter("删除项目", OptionLogTypeEnum.Del, "删除项目", false)]
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
                DELETElist ht = _IProjectManagerService.GetIsHtlist(Ids);
                DELETElist zb =  _IProjectManagerService.GetIsHt(Ids);
                if (zb.Num==0 && ht.Num==0)
                {
                   
                        _IProjectManagerService.Delete(Ids);
                   
                }
                else
                {
                    if (zb.Num>0)
                    {
                        resinfo.RetValue = 1;
                        resinfo.Msg = "当前项目下还存在对应的"+ zb.DateName + "。请请删除后在尝试删除项目";
                        resinfo.Data = Ids;
                    }
                    else if (ht.Num>0)
                    {
                        resinfo.RetValue = 1;
                        resinfo.Msg = "当前项目下还存在对应的" + ht.DateName + "。请请删除后在尝试删除项目";
                        resinfo.Data = Ids;
                    }
                    //resinfo.RetValue = 1;
                    //resinfo.Msg = "当前项目下还存在对应的"+ ht.DateName+"或"+ zb.DateName+ "。请请删除后在尝试删除项目";
                    //resinfo.Data = Ids;
                }
            }
            else
            {
                var permiision = _ISysPermissionModelService.GetProjectDeletePermission("deleteproject", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
                if (permiision.Code != 0)
                {
                    resinfo.RetValue = permiision.Code;
                    resinfo.Msg = permiision.GetOptionMsg(permiision.Code);
                    resinfo.Data = permiision.noteAllow;
                }
                else
                {
                    _IProjectManagerService.Delete(Ids);
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
            var info = _IProjectManagerService.ShowView(Id);
          
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
            var res = _IProjectManagerService.UpdateField(new UpdateFieldInfo()
            {
                Id = Id,
                FieldName = fieldName,
                FieldValue = fieldVal


            });
            RequstResult reqInfo = reqInfo = new RequstResult()
            {
                Msg = "修改成功",
                Code = 0,


            };
            if (res <=0)
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
            var res = _IProjectManagerService.UpdateField(fields);
            RequstResult reqInfo = reqInfo = new RequstResult()
            {
                Msg = "修改成功",
                Code = 0,


            };
            if (res <= 0)
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

            var pageInfo = new NoPageInfo<Model.Models.ProjectManager>();
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
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
                    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryProject(pageParam, _IUserInforService));
                }
            }
            var layPage = _IProjectManagerService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "项目", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }

        public IActionResult GetList1(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Model.Models.ProjectManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord));
            if (pageParam.selitem)
            {//选择框
                predicateAnd = predicateAnd.And(a => a.Pstate == (int)ProjStateEnum.Execution);
            }
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryProject(pageParam, _IUserInforService));
            }
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
                case "BugetCollectAmountMoneyThod"://计划收款
                    orderbyLambda = a => a.BugetCollectAmountMoney;
                    break;
                case "BudgetPayAmountMoneyThod"://计划付款
                    orderbyLambda = a => a.BudgetPayAmountMoney;
                    break;
                case "PlanBeginDateTime"://计划开始时间
                    orderbyLambda = a => a.PlanBeginDateTime;
                    break;
                case "PlanCompleteDateTime"://计划结束时间
                    orderbyLambda = a => a.PlanCompleteDateTime;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IProjectManagerService.GetList1(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }

        public IActionResult GetList2(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Model.Models.ProjectManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord));
            if (pageParam.selitem)
            {//选择框
                predicateAnd = predicateAnd.And(a => a.Pstate == (int)ProjStateEnum.Execution);
            }
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryProject(pageParam, _IUserInforService));
            }
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
                case "BugetCollectAmountMoneyThod"://计划收款
                    orderbyLambda = a => a.BugetCollectAmountMoney;
                    break;
                case "BudgetPayAmountMoneyThod"://计划付款
                    orderbyLambda = a => a.BudgetPayAmountMoney;
                    break;
                case "PlanBeginDateTime"://计划开始时间
                    orderbyLambda = a => a.PlanBeginDateTime;
                    break;
                case "PlanCompleteDateTime"://计划结束时间
                    orderbyLambda = a => a.PlanCompleteDateTime;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IProjectManagerService.GetList2(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }

        public IActionResult GetList3(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Model.Models.ProjectManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.ProjectManager>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord));
            if (pageParam.selitem)
            {//选择框
                predicateAnd = predicateAnd.And(a => a.Pstate == (int)ProjStateEnum.Execution);
            }
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryProject(pageParam, _IUserInforService));
            }

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
                case "BugetCollectAmountMoneyThod"://计划收款
                    orderbyLambda = a => a.BugetCollectAmountMoney;
                    break;
                case "BudgetPayAmountMoneyThod"://计划付款
                    orderbyLambda = a => a.BudgetPayAmountMoney;
                    break;
                case "PlanBeginDateTime"://计划开始时间
                    orderbyLambda = a => a.PlanBeginDateTime;
                    break;
                case "PlanCompleteDateTime"://计划结束时间
                    orderbyLambda = a => a.PlanCompleteDateTime;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IProjectManagerService.GetList3(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }

        /// <summary>
        /// 获取资金统计
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFundStatistics(int projId)
        {

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,
                Data = _IProjectManagerService.GetFundStatistics(projId)


            });
        }

        /// <summary>
        /// 根据项目查询相关合同
        /// </summary>
        /// <returns></returns>
        public IActionResult GetContsByProjId(int projId,int fincType)
        {
            var pageInfo = new NoPageInfo<ContractInfo>();
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            predicateAnd = predicateAnd.And(a => a.ProjectId == projId && a.IsDelete == 0&&a.FinanceType== fincType);
            predicateAnd = predicateAnd.And(a=>a.ContState==(int)ContractState.Execution
            || a.ContState == (int)ContractState.Terminated|| a.ContState == (int)ContractState.Completed);
            var layPage = _IProjectManagerService.GetContsByProjId(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }

    }
}