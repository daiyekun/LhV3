using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.AutoMapper;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.System.Controllers
{
    /// <summary>
    /// 合同模板
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class ContTxtTempController : NfBaseController
    {
        /// <summary>
        /// 合同文本模板
        /// </summary>
        private IContTxtTemplateService _IContTxtTemplateService;
        /// <summary>
        /// 映射
        /// </summary>
        private IMapper _IMapper;
        private IContTxtTemplateHistService _IContTxtTemplateHistService;

        public ContTxtTempController(IContTxtTemplateService IContTxtTemplateService
            , IMapper IMapper
            , IContTxtTemplateHistService IContTxtTemplateHistService)
        {
            _IContTxtTemplateService = IContTxtTemplateService;
            _IMapper = IMapper;
            _IContTxtTemplateHistService = IContTxtTemplateHistService;
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新建模板
        /// </summary>
        /// <returns></returns>
        public IActionResult Build(int? Id)
        {
            if ((Id??0) <=0)
            {
                var userId = HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;
                string sqlstr = $"delete ContTxtTempAndSubField where TempHistId={-userId}";
                _IContTxtTemplateService.ExecuteSqlCommand(sqlstr);
            }
            return View();
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<ContTxtTemplate>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContTxtTemplate>();
            var predicateOr = PredicateBuilder.False<ContTxtTemplate>();
            predicateAnd = predicateAnd.And(p=>p.IsDelete!=1);
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(p=>p.Name.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            Expression<Func<ContTxtTemplate, object>> orderbyLambda = a=>a.Id;
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
            var layPage = _IContTxtTemplateService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 判断字段是否唯一
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckInputValExist(ContTxtTemplateDTO txtTemplateDTO)
        {
           var res= _IContTxtTemplateService.CheckInputValExist(txtTemplateDTO);
            return new CustomResultJson(new RequstResult()
            {
                Msg = res.Msg,
                Code = 0,
                RetValue= res.Code
            });

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public IActionResult Save(ContTxtTemplateDTO txtTemplateDTO)
        {
            
                var saveInfo = _IMapper.Map<ContTxtTemplate>(txtTemplateDTO);
                saveInfo.WordEdit = 0;
                saveInfo.IsDelete = 0;
                saveInfo.ModifyUserId = this.SessionCurrUserId;
                saveInfo.ModifyDateTime = DateTime.Now;
               
                    saveInfo.CreateUserId = this.SessionCurrUserId;
                    saveInfo.CreateDateTime = DateTime.Now;
                    saveInfo.Vesion = 1;
                   var info= _IContTxtTemplateService.AddSave(saveInfo, _IMapper);

            var res = new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,
                Data = info

            };
                return new CustomResultJson(res);
            
            
        }
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateField(UpdateFieldInfo fieldInfo)
        {
            _IContTxtTemplateService.UpdateField(fieldInfo);
            var res = new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,
            };
            return new CustomResultJson(res);
        }

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

            _IContTxtTemplateService.Delete(Ids);
            

            return new CustomResultJson(resinfo);
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
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _IContTxtTemplateService.ShowView(Id);
          
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info


            });
        }
        /// <summary>
        ///模板历史
        /// </summary>
        /// <returns></returns>
        public IActionResult GetHistList(PageparamInfo pageParam,int tempId)
        {
            var pageInfo = new NoPageInfo<ContTxtTemplateHist>();
            var predicateAnd = PredicateBuilder.True<ContTxtTemplateHist>();
            var predicateOr = PredicateBuilder.False<ContTxtTemplateHist>();
            predicateAnd = predicateAnd.And(p => p.IsDelete != 1&&p.TempId== tempId);
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(p => p.Name.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            Expression<Func<ContTxtTemplateHist, object>> orderbyLambda = a => a.Id;
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
            var layPage = _IContTxtTemplateHistService.GetHistList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 修改历史字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateHistField(UpdateFieldInfo fieldInfo)
        {
            _IContTxtTemplateHistService.UpdateField(fieldInfo);
            var res = new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,
            };
            return new CustomResultJson(res);
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult DeleteHist(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };

            _IContTxtTemplateHistService.Delete(Ids);


            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 修改模板
        /// </summary>
        /// <param name="txtTemplateDTO"></param>
        /// <returns></returns>
        public IActionResult UpdateSave(ContTxtTemplateDTO txtTemplateDTO)
        {
           
           
                var findinfo = _IContTxtTemplateService.Find(txtTemplateDTO.Id);
                var saveInfo = _IMapper.Map(txtTemplateDTO, findinfo);
                saveInfo.WordEdit = 0;
                saveInfo.IsDelete = 0;
                saveInfo.ModifyUserId = this.SessionCurrUserId;
                saveInfo.ModifyDateTime = DateTime.Now;
                saveInfo.Vesion = findinfo.Vesion + 1;
                var info=_IContTxtTemplateService.UpdateSave(saveInfo,_IMapper);

                var res = new RequstResult()
                {
                    Msg = "保存成功",
                    Code = 0,
                    Data= info

                };
                return new CustomResultJson(res);
            
           

        }

        #region 上传水印
        /// <summary>
        /// 水印设置
        /// </summary>
        /// <returns></returns>
        public IActionResult SetWaterMark()
        {
            return View();
        }
        #endregion


    }
}