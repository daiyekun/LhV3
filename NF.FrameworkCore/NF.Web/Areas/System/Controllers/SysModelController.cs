using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    /// 菜单管理
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class SysModelController : NfBaseController
    {
        public ISysModelService _ISysModelService;
        public IRoleModuleService _IRoleModuleService;
        public IUserModuleService _IUserModuleService;

        public SysModelController(ISysModelService ISysModelService, IRoleModuleService IRoleModuleService, IUserModuleService IUserModuleService)
        {
            _ISysModelService = ISysModelService;
            _IRoleModuleService = IRoleModuleService;
            _IUserModuleService = IUserModuleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Build()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(int? limit, int? page, string keyWord)
        {

            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<SysModel>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<SysModel>();
            var predicateOr = PredicateBuilder.False<SysModel>();
           
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据

            if (!string.IsNullOrEmpty(keyWord))
            {
                var listall=  _ISysModelService.GetListAll();
               var exist= listall.Where(a =>!string.IsNullOrEmpty(a.PName)&& a.PName.Contains(keyWord)).Any();
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                if (exist) {
                var listIds = listall.Where(a => a.PName.Contains(keyWord)).Select(a => a.Id).ToArray();
                 predicateOr = predicateOr.Or(a =>listIds.Contains(a.Id));
                }
               
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _ISysModelService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTree()
        {
            return new CustomResultJson(_ISysModelService.GetTree());
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSelectTree()
        {
            return new CustomResultJson(_ISysModelService.GetModelTreeSelect(), (v) => { return v.Replace("Checked", "checked"); });
        }
        /// <summary>
        /// 判断输入值是否存在
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckInputValExist(string fieldName, string inputVal, int? currId)
        {

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = _ISysModelService.CheckFieldValExist(fieldName, inputVal, currId)


            });
        }
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新增菜单", OptionLogTypeEnum.Add, "新增菜单",true)]
        public IActionResult AddSave(SysModel info)
        {
            var list = _ISysModelService.GetListAll();
            info.CreateUserId = base.SessionCurrUserId;
            GetPathInfo(info, list);
            _ISysModelService.SaveInfo(info);
            return ResultUtility.ResultMsg("保存成功");

        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">修改信息实体</param>
        /// <returns></returns>
        [NfCustomActionFilter("修改菜单", OptionLogTypeEnum.Update, "修改菜单",true)]
        public IActionResult UpdateSave(SysModel info)
        {
            var list = _ISysModelService.GetListAll();
            info.ModifyUserId = base.SessionCurrUserId;
            GetPathInfo(info, list);
            _ISysModelService.SaveInfo(info);
            return ResultUtility.ResultMsg("保存成功");

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
                Data = _ISysModelService.ShowView(Id)


            });

        }

        /// <summary>
        /// 生成树目录
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="list"></param>
        private static void GetPathInfo(SysModel Info, IList<SysModelDTO> list)
        {
            if (Info.Pid == -1|| Info.Pid==null)
            {
                Info.Leaf = 1;
                Info.Mpath = "0" + (list.Where(a => a.Pid == -1).Count() + 1);
            }
            else
            {
                var partInfo = list.Where(c => c.Id == Info.Pid).FirstOrDefault();
                if (partInfo != null)
                {

                    Info.Leaf = (partInfo.Leaf??0) + 1;
                    Info.Mpath = partInfo.Mpath + "/0" + (list.Where(a => a.Pid == partInfo.Id).Count() + 1);
                }


            }
        }
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改状态", OptionLogTypeEnum.Update, "修改状态",false)]
        public IActionResult UpdateField(int Id, string fieldName, string fieldValue)
        {
            _ISysModelService.UpdateField(new UpdateFieldInfo()
            {
                Id = Id,
                FieldName = fieldName,
                FieldValue = fieldValue


            });
            return ResultUtility.ResultMsg("修改成功");
        }
        /// <summary>
        /// 删除-软删除（修改IsDelete）
        /// </summary>
        /// <param name="Ids">修改集合</param>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _ISysModelService.Delete(Ids);
            return ResultUtility.ResultMsg();
        }
        /// <summary>
        /// 树形Grid
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexTree()
        {

            return View();
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeTable()
        {
            var list = _ISysModelService.GetListAll().Where(a=>a.IsDelete!=1).OrderBy(a=>a.Sort).ToList();
            var laypage = new LayPageInfo<SysModelDTO>()
            {
                code = 0,
                msg = "ok",
                data= list

            };
            return new CustomResultJson(laypage);
        }
        /// <summary>
        /// 选择页面菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectModel()
        {
            return View();

        }
        /// <summary>
        /// 获取选择数据Tree
        /// </summary>
        /// <param name="roleId">分配对象ID：角色、用户、组、岗位ID</param>
        /// <param name="setType">对象类别：角色、用户、组、岗位</param>
        /// <returns></returns>
        public IActionResult GetSelectTreeData(int? roleId,int? setType)
        {
           
            IList<int> modeIds = new List<int>();
            if (setType == 1) { //角色
                var predicateAnd = PredicateBuilder.True<RoleModule>();
                predicateAnd = predicateAnd.And(a => a.RoleId == roleId);
                modeIds = _IRoleModuleService.GetQueryable(predicateAnd).Select(a=>a.ModuleId).ToList();
            }else if (setType == 0)
            {//用户
                var predicateAnd = PredicateBuilder.True<UserModule>();
                predicateAnd = predicateAnd.And(a => a.UserId == roleId);
                modeIds = _IUserModuleService.GetQueryable(predicateAnd).Select(a => a.ModuleId).ToList();
            }

            var listTrees = _ISysModelService.GetXtTree(modeIds);
            return new CustomResultJson(listTrees, StringHelper.RepleaceStr);
        }

       






    }
}