using NF.AutoMapper;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Common;
using AutoMapper;

namespace NF.BLL
{

    /// <summary>
    /// 流程节点
    /// </summary>
    public partial  class FlowTempNodeService
    {
        private IMapper _IMappers;
        public void Test(IMapper _IMapper)
        {
            _IMappers = _IMapper;
        }
       
        /// <summary>
        /// 保存节点信息
        /// </summary>
        /// <param name="flowNodeData">流程节点信息</param>
        /// <param name="tempId">流程模板ID</param>
        /// <returns></returns>
        public int AddFlowNodes(FlowNodeDataJson flowNodeData, int tempId, IMapper _IMapper)
        {
            Test(_IMapper);
            //删除
            DeleteFlowNodes(tempId);
            var histTemp = Db.Set<FlowTempHist>().AsTracking().OrderByDescending(a => a.Id).FirstOrDefault();
            var histTempId = histTemp ==null? 0 : histTemp.Id;
            AddNode(flowNodeData, tempId, histTempId);
            AddLine(flowNodeData, tempId, histTempId);
            AddArea(flowNodeData, tempId, histTempId);
            return this.Db.SaveChanges();
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="histTempId">历史模板ID</param>
        /// <param name="tempId">模板ID</param>
        /// <param name="flowNodeData">节点数据</param>
        private void AddNode(FlowNodeDataJson flowNodeData, int tempId,int histTempId)
        {
            var tmpnodes = flowNodeData.nodes;
            foreach (var key in tmpnodes.Keys)
            {
                var node = tmpnodes[key];
                var tempNode = _IMappers.Map<FlowTempNodeViewDTO, FlowTempNode>(node);// node.ToModel<FlowTempNodeViewDTO, FlowTempNode>();
                tempNode.Alt = Convert.ToByte(node.alt ? 1 : 0);
                tempNode.Marked = Convert.ToByte(node.marked ? 1 : 0);
                tempNode.Type = EmunUtility.GetValue(typeof(NodeTypeEnum), node.type);
                tempNode.TempId = tempId;
                this.Db.Set<FlowTempNode>().Add(tempNode);
                //创建历史
                var tempNodeHist = _IMappers.Map<FlowTempNode, FlowTempNodeHist>(tempNode);// tempNode.ToModel<FlowTempNode, FlowTempNodeHist>();
                tempNodeHist.TempHistId = histTempId;
                this.Db.Set<FlowTempNodeHist>().Add(tempNodeHist);


            }
        }
        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <param name="histTempId">模板历史ID</param>
        /// <param name="flowNodeData">保存数据</param>
        private void AddLine(FlowNodeDataJson flowNodeData, int tempId, int histTempId)
        {
            var lines = flowNodeData.lines;
            foreach (var key in lines.Keys)
            {
                var line = lines[key];
                
                var templine = _IMappers.Map<TempNodeLineViewDTO, TempNodeLine>(line);//  line.ToModel<TempNodeLineViewDTO, TempNodeLine>();
                templine.Alt = Convert.ToByte(line.alt ? 1 : 0);
                templine.Marked = Convert.ToByte(line.marked ? 1 : 0);
                templine.Type = EmunUtility.GetValue(typeof(NodeLineTypeEnum), line.type);
                templine.Dash = Convert.ToByte(line.dash ? 1 : 0);
                templine.TempId = tempId;
               
                this.Db.Set<TempNodeLine>().Add(templine);
                //创建历史
                var tempnodelinehist= _IMappers.Map<TempNodeLine, TempNodeLineHist>(templine);//  templine.ToModel<TempNodeLine, TempNodeLineHist>();
                tempnodelinehist.TempHistId = histTempId;
                this.Db.Set<TempNodeLineHist>().Add(tempnodelinehist);
            }
        }
        /// <summary>
        /// 添加区域
        /// </summary>
        /// <param name="flowNodeData"></param>
        private void AddArea(FlowNodeDataJson flowNodeData, int tempId, int histTempId)
        {
            var areas = flowNodeData.areas;
            foreach (var key in areas.Keys)
            {
                var area = areas[key];
                var temparea = _IMappers.Map<TempNodeAreaViewDTO, TempNodeArea>(area);
               // var temparea = area.ToModel<TempNodeAreaViewDTO, TempNodeArea>();
                temparea.Alt = Convert.ToByte(area.alt ? 1 : 0);
                temparea.Color = EmunUtility.GetValue(typeof(ArearColorEnum), area.color);
                temparea.TempId = tempId;
                this.Db.Set<TempNodeArea>().Add(temparea);
                //创建历史
                var tempareahist = _IMappers.Map<TempNodeArea, TempNodeAreaHist>(temparea);// temparea.ToModel<TempNodeArea, TempNodeAreaHist>();
                tempareahist.TempHistId = histTempId;
                this.Db.Set<TempNodeAreaHist>().Add(tempareahist);


            }
        }

        /// <summary>
        /// 清除节点数据
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        public int ClearFlowNodes(int tempId)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append($"delete FlowTempNode where TempId={tempId};");
            sqlstr.Append($"delete FlowTempNodeHist where TempId={tempId};");
            sqlstr.Append($"delete FlowTempNodeInfo where TempId={tempId};");
            sqlstr.Append($"delete FlowTempNodeInfoHist where TempId={tempId};");
            
            sqlstr.Append($"delete TempNodeLine where TempId={tempId};");
            sqlstr.Append($"delete TempNodeLine where TempId={tempId};");
            sqlstr.Append($"delete TempNodeLineHist where TempId={tempId};");
            sqlstr.Append($"delete TempNodeArea where TempId={tempId};");
            sqlstr.Append($"delete TempNodeAreaHist where TempId={tempId};");
            return ExecuteSqlCommand(sqlstr.ToString());


        }

