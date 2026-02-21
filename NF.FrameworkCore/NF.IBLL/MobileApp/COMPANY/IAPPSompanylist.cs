using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace NF.IBLL
{
    public partial interface IAPPSompanylist
    {
        /// <summary>
        /// 客户大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        APPLayPageInfo<CompanyViewDTO> GetList<s>(PageInfo<Company> pageInfo, Expression<Func<Company, bool>> whereLambda, Expression<Func<Company, s>> orderbyLambda, bool isAsc, int type, int start, int limit);
        /// <summary>
        /// 客户详细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        CompanyViewDTO GetIDView(int Id);
        /// <summary>
        /// 客户-联系人
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<APPCompContact> companyDetailContact(int Id);
        List<APPCompAttachmentDTO> Company_accessory(int Id);
    }
}
