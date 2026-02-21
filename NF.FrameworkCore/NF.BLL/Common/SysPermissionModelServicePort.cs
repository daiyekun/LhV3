using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 权限处理分布类接口点
    /// </summary>
    public partial class SysPermissionModelService
    {
        #region 客户权限
        /// <summary>
        /// 客户列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所在部门ID</param>
        /// <returns>客户权限表达</returns>
        public Expression<Func<Company, bool>> GetCmpListPermissionExpression(string funCode, int userId, int deptId = 0)
        {
            return GetCompanyListPermissionExpression(funCode, userId, deptId);
        }
        /// <summary>
        /// 判断当前用户是否有新建客户的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetCmpAddPermission(string funCode, int userId)
        {
            return GetCompanyAddPermission(funCode, userId);
        }

        /// <summary>
        /// 判断当前用户是否有新建客户的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="deptId">当前部门ID</param>
        /// <param name="updateObjId">当前修改的ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetCmpUpdatePermission(string funCode, int userId, int deptId, int updateObjId)
        {
            return GetCompanyUpdatePermission(funCode, userId, deptId, updateObjId);
        }
        /// <summary>
        /// 判断当前用户是否有修改合同对方次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetCmpSecFieldUpdatePermission(string funCode, int userId, int deptId, int updateObjId)
        {
            return GetCompanySecFieldUpdatePermission(funCode, userId, deptId, updateObjId);
        }
        /// <summary>
        /// 查看客户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">部门ID</param>
        /// <param name="detailObjId">对象数据ID</param>
        /// <param name="funCode">权限功能码</param>
        /// <returns>True：有权限，否则无权限</returns>
        public bool GetCmpDetailPermission(string funCode, int userId, int deptId, int detailObjId)
        {
            return GetCompanyDetailPermission(funCode, userId, deptId, detailObjId);
        }

        /// <summary>
        ///获取删除客户权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除集合ID</param>
        /// <param name="funCode">状态码标识</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetCmpDeletePermission(string funCode, int userId, int deptId, IList<int> listdelIds)
        {
            return GetCompanyDeletePermission(funCode, userId, deptId, listdelIds);
        }
        #endregion

        #region 项目

        /// <summary>
        /// 判断当前用户是否有修改项目次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <param name="funCode">功能码</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetProjSecFieldUpdatePermission(string funCode, int userId, int deptId, int updateObjId)
        {
            return GetProjectSecFieldUpdatePermission(funCode, userId, deptId, updateObjId);
        }
        #endregion

        #region  单品管理
        /// <summary>
        /// 根据功能标识码及用户ID获取当前用户所拥有的权限类型
        /// </summary>
        /// <param name="funcCode">功能标识码</param>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        public bool GetBcInstanceAddOrUpdatePermission(string funcCode, int userId)
        {

            List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
            return listFuntypes.Contains(4);
        }

        /// <summary>
        /// 单品管理列表权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
       public  bool GetBcInstanceListPermission(string funcCode, int userId)
        {
            List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
            return listFuntypes.Contains(4);
        }
        /// <summary>
        /// 单品管理查看详情权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
       public bool GetBcInstanceDetailPermission(string funcCode, int userId)
        {
            List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
            return listFuntypes.Contains(4);
        }

        #endregion



    }
}
