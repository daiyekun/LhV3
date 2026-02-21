using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.Common.Extend;
using NF.ViewModel.Extend.Enums;

namespace NF.BLL
{
    /// <summary>
    /// 角色
    /// </summary>
    public partial class RoleService
    {
        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <param name="pageInfo">角色对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns>角色列表</returns>
        public LayPageInfo<RoleDTO> GetList(PageInfo<Role> pageInfo, Expression<Func<Role, bool>> whereLambda)
        {
            //     var tempquery = _RoleSet.AsTracking().Where<Role>(whereLambda.Compile()).AsQueryable();

            //var tempquery = _ContPlanFinance.Include(a => a.Cont).ThenInclude(a => a.Comp).AsTracking()
            //   .Where<ContPlanFinance>(whereLambda.Compile()).AsQueryable();
            var tempquery = _RoleSet.Include(a=>a.CreateUser).AsTracking().Where<Role>(whereLambda.Compile()).AsQueryable();

            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<Role>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<Role>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            No = a.No,
                            CreateDatetime = a.CreateDatetime,

                            CreateUserName = a.CreateUser.Name==null?"": a.CreateUser.Name,
                           // CreateUserName = a.CreateUser.Name,
                            Rstate = a.Rstate,
                            Remark = a.Remark,
                        };
           var ssd= query.ToList();
            var local = from a in query.AsEnumerable()


                        select new RoleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            No = a.No,
                            CreateDatetime = a.CreateDatetime,
                            CreateUserName = a.CreateUserName,
                            Rstate = a.Rstate,
                            Remark = a.Remark,

                        };
            return new LayPageInfo<RoleDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">软删除ID集合</param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update Role set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 修改字段值
        /// </summary>
        /// <param name="info">修改字段新</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                case "Rstate"://状态
                    {
                        int state = 0;
                        int.TryParse(info.FieldValue, out state);
                        sqlstr = "update Role set Rstate=" + state + " where Id=" + info.Id;
                    }
                    break;


            }

            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="info">保存信息实体</param>
        /// <returns>返回当前保存对象</returns>
        public Role SaveInfo(Role info)
        {
            Role resul = null;
            if (info.Id > 0)
            {//修改

                resul = UpdateSave(info);

            }
            else
            {
                resul = AddSave(info);
            }

            return resul;



        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">修改对象</param>
        /// <returns></returns>
        private Role UpdateSave(Role info)
        {
            Role resul;
            var tempinfo = _RoleSet.FirstOrDefault(a => a.Id == info.Id);
            tempinfo.Name = info.Name;
            tempinfo.No = info.No;
            tempinfo.DepartmentId = info.DepartmentId;
            tempinfo.ModifyUserId = info.ModifyUserId;
            tempinfo.Remark = info.Remark;
            resul = tempinfo;
            Db.SaveChanges();
            return resul;
        }
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="info">新增对象</param>
        /// <returns>当前保存对象</returns>
        private Role AddSave(Role info)
        {
            info.CreateDatetime = DateTime.Now;
            info.ModifyDatetime = DateTime.Now;
            info.IsDelete = 0;
            info.Rstate = 1;
            return Add(info);

        }

        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public RoleDTO ShowView(int Id)
        {

            var tempInfo = _RoleSet.Where(a => a.Id == Id).Include(a => a.CreateUser).FirstOrDefault();
            var info = new RoleDTO
            {
                Id = tempInfo.Id,
                Name = tempInfo.Name,
                Rstate = tempInfo.Rstate,
                Remark = tempInfo.Remark,
                CreateDatetime = tempInfo.CreateDatetime.ConvertDate().StringToDate() ?? new DateTime(2018, 1, 1),
                DepartmentId = tempInfo.DepartmentId,
                IsDelete = tempInfo.IsDelete,
                CreateUserId = tempInfo.CreateUserId,
                No = tempInfo.No,


            };
            info.CreateUserName = tempInfo.CreateUser.Name;
            info.RstateDic = EmunUtility.GetDesc(typeof(DataState), tempInfo.Rstate);
            return info;


        }
        /// <summary>
        /// 判断某一字段值是否唯一
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="inputValue">输入值</param>
        /// <param name="Id">修改时的ID</param>
        /// <returns>True表示存在，否则不存在</returns>
        public bool CheckFieldValExist(string fieldName, string inputValue, int? Id)
        {
            bool result = false;
            switch (fieldName)
            {
                case "Name":
                    result = _RoleSet.AsNoTracking().Any(a => a.Name == inputValue && a.IsDelete != 1 && a.Id != Id);
                    break;

            }
            return result;

        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public LayPageInfo<RoleDTO> GetListAll()
        {
            var tempquery = _RoleSet.AsTracking().OrderByDescending(a => a.Id);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            No = a.No,
                            CreateDatetime = a.CreateDatetime,
                            CreateUserName = a.CreateUser.Name,
                            Rstate = a.Rstate,
                            Remark = a.Remark,
                            IsDelete=a.IsDelete,
                        };
            var local = from a in query.AsEnumerable()
                        select new RoleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            No = a.No,
                            CreateDatetime = a.CreateDatetime,
                            CreateUserName = a.CreateUserName,
                            Rstate = a.Rstate,
                            Remark = a.Remark,
                            IsDelete = a.IsDelete,

                        };
            var list = local.ToList();
            return new LayPageInfo<RoleDTO>()
            {
                data = list,
                count = list.Count(),
                code = 0


            };
        }


    }
}
