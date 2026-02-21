using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.Schedule;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NF.Web.Areas.Schedule.Controllers
{
    [Area("Schedule")]
    [Route("Schedule/[controller]/[action]")]
    public class ScheduleDetailController : NfBaseController
    {
        public IMapper _IMapper;
        public IScheduleDetailService _IScheduleDetailService;
        public IUserInforService _IUserInforService;
        public IDataDictionaryService _IDataDictionaryService;
        public ScheduleDetailController(IScheduleDetailService IScheduleDetailService, IMapper IMapper
            , IDataDictionaryService IDataDictionaryService, IUserInforService IUserInforService)
        {
            _IScheduleDetailService = IScheduleDetailService;
            _IMapper = IMapper;
            _IUserInforService = IUserInforService;
            _IDataDictionaryService = IDataDictionaryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Build()
        {
            _IScheduleDetailService.DeleteAtt(-this.SessionCurrUserId);
            return View();
        }
        public IActionResult PdBuild()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ScheduleDetail, bool>> GetQueryExpression(PageInfo<ScheduleDetail> pageInfo, string keyWord, int? search)
        {
            var predicateAnd = PredicateBuilder.True<ScheduleDetail>();
            var predicateOr = PredicateBuilder.False<ScheduleDetail>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);

            //predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetTenderInListPermissionExpression("querycollcontlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));

            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.ScheduleName.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.ScheduleSerNavigation.ScheduleName.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }

            return predicateAnd;
        }

        /// <summary>
        /// 进度明细列表
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<ScheduleDetail>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ScheduleDetail>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search));

            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetJBAdvQueryScheduleD(pageParam, _IUserInforService,_IDataDictionaryService));
            }
            Expression<Func<ScheduleDetail, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                //case "Name":
                //    orderbyLambda = a => a.TenderUserId;
                //    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.search==1)
            {
                predicateAnd = predicateAnd.And(p=>p.ScheduleSerNavigation.Stalker == this.SessionCurrUserId && p.State != 2 && p.IsDelete == 0);
            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IScheduleDetailService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }

        /// <summary>
        /// 进度明细保存Save
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新建进度明细", OptionLogTypeEnum.Add, "新建进度明细", true)]
        public IActionResult Save(ScheduleDetailDTO info)
        {
            try
            {
                //判断当前进度大于上一条进度
                if (info.Wancheng>_IScheduleDetailService.Selectjd(info.ScheduleSer))
                {
                info.IsDelete = 0;
                var saveInfo = _IMapper.Map<ScheduleDetail>(info);
                saveInfo.CreateUserId = this.SessionCurrUserId;
                saveInfo.State = 0;
                saveInfo.IsDelete = 0;
                saveInfo.CreateDateTime = DateTime.Now;
                saveInfo.PddateTime = DateTime.Now;
                var dic = _IScheduleDetailService.Add(saveInfo);
                    //修改进度管理
                    _IScheduleDetailService.UpdateJDgl(dic.ScheduleSer, dic.Wancheng??0);
                    //保存合同附件
                    _IScheduleDetailService.ADDAtt(dic.Id, -this.SessionCurrUserId);
                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功",
                    Data = dic

                });
                }
                else
                {
                    return GetResult(new RequstResult
                    {
                        Code = 0,
                        Msg = "建立当前进度必须大于已有进度！",
                        RetValue=1

                    });
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.Error(ex.ToString());
                return GetResult(new RequstResult
                {
                    Code = 1,
                    Msg = "Error",


                });

            }


        }

        /// <summary>
        /// 查看页面和修改页面赋值
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var info = _IScheduleDetailService.ShowView(Id);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info
            });
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除招标", OptionLogTypeEnum.Del, "删除招标", false)]
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            //var permiision = _ISysPermissionModelService.GetContractDeletePermission("delzbcollcont", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };
            //if (permiision.Code != 0)
            //{
            //    resinfo.RetValue = permiision.Code;
            //    resinfo.Msg = permiision.GetOptionMsg(permiision.Code);
            //    resinfo.Data = permiision.noteAllow;
            //}
            //else
            //{
            _IScheduleDetailService.Delete(Ids);
            //}
            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存进度明细对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("进度明细", OptionLogTypeEnum.Update, "进度明细", true)]
        public IActionResult UpdateSave(ScheduleDetailDTO info)
        {
            if (info.Id > 0)
            {
                //判断当前进度大于上一条进度
                if (info.Wancheng > _IScheduleDetailService.Selectjd(info.ScheduleSer))
                {
                    var updateinfo = _IScheduleDetailService.Find(info.Id);
                    var updatedata = _IMapper.Map(info, updateinfo);
                    var dic = _IScheduleDetailService.Update(updatedata);
                    //修改进度管理
                    _IScheduleDetailService.UpdateJDgl(info.ScheduleSer, info.Wancheng??0);
                    //_IScheduleDetailService.ADDAtt(updatedata.Id, -this.SessionCurrUserId);
                    return GetResult(new RequstResult
                    {
                        Code = 0,
                        Msg = "操作成功",
                        Data = dic

                    });
                }
                else
                {
                    return GetResult(new RequstResult
                    {
                        RetValue=1,
                        Code = 0,
                        Msg = "建立当前进度必须大于已有进度！",

                    });
                }
            }
            return GetResult();

        }
        /// <summary>
        /// 修改保存评定
        /// </summary>
        /// <param name="info">保存进度明细对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("进度明细", OptionLogTypeEnum.Update, "进度明细", true)]
        public IActionResult UpdatepdSave(string vafin,int ObjId,byte type)
        {
            if (ObjId > 0)
            {
                var updateinfo = _IScheduleDetailService.Find(ObjId);
                updateinfo.PddateTime = DateTime.Now;
                updateinfo.Pdescription = vafin;
                updateinfo.State = type;
                var updatedata = _IMapper.Map(updateinfo, updateinfo);
                var dic = _IScheduleDetailService.Update(updatedata);
                //_IScheduleDetailService.ADDAtt(updatedata.Id, -this.SessionCurrUserId);
                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功",
                    Data = dic

                });
            }
            return GetResult();

        }
        public IActionResult StateUpdate(int ObjId)
        {

            int rev = _IScheduleDetailService.StateUpdate(ObjId, this.SessionCurrUserId);
            if (rev == 1)
            {
                return GetResult(new RequstResult
                {
                    Msg = "没有权限修改状态!",
                    RetValue = 1
                });
            }
            else
            {
                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功!",
                    RetValue = 0
                });
            }

        }
        public IActionResult UpdateField(int Id, string fieldName, string fieldVal)
        {
            var res = _IScheduleDetailService.UpdateField(new UpdateFieldInfo()
            {
                Id = Id,
                FieldName = fieldName,
                FieldValue = fieldVal
            });
            RequstResult reqInfo = null;
            if (res > 0)
            {
                reqInfo = new RequstResult()
                {
                    Msg = "修改成功",
                    Code = 0,
                };
            }
            else
            {
                reqInfo = new RequstResult()
                {
                    Msg = "修改失败",
                    Code = 0,


                };
            }
            return new CustomResultJson(reqInfo);
        }

        /// <summary>
        /// 修改多字段
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateMoreField(IList<UpdateFieldInfo> fields)
        {
            var res = _IScheduleDetailService.UpdateField(fields);
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
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {
            var pageInfo = new NoPageInfo<Model.Models.ScheduleDetail>();
            var predicateAnd = PredicateBuilder.True<Model.Models.ScheduleDetail>();
            PageparamInfo pageParam = new PageparamInfo();
            pageParam.keyWord = exportRequestInfo.KeyWord;
            pageParam.jsonStr = exportRequestInfo.jsonStr;
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord, exportRequestInfo.search));
            if (exportRequestInfo.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
            }
            else
            {//所有行
                //if (!string.IsNullOrEmpty(pageParam.jsonStr))
                //{//高级查询
                //    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryQuestioning(pageParam, _IUserInforService));
                //}
            }
            var layPage = _IScheduleDetailService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "进度明细", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
    }
}
