using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IBidlabelService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<BidlabelDTO> GetKbqkList<s>(PageInfo<Bidlabel> pageInfo, Expression<Func<Bidlabel, bool>> whereLambda, Expression<Func<Bidlabel, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 保存招标人
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        bool AddSave(IList<BidlabelDTO> subs, int contid);
        bool AddSaveInfro(BidlabelDTO subs);
        /// <summary>
        /// 查询用户信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<OpenBidDTO> GetList(PageInfo<OpenBid> pageInfo, Expression<Func<OpenBid, bool>> whereLambda);
    }
}
