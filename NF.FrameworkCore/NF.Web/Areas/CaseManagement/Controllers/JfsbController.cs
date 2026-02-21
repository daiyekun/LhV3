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
    public class JfsbController : NfBaseController
    {
        private IDisputeManagerService _IDisputeManagerService;

        /// <summary>
        /// 合同操作
        /// </summary>
        private IContractInfoService _IContractInfoService;
        private IMapper _IMapper;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
 
     
     

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
    
        public JfsbController(IContractInfoService IContractInfoService, IMapper IMapper,
            ISysPermissionModelService ISysPermissionModelService
            , IRemindService IRemindService
            , IDisputeManagerService IDisputeManagerService
            , IUserInforService IUserInforService
            , INoHipler INoHipler
            , IDataDictionaryService IDataDictionaryService)
        {
            _IDisputeManagerService = IDisputeManagerService;
            _IContractInfoService = IContractInfoService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IRemindService = IRemindService;
            _IUserInforService = IUserInforService;
            _INoHipler = INoHipler;
            _IDataDictionaryService = IDataDictionaryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Build(int id)
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
        public IActionResult Save(jFsbDTO info)
        {
            var saveInfo = _IMapper.Map<DisputeManager>(info);
            saveInfo.CreateUserId = this.SessionCurrUserId; //  获取当前用户id
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.IsDelete = 0;
            saveInfo.Dstate = 0; //  获取当前用户id
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.ModifyUserId = this.SessionCurrUserId;

          var dcs= _IDisputeManagerService.AddSave(saveInfo);
          
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
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var de = _ISysPermissionModelService.GetJfsbListPermissionExpression("Jfsbllist", this.SessionCurrUserId, this.SessionCurrUserDeptId);

            var pageInfo = new PageInfo<DisputeManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<DisputeManager>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search));
            if (!string.IsNullOrEmpty(pageParam.jsonStr))
            {//高级查询
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryAjgl(pageParam, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
               predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryAjgl(pageParam, _IDataDictionaryService));
            }
            Expression<Func<DisputeManager, object>> orderbyLambda = null;
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
            //var s = "AddJfsb";
            //var er = _ISysPermissionModelService.GetJfsbAddPermission(s, 1);
          
            //LayPageInfo<jFsbDTO> sd = new LayPageInfo<jFsbDTO>();
            //if (er)
            //{
                var layPage = _IDisputeManagerService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
                return new CustomResultJson(layPage);
            //}
            //else
            //{
            //    return new CustomResultJson(sd);
            //}




        }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<DisputeManager, bool>> GetQueryExpression(PageInfo<DisputeManager> pageInfo, string keyWord, int? search)
        {
            var predicateAnd = PredicateBuilder.True<DisputeManager>();
            var predicateOr = PredicateBuilder.False<DisputeManager>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 );
            //GetJfsbListPermissionExpression
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetJfsbListPermissionExpression("Jfsbllist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
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
            var info = _IDisputeManagerService.ShowView(Id);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info
            });
        }

        public IActionResult UpdateSave(jFsbDTO info)
        {

           
            if (info.Id > 0)
            {
                var updateinfo = _IDisputeManagerService.Find(info.Id);
             //   info.Code = updateinfo.Code;
                var updatedata = _IMapper.Map(info, updateinfo);
          

                  
                    
                    updatedata.ModifyUserId = this.SessionCurrUserId;
                    updatedata.ModifyDateTime = DateTime.Now;
                    var dic = _IDisputeManagerService.UpdateSave(updatedata);

                  
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
            var permiision = _ISysPermissionModelService.GetContractDeletePermission("delcollcont", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };


            _IDisputeManagerService.Delete(Ids, 0);
                
            
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
            var permiision = _ISysPermissionModelService.GetJfsbDeletePermission("JfsbDelete", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
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
                    _IDisputeManagerService.Delete(Ids, 0);
                }
            
            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {

            var pageInfo = new NoPageInfo<DisputeManager>();
            var predicateAnd = PredicateBuilder.True<DisputeManager>();
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
                    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryAjgl(pageParam, _IUserInforService));
                }
            }
            var layPage = _IDisputeManagerService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "纠纷上报", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
    }
}
