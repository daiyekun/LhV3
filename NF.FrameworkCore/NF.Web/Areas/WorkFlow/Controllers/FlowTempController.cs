using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;


namespace NF.Web.Areas.WorkFlow.Controllers
{
    [Area("WorkFlow")]
    [Route("WorkFlow/[controller]/[action]")]
    public class FlowTempController : NfBaseController
    {
        /// <summary>
        /// 流程模板
        /// </summary>
        private IFlowTempService _IFlowTempService;
        /// <summary>
        /// 映射
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 数据字典
        /// </summary>
        private IDataDictionaryService _IDataDictionaryService;
        /// <summary>
        /// 部门
        /// </summary>
        private IDepartmentService _IDepartmentService;

        public FlowTempController(IMapper IMapper,IFlowTempService IFlowTempService
            , IDataDictionaryService IDataDictionaryService
            , IDepartmentService IDepartmentService)
        {
            _IMapper = IMapper;
            _IFlowTempService = IFlowTempService;
            _IDataDictionaryService = IDataDictionaryService;
            _IDepartmentService = IDepartmentService;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Build()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<FlowTemp>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<FlowTemp>();
            var predicateOr = PredicateBuilder.False<FlowTemp>();
            predicateAnd = predicateAnd.And(a => a.IsDelete != 1);//表示没有删除的数据
            if (!string.IsNullOrEmpty(pageParam.keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(pageParam.keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            var layPage = _IFlowTempService.GetList(pageInfo, predicateAnd,a=>a.Id,false);
            return new CustomResultJson(layPage);
            

        }
        /// <summary>
        /// 获取流程对象集合
        /// </summary>
        /// <returns></returns>
        public IActionResult GetWfObjTypes()
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = EmunUtility.GetAttr(typeof(FlowObjEnums))

            });
        }
        /// <summary>
        /// 审批事项
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFlowItems(int objEnum)
        {
           
            var result = new RequstResult()
            {
                Msg = "",
                Code = 0,


            };
            var itemObjType = EmunUtility.GetEnumItemExAttribute(typeof(FlowObjEnums), objEnum);
            var list = EmunUtility.GetAttr(itemObjType.TypeValue);
            IList<SelectMultiple> flowItems = new List<SelectMultiple>();
            foreach (var item in list)
            {
                SelectMultiple flow = new SelectMultiple();
                flow.Name = item.Desc;
                flow.Value = item.Value;
                flowItems.Add(flow);
            }
            result.Data = flowItems;
            return new CustomResultJson(result, a=>a.ToLower());

    }
        /// <summary>
        /// 对象类别
        /// </summary>
        /// <param name="objEnum">字典菜单枚举</param>
        /// <returns></returns>
        public IActionResult GetObjClass(int objEnum)
        {

            var list = _IDataDictionaryService.GetListByDataEnumType((DataDictionaryEnum)objEnum);
              IList<SelectMultiple> flowItems = new List<SelectMultiple>();
            foreach (var item in list)
            {
                SelectMultiple flow = new SelectMultiple();
                flow.Name = item.Name;
                flow.Value = item.Id;
                flowItems.Add(flow);
            }
            var result = new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = flowItems

            };
            return new CustomResultJson(result, a => a.ToLower());
        }
        /// <summary>
        /// 获取流选择树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFlowDeptTree()
        {
            var result = new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = GetGetTreeListDept()

            };
            return new CustomResultJson(result, a => a.ToLower());
        }

        /// <summary>
        /// 获取类别选择树
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFlowContTxtClassTree(int objEnum)
        {
            var result = new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = GetGetTreeListContTxtClass(objEnum)

            };
            return new CustomResultJson(result, a => a.ToLower());
        }

        #region  递归部门多选Tree
        /// <summary>
        /// Tree 递归
        /// </summary>
        /// <returns></returns>
        public IList<SelectMulTreeInfo> GetGetTreeListDept()
        {
            IList<SelectMulTreeInfo> listTree = new List<SelectMulTreeInfo>();
            var listAll = _IDepartmentService.GetListAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.Dstatus == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == 0))
            {
                SelectMulTreeInfo treeInfo = new SelectMulTreeInfo();
                treeInfo.Value= item.Id;
                treeInfo.Name = item.Name;
                
                RecursionChrenNode(list, treeInfo, item);
                listTree.Add(treeInfo);

            }
            return listTree;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        public void RecursionChrenNode(IList<DepartmentDTO> listDepts, SelectMulTreeInfo treeInfo, DepartmentDTO item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<SelectMulTreeInfo>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    SelectMulTreeInfo treeInfotmp = new SelectMulTreeInfo();
                    treeInfotmp.Value = chrenItem.Id;
                    treeInfotmp.Name = chrenItem.Name;


                    RecursionChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.Children = listchrennode;

            }




        }
        #endregion

        #region  递归合同类别多选Tree
        /// <summary>
        /// Tree 递归
        /// </summary>
        /// <returns></returns>
        public IList<SelectMulTreeInfo> GetGetTreeListContTxtClass(int objEnum)
        {
            IList<SelectMulTreeInfo> listTree = new List<SelectMulTreeInfo>();
            var listAll = _IDataDictionaryService.GetListAll((DataDictionaryEnum)objEnum);
           
            foreach (var item in listAll.Where(a => (a.Pid??0) == 0))
            {
                SelectMulTreeInfo treeInfo = new SelectMulTreeInfo();
                treeInfo.Value = item.Id;
                treeInfo.Name = item.Name;

                RecursionChrenNodeContTxtClass(listAll, treeInfo, item);
                listTree.Add(treeInfo);

            }
            return listTree;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        public void RecursionChrenNodeContTxtClass(IList<DataDictionary> listDepts, SelectMulTreeInfo treeInfo, DataDictionary item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<SelectMulTreeInfo>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    SelectMulTreeInfo treeInfotmp = new SelectMulTreeInfo();
                    treeInfotmp.Value = chrenItem.Id;
                    treeInfotmp.Name = chrenItem.Name;


                    RecursionChrenNodeContTxtClass(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.Children = listchrennode;

            }




        }
        #endregion

        /// <summary>
        /// 判断字段值是否存在
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <returns></returns>
        public IActionResult CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = _IFlowTempService.CheckInputValExist(fieldInfo)

            });
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="tempDTO"></param>
        /// <returns></returns>
        public IActionResult Save(FlowTempDTO tempDTO)
        {
            var saveInfo = _IMapper.Map<FlowTemp>(tempDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
           var info= _IFlowTempService.AddSave(saveInfo, _IMapper);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0
              

            });
        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        public IActionResult UpdateSave(FlowTempDTO info)
        {

            if (info.Id > 0)
            {
               
                  var updateinfo = _IFlowTempService.Find(info.Id);
                   var ver = updateinfo.Version;
                    var updatedata = _IMapper.Map(info, updateinfo);
                    updatedata.Version = ver + 1;
                    _IFlowTempService.UpdateSave(updatedata, _IMapper);
               
               
            }

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="Id">修改对象ID</param>
        /// <param name="fieldName">修改字段名称</param>
        /// <param name="fieldVal">修改值，如果不是String后台人为判断</param>
        /// <returns></returns>
        public IActionResult UpdateField(UpdateFieldInfo info)
        {
            var res = _IFlowTempService.UpdateField(info);
            RequstResult reqInfo = reqInfo = new RequstResult()
            {
                Msg = "修改成功",
                Code = 0,
            };
            if (res <= 0)
            {
                reqInfo.Msg = "修改失败";
            }

            return new CustomResultJson(reqInfo);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除IDs</param>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IFlowTempService.Delete(Ids);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "删除成功",
                Code = 0
            });
        }
        /// <summary>
        /// 判断流程是否存在
        /// </summary>
        /// <param name="tempDTO"></param>
        /// <returns></returns>
        public IActionResult CheckFlowUnique(FlowTempDTO tempDTO)
        {
            var saveInfo = _IMapper.Map<FlowTemp>(tempDTO);
            var res= _IFlowTempService.CheckFlowUnique(saveInfo);
            var reslt = new RequstResult()
            {
                Msg = "",
                Code = 0
            };
            if (string.IsNullOrEmpty(res))
            {
                reslt.Tag = 0;
                reslt.RetValue = "";
            }
            else
            {
                reslt.Tag = 1;
                reslt.RetValue = res;
            }
            return new CustomResultJson(reslt);
        }
        /// <summary>
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _IFlowTempService.ShowView(Id);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info
            });
        }
        /// <summary>
        /// 设置流程节点
        /// </summary>
        /// <returns></returns>
        public IActionResult SetFlowNode()
        {
            return View();
        }
        /// <summary>
        /// 根据条件获取流程模板对象
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFlowTemp(RequestTempInfo requestTemp)
        {

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data= _IFlowTempService.FindTempIdByWhere(requestTemp)
                //RetValue= _IFlowTempService.FindTempIdByWhere(requestTemp)

            });
        }
        /// <summary>
        /// 判断流程模板数据
        /// </summary>
        /// <returns></returns>
        public IActionResult ChekAppFlowData(int tempId)
        {
           var res= _IFlowTempService.ChekAppFlowData(tempId);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = res
                
            });
        }
        /// <summary>
        /// 判断流程
        /// </summary>
        /// <param name="tempId">模板ID</param>
        /// <param name="amount">金额</param>
        /// <param name="flowType">流程类型</param>
        /// <returns></returns>
        public IActionResult ChekSubmitFlowData(int? tempId, decimal? amount, int? flowType)
        {
            var res = _IFlowTempService.SubCheckFlow(tempId, amount, flowType);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = res

            });
        }
    }
}