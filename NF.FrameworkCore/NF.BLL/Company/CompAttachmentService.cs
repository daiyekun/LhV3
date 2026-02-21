using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.Utility;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 合同对方-附件
    /// </summary>
    public partial class CompAttachmentService
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
        public LayPageInfo<CompAttachmentViewDTO> GetList<s>(PageInfo<CompAttachment> pageInfo, Expression<Func<CompAttachment, bool>> whereLambda, Expression<Func<CompAttachment, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                            //join b in Db.Set<DataDictionary>()
                            //on a.CategoryId equals b.Id into ct
                            //from dci in ct.DefaultIfEmpty()
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            //CreateUserDisplyName =  //a.CreateUser.DisplyName,
                            //CategoryName = a.Category == null ? "" : a.Category.Name, //a.Category=="":null? a.Category.Name,
                            CategoryId = a.CategoryId,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,

                        };

            var local = from a in query.AsEnumerable()
                        select new CompAttachmentViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplyName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                          //  RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),//a.CreateUserDisplyName,
                            CategoryName = Type(a.CategoryId ?? 0),// DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.customerAttachmentType),

                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName
                        };
            return new LayPageInfo<CompAttachmentViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }

        /// <summary>
        /// 合同对方类型判断
        /// </summary>
        /// <param name="typeid">类型id</param>
        /// <returns></returns>
        public string Type(int typeid)
        {
            var TypeNmae = "";
            //客户
            TypeNmae = DataDicUtility.GetDicValueToRedis(typeid, DataDictionaryEnum.customerAttachmentType);
            if (TypeNmae == "" || TypeNmae == null)
            {   //供应商
                TypeNmae = DataDicUtility.GetDicValueToRedis(typeid, DataDictionaryEnum.supplierAttachmentType);
                if (TypeNmae == "" || TypeNmae == null)
                {   //其他对方
                    TypeNmae = DataDicUtility.GetDicValueToRedis(typeid, DataDictionaryEnum.otherAttachmentType);
                    return TypeNmae;
                }
                return TypeNmae;
            }
            return TypeNmae;
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update CompAttachment set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public CompAttachmentViewDTO ShowView(int Id)
        {
            var query = from a in _CompAttachmentSet.Include(a => a.Category).AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplyName = a.CreateUser.DisplyName,
                            CategoryName = a.Category.Name,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,
                            FolderName = a.FolderName,
                            CategoryId = a.CategoryId

                        };
            var local = from a in query.AsEnumerable()
                        select new CompAttachmentViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplyName = a.CreateUserDisplyName,
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
