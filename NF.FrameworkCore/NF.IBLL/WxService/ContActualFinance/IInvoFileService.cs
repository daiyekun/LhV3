using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{
    public partial interface IInvoFileService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<InvoFileViewDTO> GeWxtList<s>(PageInfo<InvoFile> pageInfo, Expression<Func<InvoFile, bool>> whereLambda, Expression<Func<InvoFile, s>> orderbyLambda, bool isAsc);

    }
}
