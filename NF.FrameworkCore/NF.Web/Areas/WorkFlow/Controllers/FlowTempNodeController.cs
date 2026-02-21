using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.Web.Areas.WorkFlow.Data;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.WorkFlow.Controllers
{
    [Area("WorkFlow")]
    [Route("WorkFlow/[controller]/[action]")]
    public class FlowTempNodeController : NfBaseController
    {
        private IFlowTempNodeService _IFlowTempNodeService;
        private IMapper _IMapper;
        private IFlowTempNodeInfoService _IFlowTempNodeInfoService;
        public FlowTempNodeController(IFlowTempNodeService IFlowTempNodeService, IMapper IMapper,
            IFlowTempNodeInfoService IFlowTempNodeInfoService)
        {
            _IFlowTempNodeService = IFlowTempNodeService;
            _IMapper = IMapper;
            _IFlowTempNodeInfoService = IFlowTempNodeInfoService;
        }
        #region 节点加载测试数据
        /// <summary>
        /// 直接让路径请求这就显示一个dome图例
        /// </summary>
        /// <param name="TempId"></param>
        /// <returns></returns>
        public IActionResult TestNodeData(int TempId)
        {
            var data = TempNodeJsonHilper.GetTempNodeData();
            return new CustomResultJson(data);
        }
        #endregion

        /// <summary>
        /// 流程节点加载
        /// </summary>
        /// <param name="TempId">模板ID</param>
        /// <returns></returns>
        public IActionResult TempFlowNodeLoad(int TempId)
        {
            var data = _IFlowTempNodeService.LoadNodes(TempId,_IMapper);
            return new CustomResultJson(data);
        }

        /// <summary>
        /// 流程节点加载
        /// </summary>
        /// <param name="submitWfRes">请求对象</param>
        /// <returns></returns>
        public IActionResult SubmitFlowNodeLoad(SubmitWfResParam submitWfRes)
        {
            var data = _IFlowTempNodeService.LoadNodes(submitWfRes, _IMapper);
            return new CustomResultJson(data);
        }


        /// <summary>
        /// 保存节点
        /// </summary>
        /// <param name="flowNodeData">节点json字符串</param>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        public IActionResult SaveNode(string flowNodeData,int tempId)
        {
           
            var nodeData=NodeJsonDataUtility.DeserializeToInfo(flowNodeData);
            _IFlowTempNodeService.AddFlowNodes(nodeData, tempId, _IMapper);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 新建流程图清除当前节点所有数据
        /// </summary>
        /// <returns></returns>
        public IActionResult ClearNodeData(int tempId)
        {
            _IFlowTempNodeService.ClearFlowNodes(tempId);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 设置节点信息
        /// </summary>
        /// <returns></returns>
        public IActionResult SetNodeInfo()
        {
            return View();
        }
        /// <summary>
        /// 保存节点信息
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveNodeInfo(FlowTempNodeInfoDTO tempNodeInfoDTO)
        {

            var saveInfo = _IMapper.Map<FlowTempNodeInfo>(tempNodeInfoDTO);
            _IFlowTempNodeInfoService.SaveFlowTempNodeInfo(saveInfo, _IMapper);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 根据节点ID查询节点信息
        /// </summary>
        /// <returns></returns>
        public IActionResult GetNodeInfoView(string nodeStr,int tempId)
        {
            var info = _IFlowTempNodeInfoService.GetNodeInfoByStrId(nodeStr, tempId);
            if (info == null)
            {
                info= new FlowTempNodeInfoViewDTO() {
                    Id=0,
                    NodeStrId= nodeStr,
                    TempId= tempId,
                    UserNames="",
                    GroupName=""

                };
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info


            });
        }

    }
}