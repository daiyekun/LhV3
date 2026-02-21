using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
 public  partial   interface IWinningItemService
    {
        LayPageInfo<WinningItemsDTO> GetList<s>(PageInfo<WinningItem> pageInfo, Expression<Func<WinningItem, bool>> whereLambda, Expression<Func<WinningItem, s>> orderbyLambda, bool isAsc);
     
        LayPageInfo<WinningItemsDTO> GetListView<s>(PageInfo<WinningItem> pageInfo, Expression<Func<WinningItem, bool>> whereLambda, Expression<Func<WinningItem, s>> orderbyLambda, bool isAsc);

        int Delete(string Ids);
    }
}
