using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 流程实例节点
    /// </summary>
    public partial  class AppInstNodeService
    {
        private IMapper _IMapper;

        public void s( IMapper _IMappers) {
            _IMapper = _IMappers;
        }
        #region 查看页面加载流图片
        public  AppFlowNodeDataJson LoadFlowChart(int instId, IMapper _IMapper)
        {
            s(_IMapper);
            AppFlowNodeDataJson dataJson = new AppFlowNodeDataJson();
            //var flowtemp = Db.Set<FlowTemp>().Where(a => a.Id == tempId).FirstOrDefault();
            dataJson.nodes = GetFlowNodeView(instId);
            dataJson.lines = GetLineView(instId);
            dataJson.areas = GetAreaView(instId);
           // dataJson.title = flowtemp == null ? "" : flowtemp.Name;
            dataJson.initNum = 16;
            return dataJson;
        }
        /// <summary>
        /// 根据模板ID获取节点字典
        /// </summary>
        /// <param name="instId">实例ID</param>
        private Dictionary<string, AppInstNodeViwDTO> GetFlowNodeView(int instId)
        {
            var nodes = Db.Set<AppInstNode>().AsNoTracking().Where(a => a.InstId == instId).ToList();
            Dictionary<string, AppInstNodeViwDTO> dicnodes = new Dictionary<string, AppInstNodeViwDTO>();
            foreach (var node in nodes)
            {
                var viewnode = _IMapper.Map<AppInstNode, AppInstNodeViwDTO>(node); //node.ToModel<AppInstNode, AppInstNodeViwDTO>();
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
        /// <param name="instId">实例ID</param>
        private Dictionary<string, AppInstNodeLineViwDTO> GetLineView(int instId)
        {
            var lines = Db.Set<AppInstNodeLine>().AsNoTracking().Where(a => a.InstId == instId).ToList();
            Dictionary<string, AppInstNodeLineViwDTO> dicnodes = new Dictionary<string, AppInstNodeLineViwDTO>();
            foreach (var line in lines)
            {
                var viewline = _IMapper.Map<AppInstNodeLine, AppInstNodeLineViwDTO>(line); // line.ToModel<AppInstNodeLine, AppInstNodeLineViwDTO>();
                viewline.type = EmunUtility.GetDesc(typeof(NodeLineTypeEnum), (line.Type ?? -1));
                viewline.alt = line.Alt == 1 ? true : false;
                viewline.marked = line.Marked == 1 ? true : false;
                viewline.dash = line.Dash == 1 ? true : false;
                dicnodes.Add(viewline.strid, viewline);

            }
            return dicnodes;
        }
        /// <summary>
        /// 区域
        /// </summary>
        /// <param name="instId">实例ID</param>
        private Dictionary<string, AppInstNodeAreaViewDTO> GetAreaView(int instId)
        {
            var areas = Db.Set<AppInstNodeArea>().AsNoTracking().Where(a => a.InstId == instId).ToList();
            Dictionary<string, AppInstNodeAreaViewDTO> dicnodes = new Dictionary<string, AppInstNodeAreaViewDTO>();
            foreach (var area in areas)
            {
                var viewarea = _IMapper.Map<AppInstNodeArea, AppInstNodeAreaViewDTO>(area); // area.ToModel<AppInstNodeArea, AppInstNodeAreaViewDTO>();
                viewarea.color = EmunUtility.GetDesc(typeof(ArearColorEnum), (area.Color ?? 0));
                viewarea.alt = area.Alt == 1 ? true : false;

                dicnodes.Add(viewarea.strid, viewarea);

            }
            return dicnodes;
        }

        #endregion

        /// <summary>
        /// 根据节点ID获取节点信息
        /// </summary>
        /// <param name="nodeStrId">节点ID</param>
        /// <param name="instId">实例节点ID</param>
        /// <returns></returns>
        public AppInstNodeInfoViewDTO GetNodeInfoByStrId(string nodeStrId, int instId)
        {
            var query = from a in this.Db.Set<AppInstNodeInfo>().AsNoTracking()
                        //join b in this.Db.Set<AppGroupUser>().AsNoTracking()
                        //on a.NodeStrId equals b.NodeStrId
                        where a.NodeStrId == nodeStrId
                               && a.InstId == instId
                        select new
                        {
                            Id = a.Id,
                            NodeStrId = a.NodeStrId,
                            Nrule = a.Nrule,
                            ReviseText = a.ReviseText,
                            Max = a.Max,
                            Min = a.Min,
                            IsMax = a.IsMax,
                            IsMin = a.IsMin,
                            GroupId = a.GroupId,
                            GroupName = a.GroupName,
                            InstId = a.InstId,
                            NodeState=a.NodeState
                           
                           
                        };
            var local = from a in query.AsEnumerable()
                        select new AppInstNodeInfoViewDTO
                        {
                            Id = a.Id,
                            NodeStrId = a.NodeStrId,
                            Nrule = a.Nrule,
                            ReviseText = a.ReviseText,
                            Max = a.Max,
                            Min = a.Min,
                            IsMax = a.IsMax,
                            IsMin = a.IsMin,
                            GroupId = a.GroupId,
                            GroupName = a.GroupName,
                            InstId = a.InstId,
                           UserNames = GetUserNames(a.GroupId??0,a.InstId??0),
                            StateDic= EmunUtility.GetDesc(typeof(NodeStateEnum), a.NodeState??-1),

                        };

            return local.FirstOrDefault();


        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        private string GetUserNames(int groupId,int instId)
        {
            //var listids = StringHelper.String2ArrayInt(userIds);
            //var listuserName = Db.Set<UserInfor>().Where(a => listids.Contains(a.Id)).Select(a => a.DisplyName).ToList();
            //return StringHelper.ArrayString2String(listuserName);


            var listIds = Db.Set<AppGroupUser>().Where(a => a.GroupId == groupId&&a.InstId== instId).Select(a => a.UserId).ToList();
            var listuserName = Db.Set<UserInfor>().Where(a => listIds.Contains(a.Id)).Select(a => a.DisplyName).ToList();
            return StringHelper.ArrayString2String(listuserName);


        }

    }
}
