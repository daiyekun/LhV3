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

namespace NF.Web.Areas.InquiryManagement.Controllers
{
    [Area("Inquiry")]
    [Route("Inquiry/[controller]/[action]")]
    public class InquiryController : NfBaseController
    {
        private IWinningInqService _IWinningInqService;
        public IMapper _IMapper;
        public IInquiryService _IInquiryService;
        private ISysPermissionModelService _ISysPermissionModelService;
        private IUserInforService _IUserInforService;
        private IDataDictionaryService _IDataDictionaryService { get; set; }
        public InquiryController (IMapper IMapper, IInquiryService IInquiryService, ISysPermissionModelService ISysPermissionModelService
      ,IUserInforService IUserInforService, IDataDictionaryService IDataDictionaryService
            )
        {
            _IMapper = IMapper;
            _IInquiryService = IInquiryService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IUserInforService = IUserInforService;
            _IDataDictionaryService = IDataDictionaryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新建询价
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            _IInquiryService.ClearJunkItemData(this.SessionCurrUserId);
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
                RetValue = _IInquiryService.CheckInputValExist(fieldInfo)

            });
        }
        /// <summary>
        /// 条件判断
        /// </summary>
        /// <param name="saveInfo"></param>
        /// <returns></returns>
        private RequstResult CheckContSave(Model.Models.Inquiry saveInfo)
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
        [NfCustomActionFilter("新建收款询价", OptionLogTypeEnum.Add, "新建收款询价", true)]
        public IActionResult Save(InquiryDTO info)
        {
           
            var saveInfo = _IMapper.Map<Model.Models.Inquiry>(info);
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
                var dic = _IInquiryService.AddSave(saveInfo);
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
        public IActionResult UpdateSave(InquiryDTO info)
        {
           
            if (info.Id > 0)
            {
                var updateinfo = _IInquiryService.Find(info.Id);
                var updatedata = _IMapper.Map(info, updateinfo);
                var chkres = CheckContSave(updatedata);
                if (Convert.ToInt32(chkres.RetValue) > 0)
                {
                    return GetResult(chkres);
                }
                else
                {
                  
                    updatedata.CreateUserId = this.SessionCurrUserId; //  获取当前用户id
                    updatedata.ModifyUserId = this.SessionCurrUserId; // 获取修改人id
                    updatedata.InState = 0;
                    updatedata.IsDelete = 0;
                    var dic = _IInquiryService.UpdateSave(updatedata);
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
        private Expression<Func<Model.Models.Inquiry, bool>> GetQueryExpression(PageInfo<Model.Models.Inquiry> pageInfo, string keyWord, int? search)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Inquiry>();
            var predicateOr = PredicateBuilder.False<Model.Models.Inquiry>();
            //       predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.FinanceType == 0);

            //   predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetInquiryListPermissionExpression("querycollcontlist", this.SessionCurrUserId, this.SessionCurrUserDeptId));

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

            //if ((search ?? 0) > 0)
            //{

            //    predicateAnd = predicateAnd.And(_IRemindService.GetContractExpression("到期收款合同"));

            //}

            return predicateAnd;
        }

        /// <summary>
        /// 列表-询价
        /// </summary>
        /// <param name="pageParam">请求参数</param>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam)
        {
            var pageInfo = new PageInfo<Model.Models.Inquiry>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.Inquiry>();
              predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.search));
            if (pageParam.selitem)
            {
                predicateAnd = predicateAnd.And(a=>a.InState==1);
            }
            #region 高级查询
            //if (!string.IsNullOrEmpty(pageParam.jsonStr))
            //{//高级查询
            //    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryContract(pageParam, _IUserInforService));
            //}
            if (!string.IsNullOrEmpty(pageParam.filterSos))
            {//基本筛选
                predicateAnd = predicateAnd.And(AdvQueryHelper.GetJBAdvQueryInquiry(pageParam, _IUserInforService));
            }
            Expression<Func<Model.Models.Inquiry, object>> orderbyLambda = null;
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
            var layPage = _IInquiryService.GetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return new CustomResultJson(layPage);

        }
        public IActionResult ShowView(int Id)
        {
            var info = _IInquiryService.ShowView(Id);
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
        [NfCustomActionFilter("删除收款合同", OptionLogTypeEnum.Del, "删除收款合同", false)]
        public IActionResult Delete(string Ids)
        {
            var listIds = StringHelper.String2ArrayInt(Ids);
            //Xjid
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };
            var usname = this.SessionCurrUserId;
            if (usname == -10000)
            {
                var zb = _IInquiryService.GetIsHt(Ids);
                if (zb==0)
                {
                  
                        _IInquiryService.Delete(Ids);
                   
                }
                else
                {
                    resinfo.RetValue = 1;
                    resinfo.Msg ="当前询价还有关联合同请删除后在尝试！";
                    resinfo.Data = Ids;
                }
            }
            else { 
                var permiision = _ISysPermissionModelService.GetInquiryDeletePermission("delInquiry", this.SessionCurrUserId, this.SessionCurrUserDeptId, listIds);
            if (permiision.Code != 0)
            {
                resinfo.RetValue = permiision.Code;
                resinfo.Msg = permiision.GetOptionMsg(permiision.Code);
                resinfo.Data = permiision.noteAllow;
            }
            else
            {
                _IInquiryService.Delete(Ids);
            }
            }


            return new CustomResultJson(resinfo);
        }




        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public IActionResult ExportExcel(ExportRequestInfo exportRequestInfo)
        {
            var pageInfo = new NoPageInfo<Model.Models.Inquiry>();
            var predicateAnd = PredicateBuilder.True<Model.Models.Inquiry>();
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
                    predicateAnd = predicateAnd.And(AdvQueryHelper.GetAdvQueryInquiry(pageParam, _IUserInforService));
                }
            }
            var layPage = _IInquiryService.GetList(pageInfo, predicateAnd, a => a.Id, true);
            var downInfo = ExportDataHelper.ExportExcelExtend(exportRequestInfo, "询价管理", layPage.data);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
        /// <summary>
        /// 修改多字段
        /// </summary>
        /// <returns></returns>
        ///  Id: checkData[0].Id, fieldName: 'InState', fieldVal: state
        public IActionResult UpdateMoreField(int Id,string  fieldName)
        {
           //var res = _IInquiryService.UpdateField(fields);
            RequstResult reqInfo = reqInfo = new RequstResult()
            {
                Msg = "修改成功",
                Code = 0,


            };
            //if (res <= 0)
            //{
            //    reqInfo.Msg = "修改失败";

            //}
            return new CustomResultJson(reqInfo);
        }

        public IActionResult UpdateField(int Id, string fieldName, string fieldVal)
        {
            var res = _IInquiryService.UpdateField(new UpdateFieldInfo()
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