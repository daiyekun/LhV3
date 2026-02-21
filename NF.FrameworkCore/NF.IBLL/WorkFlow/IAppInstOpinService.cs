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
    /// 审批意见
    /// </summary>
    public partial interface IAppInstOpinService
    {
        /// <summary>
        /// 提交审批意见
        /// </summary>
        /// <param name="submitOption">提交审批意见对象</param>
        /// <returns>
        /// -1：标识查找分支节点时没有找到满足条件的节点
        /// 1：标识成功
        /// </returns>
        int SubmintOption(SubmitOptionInfo submitOption);

        /// <summary>
        /// 不同意时提交意见
        /// </summary>
        /// <param name="submitOption">提交审批意见对象</param>
        /// <returns>
        /// </returns>
        int SubmintDisagreeOption(SubmitOptionInfo submitOption);
        /// <summary>
        /// 查询审批意见
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>审批意见</returns>
        LayPageInfo<AppInstOpinDTO> GetOptinionList<s>(PageInfo<AppInstOpin> pageInfo, Expression<Func<AppInstOpin, bool>> whereLambda, Expression<Func<AppInstOpin, s>> orderbyLambda, bool isAsc);
    }
}
