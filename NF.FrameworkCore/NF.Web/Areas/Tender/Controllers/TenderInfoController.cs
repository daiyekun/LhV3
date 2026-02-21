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
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Tender
{
    /// <summary>
    /// 招标信息
    /// </summary>
    [Area("Tender")]
    [Route("Tender/[controller]/[action]")]
    public class TenderInfoController : NfBaseController
    {
        public  IMapper _IMapper;
        //招标
        public  ITenderInforService _ITenderInforService;
        //权限
        public ISysPermissionModelService _ISysPermissionModelService;
       
        private IDataDictionaryService _IDataDictionaryService { get; set; }
        //招标情况
        public IOpeningSituationInforService _IOpeningSituationInforService;

        public IUserInforService _IUserInforService;
        public TenderInfoController(IMapper IMapper, ITenderInforService ITenderInforService, ISysPermissionModelService ISysPermissionModelService
            , IOpeningSituationInforService IOpeningSituationInforService, IDataDictionaryService IDataDictionaryService, IUserInforService IUserInforService)
        {
            _IMapper = IMapper;
            _ITenderInforService = ITenderInforService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IOpeningSituationInforService = IOpeningSituationInforService;
            _IDataDictionaryService = IDataDictionaryService;
            _IUserInforService = IUserInforService;
        }
        /// <summary>
        /// 招标列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 招标详情
        /// </summary>
        /// <returns></returns>
        public IActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 新建招标的页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            _ITenderInforService.ClearJunkItemData(this.SessionCurrUserId);
            return View();
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<TenderInfor, bool>> GetQueryExpression(PageInfo<TenderInfor> pageInfo, string keyWord, int? search)
        {
            var predicateAnd = PredicateBuilder.True<TenderInfor>();
            var predicateOr = PredicateBuilder.False<TenderInfor>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0);

            //predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetTenderInListPermissionExpression("querycollcontlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));
            if (!string.IsNullOrEmpty(keyWord) && keyWord.ToLower() != "undefined")
            {
                //工程名称、合同编号、招标类型、项目编号
                predicateOr = predicateOr.Or(a => a.Project.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.ProjectNo.Contains(keyWord));

                IList<int> GcGlXmLxINT = _IDataDictionaryService.GetDepDicKes(keyWord);
                if (GcGlXmLxINT.Count > 0)
                {
                    predicateOr = predicateOr.Or(a => GcGlXmLxINT.Contains(a.TenderUserId));
                }
                predicateAnd = predicateAnd.And(predicateOr);
            }

            //if (!string.IsNullOrEmpty(keyWord))
            //{
            //   predicateOr = predicateOr.Or(a => a.Project.Name.Contains(keyWord));
            //    predicateOr = predicateOr.Or(a => a.Project.Code.Contains(keyWord));
            //    predicateAnd = predicateAnd.And(predicateOr);
            //}

            return predicateAnd;
        }
        /// <summary>
        /// 列表-
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<TenderInfor>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<TenderInfor>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search));
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetytAdvQueryTenderInfor(pageParam, _IUserInforService));
            }
            if (pageParam.selitem)
            {//选择执行中的
                predicateAnd = predicateAnd.And(a=>a.TenderStatus==1);
            }
            Expression<Func<TenderInfor, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Name":
                    orderbyLambda = a => a.TenderUserId;
                    break;
                case "Code":
                    orderbyLambda = a => a.ProjectId;
                    break;
                case "ContAmThon"://合同金额
                    orderbyLambda = a => a.ProjectNo;
                    break;
                case "CreateDateTime"://建立时间
                    orderbyLambda = a => a.Iocation;
                    break;
                case "SngDate"://签订时间
                    orderbyLambda = a => a.TenderDate;
                    break;
                case "EfDate"://生效时间
                    orderbyLambda = a => a.ContractEnforcementDepId;
                    break;
                case "RecorderId"://记录人
                    orderbyLambda = a => a.RecorderId;  
                     break;
                case "TenderExpirationDate"://有效期
                    orderbyLambda = a => a.TenderExpirationDate;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;

            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _ITenderInforService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 招标保存
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新建招标", OptionLogTypeEnum.Add, "新建招标", true)]
        public IActionResult Save(TenderInforDTO info)
        {
            try
            {
               
                info.TenderStatus = 0;
                info.IsDelete = 0;
                //info.CreateUserId = this.SessionCurrUserId;
                var saveInfo = _IMapper.Map<TenderInfor>(info);
                saveInfo.CreateUserId = this.SessionCurrUserId;
                var dic = _ITenderInforService.AddSave(saveInfo);
                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功",
                    Data = dic

                });
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
            var info = _ITenderInforService.ShowView(Id);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = info
            });
        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存招标对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("修改招标", OptionLogTypeEnum.Update, "修改招标", true)]
        public IActionResult UpdateSave(TenderInforDTO info)
        {
            if (info.Id > 0)
            {
                var updateinfo = _ITenderInforService.Find(info.Id);
                var updatedata = _IMapper.Map(info, updateinfo);
                var dic = _ITenderInforService.UpdateSave(updatedata);
                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功",
                    Data = dic

                });


            }
            return GetResult();

        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除招标", OptionLogTypeEnum.Del, "删除招标", false)]
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };
            var usname = this.SessionCurrUserId;
            if (usname == -10000)
            {
                var Ht = _ITenderInforService.GetIsHt(Ids);
                if (Ht==0)
                {
                  
                        _ITenderInforService.Delete(Ids);
                    
                }
                else
                {
                    resinfo.RetValue = 1;
                    resinfo.Msg = "招标还有对应的招标合同请删除后在尝试删除";
                    resinfo.Data = Ids;
                }
            }
            else {

            var permiision = _ISysPermissionModelService.GetContractDeletePermission("delzbcollcont", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            if (permiision.Code != 0)
            {
                resinfo.RetValue = permiision.Code;
                resinfo.Msg = permiision.GetOptionMsg(permiision.Code);
                resinfo.Data = permiision.noteAllow;
            }
            else
            {
                _ITenderInforService.Delete(Ids);
            }
            }


            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("建立开标情况", OptionLogTypeEnum.Add, "建立开标情况", true)]
        public IActionResult SaveKbqk(int contId, OpeningSituationInforDTO OpeningSituationInfor)
        {
            var saveInfo = _IMapper.Map<OpeningSituationInfor>(OpeningSituationInfor);
            saveInfo.OpenSituationName = "新建开标情况";
            saveInfo.Unit =0;
            saveInfo.TotalPrice =0;
            saveInfo.Uitprice = 0;
            saveInfo.UserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.TenderId = (contId) <= 0 ? -this.SessionCurrUserId : contId;
            var dic = _IOpeningSituationInforService.Add(saveInfo);

            return GetResult(new RequstResult
            {
                Code = 0,
                Msg = "操作成功",
                Data = dic

            });

        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="contId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetKbqkList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<OpeningSituationInfor>();
            var predicateAnd = PredicateBuilder.True<OpeningSituationInfor>();
            var predicateOr = PredicateBuilder.False<OpeningSituationInfor>();
            predicateOr = predicateOr.Or(a => a.TenderId == -this.SessionCurrUserId && a.IsDelete == 0);
            predicateOr = predicateOr.Or(a => a.TenderId == contId && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IOpeningSituationInforService.GetKbqkList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 保存标的
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(int contId,IList<OpeningSituationInforDTO> subs)
        {
            
            _IOpeningSituationInforService.AddSave(subs, contId);
            return GetResult();
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public IActionResult Deletekb(string Ids)
        {
            _IOpeningSituationInforService.Delete(Ids);
            return GetResult();
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

            var pageInfo = new NoPageInfo<TenderInfor>();
            var predicateAnd = PredicateBuilder.True<TenderInfor>();
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
                //    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryContract(pageParam, _IUserInforService));
                //}
            }
            var layPage = _ITenderInforService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "招标信息", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
        public IActionResult UpdateField(int Id, string fieldName, string fieldVal)
        {
            var res = _ITenderInforService.UpdateField(new UpdateFieldInfo()
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

    }
}