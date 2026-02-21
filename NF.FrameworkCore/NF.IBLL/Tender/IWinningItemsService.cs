using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
 public  partial   interface IWinningItemsService
    {
        LayPageInfo<WinningItemsDTO> GetList<s>(PageInfo<WinningItems> pageInfo, Expression<Func<WinningItems, bool>> whereLambda, Expression<Func<WinningItems, s>> orderbyLambda, bool isAsc);
     
        LayPageInfo<WinningItemsDTO> GetListView<s>(PageInfo<WinningItems> pageInfo, Expression<Func<WinningItems, bool>> whereLambda, Expression<Func<WinningItems, s>> orderbyLambda, bool isAsc);

        int Delete(string Ids);
    }
}
