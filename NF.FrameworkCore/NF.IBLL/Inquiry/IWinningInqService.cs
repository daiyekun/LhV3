using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
  public partial  interface IWinningInqService
    {
        LayPageInfo<WinningInqDTO> GetList<s>(PageInfo<WinningInq> pageInfo, Expression<Func<WinningInq, bool>> whereLambda, Expression<Func<WinningInq, s>> orderbyLambda, bool isAsc);
     //   WinningInqDTO ShowView(int Id);
        //GetListView
        LayPageInfo<WinningInqDTO> GetListView<s>(PageInfo<WinningInq> pageInfo, Expression<Func<WinningInq, bool>> whereLambda, Expression<Func<WinningInq, s>> orderbyLambda, bool isAsc);

        int Delete(string Ids);
        /// <summary>
        /// 保存招标人
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        bool AddSave(IList<WinningInqDTO> subs, int contid);
    }
}
