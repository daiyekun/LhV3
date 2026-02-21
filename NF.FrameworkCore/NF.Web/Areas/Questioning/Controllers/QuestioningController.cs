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
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Questioning.Controllers
{
    [Area("Questioning")]
    [Route("Questioning/[controller]/[action]")]
    public class QuestioningController : NfBaseController
    {
        private IQuestioningService _IQuestioningService;
        public IMapper _IMapper;
        // public IInquiryService _IInquiryService;
        private ISysPermissionModelService _ISysPermissionModelService;
        private IUserInforService _IUserInforService;
      
        private IDataDictionaryService _IDataDictionaryService { get; set; }
        public QuestioningController(IMapper IMapper, ISysPermissionModelService ISysPermissionModelService
      , IUserInforService IUserInforService,
            IQuestioningService IQuestioningService, IDataDictionaryService IDataDictionaryService
           
            )
        {
          
            _IQuestioningService = IQuestioningService;
            _IMapper = IMapper;
            // _IInquiryService = IInquiryService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IUserInforService = IUserInforService;
            _IDataDictionaryService = IDataDictionaryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新建洽谈
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            _IQuestioningService.ClearJunkItemData(this.SessionCurrUserId);
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        //Detail
        /// <summary>
        /// 判断字符值是否存在
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public IActionResult CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                RetValue = _IQuestioningService.CheckInputValExist(fieldInfo)

            });
        }
        /// <summary>
        /// 条件判断
        /// </summary>
        /// <param name="saveInfo"></param>
        /// <returns></returns>
        private RequstResult CheckContSave(Model.Models.Questioning saveInfo)
        {

            var requstResult = new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,
                RetValue = 0,
            };
            return requstResult;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("新建收款合同", OptionLogTypeEnum.Add, "新建收款合同", true)]
        public IActionResult Save(Model.Models.Questioning info)
        {
          
            var saveInfo = _IMapper.Map<Model.Models.Questioning>(info);
            var chkres = CheckContSave(saveInfo);
            if (Convert.ToInt32(chkres.RetValue) > 0)
            {
                return GetResult(chkres);
            }
            else
            {
                saveInfo.CreateUserId = this.SessionCurrUserId; //  获取当前用户id
                saveInfo.ModifyUserId = this.SessionCurrUserId; // 获取修改人id
                saveInfo.InState = 0;
                saveInfo.IsDelete = 0;
                var dic = _IQuestioningService.AddSave(saveInfo);
                return GetResult(new RequstResult
                {
                    Code = 0,
                    Msg = "操作成功",
                    Data = dic
                });
            }
        }


        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">保存信息对象</param>
        /// <returns>当前实体信息</returns>
        [NfCustomActionFilter("修改收款合同", OptionLogTypeEnum.Update, "修改收款合同", true)]
   
        public IActionResult UpdateSave(QuestioningDTO info)
        {
            if (info.Id > 0)
            {
                var updateinfo = _IQuestioningService.Find(info.Id);
                var updatedata = _IMapper.Map(info, updateinfo);
                var chkres = CheckContSave(updatedata);
                if (Convert.ToInt32(chkres.RetValue) > 0)
                {
                    return GetResult(chkres);
                }
                else
                {
                    updatedata.ModifyUserId = this.SessionCurrUserId;

                    var dic = _IQuestioningService.UpdateSave(updatedata);
                    return GetResult(new RequstResult
                    {
                        Code = 0,
                        Msg = "操作成功",
                        Data = dic
                    });
                }
            }
            return GetResult();
        }



        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<Model.Models.Questioning, bool>> GetQueryExpression(PageInfo<Model.Models.Questioning> pageInfo, string keyWord, int? search)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Questioning>();
            var predicateOr = PredicateBuilder.False<Model.Models.Questioning>();
            if (!string.IsNullOrEmpty(keyWord) && keyWord.ToLower() != "undefined")
            {
                //工程名称、合同编号、招标类型、项目编号
                predicateOr = predicateOr.Or(a => a.ProjectNameNavigation.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.ProjectNumber.Contains(keyWord));

                IList<int> GcGlXmLxINT = _IDataDictionaryService.GetDepDicKes(keyWord);
                if (GcGlXmLxINT.Count > 0)
                {
                    predicateOr = predicateOr.Or(a => GcGlXmLxINT.Contains(a.Inquirer ?? 0));
                }
                predicateAnd = predicateAnd.And(predicateOr);
            }

            //if (!string.IsNullOrEmpty(keyWord))
            //{
            //    predicateOr = predicateOr.Or(a => a.ProjectNameNavigation.Name.Contains(keyWord));
            //    predicateOr = predicateOr.Or(a => a.ProjectNameNavigation.Code.Contains(keyWord));
            //    predicateAnd = predicateAnd.And(predicateOr);
            //}
            return predicateAnd;
        }

        /// <summary>
        /// 列表-洽谈
        /// </summary>
        /// <param name = "pageParam" > 请求参数 </ param >
        //// < returns ></ returns >
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Model.Models.Questioning>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.Questioning>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search));
            if (pageParam.selitem)
            {
                predicateAnd = predicateAnd.And(a => a.InState == 1);
            }
            #region 高级查询
            //if (!string.IsNullOrEmpty(pageParam.jsonStr))
            //{//高级查询
            //    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryContract(pageParam, _IUserInforService));
            //}
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetytAdvQueryQuestioning(pageParam, _IUserInforService));
            }
            Expression<Func<Model.Models.Questioning, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Code":
                    orderbyLambda = a => a.ProjectNumber;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;
            }
            #endregion
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _IQuestioningService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }
        public IActionResult ShowView(int Id)
        {
            var info = _IQuestioningService.ShowView(Id);
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
        [NfCustomActionFilter("删除洽谈", OptionLogTypeEnum.Del, "删除洽谈", false)]
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };
            var permiision = _ISysPermissionModelService.GetQuestioningDeletePermission("delQuestioning", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            var usname = this.SessionCurrUserId;
            if (usname == -10000)
            {
                var zb = _IQuestioningService.GetIsHt(Ids);
                if (zb == 0)
                {
                    _IQuestioningService.Delete(Ids);
                }
                else
                {
                    resinfo.RetValue = 1;
                    resinfo.Msg = "当前洽谈还有关联合同请删除后在尝试！";
                    resinfo.Data = Ids;
                }
                }
            else
            {
                if (permiision.Code != 0)
                {
                    resinfo.RetValue = permiision.Code;
                    resinfo.Msg = permiision.GetOptionMsg(permiision.Code);
                    resinfo.Data = permiision.noteAllow;
                }
                else
                {
                    _IQuestioningService.Delete(Ids);
                }
            }
         
            return new CustomResultJson(resinfo);
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
            var pageInfo = new NoPageInfo<Model.Models.Questioning>();
            var predicateAnd = PredicateBuilder.True<Model.Models.Questioning>();
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
                if (!string.IsNullOrEmpty(pageParam.jsonStr))
                {//高级查询
                    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryQuestioning(pageParam, _IUserInforService));
                }
            }
            var layPage = _IQuestioningService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "洽谈管理", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
        /// <summary>
        /// 修改多字段
        /// </summary>
        /// <returns></returns>
          public IActionResult UpdateField(int Id, string fieldName, string fieldVal)
        {
            var res = _IQuestioningService.UpdateField(new UpdateFieldInfo()
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
        //public IActionResult UpdateMoreField(IList<UpdateFieldInfo> fields)
        //{
        //    var res = _IQuestioningService.UpdateField(fields);
        //    RequstResult reqInfo = reqInfo = new RequstResult()
        //    {
        //        Msg = "修改成功",
        //        Code = 0,


        //    };
        //    if (res <= 0)
        //    {
        //        reqInfo.Msg = "修改失败";

        //    }
        //    return new CustomResultJson(reqInfo);
        //}
    }
}