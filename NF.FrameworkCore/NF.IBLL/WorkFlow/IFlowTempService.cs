using AutoMapper;
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
    /// 流程模板
    /// </summary>
   public partial  interface IFlowTempService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns></returns>
        LayPageInfo<FlowTempViewDTO> GetList<s>(PageInfo<FlowTemp> pageInfo, Expression<Func<FlowTemp, bool>> whereLambda, Expression<Func<FlowTemp, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 流程模板保存
        /// </summary>
        /// <param name="flowTemp">模板信息</param>
        /// <returns></returns>
        FlowTemp AddSave(FlowTemp flowTemp, IMapper _IMapper);
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="flowTemp">实体对象</param>
        /// <returns></returns>
        FlowTemp UpdateSave(FlowTemp flowTemp, IMapper _IMapper);
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID</param>
        /// <returns></returns>
        int Delete(string Ids);
        /// <summary>
        ///根据流程必要条件判断流程是否存在
        /// </summary>
        /// <param name="flowTemp">流程表单对象</param>
        /// <returns></returns>
        string CheckFlowUnique(FlowTemp flowTemp);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        FlowTempDTO ShowView(int Id);
        /// <summary>
        /// 根据条件获取流程模板ID
        /// </summary>
        /// <param name="requestTemp">请求对象</param>
        /// <returns></returns>
        ResponTemp FindTempIdByWhere(RequestTempInfo requestTemp);
        /// <summary>
        /// 判断流程模板数据是否正确
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        int ChekAppFlowData(int tempId);
        /// <summary>
        /// 判断是否有完整流程
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <param name="amount">金额</param>
        /// <param name="flowType">流程类型</param>
        /// <returns>值0:正常，-1参数错误，-2：找不到开始结束节点
        /// -3：用金额判断节点，部分节点校验失败
        /// -4：不用判断金额的节点流程，没有节点信息</returns>
        int SubCheckFlow(int? tempId, decimal? amount, int? flowType);
    }
}
