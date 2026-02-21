using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 发票明细列表
    /// </summary>
   public partial interface IInvoDescriptionService
    {
        /// <summary>
        /// 发票明细列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>发票内容列表</returns>
        LayPageInfo<InvoDescriptionViewDTO> GetList<s>(PageInfo<InvoDescription> pageInfo, Expression<Func<InvoDescription, bool>> whereLambda, Expression<Func<InvoDescription, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="field">当前字段</param>
        /// <param name="fdv">当前值</param>
        /// <returns></returns>
        bool UpdateDesc(int Id, string field, string fdv);
        /// <summary>
        /// 删除发票明细
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
    }
}
