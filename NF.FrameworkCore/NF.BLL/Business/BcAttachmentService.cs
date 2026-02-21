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
    /// 单品附件
    /// </summary>
  public partial  class BcAttachmentService
    {/// <summary>
     /// 查询分页
     /// </summary>
     /// <typeparam name="s"></typeparam>
     /// <param name="pageInfo"></param>
     /// <param name="whereLambda"></param>
     /// <param name="orderbyLambda"></param>
     /// <param name="isAsc"></param>
     /// <returns></returns>
        public LayPageInfo<BcAttachmentViewDTO> GetList<s>(PageInfo<BcAttachment> pageInfo, Expression<Func<BcAttachment, bool>> whereLambda, Expression<Func<BcAttachment, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<BcAttachment>()
                .Include(a=>a.Category)
                .AsTracking().Where<BcAttachment>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<BcAttachment>))
                tempquery = tempquery.Skip<BcAttachment>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<BcAttachment>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            //CreateUserDisplyName =  //a.CreateUser.DisplyName,
                            CategoryName = a.Category==null?"": a.Category.Name,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,

                        };
            var local = from a in query.AsEnumerable()
                        select new BcAttachmentViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName =this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),//a.CreateUserDisplyName,
                            CategoryName = a.CategoryName,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName
                        };
            return new LayPageInfo<BcAttachmentViewDTO>()
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
            string sqlstr = "update BcAttachment set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public BcAttachmentViewDTO ShowView(int Id)
        {
            var query = from a in _BcAttachmentSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = a.CreateUser.DisplyName,
                            CategoryName = a.Category.Name,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,
                            FolderName = a.FolderName,
                            CategoryId = a.CategoryId

                        };
            var local = from a in query.AsEnumerable()
                        select new BcAttachmentViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = a.CreateUserName,
                            CategoryName = a.CategoryName,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,
                            FolderName = a.FolderName,
                            CategoryId = a.CategoryId
                        };
            return local.FirstOrDefault();
        }
    }
}
