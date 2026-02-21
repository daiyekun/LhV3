using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IWinningQueService
    {
        LayPageInfo<WinningQueDTO> GetList<s>(PageInfo<WinningQue> pageInfo, Expression<Func<WinningQue, bool>> whereLambda, Expression<Func<WinningQue, s>> orderbyLambda, bool isAsc);
        //   WinningInqDTO ShowView(int Id);
        //GetListView
        LayPageInfo<WinningQueDTO> GetListView<s>(PageInfo<WinningQue> pageInfo, Expression<Func<WinningQue, bool>> whereLambda, Expression<Func<WinningQue, s>> orderbyLambda, bool isAsc);

        int Delete(string Ids);
        /// <summary>
        /// 保存招标人
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        bool AddSave(IList<WinningQueDTO> subs, int contid);
    }
}
