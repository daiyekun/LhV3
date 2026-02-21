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
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.WorkFlow.Controllers
{
    /// <summary>
    /// 组
    /// </summary>
    [Area("WorkFlow")]
    [Route("WorkFlow/[controller]/[action]")]
    public class GroupInfoController : NfBaseController
    {
        /// <summary>
        /// 组
        /// </summary>
        private IGroupInfoService _IGroupInfoService;
        private IMapper _IMapper;
        private IGroupUserService _IGroupUserService;

        public GroupInfoController(IGroupInfoService IGroupInfoService, IMapper IMapper
            , IGroupUserService IGroupUserService)
        {
            _IGroupInfoService = IGroupInfoService;
            _IMapper = IMapper;
            _IGroupUserService = IGroupUserService;
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
        /// 判断字段值是否存在
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <returns></returns>
        public IActionResult CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = _IGroupInfoService.CheckInputValExist(fieldInfo)

            });
        }

       
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult Save(GroupInfoDTO info)
        {
            var saveInfo = _IMapper.Map<GroupInfo>(info);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.Gstate = 1;//1:启用
            _IGroupInfoService.Add(saveInfo);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,
            });
        }
        
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult UpdateSave(GroupInfoDTO info)
        {

            if (info.Id > 0)
            {
                var updateinfo = _IGroupInfoService.Find(info.Id);
                var updatedata = _IMapper.Map(info, updateinfo);
                _IGroupInfoService.Update(updatedata);
            }

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
            _IGroupInfoService.Delete(Ids);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,


            };
            return new CustomResultJson(resinfo);
        }

        /// <summary>
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _IGroupInfoService.ShowView(Id);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<GroupInfo>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<GroupInfo>();
            var predicateOr = PredicateBuilder.False<GroupInfo>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据
            
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IGroupInfoService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id">修改对象ID</param>
        /// <param name="fieldName">修改字段名称</param>
        /// <param name="fieldVal">修改值，如果不是String后台人为判断</param>
        /// <returns></returns>
        public IActionResult UpdateField(UpdateFieldInfo info)
        {
            var res = _IGroupInfoService.UpdateField(info);
            RequstResult reqInfo = reqInfo = new RequstResult()
            {
                Msg = "修改成功",
                Code = 0,
            };
            if (res<= 0)
            {
                reqInfo.Msg = "修改失败";
            }
            
            return new CustomResultJson(reqInfo);
        }

        /// <summary>
        /// 获取组列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetListUser(int? limit, int? page, int groupId)
        {
            var _pageIndex = (page ?? 1) <= 0 ? 1 : (page ?? 1);
            var pageInfo = new PageInfo<UserInfor>(pageIndex: _pageIndex, pageSize: limit ?? 20);
            var predicateAnd = PredicateBuilder.True<UserInfor>();
            var predicateOr = PredicateBuilder.False<UserInfor>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据
            var userIds= _IGroupUserService.GetQueryable(a => true).Where(a => a.GroupId == groupId).Select(a => a.UserId).ToArray();
            predicateAnd = predicateAnd.And(a => userIds.Contains(a.Id));
            var layPage = _IGroupUserService.GetListUser(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 添加组用户
        /// </summary>
        /// <param name="userIds">用户IDs</param>
        /// <param name="groupId">组ID</param>
        /// <returns></returns>
        public IActionResult AddRoleUser(string userIds, int groupId)
        {
            var listuserIds = StringHelper.String2ArrayInt(userIds);
            IList<GroupUser> userGroups = new List<GroupUser>();
            foreach (var uId in listuserIds)
            {
                GroupUser ugroup = new GroupUser
                {
                    UserId = uId,
                    GroupId = groupId
                };
                userGroups.Add(ugroup);
            }
            _IGroupUserService.Add(userGroups);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0
            });
        }
        /// <summary>
        /// 删除组
        /// </summary>
        /// <param name="userIds">用户集合</param>
        /// <param name="groupId">组ID</param>
        /// <returns></returns>
        public IActionResult DeleteGroupUser(string userIds, int groupId)
        {
            _IGroupUserService.Delete(userIds, groupId);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0
            });
        }
        /// <summary>
        /// 选择组
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectGroups()
        {
            return View();
        }
        /// <summary>
        /// 选择组用户
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectGroupusinfo(int Zid)
        {
            ViewData["Zid"] = Zid;
            return View();
        }
        /// <summary>
        /// 选择组数据列表
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectGroupList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<GroupInfo>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<GroupInfo>();
            var predicateOr = PredicateBuilder.False<GroupInfo>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1&&a.Gstate==1);//表示没有删除的数据

            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IGroupInfoService.SelectGroupList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 选择组数据列表
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectGroupusinfoList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<GroupInfo>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<GroupInfo>();
            var predicateOr = PredicateBuilder.False<GroupInfo>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1 && a.Gstate == 1);//表示没有删除的数据

            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IGroupInfoService.SelectGroupUsinfoList(pageInfo, predicateAnd,pageParam.Zid);
            return new CustomResultJson(layPage);
        }
    }
}