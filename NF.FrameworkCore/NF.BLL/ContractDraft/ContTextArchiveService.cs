using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NF.ViewModel.Models;
using System.Linq.Expressions;
using NF.Common.Utility;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 归档
    /// </summary>
    public partial class ContTextArchiveService
    {
        /// <summary>
        /// 保存归档信息
        /// </summary>
        /// <param name="textArchive">归档信息主表</param>
        /// <param name="textArchiveItem">归档明细</param>
        public  void AddSave(ContTextArchive textArchive, ContTextArchiveItem textArchiveItem)
        {
            if (!GetQueryable(a => a.ContTextId == textArchive.ContTextId).Any())
            {
                var actinfo =Add(textArchive);
                textArchiveItem.ArchiveId = actinfo.Id;
            }
            else
            {
                var qactinfo =GetQueryable(a => a.ContTextId == textArchive.ContTextId).FirstOrDefault();
                textArchiveItem.ArchiveId = qactinfo.Id;
                textArchiveItem.ArcCabCode = qactinfo.ArcCabCode;
                textArchiveItem.ArcCode = qactinfo.ArcCode;
               
            }
            textArchiveItem.Path =string.IsNullOrEmpty(textArchiveItem.FolderName)? "Uploads/" + textArchiveItem.FolderName + "/" + textArchiveItem.GuidFileName:"";
            Db.Set<ContTextArchiveItem>().Add(textArchiveItem);  
            SaveChanges();//添加明细
            var sumAch = Db.Set<ContTextArchiveItem>().Where(a => a.ContTextId == textArchiveItem.ContTextId).Sum(a => (a.ArcNumber ?? 0));
            string sqlstr =$"update ContTextArchive set ArcSumNumber={sumAch} where Id={textArchiveItem.ArchiveId}";
            ExecuteSqlCommand(sqlstr);



        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<ContTextArchiveItemViewDTO> GetListArchiveItem<s>(PageInfo<ContTextArchiveItem> pageInfo,
            Expression<Func<ContTextArchiveItem, bool>> whereLambda,
            Expression<Func<ContTextArchiveItem, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = Db.Set<ContTextArchiveItem>().AsTracking().Where(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContTextArchiveItem>))
            { //分页
                tempquery = tempquery.Skip<ContTextArchiveItem>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            ArcNumber = a.ArcNumber,
                            ArcRemark = a.ArcRemark,
                            SubUser = a.SubUser,
                            SubDateTime = a.SubDateTime,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            FileName=a.FileName,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContTextArchiveItemViewDTO
                        {
                            Id = a.Id,
                            ArcNumber = a.ArcNumber,
                            ArcRemark = a.ArcRemark,
                            SubUser = a.SubUser,
                            SubDateTime = a.SubDateTime,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            FileName = a.FileName,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId)
                            //RedisValueUtility.GetUserShowName(a.CreateUserId), //经办人

                        };
            return new LayPageInfo<ContTextArchiveItemViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 根据合同文本查询归档信息
        /// </summary>
        /// <param name="textId">合同文本ID</param>
        /// <returns>归档信息</returns>
        public ContTextArchiveViewDTO ArchiveShowView(int textId)
        {
            var query = from a in _ContTextArchiveSet.AsNoTracking()
                        where a.ContTextId == textId
                        select new
                         {
                            Id=a.Id,
                            ContTextId=a.ContTextId,
                            ArcCode=a.ArcCode,
                            ArcCabCode=a.ArcCabCode,
                            ArcSumNumber=a.ArcSumNumber,
                            BorrSumNumber=a.BorrSumNumber,
                            CreateUserId=a.CreateUserId,
                            CreateDateTime=a.CreateDateTime

                        };
            var local = from a in query.AsEnumerable()
                        select new ContTextArchiveViewDTO
                        {
                            Id = a.Id,
                            ContTextId = a.ContTextId,
                            ArcCode = a.ArcCode,
                            ArcCabCode = a.ArcCabCode,
                            ArcSumNumber = a.ArcSumNumber,
                            BorrSumNumber = a.BorrSumNumber,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ResidueNumber= (a.ArcSumNumber??0)- (a.BorrSumNumber??0)
                        };
            return local.FirstOrDefault();

        }
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            //string sqlstr = "";
            StringBuilder sqlstr = new StringBuilder();
            switch (info.FieldName)
            {

                case "ArcCabCode"://归档柜号
                case "ArcCode"://归档号
                    sqlstr.Append($"update  ContTextArchive set {info.FieldName}='{info.FieldValue}' where ContTextId={info.Id};");
                    sqlstr.Append($"update  ContTextArchiveItem set {info.FieldName}='{info.FieldValue}' where ContTextId={info.Id};");
                    break;
               

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr.ToString()))
                return ExecuteSqlCommand(sqlstr.ToString());
            return 0;

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除归档明细</param>
        /// <param name="contTextId">合同文本Id </param>
        /// <returns>受影响行数</returns>
       public int DeleteArchItem(string Ids,int contTextId)
        {
            StringBuilder sqlstr = new StringBuilder();
            var listIds=StringHelper.String2ArrayInt(Ids);
            sqlstr.Append( $"delete  ContTextArchiveItem  where Id in({Ids});");
            var sum = Db.Set<ContTextArchiveItem>().Where(a => listIds.Contains(a.Id)).Sum(a => a.ArcNumber ?? 0);
            sqlstr.Append($"update  ContTextArchive set ArcSumNumber=ArcSumNumber-{sum} where ContTextId={contTextId};");
            if (!string.IsNullOrEmpty(sqlstr.ToString()))
                return ExecuteSqlCommand(sqlstr.ToString());
            return 0;
        }
    }
}
