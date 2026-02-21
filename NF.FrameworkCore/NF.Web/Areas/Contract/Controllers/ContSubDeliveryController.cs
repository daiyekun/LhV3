using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;

namespace NF.Web.Areas.Contract.Controllers
{
    /// <summary>
    /// 标的交付
    /// </summary>
    [Area("Contract")]
    [Route("Contract/[controller]/[action]")]


    public class ContSubDeliveryController : NfBaseController
    {

        private IContSubDeService _IContSubDeService;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        private IMapper _IMapper;
        public IUserInforService _IUserInforService;
        public ContSubDeliveryController(IContSubDeService IContSubDeService
            , ISysPermissionModelService ISysPermissionModelService
            , IMapper IMapper
            , IUserInforService IUserInforService)
        {
            _IMapper = IMapper;
            _IContSubDeService = IContSubDeService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IUserInforService = IUserInforService;
        }
        /// <summary>
        /// 收款
        /// </summary>
        /// <returns></returns>
        public IActionResult CollectionIndex()
        {
            return View();
        }
        /// <summary>
        /// 付款
        /// </summary>
        /// <returns></returns>
        public IActionResult PaymentIndex()
        {
            return View();
        }
        /// <summary>
        /// 交付页面
        /// </summary>
        /// <returns></returns>
        public IActionResult JiaoFu()
        {
            return View();
        }
        /// <summary>
        /// 标的交付
        /// </summary>
        /// <returns></returns>
        public IActionResult BiaoDiJaioFu(ContSubDesDTO info, IList<DevSubItem> devDatas)
        {
            info.CreateDateTime = DateTime.Now;
            info.ModifyUserId = this.SessionCurrUserId;
            info.CreateUserId = this.SessionCurrUserId;
            info.ModifyDateTime = DateTime.Now;
            info.IsDelete = 0;//删除，未来可能需要
            info.Dstate = 0;//状态,未来可能需要
            info.Path = "Uploads/" + info.FolderName + "/" + info.GuidFileName;
            _IContSubDeService.BiaoDiJiaoFu(info, devDatas, _IMapper);
            return GetResult();
        }
        /// <summary>
        /// 标的交付明细
        /// </summary>
        /// <param name="cateIds">类别ID</param>
        /// <param name="pageParam">请求对象</param>
        /// <param name="subIds">标的ID集合</param>
        /// <returns></returns>
        public IActionResult GetMainList(PageparamInfo pageParam, string cateIds,string subIds)
        {
            var pageInfo = new PageInfo<Model.Models.ContSubDe>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.ContSubDe>();
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetytAdvQueryContSubDe(pageParam, _IUserInforService));
            }
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.fType, pageParam.keyWord, pageParam.beginData, pageParam.endData, cateIds, subIds));
            Expression<Func<Model.Models.ContSubDe, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "SubName"://标的名称
                    orderbyLambda = a => a.Name;
                    break;
                //未来在扩展，单最好别小计排序。因为是计算字段
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IContSubDeService.GetMainList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 查询表达式
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="cateIds">业务品类类别Ids</param>
        /// <param name="fType">资金性质：0:收款，1：付款</param>
        /// <param name="subIds">标的ID集合</param>
        /// <param name="beginData">开始时间</param>
        /// <param name="endData">结束日期</param>
        /// 
        /// <returns></returns>
        private Expression<Func<Model.Models.ContSubDe, bool>> GetQueryExpression(PageInfo<Model.Models.ContSubDe> pageInfo,
            int fType,string keyWord,DateTime? beginData, DateTime? endData, string cateIds,string subIds)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContSubDe>();
            var predicateOr = PredicateBuilder.False<Model.Models.ContSubDe>();
            predicateAnd = predicateAnd.And(a => a.Sub.Cont.FinanceType == fType && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetListSubDesPermissionExpression((fType == 0 ? "collSubDvDetailList" : "paySubDvDetailList"), this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord) && keyWord.ToLower() != "undefined")
            {
                predicateOr = predicateOr.Or(a => a.Sub.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Sub.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Sub.Cont.Code.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Sub.Cont.Comp.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            if (!string.IsNullOrEmpty(cateIds))
            {
                var arrIds = StringHelper.String2ArrayInt(cateIds);
                if (arrIds.Any(a => a == -1))
                {//包含非业务
                    predicateAnd = predicateAnd.And(p => arrIds.Contains(p.Sub.BcInstance.LbId ?? -100) || (p.Sub.IsFromCategory ?? 0) == 0);
                }
                else
                {
                    predicateAnd = predicateAnd.And(p => arrIds.Contains(p.Sub.BcInstance.LbId ?? -100));
                }

            }
            //标的ID查询
            if (!string.IsNullOrEmpty(subIds))
            {
                var arrsubIds = StringHelper.String2ArrayInt(subIds);
                predicateAnd = predicateAnd.And(p => arrsubIds.Any(a=>a==p.SubId));

            }
            //时间筛选
            if (endData.HasValue)
            {
                endData = endData.Value.AddDays(1);//将当前时间加一天
            }
            if (!endData.HasValue && beginData.HasValue)
            {
                predicateAnd = predicateAnd.And(p=>p.ActualDateTime>= beginData);
               
            }
            else if (beginData.HasValue &&endData.HasValue)
            {
                predicateAnd = predicateAnd.And(p => p.ActualDateTime >=beginData&&p.ActualDateTime< endData);

            }
            else if (endData.HasValue && !beginData.HasValue)
            {
                predicateAnd = predicateAnd.And(p => p.ActualDateTime <endData );
               
            }
            return predicateAnd;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="subIds">选择标的ID集合</param>
        /// <param name="cateIds">类别Id</param>
        /// <param name="export">导出请求对象</param>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo export, string cateIds,string subIds)
        {

            var pageInfo = new NoPageInfo<ContSubDe>();
            var predicateAnd = PredicateBuilder.True<ContSubDe>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, export.FType, export.KeyWord, export.beginData, export.endData,cateIds,subIds));
            if (export.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => export.GetSelectListIds().Contains(p.Id));
            }
            var layPage = _IContSubDeService.GetMainList(pageInfo, predicateAnd, a => a.Id, true);
            var exceltxt = export.FType == 0 ? "收款合同标的交付明细" : "付款合同标的交付明细";
            var downInfo = ExportDataHelper.ExportExcelExtend(export, exceltxt, layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }

    }
}