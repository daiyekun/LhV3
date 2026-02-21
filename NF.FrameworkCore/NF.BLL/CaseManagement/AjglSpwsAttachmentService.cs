using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
public partial    class AjglSpwsAttachmentService
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
        public LayPageInfo<SpwsViewDTO> GetList<s>(PageInfo<AjglSpwsAttachment> pageInfo, Expression<Func<AjglSpwsAttachment, bool>> whereLambda, Expression<Func<AjglSpwsAttachment, s>> orderbyLambda, bool isAsc)
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
                            Ajglid = a.Ajglid,

                        };
            var local = from a in query.AsEnumerable()
                        select new SpwsViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            CategoryName = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ContAttachmentType),
                            //DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContAttachmentType),
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,
                            Ajglid = a.Ajglid,
                        };
            return new LayPageInfo<SpwsViewDTO>()
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
            string sqlstr = "update AjglSpwsAttachment set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public SpwsViewDTO ShowView(int Id)
        {
            var query = from a in _AjglSpwsAttachmentSet.AsNoTracking()
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
                        select new SpwsViewDTO
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
                           // RedisValueUtility.GetUserShowName(a.CreateUserId),
                            CategoryName = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ContAttachmentType),
                          //  DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContAttachmentType)
                        };
            return local.FirstOrDefault();
        }

        /// <summary>
        /// 根据合同id 获取图片集合
        /// </summary>
        /// <param name="contId">图片ID</param>
        /// <returns></returns>
        public IList<PicViewDTO> GetPicViews(int Ajglid)
        {
            var extens = new string[] {
                ".png",".jpg",".jpeg",".bmp",".svg",".gif",".tif",".psd",".pcx",".svg",".cdr",".raw",
                ".avif",".raw",".ai",".tga",".exif",".fpx",".eps",".webp"
            };
            var list = Db.Set<AjglAttachment>().Where(a => a.Ajglid == Ajglid)
                .Select(a => new PicViewDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Path = a.Path,
                    FileName = a.FileName
                }).ToList();
            IList<PicViewDTO> temlist = new List<PicViewDTO>();
            foreach (var item in list)
            {
                var exten = item.FileName.Substring(item.FileName.LastIndexOf("."));
                if (extens.Contains(exten))
                {
                    temlist.Add(item);

                }
            }
            return temlist;
        }
    }
}
