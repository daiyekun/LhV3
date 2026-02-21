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
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.System.Controllers
{
    /// <summary>
    /// 角色
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class RoleController : NfBaseController
    {
        /// <summary>
        /// 角色服务
        /// </summary>
        public IRoleService _IRoleService;
        /// <summary>
        /// 用户角色服务
        /// </summary>
        public IUserRoleService _IUserRoleService;
        /// <summary>
        /// 角色菜单分配服务
        /// </summary>
        public IRoleModuleService _IRoleModuleService;
        /// <summary>
        /// 角色功能权限分配
        /// </summary>
        public IRolePermissionService _IRolePermissionService;
        public RoleController(IRoleService IRoleService, IUserRoleService IUserRoleService, IRoleModuleService IRoleModuleService, IRolePermissionService IRolePermissionService) {
            _IRoleService = IRoleService;
            _IUserRoleService = IUserRoleService;
            _IRoleModuleService = IRoleModuleService;
            _IRolePermissionService = IRolePermissionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Build() {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(int? limit, int? page, string keyWord)
        {
          
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<Role>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<Role>();
            var predicateOr = PredicateBuilder.False<Role>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
               
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IRoleService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetListAll()
        {
            var layPage = _IRoleService.GetListAll();
            IList<RoleSimpDTO> listsimp = new List<RoleSimpDTO>();
            var listtmp = layPage.data.Where(a => a.IsDelete != 1 && a.Rstate == 1).ToList();
            foreach (var item in listtmp)
            {

                listsimp.Add(new RoleSimpDTO {
                    code = item.Id.ToString(),
                    value=item.Name
                });
            }

            return new CustomResultJson(new LayPageInfo<RoleSimpDTO> {
                code=0,
                data= listsimp
            });

        }

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改状态", OptionLogTypeEnum.Update, "修改状态",false)]
        public IActionResult UpdateField(int Id, string fieldName, string fieldValue)
        {
            _IRoleService.UpdateField(new UpdateFieldInfo()
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
        /// 删除-软删除（修改IsDelete）
        /// </summary>
        /// <param name="Ids">修改集合</param>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IRoleService.Delete(Ids);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
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
                Data = _IRoleService.ShowView(Id)


            });

        }
        /// <summary>
        /// 保存角色
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新增角色", OptionLogTypeEnum.Add, "新增角色",true)]
        public IActionResult AddSave(Role info)
        {
            info.CreateUserId = base.SessionCurrUserId;
            info.ModifyUserId = base.SessionCurrUserId;
            _IRoleService.SaveInfo(info);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">修改信息</param>
        /// <returns></returns>
        [NfCustomActionFilter("修改角色", OptionLogTypeEnum.Update, "修改角色",true)]
        public IActionResult UpdateSave(Role info)
        {

            info.ModifyUserId = base.SessionCurrUserId;
            _IRoleService.SaveInfo(info);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

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
                RetValue = _IRoleService.CheckFieldValExist(fieldName, inputVal, currId)


            });
        }

        #region 用户角色中间表
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetListUser(int? limit, int? page,int roleId)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<UserInfor>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<UserInfor>();
            var predicateOr = PredicateBuilder.False<UserInfor>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据
            
            var layPage = _IUserRoleService.GetListUser(pageInfo, predicateAnd,roleId);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 添加角色用户
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="userIds">用户ID集合</param>
        /// <returns></returns>
        [NfCustomActionFilter("角色添加用户列表", OptionLogTypeEnum.Add, "角色添加用户列表",false)]
        public IActionResult AddRoleUser(string userIds, int roleId)
        {
            var listuserIds = StringHelper.String2ArrayInt(userIds);
            IList<UserRole> userRoles = new List<UserRole>();
            foreach (var uId in listuserIds) {
                UserRole uRole = new UserRole {
                    UserId=uId,
                    RoleId=roleId
                };
                userRoles.Add(uRole);
            }
            _IUserRoleService.Add(userRoles);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID集合</param>
        /// <returns></returns>
        [NfCustomActionFilter("删除角色下用户", OptionLogTypeEnum.Del, "删除角色下用户",false)]
        public IActionResult DeleteRoleUser(string userIds,int roleId)
        {
            _IUserRoleService.Delete(userIds, roleId);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0
            });
        }
        [NfCustomActionFilter("为角色分配菜单权限", OptionLogTypeEnum.Set, "为角色分配菜单权限",false)]
        public IActionResult AllotSysModels(int Id,int setType,string ckNodeIds)
        {
            IList<RoleModule> roleModes = new List<RoleModule>();
           var nodeIds= StringHelper.String2ArrayInt(ckNodeIds);
            foreach (var nId in nodeIds) {
                var rolemodel = new RoleModule {
                    RoleId=Id,
                    ModuleId= nId,
                };
                roleModes.Add(rolemodel);
            }
            _IRoleModuleService.SaveRoleModels(Id, roleModes);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0
            });
        }
        [NfCustomActionFilter("为角色分配功能数据权限", OptionLogTypeEnum.Set, "为角色分配功能数据权限", false)]
        public IActionResult AllotFuncPermssion(IList<RolePermission> rolePermissions)
        {
            _IRolePermissionService.AddPermission(rolePermissions);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0
            });
        }
        /// <summary>
        /// 根据角色模块获取权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="modeId">模块菜单ID</param>
        /// <returns></returns>
        public IActionResult GetRolePermission(int roleId,int modeId)
        {
            var predicateAnd = PredicateBuilder.True<RolePermission>();
            predicateAnd = predicateAnd.And(a => a.RoleId == roleId && a.ModeId == modeId);
            var Iquery=  _IRolePermissionService.GetQueryable(predicateAnd);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data= Iquery.ToList()
            });
        }
        #endregion

        public IActionResult Test()
        {
            return View();
        }
    }
}