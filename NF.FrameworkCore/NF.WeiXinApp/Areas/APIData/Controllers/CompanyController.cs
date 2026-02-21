using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.WeiXin.Lib.Common;
using NF.WeiXin.Lib.Utility;
using NF.WeiXinApp.Extend;
using NF.WeiXinApp.Utility;
using NF.WeiXinApp.Utility.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Areas.APIData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private ICompanyService _ICompanyService;
        /// <summary>
        /// 映射 AutoMapper
        /// </summary>
        private IMapper _IMapper { get; set; }
        private IProjectManagerService _IProjectManagerService;
        private ICityService _ICityService;
        private IProvinceService _IProvinceService;
        private ICountryService _ICountryService;
        private ISysPermissionModelService _ISysPermissionModelService;
        private ICompAttachmentService _ICompAttachmentService;
        /// <summary>
        /// 合同
        /// </summary>
        private IContractInfoService _IContractInfoService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        /// <summary>
        /// 编号自动生成
        /// </summary>
        private INoHipler _INoHipler;
        public CompanyController(
            ICompAttachmentService ICompAttachmentService,
            ICompanyService ICompanyService, ICompContactService ICompContactService,
            IMapper IMapper, ICityService ICityService, ICountryService ICountryService,
            IProvinceService IProvinceService, ISysPermissionModelService ISysPermissionModelService
            , IContractInfoService IContractInfoService
            , IUserInforService IUserInforService
            , INoHipler INoHipler
            , IProjectManagerService IProjectManagerService
             )
        {
            _ICompAttachmentService = ICompAttachmentService;
            _ICompanyService = ICompanyService;
            _IMapper = IMapper;
            _ICityService = ICityService;
            _ICountryService = ICountryService;
            _IProvinceService = IProvinceService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContractInfoService = IContractInfoService;
            _IUserInforService = IUserInforService;
            _INoHipler = INoHipler;
            _IProjectManagerService = IProjectManagerService;
        }
        // GET: api/<UserController>
        [HttpGet("Khlist")]
        public string Get(int page, int limit, string keyWord, string Wxzh, int FinanceType)
        {
            PageparamInfo pageParam = new PageparamInfo();
            var usinfo = _IContractInfoService.Yhinfo(Wxzh);
            var UsId = usinfo.Id;
            var UsDc = usinfo.DepartmentId;
            var pageInfo = new PageInfo<Model.Models.Company>(pageIndex: page, pageSize: limit);
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam, UsId, UsDc ?? 0, FinanceType, keyWord));
            Expression<Func<Model.Models.Company, object>> orderbyLambda = null;
            bool IsAsc = false;
            switch (pageParam.orderField)
            {
                case "Name":
                    orderbyLambda = a => a.Name;

                    break;
                case "Code":
                    orderbyLambda = a => a.Code;
                    break;
                default:
                    orderbyLambda = a => a.Id;
                    break;
            }
            if (pageParam.orderType == "asc")
                IsAsc = true;
            var layPage = _ICompanyService.GetWxCompList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
            return layPage.ToWxJson();
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<Model.Models.Company, bool>> GetQueryExpression(PageInfo<Model.Models.Company> pageInfo, PageparamInfo pageParam, int UsId, int UsDc, int FinanceType, string keyWord)
        {
            var predicateAnd = PredicateBuilder.True<Model.Models.Company>();
            var predicateOr = PredicateBuilder.False<Model.Models.Company>();
            predicateAnd = predicateAnd.And(a => a.Ctype == FinanceType && a.IsDelete == 0);
            var dt = "";
            if (FinanceType==0)
            {
                dt = "querycustomerlist";
            }
            else if (FinanceType == 1)
            {
                dt = "querysupplierlist";
            }
            else if (FinanceType == 2)
            {
                dt = "queryotherlist";
            }


            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetCmpListPermissionExpression(dt, UsId, UsDc));
          //  predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetCmpListPermissionExpression(dt, UsId, UsDc));
            if (!string.IsNullOrEmpty(keyWord) && keyWord.ToLower() != "undefined")
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Code.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            return predicateAnd;
        }

        // GET api/<UserController>/5
        [HttpGet("KhView")]
        public string KhView(int id)
        {
            var Kh = _ICompanyService.KhView(id);
            return new RequestData(data: Kh).ToWxJson();
        }
        [HttpGet("WxQtlxr")]
        public string WxQtlxr(int id)
        {
            var Re = _ICompanyService.WxQtlxr(id);
            return new RequestData(data: Re).ToWxJson();

        }

        /// <summary>
        ///客户附件列表
        /// </summary>
        /// <param name="File">文件id</param>
        /// <returns></returns>
        [HttpGet("GetcompViwe")]
        public string GetcompViwe(int Id)
        {

            var khFile = _ICompAttachmentService.GetcompViwe(Id);
            return new RequestData(data: khFile).ToWxJson();

        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="txtId">合同文本ID</param>
        /// <returns></returns>
        [HttpGet("DownLoadTxt")]
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="txtId">合同文本ID</param>
        /// <returns></returns>
        [HttpGet("DownLoadFile")]
        public IActionResult DownLoadFile(int txtId)
        {
            // var txtId = 1;
            var httxinfo = _ICompAttachmentService.Find(txtId);
            DownLoadAndUploadRequestInfo downLoad = new DownLoadAndUploadRequestInfo();
            downLoad.Id = txtId;
            downLoad.folderIndex = 0;
            var txturl = $"{Constant.WxDownloadurl}/{httxinfo.Path}";
            //本地保存路径
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoad.folderIndex)
                            , httxinfo.FileName);
            RequestUtility.Download(txturl, pathf);//下载到wxapp
            var downInfo = FileStreamingHelper.Download(pathf);//下载到微信客户端

            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
