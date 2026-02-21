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
using NF.Common.SessionExtend;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using NF.ViewModel.Models.Utility;
using System.IO;
using NF.Web.Utility.Common;

namespace NF.Web.Areas.System.Controllers
{
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class UserInforController : NfBaseController //Controller
    {
        public IUserInforService _IUserInforService;
        public IUserRoleService _IUserRoleService;
        public IUserModuleService _IUserModuleService;
        public IUserPermissionService _IUserPermissionService;
        private IMapper _IMapper;
        public UserInforController(IUserInforService IUserInforService, IUserRoleService IUserRoleService, IUserModuleService IUserModuleService,IUserPermissionService IUserPermissionService, IMapper IMapper) {

            _IUserInforService = IUserInforService;
            _IUserRoleService = IUserRoleService;
            _IUserModuleService = IUserModuleService;
            _IUserPermissionService = IUserPermissionService;
            _IMapper = IMapper; 
        } 

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(int? limit, int? page, string keyWord ,int ISQy,string filterSos)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<UserInfor>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<UserInfor>();
            var predicateOr = PredicateBuilder.False<UserInfor>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据
           // predicateAnd = predicateAnd.And(a => a.Ustart == 1);//表示状态为启用的用户
            if (ISQy==1)
            {
                predicateAnd = predicateAnd.And(a => a.State == 1);//表示状态为启用的用户
            }
            if (!string.IsNullOrEmpty(filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetytAdvQueryUser(filterSos, _IUserInforService));
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Department.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IUserInforService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 选择角色为项目负责人的列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList1(int? limit, int? page, string keyWord)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<UserInfor>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<UserInfor>();
            var predicateOr = PredicateBuilder.False<UserInfor>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据
            predicateAnd = predicateAnd.And(a => a.State == 1);//表示状态为启用的用户
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Department.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IUserInforService.GetList1(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 选择用户(招标/询价/约谈)记录人默认为“季凌燕”
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetList2(int? limit, int? page, string keyWord)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<UserInfor>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<UserInfor>();
            var predicateOr = PredicateBuilder.False<UserInfor>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据
            predicateAnd = predicateAnd.And(a => a.State == 1);//表示状态为启用的用户
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.DisplyName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Department.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IUserInforService.GetList2(pageInfo, predicateAnd , "季凌燕");
            return new CustomResultJson(layPage);

        }


        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateField(int Id, string fieldName, string fieldValue)
        {
            _IUserInforService.UpdateField(new UpdateFieldInfo()
            {
                Id = Id,
                FieldName = fieldName,
                FieldValue = fieldValue


            });
            _IUserInforService.SetRedis();
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
            _IUserInforService.Delete(Ids);
            //加入队列
            RedisHelper.ListRightPush(StaticData.RedisUserDelKey, Ids);
            _IUserInforService.SetRedis();
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public IActionResult Build(int Id)
        {
           
            ViewData["ids"] = Id;
            ViewData["UserE"] =_IUserInforService.DZQZ(Id);
            return View();
        }
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新增系统用户", OptionLogTypeEnum.Add, "新增系统用户",true)]
        public IActionResult AddSave(UserInfor userInfo)
        {
            userInfo.CreateUserId = base.SessionCurrUserId;
            SaveUpdateUser(userInfo);
            _IUserInforService.SetRedis();
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 修改和添加用户
        /// </summary>
        /// <param name="userInfo">用户对象</param>
        private void SaveUpdateUser(UserInfor userInfo)
        {
            _IUserInforService.SaveInfo(userInfo);
            
            var redisUser = _IMapper.Map<UserInfor, RedisUser>(userInfo);
            redisUser.DeptName= RedisHelper.HashGet($"{StaticData.RedisDeptKey}:{userInfo.DepartmentId}","Name");
            SysIniInfoUtility.SetRedisHash(redisUser, StaticData.RedisUserKey, (key, Id) =>
            {
                return $"{key}:{Id}";
            });
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        [NfCustomActionFilter("修改系统用户", OptionLogTypeEnum.Update, "修改系统用户",true)]
        public IActionResult UpdateSave(UserInfor userInfo)
        {
            //userInfo.Pwd = "28c8edde3d61a0411511d3b1866f0636";
             userInfo.ModifyUserId= base.SessionCurrUserId;
            SaveUpdateUser(userInfo);
            _IUserInforService.SetRedis();
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
            if (Id == -100) {//自己修改基本信息
                Id = SessionCurrUserId;
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IUserInforService.ShowView(Id)


            });

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
        /// 判断输入值是否存在
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckInputValExist(UpdateFieldInfo updateField) {

            var res = _IUserInforService.CheckFieldValExist(updateField);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = res


            });
        }
        /// <summary>
        /// 显示基本信息
        /// </summary>
        /// <returns></returns>
        public IActionResult Info()
        {
            return View();

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public IActionResult SetPassword()
        {
            return View();

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        [NfCustomActionFilter("修改密码", OptionLogTypeEnum.Update, "修改密码",false)]
        public IActionResult UpdatePwd(string oldPassword,string password)
        {
            return new CustomResultJson(_IUserInforService.UpdatePwd(oldPassword, password,SessionCurrUserId));

        }
        /// <summary>
        /// 设置用户基本信息
        /// </summary>
        /// <param name="userInfo">当前对象</param>
        /// <returns></returns>
        [NfCustomActionFilter("设置基本信息", OptionLogTypeEnum.Set, "设置基本信息",true)]
        public IActionResult SetUserInfo(UserInfor userInfo)
        {
            userInfo.ModifyUserId = SessionCurrUserId;
            userInfo.ModifyDatetime = DateTime.Now;
            _IUserInforService.SetUserInfo(userInfo);
            _IUserInforService.SetRedis();
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }

        #region 选择框
        /// <summary>
        /// 选择用户框
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectUser()
        {
            return View();
        }
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <returns></returns
        
        public IActionResult UserSetRoles(int Id)
        {
            
            var predicateAnd = PredicateBuilder.True<UserRole>();
            predicateAnd = predicateAnd.And(a=>a.UserId== Id);
            var list=  _IUserRoleService.GetListUserRole(predicateAnd);
            var roleIds = list.Select(a => a.RoleId??0).ToList();
           var currRoleIds= StringHelper.ArrayInt2String(roleIds);
            ViewData["curruId"] = Id;
            ViewData["currRoleIds"] = currRoleIds;
            return View();
        }
        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("设置用户角色", OptionLogTypeEnum.Set, "设置用户角色",false)]
        public IActionResult SetUserRoles(int uId,string roleIds)
        {
            var tmproleIds = StringHelper.String2ArrayInt(roleIds);
            IList<UserRole> listuroles = new List<UserRole>();
            foreach (var item in tmproleIds)
            {
                listuroles.Add( new UserRole()
                {
                    UserId= uId,
                    RoleId=item

                });
            }
          
            _IUserRoleService.SetUserRole(listuroles, uId);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 为用户分配权限
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <param name="setType">类别</param>
        /// <param name="ckNodeIds">菜单ID</param>
        /// <returns></returns>
        [NfCustomActionFilter("为用户分配菜单权限", OptionLogTypeEnum.Set, "为用户分配菜单权限",false)]
        public IActionResult AllotSysModels(int Id, int setType, string ckNodeIds)
        {
            IList<UserModule> userModes = new List<UserModule>();
            var nodeIds = StringHelper.String2ArrayInt(ckNodeIds);
            foreach (var nId in nodeIds)
            {
                var usermodel = new UserModule
                {
                    UserId = Id,
                    ModuleId = nId,
                };
                userModes.Add(usermodel);
            }
            _IUserModuleService.SaveUserModels(Id, userModes);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0
            });
        }

        /// <summary>
        /// 根据角色模块获取权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="modeId">模块菜单ID</param>
        /// <returns></returns>
        public IActionResult GetUserPermission(int userId, int modeId)
        {
            var predicateAnd = PredicateBuilder.True<UserPermission>();
            predicateAnd = predicateAnd.And(a => a.UserId == userId && a.ModeId == modeId);
            var Iquery = _IUserPermissionService.GetQueryable(predicateAnd);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = Iquery.ToList()
            });
        }
        [NfCustomActionFilter("为用户分配功能数据权限", OptionLogTypeEnum.Set, "为用户分配功能数据权限", false)]
        public IActionResult AllotFuncPermssion(IList<UserPermission> userPermissions)
        {
            _IUserPermissionService.AddPermission(userPermissions);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0
            });
        }
        #endregion
    }
}