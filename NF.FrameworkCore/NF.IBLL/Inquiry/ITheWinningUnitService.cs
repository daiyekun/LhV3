using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface ITheWinningUnitService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<TheWinningUnitDTO> GetKbqkList<s>(PageInfo<TheWinningUnit> pageInfo, Expression<Func<TheWinningUnit, bool>> whereLambda, Expression<Func<TheWinningUnit, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 保存招标人
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        bool AddSave(IList<TheWinningUnitDTO> subs, int contid);
        /// <summary>
        /// 查询用户信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<OpenTenderConditionDTO> GetList(PageInfo<OpenTenderCondition> pageInfo, Expression<Func<OpenTenderCondition, bool>> whereLambda);
        bool SaveBZData(TheWinningUnitDTO subs);

    }
}
