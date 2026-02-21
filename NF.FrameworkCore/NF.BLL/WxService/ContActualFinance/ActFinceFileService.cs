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
        public LayPageInfo<ActFinceFileViewDTO> WxGetList<s>(PageInfo<ActFinceFile> pageInfo, Expression<Func<ActFinceFile, bool>> whereLambda, Expression<Func<ActFinceFile, s>> orderbyLambda, bool isAsc)
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

                            /// RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            CategoryName = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ActFinceFileType),
                            //RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.ActFinceFileType}", "Name"),//a.CategoryName,
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
    }
}
