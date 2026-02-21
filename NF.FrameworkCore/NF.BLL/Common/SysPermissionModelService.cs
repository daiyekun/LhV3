using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 自定义权限操作实现类
    /// </summary>
    public partial class SysPermissionModelService : BaseService<SysPermissionModel>, ISysPermissionModelService
    {
        private DbSet<SysPermissionModel> _SysPermissionModelSet = null;
        public SysPermissionModelService(DbContext dbContext)
           : base(dbContext)
        {
            _SysPermissionModelSet = base.Db.Set<SysPermissionModel>();
        }

        public SysPermissionModelService() { }

        #region 公用方法
        /// <summary>
        /// 获取权限是机构的选择机构ID
        /// </summary>
        /// <returns></returns>
        private IList<int> GetDeptIds(IList<RolePermission> rolePermissions, IList<UserPermission> userPermissions)
        {
            List<int> listdepts = new List<int>();
            if (rolePermissions != null && rolePermissions.Count() > 0)
            {
                var list = rolePermissions.Where(a => a.FuncType == 2).Select(a => a.DeptIds).ToList();
                foreach (var item in list)
                {
                    listdepts.AddRange(StringHelper.String2ArrayInt(item));
                }
            }
            if (userPermissions != null && userPermissions.Count() > 0)
            {
                var list = userPermissions.Where(a => a.FuncType == 2).Select(a => a.DeptIds).ToList();
                foreach (var item in list)
                {
                    listdepts.AddRange(StringHelper.String2ArrayInt(item));
                }
            }

            return listdepts;
        }
        /// <summary>
        /// 根据组织机构ID获取它的所有下级组织机构ID
        /// </summary>
        /// <param name="deptId">当前组织机构ID</param>
        /// <returns></returns>
        private IList<int> GetDeptAndChirdDetpId(int deptId)
        {
            IList<DepartmentDTO> list = RedisHelper.StringGetToList<DepartmentDTO>("Nf-DeptListAll");
            if (list == null)
            {
                var tempquery = Db.Set<Department>().AsNoTracking();
                var query = from a in tempquery
                            select new
                            {
                                Id = a.Id,
                                Pid = a.Pid,
                                Name = a.Name,
                                ShortName = a.ShortName,
                                No = a.No,
                                CategoryId = a.CategoryId,
                                Dsort = a.Dsort,
                                Remark = a.Remark,
                                IsMain = a.IsMain,
                                IsSubCompany = a.IsSubCompany,
                                Dstatus = a.Dstatus,
                                Dpath = a.Dpath,
                                Leaf = a.Leaf,
                                IsDelete = a.IsDelete,
                                CategoryName = a.Category.Name,
                                //PName = //Db.Set<Department>().AsNoTracking().Where(d => a.Pid == d.Id).Any() ? Db.Set<Department>().AsNoTracking().Where(d => a.Pid == d.Id).FirstOrDefault().Name : "",


                            };
                var local = from a in query.AsEnumerable()
                            select new DepartmentDTO
                            {
                                Id = a.Id,
                                Pid = a.Pid,
                                Name = a.Name,
                                ShortName = a.ShortName,
                                No = a.No,
                                CategoryId = a.CategoryId,
                                Dsort = a.Dsort,
                                Remark = a.Remark,
                                IsMain = a.IsMain,
                                IsSubCompany = a.IsSubCompany,
                                Dstatus = a.Dstatus,
                                Dpath = a.Dpath,
                                Leaf = a.Leaf,
                                CategoryName = a.CategoryName,
                                PName = this.Db.GetRedisDeptNameValue(a.Pid),
                               // RedisHelper.HashGet($"{StaticData.RedisDeptKey}:{a.Pid}", "Name"), //a.PName,
                                IsDelete = a.IsDelete,
                                IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0),
                                IsSubCompanyDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsSubCompany ?? 0)


                            };
                list = local.ToList();
                RedisHelper.ListObjToJsonStringSetAsync("Nf-DeptListAll", list);
            }
            var infopath = list.Where(a => a.Id == deptId).Any() ? list.Where(a => a.Id == deptId).FirstOrDefault().Dpath : "notdata";
            var listchds = list.Where(a => a.Dpath.StartsWith(infopath)).Select(a => a.Id).ToList();
            listchds.Add(deptId);
            return listchds;

        }
        /// <summary>
        /// 根据用户ID查询当前用户对应角色权限
        /// </summary>
        /// <returns>角色权限</returns>
        protected IList<RolePermission> GetRolePermissionByUserId(int userId)
        {
           return RedisHelper.StringGetToList<RolePermission>($"{StaticData.UserRolePermissions}:{userId}");
        }
        /// <summary>
        /// 根据用户查询当前用户对应权限
        /// </summary>
        /// <returns>用户权限</returns>
        protected IList<UserPermission> GetUserPermissionByUserId(int userId)
        {
           return RedisHelper.StringGetToList<UserPermission>($"{StaticData.UserPermissions}:{userId}");
        }
        /// <summary>
        /// 用户和功能标识获取对应权限列表
        /// </summary>
        /// <param name="funcCode">功能代码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>功能权限列表</returns>
        protected List<RolePermission> listRoleListPermsByFunCode(string funcCode, int userId)
        {
            var rolePermission = GetRolePermissionByUserId(userId);
            if (rolePermission != null)
                return rolePermission.Where(a => a.FuncCode == funcCode).ToList();
               return null;
                 

        }
        /// <summary>
        /// 用户和功能标识获取对应权限列表
        /// </summary>
        /// <param name="funcCode">功能代码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>功能权限列表</returns>
        protected List<UserPermission> listUserListPermsByFunCode(string funcCode, int userId)
        {
            var userPermission = GetUserPermissionByUserId(userId);
            if (userPermission != null)
                return userPermission.Where(a => a.FuncCode == funcCode).ToList();
            return null;


        }

        /// <summary>
        /// 根据功能标识码及用户ID获取当前用户所拥有的权限类型
        /// </summary>
        /// <param name="funcCode">功能标识码</param>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        protected List<byte?> GetPermissionTypes(string funcCode, int userId)
        {

            var rolepssion = listRoleListPermsByFunCode(funcCode, userId);
            var userpssion = listUserListPermsByFunCode(funcCode, userId);
            return GetFunTypes(rolepssion, userpssion);
        }
        /// <summary>
        /// 获取功能权限类型
        /// </summary>
        /// <param name="rolepssion">角色权限</param>
        /// <param name="userpssion">用户权限</param>
        /// <returns></returns>
        private static List<byte?> GetFunTypes(List<RolePermission> rolepssion, List<UserPermission> userpssion)
        {
            List<byte?> listFuntypes = new List<byte?>();
            if (rolepssion != null)
            {
                listFuntypes = rolepssion.Select(a => a.FuncType).ToList();
            }
            if (userpssion != null)
            {
                listFuntypes.AddRange(userpssion.Select(a => a.FuncType).ToList());
            }
            return listFuntypes;
        }

        /// <summary>
        /// 根据用户和功能标识获取相关权限
        /// </summary>
        /// <param name="funcCode">功能标识</param>
        /// <param name="userId">用户ID</param>
        /// <returns>功能权限列表</returns>
        protected PermissionInfo GetPermission(string funcCode, int userId)
        {
            PermissionInfo permission = new PermissionInfo();
            var rolepssion = listRoleListPermsByFunCode(funcCode, userId);
            if(rolepssion!=null)
            {
                permission.RolePermissions = rolepssion;
            }
            var userpssion = listUserListPermsByFunCode(funcCode, userId);
            if (userpssion != null)
            {
                permission.UserPermissions = userpssion;
            }
            permission.listFuntypes = GetFunTypes(rolepssion, userpssion);
            return permission;
        }

        

        #endregion
    }
    /// <summary>
    /// 权限功能对象
    /// </summary>
    public class PermissionInfo
    {   /// <summary>
        /// 角色权限
        /// </summary>
        public List<RolePermission> RolePermissions = new List<RolePermission>();
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<UserPermission> UserPermissions = new List<UserPermission>();
        /// <summary>
        /// 拥有权限类型集合
        /// </summary>
        public List<byte?> listFuntypes = new List<byte?>();


    }

}
