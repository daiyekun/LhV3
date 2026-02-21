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
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.System.Controllers
{
    /// <summary>
    /// 印章管理
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class SealManagerController : NfBaseController
    {

        /// <summary>
        /// AutoMapper
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 印章管理
        /// </summary>
        private ISealManagerService _ISealManagerService;
        /// <summary>
        /// 合同
        /// </summary>
        private IContractInfoService _IContractInfoService;

        public SealManagerController(IMapper IMapper
            , ISealManagerService ISealManagerService
            , IContractInfoService IContractInfoService)
        {
            _IMapper = IMapper;
            _ISealManagerService = ISealManagerService;
            _IContractInfoService = IContractInfoService;
        }
        public IActionResult Build()
        {
            return View();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam,int mainDeptId)
        {
            var pageInfo = new PageInfo<SealManager>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<SealManager>();
            //var predicateOr = PredicateBuilder.False<SealManager>();
            predicateAnd = predicateAnd.And(a => a.IsDelete ==0&&a.MainDeptId== mainDeptId);//表示没有删除的数据
            var layPage = _ISealManagerService.GetList(pageInfo, predicateAnd,a=>a.Id,false);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 下拉选择印章名称
        /// </summary>
        /// <param name="contId">当前合同ID</param>
        /// <returns></returns>
        public IActionResult SelectSeal(int contId)
        {
            var pageInfo = new NoPageInfo<SealManager>();
            var predicateAnd = PredicateBuilder.True<SealManager>();
            var contInfo= _IContractInfoService.Find(contId);
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);//表示没有删除的数据
            predicateAnd = predicateAnd.And(a=>a.MainDeptId== contInfo.MainDeptId&&a.UserId==SessionCurrUserId);
            var layPage = _ISealManagerService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _ISealManagerService.ShowView(Id);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info


            });
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除印章", OptionLogTypeEnum.Del, "删除印章", false)]
        public IActionResult Delete(string Ids)
        {

            var permiision = _ISealManagerService.Delete(Ids);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,


            };
            

            return new CustomResultJson(resinfo);
        }
        [NfCustomActionFilter("新增印章", OptionLogTypeEnum.Add, "新增印章", true)]
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult Save(SealManagerDTO info)
        {


            var saveInfo = _IMapper.Map<SealManager>(info);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.EnabledDate = DateTime.Now;
            saveInfo.SealState = 1;//1：启用。0：禁用
            _ISealManagerService.Add(saveInfo);
           
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        [NfCustomActionFilter("修改印章", OptionLogTypeEnum.Update, "修改印章", true)]
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult UpdateSave(SealManagerDTO info)
        {

            if (info.Id > 0)
            {
                var updateinfo = _ISealManagerService.Find(info.Id);
                var updatedata = _IMapper.Map(info, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _ISealManagerService.Update(updatedata);
            }

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateField(int Id, string fieldName, string fieldValue)
        {
            _ISealManagerService.UpdateField(new UpdateFieldInfo()
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

    }
}