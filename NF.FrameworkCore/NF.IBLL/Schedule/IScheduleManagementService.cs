using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IScheduleManagementService
    {
        /// <summary>
        /// 进度管理列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ScheduleManagementViewDTO> GetList<s>(PageInfo<ScheduleManagement> pageInfo, Expression<Func<ScheduleManagement, bool>> whereLambda, Expression<Func<ScheduleManagement, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ScheduleManagementViewDTO ShowView(int Id);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);

        /// <summary>
        /// 添加进度管理附件
        /// </summary>
        /// <param name="SchId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int ADDAtt(int SchId,int userId);

        /// <summary>
        /// 删除进度管理附件
        /// </summary>
        /// <param name="SchId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int DeleteAtt(int userId);

        int StateUpdate(int SchId, int userId);

        int UpdateField(UpdateFieldInfo info);
    }
}
