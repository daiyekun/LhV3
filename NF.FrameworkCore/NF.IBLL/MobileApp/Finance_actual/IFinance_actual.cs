using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.MobileApp;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{ 
    public partial interface IFinance_actual
    {


        APPLayPageInfo<ContActualFinanceListViewDTO> GetMainList<s>(PageInfo<ContActualFinance> pageInfo, Expression<Func<ContActualFinance, bool>> whereLambda, Expression<Func<ContActualFinance, s>> orderbyLambda, bool isAsc, int type, int start, int limit);
        /// <summary>
        /// 实际资金详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ContActualFinanceListViewDTO XJZJ_XQ(int Id);
        APPFinance_actual XJZJ_HT(int Id);
        List<APPContPlanFinance> XJZJ_JHZJ(int Id);
        

    }
}
