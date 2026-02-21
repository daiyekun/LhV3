using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{
    public partial interface ICompanyService
    {  /// <summary>
       /// 查询客户信息列表
       /// </summary>
       /// <param name="pageInfo">分页对象</param>
       /// <param name="whereLambda">查询条件表达式</param>
       /// <returns>返回layui所需对象</returns>
        LayPageInfo<WxKhlist> GetWxCompList<s>(PageInfo<Company> pageInfo, Expression<Func<Company, bool>> whereLambda, Expression<Func<Company, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        WxKhView KhView(int Id);
        /// <summary>
        /// 查询其它联系人
        /// </summary>
        /// <param name="id">对方id</param>
        /// <returns></returns>
        List<CompContact> WxQtlxr(int id);
    }
}
