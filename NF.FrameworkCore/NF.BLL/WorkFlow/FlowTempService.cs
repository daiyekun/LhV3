using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NF.AutoMapper;
using NF.ViewModel.Models.Common;

using NF.BLL.Extend;

using AutoMapper;

namespace NF.BLL
{
    /// <summary>
    /// 流程模板
    /// </summary>
    public partial class FlowTempService
    {
        private IMapper _IMappers;
        /// <summary>
        /// 审批模板列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<FlowTempViewDTO> GetList<s>(PageInfo<FlowTemp> pageInfo, Expression<Func<FlowTemp, bool>> whereLambda, Expression<Func<FlowTemp, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _FlowTempSet.AsTracking().Where<FlowTemp>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<FlowTemp>))
            { //分页
                tempquery = tempquery.Skip<FlowTemp>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<FlowTemp>(pageInfo.PageSize);
            }
            //部门
            var listdepts = Db.Set<Department>().AsNoTracking().Where(a => a.IsDelete != 1).Select(a => a).ToList();
            //数据字典
            var listdic = Db.Set<DataDictionary>().AsNoTracking().Where(a => a.IsDelete != 1).Select(a => a).ToList();

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Version = a.Version,
                            IsValid = a.IsValid,
                            ObjType = a.ObjType,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            DeptIds = a.DeptIds,
                            CategoryIds = a.CategoryIds,
                            FlowItems = a.FlowItems



                        };
            var local = from a in query.AsEnumerable()
                        select new FlowTempViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Version = a.Version,
                            IsValid = a.IsValid,
                            CategorysName = GetDataDics(listdic, a.CategoryIds),//类别
                            DeptsName = GetDeptNames(listdepts, a.DeptIds),//部门名称
                            FlowItemsDic = GetFlowItems(a.FlowItems, a.ObjType ?? -1),//审批事项
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId ?? 0),
                            ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType ?? -1)
                        };
            return new LayPageInfo<FlowTempViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 根据IDS获取名称"销售类、采购类"
        /// </summary>
        /// <param name="dataDictionaries">字典集合</param>
        /// <param name="Ids"></param>
        /// <returns></returns>
        private string GetDataDics(IList<DataDictionary> dataDictionaries, string Ids)
        {
            var listids = StringHelper.String2ArrayInt(Ids);
            var listdic = dataDictionaries.Where(a => listids.Contains(a.Id)).Select(a => a.Name).ToList();
            return StringHelper.ArrayString2String(listdic);

        }
        /// <summary>
        /// 根据Ids获取部门
        /// </summary>
        /// <param name="departments">部门集合</param>
        /// <param name="Ids"></param>
        /// <returns></returns>
        private string GetDeptNames(IList<Department> departments, string Ids)
        {
            var listids = StringHelper.String2ArrayInt(Ids);
            var listdic = departments.Where(a => listids.Contains(a.Id)).Select(a => a.Name).ToList();
            return StringHelper.ArrayString2String(listdic);

        }
        /// <summary>
        /// 获取审批事项
        /// </summary>
        /// <returns></returns>
        private string GetFlowItems(string Ids, int objTypeEnum)
        {
            var itemObjType = EmunUtility.GetEnumItemExAttribute(typeof(FlowObjEnums), objTypeEnum);
            var list = EmunUtility.GetAttr(itemObjType.TypeValue);
            var listids = StringHelper.String2ArrayInt(Ids);
            var listDics = list.Where(a => listids.Contains(a.Value)).Select(a => a.Desc).ToList();
            return StringHelper.ArrayString2String(listDics);

        }
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<FlowTemp>();
            //不等于fieldInfo.CurrId是排除修改的时候
            predicateAnd = predicateAnd.And(a => a.Id != fieldInfo.Id);
            switch (fieldInfo.FieldName)
            {
                case "Name":
                    predicateAnd = predicateAnd.And(a => a.Name == fieldInfo.FieldValue);
                    break;


            }
            return GetQueryable(predicateAnd).AsNoTracking().Any();

        }
        public void Test(IMapper _IMapper)
        {
            _IMappers = _IMapper;
        }
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="flowTemp">流程模板</param>
        /// <returns></returns>
        public FlowTemp AddSave(FlowTemp flowTemp, IMapper _IMapper)
        {
            Test(_IMapper);
            flowTemp.Version = 1;
            flowTemp.IsValid = 1;
         
            //var info = _IMappers.Map<ContPlanFinance, ContPlanFinanceHistory>(plance);
            var Hist = _IMappers.Map<FlowTemp, FlowTempHist>(flowTemp);// flowTemp.ToModel<FlowTemp, FlowTempHist>();
          var info = Add(flowTemp);
         //   var Hist = flowTemp.ToModel<FlowTemp, FlowTempHist>();
            //  var Hist =  flowTemp.ToModel<FlowTemp, FlowTempHist>();

            Hist.TempId = info.Id;
            Hist.CreateDateTime = DateTime.Now;
            Db.Set<FlowTempHist>().Add(Hist);
            this.SaveChanges();
            return flowTemp;

            //flowTemp.Version = 1;
            //flowTemp.IsValid = 1;
            //var Hist = flowTemp.ToModel<FlowTemp, FlowTempHist>();
            //var info = Add(flowTemp);
            //Hist.TempId = info.Id;
            //Hist.CreateDateTime = DateTime.Now;
            //Db.Set<FlowTempHist>().Add(Hist);
            //this.SaveChanges();
            //return flowTemp;

        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="flowTemp"></param>
        /// <returns></returns>
        public FlowTemp UpdateSave(FlowTemp flowTemp, IMapper _IMapper)
        {
            Test(_IMapper);
            //  IMapper _IMappers;
            //IMapper _IMappers;
            //  IMapperBase _IMappers;
            //   IMapper sd = new IMapper;
            var Hist = _IMappers.Map<FlowTemp, FlowTempHist>(flowTemp);// flowTemp.ToModel<FlowTemp, FlowTempHist>();
            Update(flowTemp);
            Hist.TempId = flowTemp.Id;
            Db.Set<FlowTempHist>().Add(Hist);
            this.SaveChanges();
            return flowTemp;

        }
        /// <summary>
        /// 根据条件判断流程是否唯一
        /// </summary>
        /// <param name="flowTemp">流程模板对象</param>
        /// <returns></returns>
        public string CheckFlowUnique(FlowTemp flowTemp)
        {
            string flowName = string.Empty;
            var querylist = _FlowTempSet.AsNoTracking().
                Where(a => a.ObjType == flowTemp.ObjType && a.Id != flowTemp.Id).ToList();
            var depts = StringHelper.String2ArrayInt(flowTemp.DeptIds);
            var flowitems = StringHelper.String2ArrayInt(flowTemp.FlowItems);
            var categorys = StringHelper.String2ArrayInt(flowTemp.CategoryIds);
            foreach (var flow in querylist)
            {
                var tempdepts = StringHelper.String2ArrayInt(flow.DeptIds);
                var tempflowitems = StringHelper.String2ArrayInt(flow.FlowItems);
                var tempcategorys = StringHelper.String2ArrayInt(flow.CategoryIds);

                var indepts = depts.Intersect(tempdepts);
                var incategorys = categorys.Intersect(tempcategorys);
                var inflowitems = flowitems.Intersect(tempflowitems);
                if (indepts.Count() > 0 && incategorys.Count() > 0 && inflowitems.Count() > 0)
                {
                    flowName = flow.Name;
                    break;
                }


            }
            return flowName;

        }
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                case "IsValid"://状态
                    var state = Convert.ToByte(info.FieldValue);
                    sqlstr = $"update  FlowTemp set IsValid={state} where Id={info.Id}";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID</param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append($"update FlowTemp set IsDelete=1 where Id in ({Ids});");
            strb.Append($"update FlowTempHist set IsDelete=1 where TempId in ({Ids});");

            return ExecuteSqlCommand(strb.ToString());
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public FlowTempDTO ShowView(int Id)
        {
            var query = from a in _FlowTempSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ObjType = a.ObjType,
                            DeptIds = a.DeptIds,
                            CategoryIds = a.CategoryIds,
                            FlowItems = a.FlowItems,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId
                        };
            var local = from a in query.AsEnumerable()

                        select new FlowTempDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ObjType = a.ObjType,
                            DeptIds = a.DeptIds,
                            DeptIdsArray = StringHelper.String2ArrayInt(a.DeptIds),
                            CategoryIds = a.CategoryIds,
                            CategoryIdsArray = StringHelper.String2ArrayInt(a.CategoryIds),
                            FlowItems = a.FlowItems,
                            FlowItemsArray = StringHelper.String2ArrayInt(a.FlowItems),
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                        };
            return local.FirstOrDefault();
        }
        /// <summary>
        /// 请求获取模板ID
        /// </summary>
        /// <param name="requestTemp">请求对象</param>
        /// <returns></returns>
        public ResponTemp FindTempIdByWhere(RequestTempInfo requestTemp)
        {
            var tempId = 0;
            var query = from a in _FlowTempSet.AsNoTracking()
                        where a.ObjType == requestTemp.ObjType
                        && a.IsDelete ==0 && a.IsValid == 1
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            FlowItems = a.FlowItems,
                            CategoryIds = a.CategoryIds,
                            DeptIds = a.DeptIds
                        };

            var list = query.ToList();
            foreach (var item in list)
            {
                var arrflowitem = StringHelper.String2ArrayInt(item.FlowItems);
                var arrcateIds = StringHelper.String2ArrayInt(item.CategoryIds);
                var deptIdsIds = StringHelper.String2ArrayInt(item.DeptIds);
                if (requestTemp.ObjType == (int)FlowObjEnums.Contract
                    || requestTemp.ObjType == (int)FlowObjEnums.InvoiceIn
                    || requestTemp.ObjType == (int)FlowObjEnums.InvoiceOut
                    || requestTemp.ObjType == (int)FlowObjEnums.payment
                    || requestTemp.ObjType == (int)FlowObjEnums.Tender
                    || requestTemp.ObjType == (int)FlowObjEnums.Inquiry
                    || requestTemp.ObjType == (int)FlowObjEnums.Questioning)
                {
                    if (arrflowitem.Contains(requestTemp.FlowItem)
                    && arrcateIds.Contains(requestTemp.ObjCateId)
                    && (deptIdsIds.Contains(requestTemp.DeptId)))
                    {
                        tempId = item.Id;
                        break;
                    }
                }
                else
                {
                    if (arrflowitem.Contains(requestTemp.FlowItem)
                   && arrcateIds.Contains(requestTemp.ObjCateId)
                   )
                    {
                        tempId = item.Id;
                        break;
                    }
                }



            }
            var tempHist = Db.Set<FlowTempHist>().AsNoTracking().Where(a => a.TempId == tempId).OrderByDescending(a => a.Id).FirstOrDefault();
            var tempHistId = tempHist == null ? 0 : tempHist.Id;

            var appinst = Db.Set<AppInst>().AsNoTracking().Where(a => a.Mission == requestTemp.FlowItem && a.ObjType == requestTemp.ObjType && a.AppObjId == requestTemp.ObjId).OrderByDescending(a => a.Id).FirstOrDefault();
            return new ResponTemp()
            {
                TempId = tempId,
                TempHistId = tempHistId,
                InstId = (appinst == null || (appinst.AppState == 3)) ? 0 : appinst.Id


            };



        }
        /// <summary>
        /// 判断数据
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <returns></returns>
        public int ChekAppFlowData(int tempId)
        {
            var listnode = Db.Set<FlowTempNode>()
                .Where(a => a.TempId == tempId && a.Type != (int)NodeTypeEnum.NType0 && a.Type != (int)NodeTypeEnum.NType1).Select(a => a.StrId).ToList();
            var querynodeinfos = Db.Set<FlowTempNodeInfo>()
                .Where(a => a.TempId == tempId && listnode.Contains(a.NodeStrId)).ToList();
            if (querynodeinfos.Count > 0)
            {
                //var temlist = querynodeinfos.ToList();
                var listnodeinfo = querynodeinfos.Where(a => (a.GroupId ?? -1) < 0);
                if (listnodeinfo.Any())
                {
                    return 2;//没有设置审批人
                }



            }
            else
            {
                return 1;//没有设置任何节点条件
            }
            //var listnodeinfos = Db.Set<FlowTempNodeInfo>()
            //    .Where(a => a.TempId == tempId).ToList();
            return 0;

        }


        /// <summary>
        /// 判断流程节点是否匹配完成
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <param name="amount">金额</param>
        /// <param name="flowType">流程类型</param>
        /// <returns></returns>
        public int SubCheckFlow(int? tempId, decimal? amount, int? flowType)
        {
            if ((tempId ?? 0) <= 0 || (flowType ?? 0) <= 0)
            {
                return -1;
            }
            else
            {
                var FlowTempNodes = Db.Set<FlowTempNode>()
                     .Where(a => a.TempId == tempId).ToList();
                var FlowTempNodeInfos = Db.Set<FlowTempNodeInfo>()
                    .Where(a => a.TempId == tempId).ToList();
                var TempNodeLines = Db.Set<TempNodeLine>()
                    .Where(a => a.TempId == tempId).ToList();
                var startnode = FlowTempNodes.Where(a => a.Type == (int)NodeTypeEnum.NType0).FirstOrDefault();
                var endnode = FlowTempNodes.Where(a => a.Type == (int)NodeTypeEnum.NType1).FirstOrDefault();
                //判断节点是否为可调整节点
                foreach (var Itms in FlowTempNodeInfos)
                {
                    var Groupinfo = Db.Set<GroupInfo>().Where(a => a.IsDelete == 0);
                    if (Itms.Isktz == 0)
                    {
                        var usinfo= Groupinfo.Where(a =>a.Id==Itms.GroupId).FirstOrDefault();
                        try
                        {
                            if (usinfo != null)
                            {
                                if ((!string.IsNullOrEmpty(Itms.UsIds)))
                                {

                                }
                                else
                                {
                                    return -5;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            return -5;
                        }
                    }
                }
                if (startnode == null || endnode == null)
                {
                    return -2;
                }
                else
                {
                    //需要节点校验金额的
                    if (flowType == (int)FlowObjEnums.Contract
                        || flowType == (int)FlowObjEnums.InvoiceIn
                        || flowType == (int)FlowObjEnums.InvoiceOut
                        || flowType == (int)FlowObjEnums.payment)
                    {
                        var nodeId = FindNode(TempNodeLines, FlowTempNodeInfos, startnode.StrId, endnode.StrId, (amount ?? 0));
                        var isest = TempNodeLines.Any(a => a.From == nodeId && a.To == endnode.StrId);
                        if (!isest)
                        {
                            return -3;//流程节点没有匹配完成
                        }
                    }
                    else
                    {
                        var existnode = FlowTempNodes.Where(a =>
                          a.StrId != startnode.StrId &&
                          a.StrId != endnode.StrId).Any();
                        var existnodeinfo = FlowTempNodeInfos
                            .Where(a => a.NodeStrId != startnode.StrId &&
                        a.NodeStrId != endnode.StrId).Any();
                        if (!existnode || !existnodeinfo)
                        {
                            return -4;//
                        }


                    }



                }

            }

            return 0;

        }
        /// <summary>
        /// 查找当前满足条件的节点
        /// </summary>
        /// <param name="tempNodeLines">节点连接线</param>
        /// <param name="flowTempNodeInfos">节点集合</param>
        /// <param name="FromNode">上级节点</param>
        /// <param name="endnodestr">结束节点</param>
        /// <param name="amount">金额</param>
        /// <returns></returns>
        private string FindNode
            (IList<TempNodeLine> tempNodeLines, IList<FlowTempNodeInfo> flowTempNodeInfos,
            string FromNode, string endnodestr, decimal amount)
        {
            //下级节点ID
            var tonodes = tempNodeLines.Where(a => a.From == FromNode)
                   .Select(a => a.To).ToList();
            //下级节点信息
            var tonodeinfos = flowTempNodeInfos.Where(a =>
            tonodes.Contains(a.NodeStrId) && a.NodeStrId != endnodestr).ToList();
            var nodestr = "";
            foreach (var item in tonodeinfos)
            {
                if ((item.IsMin ?? 0) == 1 && (item.IsMax ?? 0) == 1)
                {
                    if (amount >= (item.Min ?? 0) && amount <= (item.Max ?? 0))
                    {
                        nodestr = item.NodeStrId;
                        break;
                    }
                }
                else if ((item.IsMin ?? 0) == 1 && (item.IsMax ?? 0) == 0)
                {
                    if (amount >= (item.Min ?? 0) && amount < (item.Max ?? 0))
                    {
                        nodestr = item.NodeStrId;
                        break;
                    }
                }
                else if ((item.IsMin ?? 0) == 0 && (item.IsMax ?? 0) == 1)
                {
                    if (amount > (item.Min ?? 0) && amount <= (item.Max ?? 0))
                    {
                        nodestr = item.NodeStrId;
                        break;
                    }
                }
                else if ((item.IsMin ?? 0) == 0 && (item.IsMax ?? 0) == 0)
                {
                    if (amount > (item.Min ?? 0) && amount < (item.Max ?? 0))
                    {
                        nodestr = item.NodeStrId;
                        break;
                    }
                }



            }
            if (!string.IsNullOrEmpty(nodestr))
            {
                FindNode(tempNodeLines, flowTempNodeInfos
                    , nodestr, endnodestr, amount);
            }

            return FromNode;//返回最后满足条件的节点



        }
    }
}
