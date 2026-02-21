using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    partial interface IOpeningSituationInforService
    {
        /// <summary>
        /// 添加招标情况
        /// </summary>
        /// <param name="OpeningSituationInfor"></param>
        /// <returns></returns>
        Dictionary<string, int> AddSave(OpeningSituationInfor OpeningSituationInfor);
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<OpeningSituationInforDTO> GetKbqkList<s>(PageInfo<OpeningSituationInfor> pageInfo, Expression<Func<OpeningSituationInfor, bool>> whereLambda, Expression<Func<OpeningSituationInfor, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        bool AddSave(IList<OpeningSituationInforDTO> subs, int contid);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID</param>
        /// <param name="IsFrameWorkCont">是否是框架合同，如果是框架合同需要更新合同金额</param>
        /// <returns></returns>
        int Delete(string Ids);
    }
}
