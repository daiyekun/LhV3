using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NF.ViewModel.Extend.Enums;
using NF.IBLL;
using System.Data.Common;

namespace NF.BLL
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public partial class OptionLogService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">过滤条件</param>
        /// <returns></returns>
        public LayPageInfo<OptionLogDTO> GetList(PageInfo<OptionLog> pageInfo, Expression<Func<OptionLog, bool>> whereLambda)
        {
          
            var tempquery = _OptionLogSet.Include(a=>a.User).AsNoTracking().Where<OptionLog>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<OptionLog>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<OptionLog>(pageInfo.PageSize);

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            UserId = a.UserId,
                            UserName = a.User.Name==null?"": a.User.Name,
                         //   UserName =a.User.Name,
                            ControllerName = a.ControllerName,
                            ActionName = a.ActionName,
                            Remark = a.Remark,
                            RequestUrl = a.RequestUrl,
                            RequestMethod = a.RequestMethod,
                            RequestData = a.RequestData,
                            RequestIp = a.RequestIp,
                            RequestNetIp = a.RequestNetIp,
                            CreateDatetime = a.CreateDatetime,
                            Status = a.Status,
                            ActionTitle = a.ActionTitle,
                            OptionType = a.OptionType,
                           
                        };
            var local = from a in query.AsEnumerable()
                        select new OptionLogDTO
                        {
                            Id = a.Id,
                            UserId = a.UserId,
                            UserName = a.UserName,
                            ControllerName = a.ControllerName,
                            ActionName = a.ActionName,
                            Remark = a.Remark,
                            RequestUrl = a.RequestUrl,
                            RequestMethod = a.RequestMethod,
                            RequestData = a.RequestData,
                            RequestIp = a.RequestIp,
                            RequestNetIp = a.RequestNetIp,
                            CreateDatetime = a.CreateDatetime,
                            Status = a.Status,
                            ActionTitle = a.ActionTitle,
                            OptionType = a.OptionType,
                            OptionTypeDic = EmunUtility.GetDesc(typeof(OptionLogTypeEnum), a.OptionType ?? -1),
                            RequestMethodDic = EmunUtility.GetDesc(typeof(RequestMethodEnum), a.RequestMethod ?? -1),

                        };


            return new LayPageInfo<OptionLogDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };




        }
        public int UpdateState(string Ids)
        {
            string sqlstr= "update OptionLog set Status=1 where Id in("+ Ids + ")";
           return ExecuteSqlCommand(sqlstr);
        }



    }
}
