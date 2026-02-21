using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Contract.Controllers
{
    /// <summary>
    /// 合同标的
    /// </summary>
    [Area("Contract")]
    [Route("Contract/[controller]/[action]")]
    public class ContSubjectMatterController : NfBaseController
    {/// <summary>
    /// 标的
    /// </summary>
        private IContSubjectMatterService _IContSubjectMatterService;
        /// <summary>
        /// 映射
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 单品
        /// </summary>
        private IBcInstanceService _IBcInstanceService;
        /// <summary>
        /// 标的历史
        /// </summary>
        private IContSubjectMatterHistoryService _IContSubjectMatterHistoryService;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;

        private IContPlanFinanceService _IContPlanFinanceService;

        private IBusinessCategoryService _IBusinessCategoryService;
        public ContSubjectMatterController(IContSubjectMatterService IContSubjectMatterService,
            IMapper IMapper
            , IBcInstanceService IBcInstanceService
            , IContSubjectMatterHistoryService IContSubjectMatterHistoryService
            , ISysPermissionModelService ISysPermissionModelService
            , IContPlanFinanceService IContPlanFinanceService
            , IBusinessCategoryService IBusinessCategoryService)
        {
            _IContSubjectMatterService = IContSubjectMatterService;
            _IMapper = IMapper;
            _IBcInstanceService = IBcInstanceService;
            _IContSubjectMatterHistoryService = IContSubjectMatterHistoryService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContPlanFinanceService = IContPlanFinanceService;
            _IBusinessCategoryService = IBusinessCategoryService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">合同ID</param>
        /// <param name="pageParam">分页对象</param>
        /// <param name="cateIds">类别Ids</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId,string cateIds)
        {
            var pageInfo = new PageInfo<ContSubjectMatter>();
            var predicateAnd = PredicateBuilder.True<ContSubjectMatter>();
            var predicateOr = PredicateBuilder.False<ContSubjectMatter>();
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ContId == contId && a.IsDelete == 0);
            }
           
            predicateAnd = predicateAnd.And(predicateOr);
            if (!string.IsNullOrEmpty(cateIds))
            {
                var arrayIds = StringHelper.String2ArrayInt(cateIds);
                if (arrayIds.Any(a=>a==-1))
                {
                    predicateAnd = predicateAnd.And(a =>(a.IsFromCategory??0)==0||a.BcInstanceId==null);
                    //predicateAnd = predicateAnd.And(predicateOr);
                }
                else
                {
                    //foreach (var tid in arrayIds)
                    //{
                    //    _IBusinessCategoryService.GetCateIds(tid)
                    //}
                    if (arrayIds.Count==1)
                    {
                        var listids = _IBusinessCategoryService.GetCateIds(arrayIds[0]);
                        predicateAnd = predicateAnd.And(a => a.BcInstance != null && listids.Contains(a.BcInstance.LbId ?? 0));
                    }
                    else
                    {
                        predicateAnd = predicateAnd.And(a => a.BcInstance != null && arrayIds.Contains(a.BcInstance.LbId ?? 0));
                    }
                   
                }
                

            }
            var layPage = _IContSubjectMatterService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            return View();

        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新建标的", OptionLogTypeEnum.Add, "新建标的", true)]
        public IActionResult Save(ContSubjectMatterDTO ContSubjectMatterDTO)
        {

            var saveInfo = _IMapper.Map<ContSubjectMatter>(ContSubjectMatterDTO);

            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.ContId = (ContSubjectMatterDTO.ContId ?? 0) <= 0 ? -this.SessionCurrUserId : ContSubjectMatterDTO.ContId;
            //var saveInfoHis = _IMapper.Map<ContSubjectMatterHistory>(saveInfo);
            _IContSubjectMatterService.AddSave(saveInfo, _IMapper);

            return GetResult();

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改标的", OptionLogTypeEnum.Update, "修改标的", true)]
        public IActionResult UpdateSave(ContSubjectMatterDTO ContSubjectMatterDTO)
        {
            if (ContSubjectMatterDTO.Id > 0)
            {
                var updateinfo = _IContSubjectMatterService.Find(ContSubjectMatterDTO.Id);
                var updatedata = _IMapper.Map(ContSubjectMatterDTO, updateinfo);
                updatedata.ModifyUserId = this.SessionCurrUserId;
                updatedata.ModifyDateTime = DateTime.Now;
                _IContSubjectMatterService.UpdateSave(updatedata , _IMapper);
            }

            return GetResult();

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(string Ids)
        {
            _IContSubjectMatterService.Delete(Ids);
            return GetResult();
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContSubjectMatterService.ShowView(Id)


            });
        }
        /// <summary>
        /// 收款合同标的
        /// </summary>
        /// <returns></returns>
        public IActionResult CollContSubmetIndex()
        {
            return View();

        }
        /// <summary>
        /// 付款合同标的
        /// </summary>
        /// <returns></returns>
        public IActionResult PayContSubmetIndex()
        {
            return View();

        }
        /// <summary>
        /// 查询大列表
        /// </summary>
        /// <param name="cateIds">业务品类类别ID</param>
        /// <param name="pageParam">请求一些其他参数</param>
        /// <returns></returns>
        public IActionResult GetMainList(PageparamInfo pageParam,string cateIds)
        {
            var pageInfo = new PageInfo<Model.Models.ContSubjectMatter>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.ContSubjectMatter>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord,pageParam.fType, cateIds));
            Expression<Func<Model.Models.ContSubjectMatter, object>> orderbyLambda = null;
            bool IsAsc = false;
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvJBSXQueryHtBD(pageParam, _IContPlanFinanceService));
            }

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
            var layPage = _IContSubjectMatterService.GetMainList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 查询表达式
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="cateIds">业务品类类别Ids</param>
        /// <param name="fType">资金性质：0:收款，1：付款</param>
        /// <returns></returns>
        private Expression<Func<Model.Models.ContSubjectMatter, bool>> GetQueryExpression(PageInfo<Model.Models.ContSubjectMatter> pageInfo, string keyWord,int fType,string cateIds)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.ContSubjectMatter>();
            var predicateOr = PredicateBuilder.False<Model.Models.ContSubjectMatter>();
            predicateAnd = predicateAnd.And(a => a.Cont!=null&&a.Cont.FinanceType== fType && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetListSubjectMatterPermissionExpression((fType==0? "collSubList" : "paySubList"), this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord) && keyWord.ToLower() != "undefined")
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont!=null&& a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont!=null&&a.Cont.Code.Contains(keyWord));
                predicateOr = predicateOr.Or(a => (a.Cont!=null&& a.Cont.Comp!=null) &&a.Cont.Comp.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            if (!string.IsNullOrEmpty(cateIds))
            {
                var arrIds = StringHelper.String2ArrayInt(cateIds);
                if (arrIds.Any(a=>a==-1))
                {//包含非业务
                    predicateAnd = predicateAnd.And(p => arrIds.Contains(p.BcInstance.LbId??-100)||(p.IsFromCategory??0)==0);
                }
                else
                {
                    predicateAnd = predicateAnd.And(p => arrIds.Contains(p.BcInstance.LbId ?? -100));
                }
                
            }

            return predicateAnd;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo,string cateIds)
        {

            var pageInfo = new NoPageInfo<ContSubjectMatter>();
            var predicateAnd = PredicateBuilder.True<ContSubjectMatter>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, exportRequestInfo.KeyWord,exportRequestInfo.FType, cateIds));
            if (exportRequestInfo.SelRow)
            {//选择行
                predicateAnd = predicateAnd.And(p => exportRequestInfo.GetSelectListIds().Contains(p.Id));
            }
            var layPage = _IContSubjectMatterService.GetMainList(pageInfo, predicateAnd, a => a.Id, true);
            var exceltxt = exportRequestInfo.FType == 0 ? "收款合同标的" : "付款合同标的";
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, exceltxt, layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
        /// <summary>
        /// 添加非业务
        /// </summary>
        /// <param name="lineNum"></param>
        /// <returns></returns>
        public IActionResult AddLine(int lineNum)
        {
            IList<ContSubjectMatter> subs = new List<ContSubjectMatter>();
            for (var i = 0; i < lineNum; i++)
            {
                ContSubjectMatter sub = new ContSubjectMatter();
                sub.CreateDateTime = DateTime.Now;
                sub.ModifyDateTime = DateTime.Now;
                sub.CreateUserId =this.SessionCurrUserId;
                sub.ModifyUserId =this.SessionCurrUserId;
                sub.IsFromCategory =0;//普通非业务标的
                sub.IsDelete = 0;
                sub.ContId = -this.SessionCurrUserId;//合同ID
                sub.Name = "非业务标的"+i;
                subs.Add(sub);
            }
            _IContSubjectMatterService.Add(subs);
            return GetResult();

        }
        /// <summary>
        /// 添加业务品类行
        /// </summary>
        /// <param name="bcIds">单品ID</param>
        /// <returns></returns>
        public IActionResult AddBcLine(string bcIds)
        {
           if(!string.IsNullOrEmpty(bcIds))
            {
                var ids = StringHelper.String2ArrayInt(bcIds);
                IList<ContSubjectMatter> contSubjects = new List<ContSubjectMatter>();
                var bcInstances= _IBcInstanceService.GetQueryable(a => ids.Contains(a.Id)).ToList();
                foreach (var item in bcInstances)
                {
                    ContSubjectMatter sub = new ContSubjectMatter();
                    sub.IsDelete = 0;
                    sub.ModifyDateTime = DateTime.Now;
                    sub.ModifyUserId = this.SessionCurrUserId;
                    sub.ContId = -this.SessionCurrUserId;
                    sub.BcInstanceId = item.Id;
                    sub.IsFromCategory = 1;
                    sub.CreateDateTime = DateTime.Now;
                    sub.CreateUserId = this.SessionCurrUserId;
                    sub.Price = item.Price;
                    sub.Unit = item.Unit;
                    sub.Name = item.Name;
                    contSubjects.Add(sub);
                }
                _IContSubjectMatterService.Add(contSubjects);

            }
            return GetResult();

        }
        /// <summary>
        /// 保存标的
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(IList<ContSubjectMatterDTO> subs)
        {

            _IContSubjectMatterService.AddSave(subs, _IMapper);
            return GetResult();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除的ID</param>
        /// <returns></returns>
        public IActionResult DelSelSub(string Ids)
        {
            var arrIds = StringHelper.String2ArrayInt(Ids);
            _IContSubjectMatterService.Delete(a => arrIds.Contains(a.Id));
            _IContSubjectMatterHistoryService.Delete(a => arrIds.Contains(a.SubjId ?? 0));
            return GetResult();
        }
        /// <summary>
        /// 交付列表
        /// </summary>
        /// <param name="pageParam">页面请求对象</param>
        /// <param name="subIds">选择标的IDs</param>
        /// <returns></returns>
        public IActionResult GetJiaoFuList(PageparamInfo pageParam,string subIds)
        {
            var pageInfo = new NoPageInfo<ContSubjectMatter>();
            var predicateAnd = PredicateBuilder.True<ContSubjectMatter>();
            if (!string.IsNullOrEmpty(subIds))
            {
                var arrIds = StringHelper.String2ArrayInt(subIds);
                predicateAnd = predicateAnd.And(a => arrIds.Any(b=>b==a.Id));
            }
            else
            {
                predicateAnd = predicateAnd.And(a =>a.Id==-1);//不可能出现
            }
           var layPage= _IContSubjectMatterService.GetJiaoFuList(pageInfo, predicateAnd,a=>a.Id,true,pageParam.search);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 获取标的总额
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSubTotal(int contId)
        {
          var   requstResult1 = new RequstResult()
            {
                Msg = "success",
                Code = 0,
                RetValue = 0,
            };
            requstResult1.Data= _IContSubjectMatterService.GetSubTotal(contId,SessionCurrUserId);
            return new CustomResultJson(requstResult1);


        }



    }
}