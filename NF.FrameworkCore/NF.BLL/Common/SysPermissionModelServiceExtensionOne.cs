using NF.Common.Utility;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NF.ViewModel.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel;
using NF.ViewModel.Models.Finance.Enums;

namespace NF.BLL
{
    /// <summary>
    /// 手动添加获取模块权限代码
    /// </summary>
    public partial class SysPermissionModelService
    {



        #region 合同对方
        /// <summary>
        /// 获取合同对方列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// 权限类型：
        /// 1类：4是/5否
        /// 2类：1个人、2机构、3全部、6本机构、7本机构及子机构,
        /// 如果一个人拥有权限基本多种，取权限范围最大值：
        /// 1<6<2<7<3;4，5只有新建之类的才有
        /// <returns>合同对方权限表达式树</returns>
        public Expression<Func<Company, bool>> GetCompanyListPermissionExpression(string funcCode, int userId, int deptId = 0)
        {
            var predicate = PredicateBuilder.True<Company>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
            }
            else
            {
                //查询对应角色
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<Company>();
                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (pession.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<Company>();
                    predicate2 = predicate2.Or(p => p.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
            }
            return predicate;
        }
        /// <summary>
        /// 判断当前用户是否有新建合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetCompanyAddPermission(string funcCode, int userId)
        {
            List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
            return listFuntypes.Contains(4);
        }
        /// <summary>
        /// 判断当前用户是否有修改合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetCompanyUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<Company>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {

                var datainfo = Db.Set<Company>().Include(a => a.CreateUser).AsNoTracking().FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);
                if (datainfo == null
                    || (datainfo.Cstate == (byte)CompStateEnum.Unreviewed && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                    || (datainfo.Cstate == (byte)CompStateEnum.Audited)
                    || (datainfo.Cstate == (byte)CompStateEnum.Audited && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                    || (datainfo.Cstate == (byte)CompStateEnum.Terminated)
                    )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        /// 判断当前用户是否有修改合同对方次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetCompanySecFieldUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<Company>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<Company>().Include(a => a.CreateUser).AsNoTracking().FirstOrDefault(a => a.Id == updateObjId);
                if (datainfo.WfState == (int)WfStateEnum.ShenPiZhong)
                {
                    return PermissionDicEnum.NotState;//审批中
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        /// 判断当前用户是否有查看合同对方详情的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="detailObjId">修改数据的ID</param>
        /// <returns>True：有权限，False：没权限</returns>
        public bool GetCompanyDetailPermission(string funcCode, int userId, int deptId, int detailObjId)
        {
            var predicate = PredicateBuilder.True<Company>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return true;
            }
            else
            {
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = Db.Set<Company>().Include(a => a.CreateUser).Where(a => a.Id == detailObjId).FirstOrDefault();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return true;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var datainfo = Db.Set<Company>().Find(detailObjId);
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;
                }
                return false;
            }

        }



        /// <summary>
        //获取删除合同对方权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetCompanyDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {

            var predicate = PredicateBuilder.True<Company>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                predicate = predicate.And(a => true);
                permiss.Code = 0;
                return permiss;
            }
            else
            {

                var permiss = new PermissionDataInfo();
                var querylist = Db.Set<Company>().AsEnumerable().Where(a => listdelIds.Contains(a.Id)).ToList();
                var listnot = querylist.Where(a => a.Cstate != (byte)SysDataSateEnum.Unreviewed
                || (a.Cstate == (byte)SysDataSateEnum.Unreviewed && (a.WfState ?? 0) != 0)).Select(a => a.Name).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);


                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser != null && a.CreateUser.DepartmentId == deptId).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;


                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;


            }

        }

        /// <summary>
        /// 项目查看页面按钮权限
        /// </summary>
        /// <param name="Id">当前对象</param>
        /// <returns></returns>
        public BtnPremissionInfo GetCmpBtnPermission(int Id)
        {

            BtnPremissionInfo btnPremission = new BtnPremissionInfo();
            var datainfo = Db.Set<Company>().AsNoTracking().FirstOrDefault(a => a.Id == Id);
            if (datainfo != null)
            {

                //修改按钮
                if (datainfo.Cstate == 0 && (datainfo.WfState ?? 0) == 0)
                {
                    btnPremission.Update = 1;
                }
                //删除按钮
                if (datainfo.Cstate == 0 && (datainfo.WfState ?? 0) == 0)
                {
                    btnPremission.Delete = 1;
                }
            }

            return btnPremission;

        }

        /// <summary>
        /// 项目查看页面按钮权限
        /// </summary>
        /// <param name="Id">当前对象</param>
        /// <returns></returns>
        public BtnPremissionInfo GetscheBtnPermission(int Id)
        {
            BtnPremissionInfo btnPremission = new BtnPremissionInfo();
            var datainfo = Db.Set<ScheduleDetail>().AsNoTracking().FirstOrDefault(a => a.Id == Id);
            if (datainfo != null)
            {

                //修改按钮
                if (datainfo.State == 0)
                {
                    btnPremission.Update = 1;
                }

            }

            return btnPremission;

        }


        public PermissionDicEnum UpdateCmp(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<Company>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<Company>().Include(a => a.CreateUser).AsNoTracking().FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);

                if (datainfo == null
                    || (datainfo.Cstate == (byte)CompStateEnum.Unreviewed && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                    //|| (datainfo.Cstate == (byte)CompStateEnum.Audited)
                    //|| (datainfo.Cstate == (byte)CompStateEnum.Audited && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                    || (datainfo.Cstate == (byte)CompStateEnum.Terminated)
                    )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }




        #endregion
        #region 项目

        /// <summary>
        /// 判断当前用户是否有修改项目次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetProjectSecFieldUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<ProjectManager>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;

            }
        }

        public PermissionDicEnum UpdateProj(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<ProjectManager>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;
                }
                return PermissionDicEnum.None;
            }
        }
        public PermissionDicEnum UpdateContract(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<ContractInfo>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;
                }
                return PermissionDicEnum.None;
            }
        }
        public PermissionDicEnum UpdateZhao(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<TenderInfor>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    if (datainfo.CreateUserId == userId || datainfo.TenderUserId == userId)
                        return PermissionDicEnum.OK;
                }
                return PermissionDicEnum.None;
            }
        }
        public PermissionDicEnum UpdateXunjia(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<Inquiry>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    if (datainfo.CreateUserId == userId)
                        return PermissionDicEnum.OK;
                }
                return PermissionDicEnum.None;
            }
        }
        public PermissionDicEnum UpdateQia(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<Questioning>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    if (datainfo.CreateUserId == userId)
                        return PermissionDicEnum.OK;
                }
                return PermissionDicEnum.None;
            }
        }




        /// <summary>
        /// 获取项目列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// 权限类型：
        /// 1类：4是/5否
        /// 2类：1个人、2机构、3全部、6本机构、7本机构及子机构,
        /// 如果一个人拥有权限基本多种，取权限范围最大值：
        /// 1<6<2<7<3;4，5只有新建之类的才有
        /// <returns>项目权限表达式树</returns>
        public Expression<Func<ProjectManager, bool>> GetProjectListPermissionExpression(string funcCode, int userId, int deptId = 0)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return predicate;
            }
            else
            {

                 predicate = PredicateBuilder.True<ProjectManager>();
                //查询对应角色
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<ProjectManager>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<ProjectManager>();
                    predicate2 = predicate2.Or(p => p.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }


                return predicate;
            }


        }
        /// <summary>
        /// 判断当前用户是否有新建项目的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetProjectAddPermission(string funcCode, int userId)
        {
        
                List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
                return listFuntypes.Contains(4);
          
        }

        /// <summary>
        /// 判断当前用户是否有修改项目的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetProjectUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<ProjectManager>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);

                if (datainfo == null
                   || (datainfo.Pstate == (byte)ProjStateEnum.Unexecuted && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                   || (datainfo.Pstate == (byte)ProjStateEnum.Approve)
                   || (datainfo.Pstate == (byte)ProjStateEnum.Approve && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                   || (datainfo.Pstate == (byte)ProjStateEnum.Completed)
                   || (datainfo.Pstate == (byte)ProjStateEnum.Terminated)
                   || (datainfo.Pstate == (byte)ProjStateEnum.Dozee)
                    || (datainfo.Pstate == (byte)ProjStateEnum.Execution)
                   )
                {

                    return PermissionDicEnum.NotState;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;

            }
        }

        /// <summary>
        /// 判断当前用户是否有查看项目详情的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="detailObjId">修改数据的ID</param>
        /// <returns>True：有权限，False：没权限</returns>
        public bool GetProjectDetailPermission(string funcCode, int userId, int deptId, int detailObjId)
        {

            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return true;
            }
            else
            {

                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = Db.Set<ProjectManager>().AsNoTracking().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == detailObjId);
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return true;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var datainfo = Db.Set<ProjectManager>().Find(detailObjId);
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;
                }
                return false;
            }

        }

        /// <summary>
        //获取删除项目权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetProjectDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
            var predicate = PredicateBuilder.True<ProjectManager>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                permiss.Code = 0;
                return permiss;
            }
            else
            {
                var permiss = new PermissionDataInfo();
                var querylist = Db.Set<ProjectManager>().AsEnumerable().Where(a => listdelIds.Contains(a.Id)).ToList();
                var listnot = querylist.Where(a => a.Pstate != (int)ProjStateEnum.Unexecuted
                || (a.Pstate == (byte)ProjStateEnum.Unexecuted && (a.WfState ?? 0) != 0)).Select(a => a.Name).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);


                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser != null && (a.CreateUser.DepartmentId == deptId)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;


                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;

            }


        }
        /// <summary>
        /// 项目查看页面按钮权限
        /// </summary>
        /// <param name="Id">当前对象</param>
        /// <returns></returns>
        public BtnPremissionInfo GetProjBtnPermission(int Id)
        {


            BtnPremissionInfo btnPremission = new BtnPremissionInfo();
            var datainfo = Db.Set<ProjectManager>().AsNoTracking().FirstOrDefault(a => a.Id == Id);
            if (datainfo != null)
            {

                //修改按钮
                if (datainfo.Pstate == 0 && (datainfo.WfState ?? 0) == 0)
                {
                    btnPremission.Update = 1;
                }
                //删除按钮
                if (datainfo.Pstate == 0 && (datainfo.WfState ?? 0) == 0)
                {
                    btnPremission.Delete = 1;
                }
            }

            return btnPremission;

        }
        #endregion  项目
        #region 合同权限
        /// <summary>
        /// 获取合同列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// 权限类型：
        /// 1类：4是/5否
        /// 2类：1个人、2机构、3全部、6本机构、7本机构及子机构,
        /// 如果一个人拥有权限基本多种，取权限范围最大值：
        /// 1<6<2<7<3;4，5只有新建之类的才有
        /// <returns>合同权限表达式树</returns>
        public Expression<Func<ContractInfo, bool>> GetContractListPermissionExpression(string funcCode, int userId, int deptId = 0)
        {
            var predicate = PredicateBuilder.True<ContractInfo>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                 predicate = PredicateBuilder.True<ContractInfo>();
                //查询对应角色
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<ContractInfo>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<ContractInfo>();
                    predicate2 = predicate2.Or(p => p.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
                return predicate;
            }

        }
        /// <summary>
        //获取删除合同权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetContractDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
            var predicate = PredicateBuilder.True<ContractInfo>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                permiss.Code = 0;
                return permiss;
            }
            else
            {

                var permiss = new PermissionDataInfo();
                //var querylist = Db.Set<ContractInfo>().Where(a => listdelIds.Contains(a.Id)).ToList();
                var querylist = Db.Set<ContractInfo>().Where(a => listdelIds.Any(p => p == a.Id)).ToList();
                //var listnot = querylist.Where(a => a.ContState != (int)ContractState.NonExecution&& a.ContState != (int)ContractState.Dozee).Select(a => a.Name).ToList();
                var listnot = querylist.Where(a => a.ContState != (byte)ContractState.NonExecution
                || (a.ContState == (byte)ContractState.NonExecution && (a.WfState ?? 0) != 0)).Select(a => a.Name).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);


                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser != null && (a.CreateUser.DepartmentId == deptId)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;


                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;
            }
        }
        /// <summary>
        /// 判断当前用户是否有修改合同次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetContractSecFieldUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ContractInfo>();
            if (userId == -10000)
            {//超级管理员
               
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<ContractInfo>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        /// 判断当前用户是否有修改合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetContractUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {

            var predicate = PredicateBuilder.True<ContractInfo>();
            if (userId == -10000)
            {//超级管理员

                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<ContractInfo>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);
                                                                                                                           //if (datainfo == null ||


                if (datainfo == null
                || (datainfo.ContState == (byte)ContractState.NonExecution && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                || (datainfo.ContState == (byte)ContractState.Approve)
                || (datainfo.ContState == (byte)ContractState.Approve && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }
        /// <summary>
        /// 判断合同按钮权限
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <returns>{Change:1,Update:1,Delete:0}=>1：标识允许，0：标识不允许</returns>
        public BtnPremissionInfo GetContractBtnPermission(int Id)
        {

            BtnPremissionInfo btnPremission = new BtnPremissionInfo();
            var datainfo = Db.Set<ContractInfo>().AsNoTracking().FirstOrDefault(a => a.Id == Id);
            if (datainfo != null)
            {
                //变更按钮
                if (datainfo.ContState == (int)ContractState.Execution
                     || (datainfo.ContState == 0 && datainfo.ModificationTimes > 0))
                {
                    btnPremission.Change = 1;
                }
                //修改按钮
                if (datainfo.ContState == (int)ContractState.NonExecution
                    && (datainfo.ModificationTimes ?? 0) <= 0 && (datainfo.WfState ?? 0) == 0)
                {
                    btnPremission.Update = 1;
                }
                //删除按钮
                if (datainfo.ContState == (int)ContractState.NonExecution
                    && (datainfo.ModificationTimes ?? 0) <= 0 && (datainfo.WfState ?? 0) == 0)
                {
                    btnPremission.Delete = 1;
                }
            }

            return btnPremission;
        }

        /// <summary>
        /// 判断当前用户是否有新建合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetContractAddPermission(string funcCode, int userId)
        {
          
          
                List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
                return listFuntypes.Contains(4);
           
        }
        /// <summary>
        /// 判断当前用户是否有查看合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="detailObjId">修改数据的ID</param>
        /// <returns>True：有权限，False：没权限</returns>
        public bool GetContractDetailPermission(string funcCode, int userId, int deptId, int detailObjId)
        {
            var predicate = PredicateBuilder.True<ContractInfo>();
            if (userId == -10000)
            {//超级管理员
                return true;
            }
            else
            {
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(1))
                {//个人
                    var datainfo = Db.Set<ContractInfo>().Find(detailObjId);
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(detailObjId);
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.DeptId == deptId)
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }

                }
                return false;
            }
        }
        #endregion
        #region 计划资金
        /// <summary>
        /// 计划资金列表权限
        /// </summary>
        /// <param name="funCode">功能标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属部门ID</param>
        /// <returns>计划资金权限表达式</returns>
        public Expression<Func<ContPlanFinance, bool>> GetFinanceListPermissionExpression(string funCode, int userId, int deptId)
        {
            var predicate = PredicateBuilder.True<ContPlanFinance>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                var perssion = GetPermission(funCode, userId);
               // var predicate = PredicateBuilder.True<ContPlanFinance>();

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<ContPlanFinance>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.Cont.CreateUser == null ? 0 : p.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.Cont.CreateUser == null ? 0 : p.Cont.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.Cont.CreateUser == null ? 0 : p.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    bool s = false;
                    predicatedept = predicatedept.Or(p => p.Cont == null ? s : p.Cont.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.Cont == null ? s : p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    bool s = false;
                    var predicate2 = PredicateBuilder.False<ContPlanFinance>();
                    predicate2 = predicate2.Or(p => p.Cont == null ? s : p.Cont.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.Cont == null ? s : p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }


                return predicate;
            }
        }
        #endregion
        #region 合同文本
        /// <summary>
        /// 合同文本列表权限
        /// </summary>
        /// <param name="funCode">功能标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属部门ID</param>
        /// <returns>合同文本权限达式</returns>
        public Expression<Func<ContText, bool>> GetContractTextListPermissionExpression(string funCode, int userId, int deptId)
        {
            var predicate = PredicateBuilder.True<ContText>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                var perssion = GetPermission(funCode, userId);
               // var predicate = PredicateBuilder.True<ContText>();

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<ContText>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.Cont.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.Cont.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<ContText>();
                    predicate2 = predicate2.Or(p => p.Cont.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }


                return predicate;
            }
        }
        /// <summary>
        /// 盖章权限
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ContTextSealPermission(string funCode, int userId)
        {
            if (userId == -10000)
            {//超级管理员
                return true;
            }
            else
            {
                List<byte?> listFuntypes = GetPermissionTypes(funCode, userId);
                return listFuntypes.Contains(4);
            }
        }
        /// <summary>
        /// 归档权限
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ContTextArchivePermission(string funCode, int userId)
        {
            if (userId == -10000)
            {//超级管理员

                return true;
            }
            else
            {
                List<byte?> listFuntypes = GetPermissionTypes(funCode, userId);
                return listFuntypes.Contains(4);
            }
        }
        #endregion
        #region 发票权限
        /// <summary>
        /// 发票列表列表权限
        /// </summary>
        /// <param name="funCode">功能标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属部门ID</param>
        /// <returns>发票限表达式</returns>
        public Expression<Func<ContInvoice, bool>> GetInvoiceListPermissionExpression(string funCode, int userId, int deptId)
        {
            var predicate = PredicateBuilder.True<ContInvoice>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                var perssion = GetPermission(funCode, userId);
               // var predicate = PredicateBuilder.True<ContInvoice>();

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<ContInvoice>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.Cont.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.Cont.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<ContInvoice>();
                    predicate2 = predicate2.Or(p => p.Cont.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }


                return predicate;
            }
        }


        /// <summary>
        /// 建立发票权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="contId">合同ID</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetAddUpdateInvoicePermission(string funcCode, int userId, int deptId, int contId)
        {
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(contId);
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.DeptId == deptId)
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(contId);
                        if (listchiddeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(contId);
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;
                }
                return false;
        }


        /// <summary>
        ///删除发票权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetDeleteInvoicePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
            var permiss = new PermissionDataInfo();
            if (userId == -10000)
            {//超级管理员
              permiss.Code = 0;
                return permiss;
            }
            else
            {
               
                var querylist = this.Db.Set<ContInvoice>().Where(a => listdelIds.Any(p => p == a.Id)).ToList();
                var listnot = querylist.Where(a => a.InState != (int)InvoiceStateEnum.Uncommitted).Select(a => a.InCode).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);


                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser != null && (a.CreateUser.DepartmentId == deptId)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.InCode).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;


                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.InCode).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;
            }
        }


        /// <summary>
        ///确认打回发票权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>True/False</returns>
        public bool GetConfirmBackInvoicePermission(string funcCode, int userId, int deptId, int contId)
        {

            if (userId == -10000)
            {//超级管理员

                return true;
            }
            else
            {
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(contId);
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.DeptId == deptId)
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(contId);
                        if (listchiddeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(contId);
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;
                }
                return false;
            }
        }
        #endregion
        #region 实际资金权限
        /// <summary>
        ///实际资金列表列表权限
        /// </summary>
        /// <param name="funCode">功能标识</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deptId">用户所属部门ID</param>
        /// <returns>实际资金权限表达式</returns>
        public Expression<Func<ContActualFinance, bool>> GetActFinanceListPermissionExpression(string funCode, int userId, int deptId)
        {
            var predicate = PredicateBuilder.True<ContActualFinance>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                var perssion = GetPermission(funCode, userId);
              //  var predicate = PredicateBuilder.True<ContActualFinance>();

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<ContActualFinance>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains((p.Cont != null && p.Cont.CreateUser != null) ? p.Cont.CreateUser.DepartmentId ?? -100 : 0));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.Cont.CreateUser == null ? 0 : p.Cont.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.Cont.CreateUser == null ? 0 : p.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    bool s = false;
                    predicatedept = predicatedept.Or(p => p.Cont == null ? s : p.Cont.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.Cont == null ? s : p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    bool s = false;
                    var predicate2 = PredicateBuilder.False<ContActualFinance>();
                    predicate2 = predicate2.Or(p => p.Cont == null ? s : p.Cont.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.Cont == null ? s : p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
                return predicate;
            }
        }

        /// <summary>
        /// 建立/修改实际资金权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="contId">合同ID</param>
        /// <param name="actId">实际资金ID </param>
        /// <param name="deptId">部门ID</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetAddUpdateActFinancePermission(string funcCode, int userId, int deptId, int contId, int actId)
        {
            var predicate = PredicateBuilder.True<ContActualFinance>();
            if (userId == -10000&& actId > 0)
            {//超级管理员
             //   predicate = predicate.And(p => true);
                return true;
            }
            else
            {
                if (actId > 0)
                {//修改时判断
                    var actInfo = Db.Set<ContActualFinance>().AsNoTracking().Where(a => a.Id == actId).FirstOrDefault();
                    if (actInfo == null
                                   || (actInfo.Astate == (byte)ActFinanceStateEnum.Uncommitted && (actInfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && actInfo.WfState != (int)WfStateEnum.BeiDaHui)
                                   || (actInfo.Astate == (byte)ActFinanceStateEnum.Approved)
                                   || (actInfo.Astate == (byte)ActFinanceStateEnum.Approved && (actInfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && actInfo.WfState != (int)WfStateEnum.BeiDaHui)
                                   )
                    {
                        return false;
                    }
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(contId);
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.DeptId == deptId)
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(contId);
                        if (listchiddeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(contId);
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;
                }
                return false;
            }
        }

        /// <summary>
        ///删除实际资金权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetDeleteActFinancePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
            var permiss = new PermissionDataInfo();
            if (userId == -10000 )
            {//超级管理员
             //   predicate = predicate.And(p => true);
                permiss.Code = 0;
                return permiss;
            }
            else
            {
               
                var querylist = Db.Set<ContActualFinance>().AsEnumerable().Where(a => listdelIds.Contains(a.Id)).ToList();
                var listnot = querylist.Where(a => a.Astate != (int)ActFinanceStateEnum.Uncommitted).Select(a => a.AmountMoney.ToString()).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);


                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构  //p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100)
                        var temIds2 = querylist.Where(a => (a.CreateUser == null ? 0 : a.CreateUser.DepartmentId) == deptId).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.AmountMoney.ToString()).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;


                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.AmountMoney.ToString()).ToList();
                    if (listnotdeptdata.Any())
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;
            }
        }

        /// <summary>
        ///确认打回实际资金权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>True/False</returns>
        public bool GetConfirmBackActFinancePermission(string funcCode, int userId, int deptId, int contId)
        {
            if (userId == -10000)
            {//超级管理员
             //   predicate = predicate.And(p => true);
                return true;
            }
            else
            {
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(contId);
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.DeptId == deptId)
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(contId);
                        if (listchiddeptIds.Contains(datainfo.DeptId ?? -100))
                        {
                            return true;
                        }
                    }
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var datainfo = Db.Set<ContractInfo>().Find(contId);
                    if (datainfo == null)
                    {
                        var datainfoy = Db.Set<ContActualFinance>().Find(contId);
                        return datainfoy.CreateUserId == userId || datainfoy.CreateUserId == userId;
                    }
                    return datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId;
                }
                return false;
            }
        }
        #endregion
        #region 标的相关权限

        /// <summary>
        /// 标的权限
        /// </summary>
        /// <param name="funCode">功能码</param>
        /// <param name="userId">当前登录用户ID</param>
        /// <param name="deptId">当前所属部门ID</param>
        /// <returns>标的权限表达式</returns>
        public Expression<Func<ContSubjectMatter, bool>> GetListSubjectMatterPermissionExpression(string funCode, int userId, int deptId)
        {
            var predicate = PredicateBuilder.True<ContSubjectMatter>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                var perssion = GetPermission(funCode, userId);
              //   predicate = PredicateBuilder.True<ContSubjectMatter>();

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<ContSubjectMatter>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains((p.Cont != null && p.Cont.CreateUser != null) ? (p.Cont.CreateUser.DepartmentId ?? -100) : -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => ((p.Cont != null && p.Cont.CreateUser != null) ? (p.Cont.CreateUser.DepartmentId ?? -100) : -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.Cont != null && p.Cont.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.Cont != null && p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<ContSubjectMatter>();
                    predicate2 = predicate2.Or(p => p.Cont != null && p.Cont.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.Cont != null && p.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
                return predicate;
            }
        }

        /// <summary>
        /// 标的明细权限
        /// </summary>
        /// <param name="funCode">功能码</param>
        /// <param name="userId">当前登录用户ID</param>
        /// <param name="deptId">当前所属部门ID</param>
        /// <returns>标的明细权限表达式</returns>
        public Expression<Func<ContSubDe, bool>> GetListSubDesPermissionExpression(string funCode, int userId, int deptId)
        {
            var predicate = PredicateBuilder.True<ContSubDe>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                var perssion = GetPermission(funCode, userId);
                //var predicate = PredicateBuilder.True<ContSubDe>();
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<ContSubDe>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.Sub.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.Sub.Cont.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.Sub.Cont.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.Sub.Cont.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.Sub.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<ContSubDe>();
                    predicate2 = predicate2.Or(p => p.Sub.Cont.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.Sub.Cont.PrincipalUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
                return predicate;
            }
        }
        #endregion


        #region 招标权限 (添加-删除-修改)
        /// <summary>
        /// 招标添加权限
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool GetaddTenderAddPermission(string funcCode, int userId)
        {
         
          
                List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
                return listFuntypes.Contains(4);
           
        }
        /// <summary>
        /// 招标修改
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="updateObjId"></param>
        /// <returns></returns>
        public PermissionDicEnum GetTenderInforUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
           
            if (userId == -10000)
            {//超级管理员
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<TenderInfor>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);
                if (datainfo == null
                || (datainfo.TenderStatus == (byte)ContractState.NonExecution && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                || (datainfo.TenderStatus == (byte)ContractState.Approve)
                || (datainfo.TenderStatus == (byte)ContractState.Approve && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        //获取删招标权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetTenderInforDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
           
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                permiss.Code = 0;
                return permiss;
            }
            else
            {
                var permiss = new PermissionDataInfo();
                var querylist = Db.Set<TenderInfor>().Where(a => listdelIds.Any(p => p == a.Id)).ToList();
                //     var querylist = Db.Set<Inquiry>().Where(a => listdelIds.Contains(a.Id)).ToList();
                //var listnot = querylist.Where(a => a.ContState != (int)ContractState.NonExecution&& a.ContState != (int)ContractState.Dozee).Select(a => a.Name).ToList();
                var listnot = querylist.Where(a => a.TenderStatus != (byte)ContractState.NonExecution
                || (a.TenderStatus == (byte)ContractState.NonExecution && (a.WfState ?? 0) != 0)).Select(a => a.ProjectNo).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser.DepartmentId == deptId).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();
                    //    var temIds4 = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.ProjectNo).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();//
                                                                                                                // var usertempIds = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.ProjectNo).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;
            }
        }
        #endregion
        #region 询价权限 (添加-删除-修改)
        /// <summary>
        /// 询价添加权限
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool GetInquiryAddPermission(string funcCode, int userId)
        {
           
                List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
                return listFuntypes.Contains(4);
           
        }
        /// <summary>
        /// 询价修改
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="updateObjId"></param>
        /// <returns></returns>
        public PermissionDicEnum GetInquiryUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            if (userId == -10000)
            {//超级管理员
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<Inquiry>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);
                                                                                                                      //if (datainfo == null ||
                if (datainfo == null
                || (datainfo.InState == (byte)ContractState.NonExecution && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                || (datainfo.InState == (byte)ContractState.Approve)
                || (datainfo.InState == (byte)ContractState.Approve && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        //获取删除询价权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetInquiryDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
            var predicate = PredicateBuilder.True<Inquiry>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                permiss.Code = 0;
                return permiss;
            }
            else
            {

                var permiss = new PermissionDataInfo();
                var querylist = Db.Set<Inquiry>().Where(a => listdelIds.Any(p => p == a.Id)).ToList();
                //     var querylist = Db.Set<Inquiry>().Where(a => listdelIds.Contains(a.Id)).ToList();
                //var listnot = querylist.Where(a => a.ContState != (int)ContractState.NonExecution&& a.ContState != (int)ContractState.Dozee).Select(a => a.Name).ToList();
                var listnot = querylist.Where(a => a.InState != (byte)ContractState.NonExecution
                || (a.InState == (byte)ContractState.NonExecution && (a.WfState ?? 0) != 0)).Select(a => a.ProjectNumber).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser.DepartmentId == deptId).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();
                    //    var temIds4 = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.ProjectNumber).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();//
                                                                                                                // var usertempIds = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.ProjectNumber).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;
            }
        }
        #endregion
        #region 约谈权限(添加-删除-修改)
        /// <summary>
        /// 约谈添加权限
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool GetQuestioningAddPermission(string funcCode, int userId)
        {
           
                List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
                return listFuntypes.Contains(4);
            
        }

        /// <summary>
        /// 约谈修改
        /// </summary>
        /// <param name="funcCode"></param>
        /// <param name="userId"></param>
        /// <param name="deptId"></param>
        /// <param name="updateObjId"></param>
        /// <returns></returns>
        public PermissionDicEnum GetQuestioningUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            if (userId == -10000)
            {//超级管理员
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<Questioning>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);
                                                                                                                          //if (datainfo == null ||


                if (datainfo == null
                || (datainfo.InState == (byte)ContractState.NonExecution && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                || (datainfo.InState == (byte)ContractState.Approve)
                || (datainfo.InState == (byte)ContractState.Approve && (datainfo.WfState ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.WfState != (int)WfStateEnum.BeiDaHui)
                )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;


            }
        }
        /// <summary>
        //获取删除约谈权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetQuestioningDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
            var predicate = PredicateBuilder.True<Questioning>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                permiss.Code = 0;
                return permiss;
            }
            else
            {
                var permiss = new PermissionDataInfo();
                var querylist = Db.Set<Questioning>().Where(a => listdelIds.Any(p => p == a.Id)).ToList();
                //     var querylist = Db.Set<Inquiry>().Where(a => listdelIds.Contains(a.Id)).ToList();
                //var listnot = querylist.Where(a => a.ContState != (int)ContractState.NonExecution&& a.ContState != (int)ContractState.Dozee).Select(a => a.Name).ToList();
                var listnot = querylist.Where(a => a.InState != (byte)ContractState.NonExecution
                || (a.InState == (byte)ContractState.NonExecution && (a.WfState ?? 0) != 0)).Select(a => a.ProjectNumber).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser.DepartmentId == deptId).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();
                    //    var temIds4 = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.ProjectNumber).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId).Select(a => a.Id).ToList();//
                                                                                                                // var usertempIds = querylist.Where(a => a.CreateUserId == userId || a.PrincipalUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.ProjectNumber).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;
            }
        }
        #endregion

        #region 案件管理
        /// <summary>
        /// 获取合同列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// 权限类型：
        /// 1类：4是/5否
        /// 2类：1个人、2机构、3全部、6本机构、7本机构及子机构,
        /// 如果一个人拥有权限基本多种，取权限范围最大值：
        /// 1<6<2<7<3;4，5只有新建之类的才有
        /// <returns>合同权限表达式树</returns>
        public Expression<Func<CaseManager, bool>> GetAjglListPermissionExpression(string funcCode, int userId, int deptId = 0)
        {
            var predicate = PredicateBuilder.True<CaseManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                predicate = PredicateBuilder.True<CaseManager>();
                //查询对应角色
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<CaseManager>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.ContId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<CaseManager>();
                    predicate2 = predicate2.Or(p => p.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.ContId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
                return predicate;
            }

        }
        /// <summary>
        //获取删除合同权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetAjglDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
            var predicate = PredicateBuilder.True<CaseManager>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                permiss.Code = 0;
                return permiss;
            }
            else
            {

                var permiss = new PermissionDataInfo();
                //var querylist = Db.Set<CaseManager>().Where(a => listdelIds.Contains(a.Id)).ToList();
                var querylist = Db.Set<CaseManager>().Where(a => listdelIds.Any(p => p == a.Id)).ToList();
                //var listnot = querylist.Where(a => a.ContState != (int)ContractState.NonExecution&& a.ContState != (int)ContractState.Dozee).Select(a => a.Name).ToList();
                var listnot = querylist.Where(a => a.Dstate == (byte)AjglState.Dozee
                || (a.Dstate == (byte)AjglState.Approvaling)).Select(a => a.Name).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);


                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser != null && (a.CreateUser.DepartmentId == deptId)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId || a.ContId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;


                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId || a.ContId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;
            }
        }
        /// <summary>
        /// 判断当前用户是否有修改合同次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetAjglSecFieldUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredicateBuilder.True<ContractInfo>();
            if (userId == -10000)
            {//超级管理员

                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<ContractInfo>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    if (datainfo.CreateUserId == userId || datainfo.PrincipalUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        /// 判断当前用户是否有修改合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetAjglUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {

            var predicate = PredicateBuilder.True<CaseManager>();
            if (userId == -10000)
            {//超级管理员

                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<CaseManager>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);
                                                                                                                           //if (datainfo == null ||


                if (datainfo == null
                || (datainfo.Dstate == (byte)AjglState.Dozee)
                || (datainfo.Dstate == (byte)AjglState.Approvaling)
               
                )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId || datainfo.ContId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }
    

        /// <summary>
        /// 判断当前用户是否有新建合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetAjglAddPermission(string funcCode, int userId)
        {


            List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
            return listFuntypes.Contains(4);

        }
        /// <summary>
        /// 判断当前用户是否有查看合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="detailObjId">修改数据的ID</param>
        /// <returns>True：有权限，False：没权限</returns>
        public bool GetAjglDetailPermission(string funcCode, int userId, int deptId, int detailObjId)
        {
            var predicate = PredicateBuilder.True<CaseManager>();
            if (userId == -10000)
            {//超级管理员
                return true;
            }
            else
            {
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(1))
                {//个人
                    var datainfo = Db.Set<CaseManager>().Find(detailObjId);
                    return datainfo.CreateUserId == userId || datainfo.ContId == userId;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = Db.Set<CaseManager>().Find(detailObjId);
                    //if (perssion.listFuntypes.Contains(2))
                    //{//机构
                    // return true;
                    //    //var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                    //    //if (listdeptIds.Contains(datainfo.DeptId ?? -100))
                    //    //{
                    //    //return true;
                    //    //}
                    //}
                    //if (perssion.listFuntypes.Contains(6))
                    //{//本机构
                    //    if (datainfo.DeptId == deptId)
                    //    {
                    //        return true;
                    //    }
                    //}
                    //if (perssion.listFuntypes.Contains(7))
                    //{ //本机构及子机构
                    //    var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                    //    if (listchiddeptIds.Contains(datainfo.DeptId ?? -100))
                    //    {
                    //        return true;
                    //    }
                    //}

                }
                return false;
            }
        }

           public PermissionDicEnum UpdateAjgl(string funcCode, int userId, int deptId, int updateObjId)
        {
          
            if (userId == -10000)
            {//超级管理员
             
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<CaseManager>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId);
                var perssion = GetPermission(funcCode, userId);

                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    if (datainfo.CreateUserId == userId || datainfo.ContId == userId)
                        return PermissionDicEnum.OK;
                }
                return PermissionDicEnum.None;
            }
        }
        #endregion

   
        #region 纠纷上报
        /// <summary>
        /// 获取合同列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// 权限类型：
        /// 1类：4是/5否
        /// 2类：1个人、2机构、3全部、6本机构、7本机构及子机构,
        /// 如果一个人拥有权限基本多种，取权限范围最大值：
        /// 1<6<2<7<3;4，5只有新建之类的才有
        /// <returns>合同权限表达式树</returns>
        public Expression<Func<DisputeManager, bool>> GetJfsbListPermissionExpression(string funcCode, int userId, int deptId = 0)
        {
            var predicate = PredicateBuilder.True<DisputeManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                predicate = PredicateBuilder.True<DisputeManager>();
                //查询对应角色
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<DisputeManager>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.HandlerId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<DisputeManager>();
                    predicate2 = predicate2.Or(p => p.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.HandlerId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
                return predicate;
            }

        }

        public Expression<Func<DisputeManager, bool>> GetJfsb1ListPermissionExpression(string funcCode, int userId, int deptId = 0)
        {
            var predicate = PredicateBuilder.True<DisputeManager>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(p => true);
                return predicate;
            }
            else
            {
                predicate = PredicateBuilder.True<DisputeManager>();
                //查询对应角色
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var predicatedept = PredicateBuilder.False<DisputeManager>();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100) == deptId);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.CreateUser == null ? 0 : p.CreateUser.DepartmentId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.CreateUserId == userId);
                    predicatedept = predicatedept.Or(p => p.HandlerId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredicateBuilder.False<DisputeManager>();
                    predicate2 = predicate2.Or(p => p.CreateUserId == userId);
                    predicate2 = predicate2.Or(p => p.HandlerId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
                return predicate;
            }

        }

        /// <summary>
        //获取删除合同权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetJfsbDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {
            var predicate = PredicateBuilder.True<DisputeManager>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                permiss.Code = 0;
                return permiss;
            }
            else
            {

                var permiss = new PermissionDataInfo();
                //var querylist = Db.Set<DisputeManager>().Where(a => listdelIds.Contains(a.Id)).ToList();
                var querylist = Db.Set<DisputeManager>().Where(a => listdelIds.Any(p => p == a.Id)).ToList();
                //var listnot = querylist.Where(a => a.ContState != (int)ContractState.NonExecution&& a.ContState != (int)ContractState.Dozee).Select(a => a.Name).ToList();
                var listnot = querylist.Where(a => a.Dstate != (byte)AjglState.NonExecution
                || (a.Dstate == (byte)AjglState.NonExecution)).Select(a => a.Name).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);


                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.CreateUser != null && (a.CreateUser.DepartmentId == deptId)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.CreateUser.DepartmentId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.CreateUserId == userId || a.ContId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;


                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.CreateUserId == userId || a.ContId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;
            }
        }

        /// <summary>
        /// 判断当前用户是否有修改合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetJfsbUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {

            var predicate = PredicateBuilder.True<DisputeManager>();
            if (userId == -10000)
            {//超级管理员

                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = Db.Set<DisputeManager>().Include(a => a.CreateUser).FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);
                                                                                                                             //if (datainfo == null ||


             //   if (datainfo == null
             //   || (datainfo.Dstate == (byte)AjglState.NonExecution)
             //|| (datainfo.Dstate == (byte)AjglState.NonExecution)
             //|| (datainfo.Dstate == (byte)AjglState.NonExecution)
             //|| (datainfo.Dstate == (byte)AjglState.NonExecution)
             //   )
             //   {
             //       return PermissionDicEnum.NotState;
             //   }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions, pession.UserPermissions);
                        if (listdeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.CreateUser.DepartmentId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.CreateUser.DepartmentId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.CreateUserId == userId || datainfo.ContId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }


        /// <summary>
        /// 判断当前用户是否有新建合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetJfsbAddPermission(string funcCode, int userId)
        {


            List<byte?> listFuntypes = GetPermissionTypes(funcCode, userId);
            return listFuntypes.Contains(4);

        }
        /// <summary>
        /// 判断当前用户是否有查看合同的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="detailObjId">修改数据的ID</param>
        /// <returns>True：有权限，False：没权限</returns>
        public bool GetJfsbDetailPermission(string funcCode, int userId, int deptId, int detailObjId)
        {
            var predicate = PredicateBuilder.True<DisputeManager>();
            if (userId == -10000)
            {//超级管理员
                return true;
            }
            else
            {
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(1))
                {//个人
                    var datainfo = Db.Set<DisputeManager>().Find(detailObjId);
                    return datainfo.CreateUserId == userId || datainfo.ContId == userId;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    return true;
                    //var datainfo = Db.Set<DisputeManager>().Find(detailObjId);
                    //if (perssion.listFuntypes.Contains(2))
                    //{//机构
                    //    return true;
                    //    //var listdeptIds = GetDeptIds(perssion.RolePermissions, perssion.UserPermissions);
                    //    //if (listdeptIds.Contains(datainfo.DeptId ?? -100))
                    //    //{
                    //    //return true;
                    //    //}
                    //}
                    //if (perssion.listFuntypes.Contains(6))
                    //{//本机构
                    //    if (datainfo.DeptId == deptId)
                    //    {
                    //        return true;
                    //    }
                    //}
                    //if (perssion.listFuntypes.Contains(7))
                    //{ //本机构及子机构
                    //    var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                    //    if (listchiddeptIds.Contains(datainfo.DeptId ?? -100))
                    //    {
                    //        return true;
                    //    }
                    //}

                }
                return false;
            }
        }
        #endregion


    }
}
