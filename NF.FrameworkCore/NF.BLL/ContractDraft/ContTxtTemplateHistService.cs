using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 模板历史
    /// </summary>
  public partial  class ContTxtTemplateHistService
    {
        /// <summary>
        /// 模板历史
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContTxtTemplateHistListDTO> GetHistList<s>(PageInfo<ContTxtTemplateHist> pageInfo, Expression<Func<ContTxtTemplateHist, bool>> whereLambda,
            Expression<Func<ContTxtTemplateHist, s>> orderbyLambda, bool isAsc)
        {

            var tempquery = Db.Set<ContTxtTemplateHist>().AsTracking().Where<ContTxtTemplateHist>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContTxtTemplateHist>))
            { //分页
                tempquery = tempquery.Skip<ContTxtTemplateHist>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContTxtTemplateHist>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            ModifyDateTime = a.ModifyDateTime,//修改时间
                            Vesion = a.Vesion,//版本
                            ModifyUserId = a.ModifyUserId,//修改人
                            UseVersion = a.UseVersion,//使用此版本
                            TempId=a.TempId,

                        };
            var local = from a in query.AsEnumerable()
                        select new ContTxtTemplateHistListDTO
                        {
                            Id = a.Id,
                            ModifyDateTime = a.ModifyDateTime,//修改时间
                            Vesion = a.Vesion ?? 0,//版本
                            UseVersion = a.UseVersion ?? 0,//使用此版本
                            TempId = a.TempId??0,
                            ModifyUserName = this.Db.GetRedisUserFieldValue(a.ModifyUserId ?? -1),
                           // RedisValueUtility.GetUserShowName(a.ModifyUserId ?? 0),

                        };
            return new LayPageInfo<ContTxtTemplateHistListDTO>()
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
            StringBuilder sqlstr = new StringBuilder();
            switch (info.FieldName)
            {
                case "UseVersion"://状态
                    var state = Convert.ToInt32(info.FieldValue);
                    sqlstr.Append($"update  ContTxtTemplateHist set UseVersion=0 where TempId={info.OtherId};");
                    sqlstr.Append($"update  ContTxtTemplateHist set UseVersion={state} where Id={info.Id}");
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr.ToString()))
                return ExecuteSqlCommand(sqlstr.ToString());
            return 0;

        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("update ContTxtTemplateHist set IsDelete=1 where Id in(" + Ids + ")");
            return ExecuteSqlCommand(strb.ToString());
        }
    }
}
