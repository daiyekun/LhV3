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
    /// 合同文本借阅
    /// </summary>
    public partial interface IContTextBorrowService
    {
        /// <summary>
        /// 新建借阅
        /// </summary>
        /// <param name="textBorrow">借阅对象</param>
        /// <returns>借阅对象</returns>
        ContTextBorrow AddSave(ContTextBorrow textBorrow);
        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="textRepay">归还对象</param>
        /// <returns>归还对象</returns>
        ContTextBorrow SaveRepay(ContTextBorrow textRepay);
        /// <summary>
        /// 查询借阅列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<ContTextBorrowViewDTO> GetListBorrow<s>(PageInfo<ContTextBorrow> pageInfo,
           Expression<Func<ContTextBorrow, bool>> whereLambda,
           Expression<Func<ContTextBorrow, s>> orderbyLambda, bool isAsc);
    }
}
