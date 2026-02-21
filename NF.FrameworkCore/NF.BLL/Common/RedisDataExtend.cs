using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL.Extend
{

    /// <summary>
    /// redis 获取扩展
    /// </summary>
    public static class RedisDataExtend
    {


        #region 用户设置
        /// <summary>
        /// 获取redis 字符串
        /// </summary>
        /// <param name="service">数据库查询服务</param>
        /// <param name="fileName">字段名称</param>
        /// <returns></returns>
        public static string GetRedisUserFieldValue(this DbContext Db, int userId,string fileName= "DisplyName")
        {
            string restr = "";
            switch (fileName)
            {
                default://DisplyName
                    restr = RedisValueUtility.GetUserShowName(userId);
                    if (string.IsNullOrEmpty(restr))
                    {//redis 没有，查询数据库，然后在存入到redis 并返回

                          var redisuser= GetredisUserInfo(Db,userId);
                        //设置redis
                        if (redisuser!=null)
                        {
                            restr = redisuser.DisplyName;
                            SysIniInfoUtility.SetRedisHash(redisuser, StaticData.RedisUserKey, (a, c) =>
                            {
                                return $"{a}:{c}";
                            });
                        }
                           


                       
                    }
                    break;
                case "DeptName"://当前用户所属部门名称
                    restr = RedisValueUtility.GetUserShowName(userId,fieldName: "DeptName");
                    if (string.IsNullOrEmpty(restr))
                    {//redis 没有，查询数据库，然后在存入到redis 并返回,如果有直接返回

                        var redisuser = GetredisUserInfo(Db, userId);
                        //设置redis
                        if (redisuser != null)
                        {
                            restr = redisuser.DeptName;
                            SysIniInfoUtility.SetRedisHash(redisuser, StaticData.RedisUserKey, (a, c) =>
                            {
                                return $"{a}:{c}";
                            });
                        }




                    }
                    break;
                case "DepartmentId":
                    restr = RedisValueUtility.GetRedisUserDeptId(userId).ToString();
                    if (string.IsNullOrEmpty(restr))
                    {//redis 没有，查询数据库，然后在存入到redis 并返回,如果有直接返回
                        var redisuser = GetredisUserInfo(Db, userId);
                        //设置redis
                        if (redisuser != null)
                        {
                            restr = redisuser.DepartmentId.ToString();
                            SysIniInfoUtility.SetRedisHash(redisuser, StaticData.NFUserDeptId, (a, c) =>
                            {
                                return $"{a}:{c}";
                            });
                        }
                    }
                    break;
            }
            return restr;
        }
        /// <summary>
        /// 根据ID查询用户redis信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>redis 存储信息</returns>
        private static RedisUser GetredisUserInfo(DbContext Db,int userId)
        {
            var query0 = Db.Set<UserInfor>().Where(a=>a.Id== userId).Include(a => a.Department);
            var query = from a in query0
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DisplyName = a.DisplyName,
                            Email = a.Email,
                            DepartmentId = a.DepartmentId,
                            DeptName = a.Department.Name,
                            State = a.State,
                            UserEs = a.UserEs,
                            UserEsTy = a.UserEsTy
                        };
            var local = from a in query
                        select new RedisUser
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DisplyName = a.DisplyName,
                            Email = a.Email,
                            DepartmentId = a.DepartmentId,
                            DeptName = a.DeptName,
                            State = a.State,
                            UserEs = a.UserEs,
                            UserEsTy = a.UserEsTy
                        };
            var redisuser = local.FirstOrDefault();
            return redisuser;
            


        }
        #endregion

        #region 部门 组织机构
        /// <summary>
        /// 获取redis 字符串
        /// </summary>
        /// <param name="Db">数据库查询服务</param>
        /// <param name="deptId">组织机构id</param>
        /// <param name="fileName">字段名称</param>
        /// <returns></returns>
        public static string GetRedisDeptNameValue(this DbContext Db, int deptId, string fileName = "Name") 
        {
            string restr = "";

            switch (fileName)
            {


                default:
                    restr = RedisValueUtility.GetDeptName(deptId);
                    if (string.IsNullOrEmpty(restr))
                    {//redis 没有，查询数据库，然后在存入到redis 并返回
                        var redisuser = GetredisDepartment(Db, deptId);
                        //设置redis
                        if (redisuser != null)
                        {
                            restr = redisuser.Name;
                            SysIniInfoUtility.SetRedisHash(redisuser, StaticData.RedisDeptKey, (a, c) =>
                            {
                                return $"{a}:{c}";
                            });
                        }
                    }
                    break;
            }
            return restr;
        }

        /// <summary>
        /// 根据ID查询组织机构redis信息
        /// </summary>
        /// <param name="Db">用户ID</param>
        /// <param name="deptId">存储信息</param>
        /// <returns></returns>
        private static RedisDept GetredisDepartment(DbContext Db, int deptId) 
        {
            var query0 = Db.Set<Department>().Where(a => a.Id == deptId);
            var query = from a in query0
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Pid = a.Pid,
                            No = a.No,
                            ShortName = a.ShortName
                        };
            var local = from a in query
                        select new RedisDept
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Pid = a.Pid,
                            No = a.No,
                            ShortName = a.ShortName
                        };
            var redisuser = local.FirstOrDefault();
            return redisuser;
        }
        #endregion

        #region 字典
        public static string GetRedisDataDictionaryValue(this DbContext Db, int dicId, DataDictionaryEnum dictionaryEnum, string fileName = "Name")
        {
            string restr = "";
            switch (fileName)
            {
                default://字典Name
                    restr = DataDicUtility.GetDicValueToRedis(dicId, dictionaryEnum);
                    if (string.IsNullOrEmpty(restr))
                    {//redis 没有，查询数据库，然后在存入到redis 并返回
                        var redisuser = GetredisDataDictionary(Db, dicId, dictionaryEnum);
                        //设置redis
                        if (redisuser != null)
                        {
                            restr = redisuser.Name;
                            SysIniInfoUtility.SetRedisHash(redisuser, StaticData.RedisDataKey, (a, c) =>
                            {
                                return $"{a}:{c}";
                            });
                        }
                    }
                    break;
            }
            return restr;
        }

        private static RedisData GetredisDataDictionary(DbContext Db, int dicId, DataDictionaryEnum dictionaryEnum)
        {
            var dataint = (int)dictionaryEnum;
            var query0 = Db.Set<DataDictionary>().Where(a => a.Id == dicId && a.DtypeNumber== dataint );
            var query = from a in query0
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DtypeNumber = a.DtypeNumber
                        };
            var local = from a in query
                        select new RedisData
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DtypeNumber = a.DtypeNumber
                        };
            var redisuser = local.FirstOrDefault();
            return redisuser;
        }
        #endregion
    }
}
