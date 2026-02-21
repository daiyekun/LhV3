using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
    /// <summary>
    /// 实际资金附件
    /// </summary>
    public partial class ActFinceFileService
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ActFinceFileViewDTO> GetList<s>(PageInfo<ActFinceFile> pageInfo, Expression<Func<ActFinceFile, bool>> whereLambda, Expression<Func<ActFinceFile, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            CategoryId = a.CategoryId,
                            
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,

                        };
            var local = from a in query.AsEnumerable()
                        select new ActFinceFileViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                           // RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CategoryName = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ActFinceFileType),
                           // RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.ActFinceFileType}", "Name"),//a.CategoryName,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName
                        };
            return new LayPageInfo<ActFinceFileViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update ActFinceFile set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ActFinceFileViewDTO ShowView(int Id)
        {
            var query = from a in _ActFinceFileSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,
                            FolderName = a.FolderName,
                            CategoryId = a.CategoryId,
                            CreateUserId = a.CreateUserId

                        };
            var local = from a in query.AsEnumerable()
                        select new ActFinceFileViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,
                            FolderName = a.FolderName,
                            CategoryId = a.CategoryId,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CategoryName = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ActFinceFileType),
                            //RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.ActFinceFileType}", "Name"),//a.CategoryName,
                        };
            return local.FirstOrDefault();
        }
    }
}
