using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace NF.IBLL
{
    public partial interface IAPPfinance_plan
    {
        APPLayPageInfo<ContPlanFinanceListViewDTO> GetMainList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc, int type, int start, int limit);
        //  APPLayPageInfo<CompanyViewDTO> GetList<s>(PageInfo<Company> pageInfo, Expression<Func<Company, bool>> whereLambda, Expression<Func<Company, s>> orderbyLambda, bool isAsc, int type);
      //  APPLayPageInfo<ContPlanFinanceListViewDTO> GetMainList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc, int? type);
         //  APPLayPageInfo<ContPlanFinanceListViewDTO> GetMainList(int type);

    }
}
