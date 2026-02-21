using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Extend.Enums;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 合同文本历史
    /// </summary>
    public partial class ContTextHistoryService
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
        public LayPageInfo<ContTextHistoryViewDTO> GetList<s>(PageInfo<ContTextHistory> pageInfo, Expression<Func<ContTextHistory, bool>> whereLambda, Expression<Func<ContTextHistory, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContTextHistory>()
                .Include(a=>a.Template)
                .AsTracking().Where<ContTextHistory>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContTextHistory>))
                tempquery = tempquery.Skip<ContTextHistory>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContTextHistory>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId = a.CategoryId,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            TemplateId = a.TemplateId,//合同模板ID
                            IsFromTemp = a.IsFromTemp,//文本来源
                            //CreateUserDisplyName =  //a.CreateUser.DisplyName,
                            Stage = a.Stage,//阶段
                            Path = a.Path,
                            FileName = a.FileName,
                            Versions = a.Versions,//版本
                            ModifyDateTime = a.ModifyDateTime,//变更日期
                            ExtenName = a.ExtenName,//扩展名称
                            GuidFileName = a.GuidFileName,//Guid文件名称
                            ContId = a.ContId,
                            TempName = a.Template == null ? "" : a.Template.Name//模板名称
                        };
            var local = from a in query.AsEnumerable()
                        select new ContTextHistoryViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId = a.CategoryId,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            TemplateId = a.TemplateId,//合同模板ID
                            IsFromTxt = EmunUtility.GetDesc(typeof(SourceTxtEnum), a.IsFromTemp ?? -1),//文本来源
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            Stagetxt = EmunUtility.GetDesc(typeof(StageTxtEnum), a.Stage ?? -1),//阶段
                            ContTxtType = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ContTxtType),
                          //  DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContTxtType),//文本类别
                            Path = a.Path,
                            FileName = a.FileName,
                            Versions = a.Versions,//版本
                            ModifyDateTime = a.ModifyDateTime,//变更日期
                            ExtenName = a.ExtenName,//扩展名称
                            GuidFileName = a.GuidFileName,//Guid文件名称
                            ContId = a.ContId,
                            TempName = a.TempName,//模板名称
                        };
            return new LayPageInfo<ContTextHistoryViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
    }
}
