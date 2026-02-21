using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 印章管理
    /// </summary>
    public partial  class SealManagerService
    {
        /// <summary>
        /// 印章大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<SealManagerViewDTO> GetList<s>(PageInfo<SealManager> pageInfo
            , Expression<Func<SealManager, bool>> whereLambda, Expression<Func<SealManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            MainDeptId=a.MainDeptId,
                            SealName=a.SealName,
                            SealCode= a.SealCode,
                            UserId =a.UserId,
                            DeptId=a.DeptId,
                            EnabledDate=a.EnabledDate,
                            SealState=a.SealState,
                            Remark=a.Remark,
                            CreateUserId=a.CreateUserId,
                            CreateDateTime=a.CreateDateTime


                        };
            var local = from a in query.AsEnumerable()
                        select new SealManagerViewDTO
                        {
                            Id = a.Id,
                            MainDeptId = a.MainDeptId,
                            SealName = a.SealName,
                            SealCode = a.SealCode,
                            UserId = a.UserId,
                            DeptId = a.DeptId,
                            EnabledDate = a.EnabledDate,
                            SealState = a.SealState,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            KeeperUserName = this.Db.GetRedisUserFieldValue(a.UserId??0),
                            //RedisValueUtility.GetUserShowName(a.UserId ?? 0),
                            DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.DeptId??-100)

                        };
            
            return new LayPageInfo<SealManagerViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };




        }
        /// <summary>
        /// 查询计划资金大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<SealManagerViewDTO> GetSelectList<s>(PageInfo<SealManager> pageInfo
            , Expression<Func<SealManager, bool>> whereLambda, Expression<Func<SealManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            MainDeptId = a.MainDeptId,
                            SealName = a.SealName,
                            SealCode = a.SealCode,
                            UserId = a.UserId,
                            DeptId = a.DeptId,
                            EnabledDate = a.EnabledDate,
                            SealState = a.SealState,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime


                        };
            var local = from a in query.AsEnumerable()
                        select new SealManagerViewDTO
                        {
                            Id = a.Id,
                            MainDeptId = a.MainDeptId,
                            SealName = a.SealName,
                            SealCode = a.SealCode,
                            UserId = a.UserId,
                            DeptId = a.DeptId,
                            EnabledDate = a.EnabledDate,
                            SealState = a.SealState,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                           

                        };

            return new LayPageInfo<SealManagerViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };




        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public SealManagerViewDTO ShowView(int Id)
        {
            var query = from a in _SealManagerSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            MainDeptId = a.MainDeptId,
                            SealName = a.SealName,
                            SealCode = a.SealCode,
                            UserId = a.UserId,
                            DeptId = a.DeptId,
                            EnabledDate = a.EnabledDate,
                            SealState = a.SealState,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime


                        };
            var local = from a in query.AsEnumerable()
                        select new SealManagerViewDTO
                        {
                            Id = a.Id,
                            MainDeptId = a.MainDeptId,
                            SealName = a.SealName,
                            SealCode = a.SealCode,
                            UserId = a.UserId,
                            DeptId = a.DeptId,
                            EnabledDate = a.EnabledDate,
                            SealState = a.SealState,
                            Remark = a.Remark,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            KeeperUserName = this.Db.GetRedisUserFieldValue(a.UserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.UserId ?? 0),
                            DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.DeptId ?? -100)
                        };
            return local.FirstOrDefault();
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update SealManager set IsDelete=1 where Id in(" + Ids + ")";
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
                case "SealState"://状态
                    {
                        int state = 0;
                        int.TryParse(info.FieldValue, out state);
                        sqlstr = "update SealManager set SealState=" + state + " where Id=" + info.Id;
                    }
                    break;
               

            }
            
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }
    }
}
