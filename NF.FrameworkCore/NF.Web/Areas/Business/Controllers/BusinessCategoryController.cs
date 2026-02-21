using System;
using System.Collections.Generic;
using System.Linq;
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

namespace NF.Web.Areas.Business.Controllers
{
    /// <summary>
    /// 业务品类管理
    /// </summary>
    [Area("Business")]
    [Route("Business/[controller]/[action]")]
    public class BusinessCategoryController : NfBaseController
    {
        /// <summary>
        /// 业务品类
        /// </summary>
        private IBusinessCategoryService _IBusinessCategoryService;
        /// <summary>
        /// 映射
        /// </summary>
        private IMapper _IMapper;

        public BusinessCategoryController(IBusinessCategoryService IBusinessCategoryService
            , IMapper IMapper)
        {
            _IBusinessCategoryService = IBusinessCategoryService;
            _IMapper = IMapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取业务品类树
        /// </summary>
        /// <param name="tempHistId">合同模板查看页面需要</param>
        /// <returns></returns>
        public IActionResult GetBcCateTreeData(int tempHistId=0)
        {
               
            
                RequstResult requestRel = new RequstResult
                {
                    Code = 0,
                    Data = _IBusinessCategoryService.GetLayUITreeData()
                };
                Func<string, string> func = a => a.Replace("Checked", "checked");
                return new CustomResultJson(requestRel, func);
            
            
        }
        /// <summary>
        /// 新增业务品类
        /// </summary>
        /// <returns></returns>
        public IActionResult CateSave(BusinessCategoryDTO business)
        {
            var saveInfo = _IMapper.Map<BusinessCategory>(business);
            if (business.Id > 0)
            {
                saveInfo.IsDelete = 0;
                _IBusinessCategoryService.Update(saveInfo);
            }
            else { 
            
            saveInfo.IsDelete = 0;
            _IBusinessCategoryService.AddSave(saveInfo);
            }
            RedisHelper.KeyDelete("NF-BcCateGoryListAll");
            var requstResult = new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,
                RetValue = 0,
            };

            return new CustomResultJson(requstResult);
            
        }

       
        /// <summary>
        /// 根据ID查询当前对象
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInfoById(int Id)
        {
            var info = _IBusinessCategoryService.GetTreeDataById(Id);
            var requstResult = new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = 0,
                Data= info
            };

            return new CustomResultJson(requstResult);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">当前节点ID</param>
        /// <returns></returns>
        public IActionResult Delete(int Id)
        {
            _IBusinessCategoryService.DeleteInfo(Id);
            var requstResult = new RequstResult()
            {
                Msg = "删除成功",
                Code = 0,
                RetValue = 0
               
            };

            return new CustomResultJson(requstResult);
        }
        /// <summary>
        /// 获取类别选择树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCateTree()
        {

            return new CustomResultJson(_IBusinessCategoryService.GetTreeselect(), (v) => { return v.Replace("Checked", "checked"); });

        }
        /// <summary>
        /// 类别树
        /// </summary>
        /// <returns></returns>
        public IActionResult CateTree()
        {
            return View();
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
                RetValue = _IBusinessCategoryService.CheckInputValExist(fieldInfo)

            });
        }

    }
}