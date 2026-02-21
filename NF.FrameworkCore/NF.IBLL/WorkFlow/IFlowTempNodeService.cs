using AutoMapper;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 流程节点添加
    /// </summary>
    public partial interface IFlowTempNodeService
    {
        /// <summary>
        /// 保存节点信息
        /// </summary>
        /// <param name="flowNodeData">流程节点信息</param>
        /// <param name="tempId">流程模板ID</param>
        /// <returns></returns>
        int AddFlowNodes(FlowNodeDataJson flowNodeData,int tempId, IMapper _IMapper);
        /// <summary>
        /// 清除节点数据
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        int ClearFlowNodes(int tempId);
        /// <summary>
        /// 根据模板加载节点
        /// </summary>
        /// <param name="submitWfRes">请求对象参数</param>
        /// <returns></returns>
        FlowNodeDataJson LoadNodes(SubmitWfResParam submitWfRes, IMapper _IMapper);

        /// <summary>
        /// 加载模板节点
        /// </summary>
        /// <param name="tempId">模板Id</param>
        /// <returns></returns>
        FlowNodeDataJson LoadNodes(int tempId, IMapper _IMapper);

    }
}
