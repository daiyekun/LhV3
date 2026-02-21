using Microsoft.EntityFrameworkCore;
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
    /// 系统功能点
    /// </summary>
    public partial  class SysFunctionService
    {
        /// <summary>
        /// 查询列表并分页
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
       public LayPageInfo<SysFunctionDTO> GetList(PageInfo<SysFunction> pageInfo, Expression<Func<SysFunction, bool>> whereLambda)
        {
            var tempquery = Db.Set<SysFunction>().AsNoTracking().Where<SysFunction>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<SysFunction>((pageInfo.PageIndex - 1) * pageInfo.PageSize)
                 .Take<SysFunction>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Fcode = a.Fcode,
                            Remark= a.Remark,
                        };
            var local = from a in query.AsEnumerable()
                        select new SysFunctionDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Fcode = a.Fcode,
                            Remark = a.Remark,

                        };
            return new LayPageInfo<SysFunctionDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
    }
}