        private int DeleteFlowNodes(int tempId)
        {
            StringBuilder sqlstr = new StringBuilder();
            //节点
            sqlstr.Append($"delete FlowTempNode where TempId={tempId};");
            //节点信息
            sqlstr.Append($"delete FlowTempNodeInfo where TempId={tempId};");
            //节点连线
            sqlstr.Append($"delete TempNodeLine where TempId={tempId};");
            //区域
            sqlstr.Append($"delete TempNodeArea where TempId={tempId};");

            return ExecuteSqlCommand(sqlstr.ToString());
        }

        #region 加载节点
        /// <summary>
        /// 加载模板节点
        /// </summary>
        /// <param name="tempId">模板Id</param>
        /// <returns></returns>
        public FlowNodeDataJson LoadNodes(int tempId, IMapper _IMapper)
        {
            Test(_IMapper);
            FlowNodeDataJson dataJson = new FlowNodeDataJson();
            var flowtemp = Db.Set<FlowTemp>().Where(a=>a.Id== tempId).FirstOrDefault();
            dataJson.nodes = GetFlowNodeView(tempId);
            dataJson.lines = GetLineView(tempId);
            dataJson.areas = GetAreaView(tempId);
            dataJson.title = flowtemp == null ? "" : flowtemp.Name;
            dataJson.initNum = 16;
            return dataJson;
        }

        /// <summary>
        /// 加载模板节点
        /// </summary>
        /// <param name="tempinfo">模板对象</param>
        /// <returns></returns>
        public FlowNodeDataJson LoadNodes(FlowTemp tempinfo, IMapper _IMapper)
        {
            Test(_IMapper);
            FlowNodeDataJson dataJson = new FlowNodeDataJson();
           // var flowtemp = Db.Set<FlowTemp>().Where(a => a.Id == tempinfo).FirstOrDefault();
            dataJson.nodes = GetFlowNodeView(tempinfo.Id);
            dataJson.lines = GetLineView(tempinfo.Id);
            dataJson.areas = GetAreaView(tempinfo.Id);
            dataJson.title = tempinfo == null ? "" : tempinfo.Name;
            dataJson.initNum = 16;
            return dataJson;
        }
        /// <summary>
        /// 根据模板ID获取节点字典
        /// </summary>
        /// <param name="tempId">模板ID</param>
        private Dictionary<string, FlowTempNodeViewDTO> GetFlowNodeView(int tempId)
        {
            var nodes = Db.Set<FlowTempNode>().AsNoTracking().Where(a => a.TempId == tempId).ToList();
            Dictionary<string, FlowTempNodeViewDTO> dicnodes = new Dictionary<string, FlowTempNodeViewDTO>();
            foreach (var node in nodes)
            {
                var viewnode = _IMappers.Map<FlowTempNode, FlowTempNodeViewDTO>(node);// node.ToModel<FlowTempNode, FlowTempNodeViewDTO>();
                viewnode.type = EmunUtility.GetDesc(typeof(NodeTypeEnum), (node.Type ?? -1));
                viewnode.alt = node.Alt == 1 ? true : false;
                viewnode.marked = node.Marked == 1 ? true : false;
                dicnodes.Add(viewnode.strid, viewnode);

            }
            return dicnodes;
        }

