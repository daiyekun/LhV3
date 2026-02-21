using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IOpenTenderConditionService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<OpenTenderConditionDTO> GetKbqkList<s>(PageInfo<OpenTenderCondition> pageInfo, Expression<Func<OpenTenderCondition, bool>> whereLambda, Expression<Func<OpenTenderCondition, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        bool AddSave(IList<OpenTenderConditionDTO> subs, int contid);

        bool AddSaves(IList<OpenTenderConditionDTO> subs, int contid);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID</param>
        /// <param name="IsFrameWorkCont">是否是框架合同，如果是框架合同需要更新合同金额</param>
        /// <returns></returns>
        int Delete(string Ids);
    }
}
