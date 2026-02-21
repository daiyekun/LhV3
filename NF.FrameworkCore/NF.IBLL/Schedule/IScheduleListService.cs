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
    public partial interface IScheduleListService
    {

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ScheduleListViewDTO> GetList<s>(PageInfo<ScheduleList> pageInfo, Expression<Func<ScheduleList, bool>> whereLambda, Expression<Func<ScheduleList, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ScheduleListViewDTO ShowView(int Id);

        int UpdateField(IList<UpdateFieldInfo> fields);
        LayPageInfo<ScheduleListViewDTO> GetListdesk<s>(PageInfo<ScheduleList> pageInfo, Expression<Func<ScheduleList, bool>> whereLambda, Expression<Func<ScheduleList, s>> orderbyLambda, bool isAsc, List<int?> ids,int usid);

        int Updstate(int id,int ty);

        int SJZd(string name);
    }
}
