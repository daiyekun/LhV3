using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.System.Controllers
{
    /// <summary>
    /// 功能点
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class SysFunctionController : NfBaseController
    { 
        /// <summary>
        /// 系统功能
        /// </summary>
        public ISysFunctionService _SysFunctionService;
        /// <summary>
        /// 系统模块
        /// </summary>
        public ISysModelService _ISysModelService;
        /// <summary>
        /// 组织机构
        /// </summary>
        public IDepartmentService _IDepartmentService;
        /// <summary>
        /// 角色权限
        /// </summary>
        public IRolePermissionService _IRolePermissionService;

        public SysFunctionController(ISysFunctionService SysFunctionService, ISysModelService ISysModelService, IRolePermissionService IRolePermissionService, IDepartmentService IDepartmentService)
        {
            _SysFunctionService = SysFunctionService;
            _ISysModelService = ISysModelService;
            _IRolePermissionService = IRolePermissionService;
            _IDepartmentService = IDepartmentService;
        }

        public IActionResult Index()
        {
            return View();
        }
        

        /// <summary>
        /// 角色权限分配
        /// </summary>
        /// <returns></returns>
        public IActionResult PermissonAllot()
        {
            return View();

        }
        /// <summary>
        /// 用户权限分配
        /// </summary>
        /// <returns></returns>
        public IActionResult UserPermissonAllot()
        {
            return View();

        }
        /// <summary>
        /// 获取功能点
        /// </summary>
        /// <param name="limit">每页显示条数</param>
        /// <param name="modeId">模块ID</param>
        /// <param name="page">页码</param>
        /// <param name="Id">角色、用户、岗位ID</param>
        /// <returns></returns>
        public IActionResult GetList(int? limit, int? page, int? modeId, int? Id)
        {
            var predicateAnd = PredicateBuilder.True<RolePermission>();
            predicateAnd = predicateAnd.And(a => a.RoleId == Id && a.ModeId == modeId);
            var listpession = _IRolePermissionService.GetQueryable(predicateAnd).ToList();
            var listdeptAll = _IDepartmentService.GetListAll();
            var list = SysFunctionHelper.GetListMFunction(modeId);
            foreach (var item in list)
            {
                if (listpession.Any(a => a.FuncId == item.Id && a.FuncType == 2))
                {//如果选择的是机构
                    var pession = listpession.Find(a => a.FuncId == item.Id && a.FuncType == 2);
                    if (pession != null)
                    {
                        var arrayids = StringHelper.String2ArrayInt(pession.DeptIds);
                        var funcDepts = listdeptAll.Where(a => arrayids.Contains(a.Id)).Select(a => a.Name).ToArray();
                        item.PermssionRang = string.Join(',', funcDepts);
                    }

                }

            }
            var layPage = new LayPageInfo<SysModelFunction>()
            {
                data = list,
                count = list.Count(),
                code = 0


            };
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 菜单树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMenuTree()
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data= _ISysModelService.GetTree()
        });
        }
        /// <summary>
        /// 菜单树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMenuTree2(int IsUser, int userIdorRoleId, bool fpQx)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _ISysModelService.GetTree(IsUser, userIdorRoleId, fpQx)
            });
        }
    }
}