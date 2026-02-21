using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IQuestioningService
    {

        int UpdateField(UpdateFieldInfo info);
        int ClearJunkItemData(int currUserId);
        //    int ClearJunkItemData(int currUserId);
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        Dictionary<string, int> AddSave(Questioning contractInfo);
        QuestioningDTO ShowView(int Id);
        int UpdateField(IList<UpdateFieldInfo> fields);
        //int UpdateField(UpdateFieldInfo info);
        Dictionary<string, int> UpdateSave(Questioning contractInfo);

        int Delete(string Ids);
        int GetIsHt(string Ids);

        LayPageInfo<QuestioningListDTO> GetList<s>(PageInfo<Questioning> pageInfo, Expression<Func<Questioning, bool>> whereLambda, Expression<Func<Questioning, s>> orderbyLambda, bool isAsc);
        //   void Add(IList<WinningItems> addlist);
    }
}
