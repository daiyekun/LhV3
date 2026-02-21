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
    public partial interface IContInvoiceService
    {
        /// <summary>
        /// 发票大列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>返回列表</returns>
        LayPageInfo<ContInvoiceListViewDTO> GetWxMainList<s>(PageInfo<ContInvoice> pageInfo, Expression<Func<ContInvoice, bool>> whereLambda, Expression<Func<ContInvoice, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        WxContInvoice WxShowView(int Id);
    }
}
