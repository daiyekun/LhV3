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
    /// <summary>
    /// 询价接口
    /// </summary>
    public partial interface IInquiryService
    {
        int ClearJunkItemData(int currUserId);
    //    int ClearJunkItemData(int currUserId);
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        Dictionary<string, int> AddSave(Inquiry contractInfo);
        InquiryDTO ShowView(int Id);
        int UpdateField(UpdateFieldInfo info);
        int UpdateField(IList<UpdateFieldInfo> fields);
        //int UpdateField(UpdateFieldInfo info);
        int Delete(string Ids);
        //  int UpdateField(IList<UpdateFieldInfo> fields);

        int GetIsHt(string Ids);
        Dictionary<string, int> UpdateSave(Inquiry contractInfo);

        LayPageInfo<InquiryListDTO> GetList<s>(PageInfo<Inquiry> pageInfo, Expression<Func<Inquiry, bool>> whereLambda, Expression<Func<Inquiry, s>> orderbyLambda, bool isAsc);
        //   void Add(IList<WinningItems> addlist);
        byte? Yh(string name);
    }
}
