using AutoMapper;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.AutoMapper
{
    /// <summary>
    /// 流程相关映射文件
    /// </summary>
    public class FlowProfile : Profile, IProfile
    {
        public FlowProfile()
        {
            #region 流程 模板

            //组
            CreateMap<GroupInfoDTO, GroupInfo>()
          .ForMember(a => a.Gstate, opt => opt.Ignore())
          .ForMember(a => a.IsDelete, opt => opt.Ignore())
          .ForMember(a => a.CreateUserId, opt => opt.Ignore())
          .ForMember(a => a.CreateDateTime, opt => opt.Ignore());
            //流程模板
            CreateMap<FlowTempDTO, FlowTemp>()
           .ForMember(a => a.IsValid, opt => opt.Ignore())
           .ForMember(a => a.IsDelete, opt => opt.Ignore())
           .ForMember(a => a.CreateUserId, opt => opt.Ignore())
           .ForMember(a => a.CreateDateTime, opt => opt.Ignore());
            //流程模板->模板历史
            CreateMap<FlowTemp, FlowTempHist>()
           .ForMember(a => a.Id, opt => opt.Ignore())
           .ForMember(a => a.TempId, opt => opt.Ignore());
            //模板显示节点-模板数据节点
            CreateMap<FlowTempNodeViewDTO, FlowTempNode>()
            .ForMember(d => d.Name, opt =>
            {
                opt.MapFrom(s => s.name);
            }).ForMember(d => d.StrId, opt =>
            {
                opt.MapFrom(s => s.strid);
            }).ForMember(d => d.Height, opt =>
            {
                opt.MapFrom(s => s.height);
            }).ForMember(d => d.Width, opt =>
            {
                opt.MapFrom(s => s.width);
            })
           .ForMember(d => d.Left, opt =>
           {
               opt.MapFrom(s => s.left);
           }).ForMember(d => d.Top, opt =>
           {
               opt.MapFrom(s => s.top);
           })
            .ForMember(a => a.Type, opt => opt.Ignore())
             .ForMember(a => a.Alt, opt => opt.Ignore())
             .ForMember(a => a.Marked, opt => opt.Ignore())
             .ForMember(a => a.TempId, opt => opt.Ignore())
             .ForMember(a => a.Id, opt => opt.Ignore());

            //节点显示线-节点线实体
            CreateMap<TempNodeLineViewDTO, TempNodeLine>()
            .ForMember(d => d.Name, opt =>
            {
                opt.MapFrom(s => s.name);
            }).ForMember(d => d.StrId, opt =>
            {
                opt.MapFrom(s => s.strid);
            }).ForMember(d => d.From, opt =>
            {
                opt.MapFrom(s => s.from);
            }).ForMember(d => d.To, opt =>
            {
                opt.MapFrom(s => s.to);
            })
            .ForMember(a => a.Type, opt => opt.Ignore())
             .ForMember(a => a.Alt, opt => opt.Ignore())
             .ForMember(a => a.Marked, opt => opt.Ignore())
             .ForMember(a => a.TempId, opt => opt.Ignore())
             .ForMember(a => a.Id, opt => opt.Ignore())
             .ForMember(a => a.Dash, opt => opt.Ignore());

            //模板显示区域-区域数据节点
            CreateMap<TempNodeAreaViewDTO, TempNodeArea>()
            .ForMember(d => d.Name, opt =>
            {
                opt.MapFrom(s => s.name);
            }).ForMember(d => d.StrId, opt =>
            {
                opt.MapFrom(s => s.strid);
            }).ForMember(d => d.Height, opt =>
            {
                opt.MapFrom(s => s.height);
            }).ForMember(d => d.Width, opt =>
            {
                opt.MapFrom(s => s.width);
            })
            .ForMember(d => d.Left, opt =>
            {
                opt.MapFrom(s => s.left);
            }).ForMember(d => d.Top, opt =>
            {
                opt.MapFrom(s => s.top);
            })

             .ForMember(a => a.Alt, opt => opt.Ignore())
            .ForMember(a => a.Color, opt => opt.Ignore())
             .ForMember(a => a.TempId, opt => opt.Ignore())
             .ForMember(a => a.Id, opt => opt.Ignore());

            //流程模板节点->模板节点历史
            CreateMap<FlowTempNode, FlowTempNodeHist>()
            .ForMember(a => a.Id, opt => opt.Ignore())
             .ForMember(a => a.TempHistId, opt => opt.Ignore());
            //流程模板节点连线->模板节点连线历史
            CreateMap<TempNodeLine, TempNodeLineHist>()
            .ForMember(a => a.Id, opt => opt.Ignore())
             .ForMember(a => a.TempHistId, opt => opt.Ignore());
            //流程模板节点区域->模板节点区域历史
            CreateMap<TempNodeArea, TempNodeAreaHist>()
            .ForMember(a => a.Id, opt => opt.Ignore())
             .ForMember(a => a.TempHistId, opt => opt.Ignore());

            //模板显示节点-模板数据节点
            CreateMap<FlowTempNode, FlowTempNodeViewDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.StrId);
            }).ForMember(d => d.height, opt =>
            {
                opt.MapFrom(s => s.Height);
            }).ForMember(d => d.width, opt =>
            {
                opt.MapFrom(s => s.Width);
            })
           .ForMember(d => d.left, opt =>
           {
               opt.MapFrom(s => s.Left);
           }).ForMember(d => d.top, opt =>
           {
               opt.MapFrom(s => s.Top);
           })
            .ForMember(a => a.type, opt => opt.Ignore())
             .ForMember(a => a.alt, opt => opt.Ignore())
             .ForMember(a => a.marked, opt => opt.Ignore());

            //区域数据节点->模板显示区域
            CreateMap<TempNodeArea, TempNodeAreaViewDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.StrId);
            }).ForMember(d => d.height, opt =>
            {
                opt.MapFrom(s => s.Height);
            }).ForMember(d => d.width, opt =>
            {
                opt.MapFrom(s => s.Width);
            })
            .ForMember(d => d.left, opt =>
            {
                opt.MapFrom(s => s.Left);
            }).ForMember(d => d.top, opt =>
            {
                opt.MapFrom(s => s.Top);
            })

             .ForMember(a => a.alt, opt => opt.Ignore())
            .ForMember(a => a.color, opt => opt.Ignore());

            //节点线实体->节点显示线
            CreateMap<TempNodeLine, TempNodeLineViewDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.StrId);
            }).ForMember(d => d.from, opt =>
            {
                opt.MapFrom(s => s.From);
            }).ForMember(d => d.to, opt =>
            {
                opt.MapFrom(s => s.To);
            })
            .ForMember(a => a.type, opt => opt.Ignore())
             .ForMember(a => a.alt, opt => opt.Ignore())
             .ForMember(a => a.marked, opt => opt.Ignore())
             .ForMember(a => a.dash, opt => opt.Ignore());

            //流程模板节点信息-模板节点信息历史
            CreateMap<FlowTempNodeInfo, FlowTempNodeInfoHist>()
                .ForMember(a => a.Id, opt => opt.Ignore())
                .ForMember(a => a.TempHistId, opt => opt.Ignore());
            //节点信息DTO->节点信息
            CreateMap<FlowTempNodeInfoDTO, FlowTempNodeInfo>()
               .ForMember(a => a.Id, opt => opt.Ignore());

            #endregion

            #region 流程提交

            #region 实例->实例历史
            CreateMap<AppInst, AppInstHist>()
           .ForMember(a => a.Id, opt => opt.Ignore())
           .ForMember(a => a.InstId, opt => opt.Ignore())
           .ForMember(a => a.CreateDateTime, opt => opt.Ignore());
            #endregion

            #region 模板节点->实例节点
            CreateMap<FlowTempNode, AppInstNode>()
           .ForMember(a => a.Id, opt => opt.Ignore())
           .ForMember(a => a.InstId, opt => opt.Ignore())
           .ForMember(a => a.TempHistId, opt => opt.Ignore())
           .ForMember(a => a.Norder, opt => opt.Ignore())
           .ForMember(a => a.NodeState, opt => opt.Ignore())
           .ForMember(a => a.NodeStrId, opt =>
           {
               opt.MapFrom(s => s.StrId);
           });
            #endregion

            #region 模板节点->实例历史节点
            CreateMap<FlowTempNode, AppInstNodeHist>()
           .ForMember(a => a.Id, opt => opt.Ignore())
           .ForMember(a => a.InstHistId, opt => opt.Ignore())
           .ForMember(a => a.TempHistId, opt => opt.Ignore())
           .ForMember(a => a.Norder, opt => opt.Ignore())
           .ForMember(a => a.NodeState, opt => opt.Ignore())
            .ForMember(a => a.NodeStrId, opt =>
            {
                opt.MapFrom(s => s.StrId);
            });
            #endregion

            #region 模板节点信息->实例节点信息
            CreateMap<FlowTempNodeInfo, AppInstNodeInfo>()
           .ForMember(a => a.Id, opt => opt.Ignore())
            .ForMember(a => a.InstId, opt => opt.Ignore())
           .ForMember(a => a.InstNodeId, opt => opt.Ignore());
            #endregion

            #region 模板节点信息->实例节点历史信息
            CreateMap<FlowTempNodeInfo, AppInstNodeInfoHist>()
           .ForMember(a => a.Id, opt => opt.Ignore())
            .ForMember(a => a.InstHistId, opt => opt.Ignore())
           .ForMember(a => a.InstNodeHistId, opt => opt.Ignore());
            #endregion

            #region 模板节点连线->实例节点连线信息
            CreateMap<TempNodeLine, AppInstNodeLine>()
           .ForMember(a => a.Id, opt => opt.Ignore())
            .ForMember(a => a.InstId, opt => opt.Ignore());
            #endregion

            #region 模板节点连线->实例节点连线历史信息
            CreateMap<TempNodeLine, AppInstNodeLineHist>()
           .ForMember(a => a.Id, opt => opt.Ignore())
            .ForMember(a => a.InstHistId, opt => opt.Ignore());
            #endregion

            #region 模板节点区域->实例节点区域信息
            CreateMap<TempNodeArea, AppInstNodeArea>()
           .ForMember(a => a.Id, opt => opt.Ignore())
            .ForMember(a => a.InstId, opt => opt.Ignore());
            #endregion

            #region 模板节点连线->实例节点连线历史信息
            CreateMap<TempNodeArea, AppInstNodeAreaHist>()
           .ForMember(a => a.Id, opt => opt.Ignore())
            .ForMember(a => a.InstHistId, opt => opt.Ignore());
            #endregion

            #region 审批实例DTO->审批实例
             CreateMap<AppInstDTO, AppInst>()
             .ForMember(a => a.Id, opt => opt.Ignore())
            .ForMember(a => a.Version, opt => opt.Ignore())
            .ForMember(a => a.AppState, opt => opt.Ignore())
            .ForMember(a => a.StartUserId, opt => opt.Ignore())
            .ForMember(a => a.StartDateTime, opt => opt.Ignore())
            .ForMember(a => a.CreateUserId, opt => opt.Ignore())
            .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
            .ForMember(a => a.CurrentNodeId, opt => opt.Ignore())
            .ForMember(a => a.CurrentNodeName, opt => opt.Ignore())
            .ForMember(a => a.CompleteDateTime, opt => opt.Ignore());
            #endregion


            #region 实例流程图

            //实例显示节点-实例数据节点
            CreateMap<AppInstNode, AppInstNodeViwDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.NodeStrId);
            }).ForMember(d => d.height, opt =>
            {
                opt.MapFrom(s => s.Height);
            }).ForMember(d => d.width, opt =>
            {
                opt.MapFrom(s => s.Width);
            })
           .ForMember(d => d.left, opt =>
           {
               opt.MapFrom(s => s.Left);
           }).ForMember(d => d.top, opt =>
           {
               opt.MapFrom(s => s.Top);
           })
            .ForMember(a => a.type, opt => opt.Ignore())
             .ForMember(a => a.alt, opt => opt.Ignore())
             .ForMember(a => a.marked, opt => opt.Ignore());

            //实例区域数据节点->实例显示区域
            CreateMap<AppInstNodeArea, AppInstNodeAreaViewDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.StrId);
            }).ForMember(d => d.height, opt =>
            {
                opt.MapFrom(s => s.Height);
            }).ForMember(d => d.width, opt =>
            {
                opt.MapFrom(s => s.Width);
            })
            .ForMember(d => d.left, opt =>
            {
                opt.MapFrom(s => s.Left);
            }).ForMember(d => d.top, opt =>
            {
                opt.MapFrom(s => s.Top);
            })

             .ForMember(a => a.alt, opt => opt.Ignore())
            .ForMember(a => a.color, opt => opt.Ignore());

            //实例节点线实体->实例节点显示线
            CreateMap<AppInstNodeLine, AppInstNodeLineViwDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.StrId);
            }).ForMember(d => d.from, opt =>
            {
                opt.MapFrom(s => s.From);
            }).ForMember(d => d.to, opt =>
            {
                opt.MapFrom(s => s.To);
            })
            .ForMember(a => a.type, opt => opt.Ignore())
             .ForMember(a => a.alt, opt => opt.Ignore())
             .ForMember(a => a.marked, opt => opt.Ignore())
             .ForMember(a => a.dash, opt => opt.Ignore());


            #endregion




            #endregion

        }

    }
}
