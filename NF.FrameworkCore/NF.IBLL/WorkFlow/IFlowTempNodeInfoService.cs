using AutoMapper;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 节点信息
    /// </summary>
    public partial  interface IFlowTempNodeInfoService
    {
        /// <summary>
        /// 保存流程节点信息
        /// </summary>
        /// <param name="flowTempNodeInfo">流程节点信息</param>
        /// <returns></returns>
        int SaveFlowTempNodeInfo(FlowTempNodeInfo flowTempNodeInfo, IMapper _IMapper);
        /// <summary>
        /// 根据节点ID获取节点信息
        /// </summary>
        /// <param name="nodeStrId">节点ID</param>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        FlowTempNodeInfoViewDTO GetNodeInfoByStrId(string nodeStrId, int tempId);

    }
}
