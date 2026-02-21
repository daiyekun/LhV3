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
using NF.ViewModel.Models.Utility;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.System.Controllers
{
    /// <summary>
    /// 部门
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class DepartmentController : NfBaseController //Controller
    {
        /// <summary>
        /// 组织机构
        /// </summary>
        public IDepartmentService _IDepartmentService;
        /// <summary>
        ///角色功能权限
        /// </summary>
        public IRolePermissionService _IRolePermissionService;
        /// <summary>
        /// 用户功能权限
        /// </summary>
        public IUserPermissionService _IUserPermissionService;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 印章管理
        /// </summary>
        private ISealManagerService _ISealManagerService;

        public DepartmentController(IDepartmentService IDepartmentService, IRolePermissionService IRolePermissionService
            , IUserPermissionService IUserPermissionService, IMapper IMapper, ISealManagerService ISealManagerService)
        {
            _IDepartmentService = IDepartmentService;
            _IRolePermissionService = IRolePermissionService;
            _IUserPermissionService =IUserPermissionService;
            _IMapper = IMapper;
            _ISealManagerService = ISealManagerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Department>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Department>();
            var predicateOr = PredicateBuilder.False<Department>();
            predicateAnd = predicateAnd.And(a=>a.IsDelete!=1);//表示没有删除的数据
            if (pageParam.selitem)
            {//选择签约主体
                predicateAnd = predicateAnd.And(a => a.IsMain==1);
            }
            if (!string.IsNullOrEmpty(pageParam.keyWord)) 
            {
                var listIds = _IDepartmentService.GetQueryable(a => a.Name.Contains(pageParam.keyWord)).Select(a => a.Id).ToList();

                predicateOr = predicateOr.Or(a => a.Category!=null&& a.Category.Name.Contains(pageParam.keyWord));
                predicateOr = predicateOr.Or(a => a.Name.Contains(pageParam.keyWord));
                predicateOr = predicateOr.Or(a => a.No.Contains(pageParam.keyWord));
                if (listIds.Count > 0) { 
                predicateOr = predicateOr.Or(a => listIds.Any(p=>p==a.Pid));
                }
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IDepartmentService.GetList(pageInfo, predicateAnd);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 删除-软删除（修改IsDelete）
        /// </summary>
        /// <param name="Ids">修改集合</param>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IDepartmentService.Delete(Ids);
            RedisHelper.ListRightPush(StaticData.RedisDelDeptKey, Ids);
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
        public IActionResult Build()
        {
           
            return View();
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public IActionResult Detail() {
            return View();
        }
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateField(int Id,string fieldName,string fieldValue)
        {
            _IDepartmentService.UpdateField(new UpdateFieldInfo()
            {
                Id=Id,
                FieldName= fieldName,
                FieldValue= fieldValue
               

            });
            return new CustomResultJson(new RequstResult()
            {
                Msg = "修改成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 获取部门选择树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeDept()
        {
            var requeststr = RedisHelper.StringGet(StaticData.RedisTreeSelDeptKey);
            if (!string.IsNullOrEmpty(requeststr))
            {
                return Content(requeststr);
            }
            else
            {
                var str = JsonUtility.SerializeObject(_IDepartmentService.GetGetTreeListDept());
                RedisHelper.StringSet(StaticData.RedisTreeSelDeptKey, str);
                return Content(str);
            }
             
           
            
            //return new CustomResultJson(_IDepartmentService.GetGetTreeListDept()
           
            //     );


        }
        /// <summary>
        /// 获取部门选择树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeSelectDept()
        {

            return new CustomResultJson(_IDepartmentService.GetGetTreeselectListDept(), (v) => { return v.Replace("Checked", "checked"); } );



        }
        /// <summary>
        /// 更新字段
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新增组织机构", OptionLogTypeEnum.Add, "新增组织机构",true)]
        public IActionResult AddSave(Department deptInfo, DeptMain deptMain)
        {
            var list = _IDepartmentService.GetListAll();
            deptInfo.IsMain = deptInfo.IsMain == null ? 0 : deptInfo.IsMain;
            deptInfo.IsSubCompany = deptInfo.IsSubCompany == null ? 0 : deptInfo.IsSubCompany;
            GetDeptPathInfo(deptInfo, list);
            _IDepartmentService.SaveDeptInfo(deptInfo, deptMain);
           return DeptSubmitSave(deptInfo, deptMain);
           

        }
        /// <summary>
        /// 生成树目录
        /// </summary>
        /// <param name="deptInfo"></param>
        /// <param name="list"></param>
        private static void GetDeptPathInfo(Department deptInfo, IList<DepartmentDTO> list)
        {
           
            if (deptInfo.Pid == 0)
            {
                deptInfo.Leaf = 1;
                deptInfo.Dpath = "0" + (list.Where(a => a.Pid == 0).Count() + 1);
            }
            else
            {
                var partInfo = list.Where(c => c.Id == deptInfo.Pid).FirstOrDefault();
                if (partInfo != null)
                {
                    deptInfo.Leaf = partInfo.Leaf + 1;
                    deptInfo.Dpath = partInfo.Dpath + "/0" + (list.Where(a => a.Pid == partInfo.Id).Count() + 1);
                }

            }
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="deptInfo">部门对象</param>
        /// <param name="deptMain">签约主体信息对象</param>
        /// <returns></returns>
        [NfCustomActionFilter("修改组织机构", OptionLogTypeEnum.Update, "修改组织机构", true)]
        public IActionResult UpdateSave(Department deptInfo, DeptMain deptMain)
        {
            var list = _IDepartmentService.GetListAll();
            GetDeptPathInfo(deptInfo, list);
           return DeptSubmitSave(deptInfo, deptMain);
           

        }

        private IActionResult DeptSubmitSave(Department deptInfo, DeptMain deptMain)
        {
            _IDepartmentService.SaveDeptInfo(deptInfo, deptMain);
            var redisDept = _IMapper.Map<Department, RedisDept>(deptInfo);
            SysIniInfoUtility.SetRedisHash(redisDept, StaticData.RedisDeptKey, (key, Id) =>
            {
                return $"{key}:{Id}";
            });
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
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IDepartmentService.ShowView(Id)


            });

        }
        /// <summary>
        /// 组织机构多选
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectWinMultDept()
        {
            return View();

        }
        /// <summary>
        /// 获取Xtree部门数据
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeData()
        {
            IList<int> deptIds = new List<int>();
            var listTrees = _IDepartmentService.GetXtTree(deptIds);
            return new CustomResultJson(listTrees, StringHelper.RepleaceStr);
        }
        /// <summary>
        /// 根据角色、模块、分配对象类型查询所拥有的部门
        /// </summary>
        /// <param name="funcId">功能ID</param>
        /// <param name="Id">角色、用户、岗位ID</param>
        /// <param name="setType">1、角色、0用户</param>
        /// <returns></returns>
        public IActionResult GetPessionDeptXTree(int? funcId,int? Id,int? setType)
        {
            IList<XTree> xTrees = new List<XTree>();
            if (setType == 1) {//角色
                var predicateAnd = PredicateBuilder.True<RolePermission>();
                predicateAnd = predicateAnd.And(a => a.FuncId == funcId && a.RoleId == Id);
                var deptIds = _IRolePermissionService.GetQueryable(predicateAnd).Select(a => a.DeptIds);
                var listdeptIds = StringHelper.String2ArrayInt(string.Join(',', deptIds));
                xTrees = _IDepartmentService.GetXtTree(listdeptIds);
                
            }
            else if(setType==0) {//用户
                var predicateAnd = PredicateBuilder.True<UserPermission>();
                predicateAnd = predicateAnd.And(a => a.FuncId == funcId && a.UserId == Id);
                var deptIds = _IUserPermissionService.GetQueryable(predicateAnd).Select(a => a.DeptIds);
                var listdeptIds = StringHelper.String2ArrayInt(string.Join(',', deptIds));
                xTrees = _IDepartmentService.GetXtTree(listdeptIds);
                
            }
            return new CustomResultJson(xTrees, StringHelper.RepleaceStr);
        }
        /// <summary>
        /// 查看组织机构树
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowDeptTree()
        {
            return View();
        }

        





    }
    }

        
