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
    /// 归档
    /// </summary>
    public partial interface IContTextArchiveService
    {
        /// <summary>
        /// 保存归档信息
        /// </summary>
        /// <param name="textArchive">归档主表</param>
        /// <param name="textArchiveItem">归档明细</param>
        /// <returns></returns>
        void AddSave(ContTextArchive textArchive, ContTextArchiveItem textArchiveItem);
        /// <summary>
        /// 归档明细列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        LayPageInfo<ContTextArchiveItemViewDTO> GetListArchiveItem<s>(PageInfo<ContTextArchiveItem> pageInfo, Expression<Func<ContTextArchiveItem, bool>> whereLambda,
            Expression<Func<ContTextArchiveItem, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 根据合同文本查询归档信息
        /// </summary>
        /// <param name="textId">合同文本ID</param>
        /// <returns>归档信息</returns>
        ContTextArchiveViewDTO ArchiveShowView(int textId);

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段对象</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除归档明细</param>
        /// <param name="contTextId">合同文本ID</param>
        /// <returns>受影响行数</returns>
        int DeleteArchItem(string Ids, int contTextId);
    }
}
