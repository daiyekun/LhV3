using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.System.Controllers
{

    /// <summary>
    /// 系统新增字段
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class SysFieldController : NfBaseController // Controller
    {
        private ISysFieldService _ISysFieldService;

        private IMapper _IMapper;
        public SysFieldController(

            IMapper IMapper,
           ISysFieldService ISysFieldService
            )
        {
           _ISysFieldService = ISysFieldService;

            _IMapper = IMapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        //nf-system-SysField-build
        public IActionResult Build() 
        { return View(); }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(int? limit, int? page, string keyWord, string filterSos)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<SysField>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<SysField>();
            var predicateOr = PredicateBuilder.False<SysField>();
            // predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据


            //if (!string.IsNullOrEmpty(filterSos))
            //{//基本筛选
            //    predicateAnd = predicateAnd.And(AdvQueryHelper.GetytAdvQueryUser(filterSos, _ISysFieldService));
            //}
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Lable.Contains(keyWord));

                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _ISysFieldService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);

        }
        [NfCustomActionFilter("系统新增字段", OptionLogTypeEnum.Add, "系统新增字段", true)]
        public IActionResult AddSave(SysField SysField)
        {

            _ISysFieldService.SaveInfo(SysField);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        [NfCustomActionFilter("修改系统用户", OptionLogTypeEnum.Update, "修改系统用户", true)]
        public IActionResult UpdateSave(SysField SysField)
        {
            _ISysFieldService.SaveInfo(SysField);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }


        /// <summary>
        /// 显示页面信息-主要用于修改和查看
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            if (Id == -100)
            {//自己修改基本信息
                Id = SessionCurrUserId;
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _ISysFieldService.ShowView(Id)


            });

        }

        public IActionResult Delete(string Ids)
        {
            _ISysFieldService.Delete(Ids);
          
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateField(int Id, string fieldName, string fieldValue)
        {
            _ISysFieldService.UpdateField(new UpdateFieldInfo()
            {
                Id = Id,
                FieldName = fieldName,
                FieldValue = fieldValue


            });
          
            return new CustomResultJson(new RequstResult()
            {
                Msg = "修改成功",
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
            var res = _ISysFieldService.UpdateDesc(Id, field, fdv);
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
    }
}
