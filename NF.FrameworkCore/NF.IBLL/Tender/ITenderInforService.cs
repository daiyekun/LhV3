using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
  public partial interface ITenderInforService
    {

        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 添加招标信息
        /// </summary>
        /// <param name="tenderInfor">招标</param>
        /// <returns></returns>
        Dictionary<string, int> AddSave(TenderInfor tenderInfor);
        /// <summary>
        /// 查询信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<TenderInforListViewDTO> GetList<s>(PageInfo<TenderInfor> pageInfo, Expression<Func<TenderInfor, bool>> whereLambda, Expression<Func<TenderInfor, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        TenderInforListViewDTO ShowView(int Id);
        /// <summary>
        /// 修改合同信息
        /// </summary>
        /// <param name="TenderInfor">合同修改信息对象</param>
        /// <returns>Id:\Hid:字典</returns>
        Dictionary<string, int> UpdateSave(TenderInfor tenderInfor);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);

        int GetIsHt(string ids);

        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        int ClearJunkItemData(int currUserId);
    }
}