        /// <summary>
        /// 根据模板ID获取节点字典
        /// </summary>
        /// <param name="tempId">模板ID</param>
        private Dictionary<string, TempNodeLineViewDTO> GetLineView(int tempId)
        {
            var lines = Db.Set<TempNodeLine>().AsNoTracking().Where(a => a.TempId == tempId).ToList();
            Dictionary<string, TempNodeLineViewDTO> dicnodes = new Dictionary<string, TempNodeLineViewDTO>();
            foreach (var line in lines)
            {
                var viewline = _IMappers.Map<TempNodeLine, TempNodeLineViewDTO>(line);// line.ToModel<TempNodeLine, TempNodeLineViewDTO>();
                viewline.type = EmunUtility.GetDesc(typeof(NodeLineTypeEnum), (line.Type ?? -1));
                viewline.alt = line.Alt == 1 ? true : false;
                viewline.marked = line.Marked == 1 ? true : false;
                viewline.dash = line.Marked == 1 ? true : false;
                dicnodes.Add(viewline.strid, viewline);

            }
            return dicnodes;
        }
        /// <summary>
        /// 根据模板ID获取节点字典
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <param name="nodestrId">匹配上的节点ID集合</param>
        private Dictionary<string, TempNodeLineViewDTO> GetLineView(int tempId, IList<string> nodestrIds)
        {
            var lines = Db.Set<TempNodeLine>().AsNoTracking().Where(a => a.TempId == tempId).ToList();
            Dictionary<string, TempNodeLineViewDTO> dicnodes = new Dictionary<string, TempNodeLineViewDTO>();
            foreach (var line in lines)
            {
                var viewline = _IMappers.Map<TempNodeLine, TempNodeLineViewDTO>(line);// line.ToModel<TempNodeLine, TempNodeLineViewDTO>();
                viewline.type = EmunUtility.GetDesc(typeof(NodeLineTypeEnum), (line.Type ?? -1));
                viewline.alt = line.Alt == 1 ? true : false;
                viewline.marked = line.Marked == 1 ? true : false;
                viewline.dash = line.Dash == 1 ? true : false;
                if (!nodestrIds.Any(a => a == viewline.to) && !nodestrIds.Any(a => a == viewline.from))
                {
                    viewline.dash = true;//细线
                }
                dicnodes.Add(viewline.strid, viewline);

            }
            return dicnodes;
        }
        /// <summary>
        /// 区域
        /// </summary>
        /// <param name="tempId">模板ID</param>
        private Dictionary<string, TempNodeAreaViewDTO> GetAreaView(int tempId)
        {
            var areas = Db.Set<TempNodeArea>().AsNoTracking().Where(a => a.TempId == tempId).ToList();
            Dictionary<string, TempNodeAreaViewDTO> dicnodes = new Dictionary<string, TempNodeAreaViewDTO>();
            foreach (var area in areas)
            {
                var viewarea = _IMappers.Map<TempNodeArea, TempNodeAreaViewDTO>(area);// area.ToModel<TempNodeArea, TempNodeAreaViewDTO>();
                viewarea.color = EmunUtility.GetDesc(typeof(ArearColorEnum), (area.Color??0));
                viewarea.alt = area.Alt == 1 ? true : false;
              
                dicnodes.Add(viewarea.strid, viewarea);

            }
            return dicnodes;
        }

        #endregion

        #region 加载节点重装
        /// <summary>
        /// 提交流程时显示流程图
        /// </summary>
        /// <param name="submitWfRes">提交流程时参数对象</param>
        /// <returns></returns>
        public FlowNodeDataJson LoadNodes(SubmitWfResParam submitWfRes, IMapper _IMapper)
        {
            FlowNodeDataJson dataJson = new FlowNodeDataJson();
            var flowtemp = Db.Set<FlowTemp>().Where(a => a.Id == submitWfRes.TempId).FirstOrDefault();
            switch (flowtemp.ObjType)
            {
                case (int)FlowObjEnums.Customer:
                case (int)FlowObjEnums.Supplier:
                case (int)FlowObjEnums.Other:
                case (int)FlowObjEnums.project:
                case (int)FlowObjEnums.Inquiry:
                case (int)FlowObjEnums.Questioning:
                case (int)FlowObjEnums.Tender:

                    dataJson = LoadNodes(flowtemp , _IMapper);
                    break;
                case (int)FlowObjEnums.Contract:
                case (int)FlowObjEnums.InvoiceIn:
                case (int)FlowObjEnums.InvoiceOut:
                case (int)FlowObjEnums.payment:
                    {
                        dataJson= LoadNodes(submitWfRes.TempId, _IMapper);
                        var listnodes = FlowServoceUtility.GetNodeStrIds(submitWfRes,this.Db);
                        dataJson.lines = GetLineView(submitWfRes.TempId, listnodes);
                    }
                    break;


            }
            return dataJson;
        }

        #endregion

        
        /// <summary>
        /// 提交流程时显示流程图程序入口
        /// </summary>
        /// <param name="submitWfRes">提交流程是参数对象</param>
        /// <returns></returns>
        public FlowNodeDataJson SubmitLoadNodes(SubmitWfResParam submitWfRes, IMapper _IMapper)
        {
            return LoadNodes(submitWfRes,_IMapper);
        }
           
        }
}
