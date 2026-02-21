using AutoMapper;
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
    /// 交付描述
    /// </summary>
    public partial interface IContSubDeService
    {
        /// <summary>
        /// 标的交付
        /// </summary>
        /// <param name="info">交付描述信息</param>
        /// <param name="devDatas">交付行</param>
        /// <returns></returns>
        bool BiaoDiJiaoFu(ContSubDesDTO info, IList<DevSubItem> devDatas, IMapper _IMapper);
        /// <summary>
        /// 标的交付明细大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <param name="orderbyLambda">排序字段</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        LayPageInfo<ContSubDesListDTO> GetMainList<s>(PageInfo<ContSubDe> pageInfo, Expression<Func<ContSubDe, bool>> whereLambda, Expression<Func<ContSubDe, s>> orderbyLambda, bool isAsc);
    }
}
