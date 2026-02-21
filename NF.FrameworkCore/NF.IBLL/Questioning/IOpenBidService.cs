using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IOpenBidService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<OpenBidDTO> GetKbqkList<s>(PageInfo<OpenBid> pageInfo, Expression<Func<OpenBid, bool>> whereLambda, Expression<Func<OpenBid, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        bool AddSave(IList<OpenBidDTO> subs, int contid);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID</param>
        /// <param name="IsFrameWorkCont">是否是框架合同，如果是框架合同需要更新合同金额</param>
        /// <returns></returns>
        int Delete(string Ids);
    }
}
