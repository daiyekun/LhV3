using NF.Common.Utility;
using NF.Model.Extend;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    public partial class SysPermissionModelService
    {
        /// <summary>
        /// 获取列表权限
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="funCode">功能标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">登录人所属部门ID</param>
        /// <returns></returns>
        public Expression<Func<T, bool>> GetListPermissionExpression<T>(string funCode, int userId, int deptId = 0)
            where T : ICreateUser, IPrincipalUser
        {
            var predicate = PredicateBuilder.True<T>();
            //查询对应角色
            var pession = GetPermission(funCode, userId);
            if (pession.listFuntypes.Contains(3))
            {//全部
                predicate = predicate.And(p => true);
            }
            else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
            {
                var predicatedept = PredicateBuilder.False<T>();
                if (pession.listFuntypes.Contains(2))
                {//机构
                    var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                    predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.CreateUser.DepartmentId ?? -100));
                }
                if (pession.listFuntypes.Contains(6))
                {//本机构
                    predicatedept = predicatedept.Or(p => (p.CreateUser.DepartmentId ?? -100) == deptId);
                }
                if (pession.listFuntypes.Contains(7))
                {//本机构及子机构
                    var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                    predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.CreateUser.DepartmentId ?? -100));
                }
                predicatedept = predicatedept.Or(p => p.CreateUserId == userId);
                predicatedept = predicatedept.Or(p => p.PrincipalUserId == userId);//负责人
                predicate = predicate.And(predicatedept);
            }
            else
            {
                var predicate2 = PredicateBuilder.False<T>();
                predicate2 = predicate2.Or(p => p.CreateUserId == userId);
                predicate2 = predicate2.Or(p => p.PrincipalUserId == userId);//负责人
                predicate = predicate.And(predicate2);
            }
            return predicate;
        }

        /// <summary>
        /// 新建权限
        /// </summary>
        /// <param name="funcCode">功能标识码</param>
        /// <param name="userId">当前用户</param>
        /// <returns></returns>
        public bool GetAddPermission(string funcCode, int userId)
        {
            List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
            return listFuntypes.Contains(4);

        }

    }
}
