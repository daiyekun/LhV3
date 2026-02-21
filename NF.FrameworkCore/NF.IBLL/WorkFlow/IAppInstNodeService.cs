using AutoMapper;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 流程实例节点
    /// </summary>
    public partial interface IAppInstNodeService
    {
        /// <summary>
        /// 根据流程实例ID获取流程图
        /// </summary>
        /// <param name="instId">流程实例ID</param>
        /// <returns></returns>
        AppFlowNodeDataJson LoadFlowChart(int instId, IMapper _IMapper);
        /// <summary>
        /// 根据节点ID获取节点信息
        /// </summary>
        /// <param name="nodeStrId">节点ID</param>
        /// <param name="instId">实例节点ID</param>
        /// <returns></returns>
        AppInstNodeInfoViewDTO GetNodeInfoByStrId(string nodeStrId, int instId);

    }
}
