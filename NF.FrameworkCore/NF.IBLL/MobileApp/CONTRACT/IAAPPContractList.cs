using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL.MobileApp.CONTRACT
{
    public partial interface IAAPPContractList  
    {
        /// <summary>
        /// 查询信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        APPLayPageInfo<ContractInfoListViewDTO> GetList<s>(Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc,int type,int start,int limit);
        /// <summary>
        /// 根据合同ID查询合同基本信息
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        ContractInfoHistoryViewDTO GetIDView(int Id);

        /// <summary>
        /// 根据合同ID查询合同文附件
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        List<APPContTextListViewDTO> AppcontractDetailFile1(int Id);

        /// <summary>
        /// 根据合同ID查询合同文本
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        List<APPContFUjianListViewDTO> AppcontractDetailFile2(int Id);

        /// <summary>
        /// APP根据合同ID查询计划资金
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        List<APPContfINCTListViewDTO> AppcontractDetailAFinance(int Id);
        /// <summary>
        /// APP根据合同ID查询实际资金
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        List<APPContSHIJIfINCTListViewDTO> AppfinanceActualList(int Id);
        /// <summary>
        /// 资金统计
        /// </summary>
        /// <param name="ContId">合同ID</param>
        /// <returns></returns>
        APPContractStatic GetContractStatic(int ContId);
        /// <summary>
        /// APP根据合同ID查询合同备忘
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        List<APPContDescription> AppcontractDetailRemark(int Id);

        /// <summary>
        /// APP根据合同ID查询审批记录
        /// </summary>
        /// <param name="Id">合同ID</param>
        /// <returns></returns>
        List<APPAppInst> AppcontractDetailWorkflow(int Id);
        
    }
}
