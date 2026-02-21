using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 组
    /// </summary>
    public partial class GroupInfoService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<GroupInfo>();
            //不等于fieldInfo.CurrId是排除修改的时候
             predicateAnd = predicateAnd.And(a=>a.Id != fieldInfo.Id);
            switch (fieldInfo.FieldName)
            {
                case "Name":
                    predicateAnd = predicateAnd.And(a => a.Name == fieldInfo.FieldValue);
                    break;


            }
            return GetQueryable(predicateAnd).AsNoTracking().Any();

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update GroupInfo set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }

        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public GroupInfoViewDTO ShowView(int Id)
        {
            var query = from a in _GroupInfoSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId
                        };
            var local = from tempInfo in query.AsEnumerable()

                        select new GroupInfoViewDTO
                        {
                            Id = tempInfo.Id,
                            Name = tempInfo.Name,
                            Remark = tempInfo.Remark,
                            CreateDateTime = tempInfo.CreateDateTime,
                            CreateUserName = this.Db.GetRedisUserFieldValue(tempInfo.CreateUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{tempInfo.CreateUserId}", "DisplyName")
                        };
            return local.FirstOrDefault();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns></returns>
        public LayPageInfo<GroupInfoDTO> GetList(PageInfo<GroupInfo> pageInfo, Expression<Func<GroupInfo, bool>> whereLambda)
        {
            //_GroupInfoSet
          //  var tempquery = Db.Set<GroupInfo>().AsNoTracking().Where<GroupInfo>(whereLambda.Compile()).AsQueryable()
                    var tempquery = _GroupInfoSet.AsNoTracking().Where<GroupInfo>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<GroupInfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize)
                 .Take<GroupInfo>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Gstate=a.Gstate
                            
                        };
            var local = from a in query.AsEnumerable()
                        select new GroupInfoDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Gstate = a.Gstate,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName")
                        };
            return new LayPageInfo<GroupInfoDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                case "Gstate"://状态
                    var state = Convert.ToByte(info.FieldValue);
                    sqlstr = $"update  GroupInfo set Gstate={state} where Id={info.Id}";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }
        /// <summary>
        /// 选择查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件表达式</param>
        /// <returns></returns>
        public LayPageInfo<SelectGroupList> SelectGroupList(PageInfo<GroupInfo> pageInfo, Expression<Func<GroupInfo, bool>> whereLambda)
        {
            var tempquery = Db.Set<GroupInfo>().AsNoTracking().Where<GroupInfo>(whereLambda.Compile());
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<GroupInfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize)
                 .Take<GroupInfo>(pageInfo.PageSize);
            IList<UserTemp> listUser = Db.Set<UserInfor>().Where(a => a.IsDelete != 1).Select(a => new UserTemp {
              Id=  a.Id,Name= a.Name,DisplyName= a.DisplyName
            }).ToList();
            IList<GroupUser> groupUsers = Db.Set<GroupUser>().ToList();


            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            Gstate = a.Gstate

                        };
            var local = from a in query.AsEnumerable()
                        select new SelectGroupList
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            Gstate = a.Gstate,
                            UserNames= GetGroupUsers(a.Id, listUser, groupUsers)

                        };
            return new LayPageInfo<SelectGroupList>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }
        /// <summary>
        /// 返回组里所有用户
        /// </summary>
        /// <returns></returns>
        private string GetGroupUsers(int groupId, IList<UserTemp> listUser,IList<GroupUser> groups)
        {

            var userIds = groups.Where(a => a.GroupId == groupId).Select(a => a.UserId).ToList();
            var userNames = listUser.Where(a => userIds.Contains(a.Id)).Select(a => a.DisplyName).ToList();
            return StringHelper.ArrayString2String(userNames);
        }


        /// <summary>
        /// 选择查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件表达式</param>
        /// <returns></returns>
        public LayPageInfo<SelectGroupusinfoList> SelectGroupUsinfoList(PageInfo<GroupInfo> pageInfo, Expression<Func<GroupInfo, bool>> whereLambda,int Zid)
        {
            var tempquery = Db.Set<GroupInfo>().AsNoTracking().Where<GroupInfo>(whereLambda.Compile());
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<GroupInfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize)
                 .Take<GroupInfo>(pageInfo.PageSize);
            IList<UserTemp> listUser = Db.Set<UserInfor>().Where(a => a.IsDelete != 1).Select(a => new UserTemp
            {
                Id = a.Id,
                Name = a.Name,
                DisplyName = a.DisplyName
            }).ToList();
            IList<GroupUser> groupUsers = Db.Set<GroupUser>().ToList();


            var sd = tempquery.Where(a => a.Id == Zid).FirstOrDefault();

            var userIds = groupUsers.Where(a => a.GroupId == Zid).Select(a => a.UserId).ToList();
            var userNames = listUser.Where(a => userIds.Contains(a.Id)).ToList();
            var query = from a in userNames
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            //Remark = a.Remark,
                            //Gstate = a.Gstate

                        };
            var local = from a in query.AsEnumerable()
                        select new SelectGroupusinfoList
                        {
                            Id = a.Id,
                            Name = a.Name,
                            //Remark = a.Remark,
                            //Gstate = a.Gstate,
                            GroupNames = sd.Name,
                            GroupId=sd.Id,

                        };
            return new LayPageInfo<SelectGroupusinfoList>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }
    }
}
