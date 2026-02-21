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
    /// 节点信息
    /// </summary>
    public partial class FlowTempNodeInfoService
    {
        private IMapper _IMappers;
        public void Test(IMapper _IMapper)
        {
            _IMappers = _IMapper;
        }
        /// <summary>
        /// 保存节点信息
        /// </summary>
        /// <param name="flowTempNodeInfo">保存节点信息</param>
        /// <returns></returns>
        public int SaveFlowTempNodeInfo(FlowTempNodeInfo flowTempNodeInfo, IMapper _IMapper)
        {
            Test(_IMapper);
            string sqlstr = $"delete FlowTempNodeInfo where NodeStrId='{flowTempNodeInfo.NodeStrId}'";
            ExecuteSqlCommand(sqlstr);
            Db.Set<FlowTempNodeInfo>().Add(flowTempNodeInfo);
            var histTemp = Db.Set<FlowTempHist>().Where(a => a.TempId == flowTempNodeInfo.TempId)
                .OrderByDescending(a => a.Id).FirstOrDefault();
            var tempNodeInfoHist = _IMapper.Map<FlowTempNodeInfo, FlowTempNodeInfoHist>(flowTempNodeInfo);// flowTempNodeInfo.ToModel<FlowTempNodeInfo, FlowTempNodeInfoHist>();
            tempNodeInfoHist.TempHistId = histTemp != null ? histTemp.Id : 0;
            Db.Set<FlowTempNodeInfoHist>().Add(tempNodeInfoHist);
            return Db.SaveChanges();

        }
        /// <summary>
        /// 根据节点ID获取节点信息
        /// </summary>
        /// <param name="nodeStrId">节点ID</param>
        /// <returns></returns>
        public FlowTempNodeInfoViewDTO GetNodeInfoByStrId(string nodeStrId, int tempId)
        {
            var query = from a in _FlowTempNodeInfoSet.AsNoTracking()
                        where a.NodeStrId == nodeStrId
                               &&a.TempId== tempId
                        select new
                        {
                            Id=a.Id,
                            NodeStrId=a.NodeStrId,
                            Nrule=a.Nrule,
                            ReviseText= a.ReviseText,
                            Max=a.Max,
                            Min=a.Min,
                            IsMax=a.IsMax,
                            IsMin=a.IsMin,
                            GroupId=a.GroupId,
                            GroupName= a.Group.Name,
                            TempId=a.TempId,
                            Isktz = a.Isktz
                        };
            var local = from a in query.AsEnumerable()
                        select new FlowTempNodeInfoViewDTO
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
                            GroupName=a.GroupName,
                            TempId = a.TempId,
                           UserNames = GetUserNames(a.GroupId??0),
                             Isktz = a.Isktz

                        };

            return local.FirstOrDefault();


        }
       /// <summary>
       /// 用户名称
       /// </summary>
       /// <param name="groupId">组ID</param>
       /// <returns></returns>
        private string GetUserNames(int groupId)
        {
            var listIds = this.Db.Set<GroupUser>().Where(a => a.GroupId == groupId).Select(a => a.UserId).ToList();
            var listuserName = this.Db.Set<UserInfor>().Where(a => listIds.Contains(a.Id)).Select(a => a.DisplyName).ToList();
            return StringHelper.ArrayString2String(listuserName);
        }
    }
}
