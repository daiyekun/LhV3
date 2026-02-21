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
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;

namespace NF.Web.Areas.Business.Controllers
{
    /// <summary>
    /// 单品管理
    /// </summary>
    [Area("Business")]
    [Route("Business/[controller]/[action]")]
    public class BcInstanceController : NfBaseController
    {
        /// <summary>
        /// 单品管理
        /// </summary>
        private IBcInstanceService _IBcInstanceService;
        /// <summary>
        /// 对象映射
        /// </summary>
        private IMapper _IMapper;

        public BcInstanceController(IBcInstanceService IBcInstanceService
            , IMapper IMapper)
        {
            _IBcInstanceService = IBcInstanceService;
            _IMapper = IMapper;

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
        /// 新建页
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            _IBcInstanceService.ClearJunkItemData(this.SessionCurrUserId);
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
        /// 单品列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam,string cateIds)
        {
            var pageInfo = new PageInfo<BcInstance>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<BcInstance>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo,pageParam.keyWord, cateIds)); 
            Expression<Func<BcInstance, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Name":
                    orderbyLambda = a => a.Name;
                    break;
                case "Code":
                    orderbyLambda = a => a.Code;
                    break;
                case "CreateDateTime"://建立时间
                    orderbyLambda = a => a.CreateDateTime;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IBcInstanceService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 校验判断编号和名称等字段是否唯一
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public IActionResult CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = _IBcInstanceService.CheckInputValExist(fieldInfo)

            });
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult Save(BcInstanceDTO info)
        {

            BcInstance saveInfo = null;
           
                if (info.Id<=0)
                {
                     saveInfo = _IMapper.Map<BcInstance>(info);
                    saveInfo.CreateUserId = this.SessionCurrUserId;
                }
                else
                {
                    var updateinfo = _IBcInstanceService.Find(info.Id);
                    saveInfo = _IMapper.Map(info, updateinfo);
                }
               
                //saveInfo.CreateDateTime = DateTime.Now;
                saveInfo.ModifyDateTime = DateTime.Now;
                
                saveInfo.ModifyUserId = this.SessionCurrUserId;
                saveInfo.IsDelete = 0;
                _IBcInstanceService.Save(saveInfo);

                return new CustomResultJson(new RequstResult()
                {
                    Msg = "保存成功",
                    Code = 0,


                });
            
            
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            //var listIds = StringHelper.String2ArrayInt(Ids);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,


            };
            _IBcInstanceService.UpdateIsDel(Ids);
            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _IBcInstanceService.ShowView(Id);
            
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info


            });
        }
        





        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo,string cateIds)
        {

            
            
                var pageInfo = new NoPageInfo<Model.Models.BcInstance>();
                var predicateAnd = PredicateBuilder.True<Model.Models.BcInstance>();
                predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord, cateIds));
                if (exportRequestInfo.SelRow)
                {//选择行
                    predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
                }
                var layPage = _IBcInstanceService.GetList(pageInfo, predicateAnd, a => a.Id, true);
                var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "单品", layPage.data);
                return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
            
            

        }

        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <param name="cateIds">业务品类Ids：1，2，3</param>
        /// <returns></returns>
        private Expression<Func<Model.Models.BcInstance, bool>> GetQueryExpression(PageInfo<Model.Models.BcInstance> pageInfo, string keyWord,string cateIds)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.BcInstance>();
            var predicateOr = PredicateBuilder.False<Model.Models.BcInstance>();
            predicateAnd = predicateAnd.And(a =>a.IsDelete == 0);
           
            if (!string.IsNullOrEmpty(keyWord) && keyWord.ToLower() != "undefined")
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.CreateUser!=null&& a.CreateUser.DisplyName.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            if (!string.IsNullOrEmpty(cateIds))
            {
               var arrIds= StringHelper.String2ArrayInt(cateIds);
                predicateAnd = predicateAnd.And(a => arrIds.Contains(a.LbId??0));
            }

            return predicateAnd;
        }
       

    }
}