using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.ViewModel.Extend.Enums;
using NF.WeiXin.Lib.Common;
using NF.WeiXin.Lib.Utility;
using NF.WeiXinApp.Utility.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Controllers
{

    /// <summary>
    /// 合同对方所有页面
    /// </summary>
    public class CompanyController : Controller
    {
        private IHttpContextAccessor _accessor;
        private ICompAttachmentService _ICompAttachmentService;
        public CompanyController(IHttpContextAccessor accessor, ICompAttachmentService ICompAttachmentService)
        {
            _accessor = accessor;
            _ICompAttachmentService = ICompAttachmentService;
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <returns></returns>
        public IActionResult CustomerIndex(string Wxzh, int FinanceType)
        {
            ViewData["wxzh"] = Wxzh;
            ViewData["HtTye"] = FinanceType;
            return View();
        }
        public IActionResult Detail(int Id, int FinanceType)
        {

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("WxUserId")))
            {

                var httpcontext = _accessor.HttpContext;
                var code = httpcontext.Request.Query["Code"];

                var accessToken = WeixinUtiliy.GetAccessTokenStr();
                var wxUser = WxQYHOAuth2Utility.SetSessionUser(accessToken, code);

                HttpContext.Session.SetString("WxUserId", wxUser.UserId);
                //var userwx = HttpContext.Session.GetString("WxUserId");
            }
            ViewData["contId"] = Id;
            var d = HttpContext.Session.GetString("WxUserId");
            ViewData["WxCurrUserId"] = d;// HttpContext.Session.GetString("WxUserId");
            ViewData["FinanceType"] = FinanceType;
            return View();
        }

        public IActionResult Lxr(int Id, string Wxzh)
        {
            ViewData["contIds"] = Id;
            ViewData["Wxzh"] = Wxzh;
            return View();

        }

        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <returns></returns>
        public IActionResult SupplierIndex(string Wxzh, int FinanceType)
        {
            ViewData["wxzh"] = Wxzh;
            ViewData["HtTye"] = FinanceType;
            return View();
        }
        /// <summary>
        /// 其他对方列表
        /// </summary>
        /// <returns></returns>
        public IActionResult OtherIndex(string Wxzh, int FinanceType)
        {
            ViewData["wxzh"] = Wxzh;
            ViewData["HtTye"] = FinanceType;
            return View();
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="txtId">合同文本ID</param>
        /// <returns></returns>
        public IActionResult DownLoadTxt(int txtId)
        {
            var txtur3 = $"{Constant.WxAPPRequestUrl}";
            var txtur2 = $"{Constant.WxAppBaseURL}";
            var httxinfo = _ICompAttachmentService.Find(txtId);
            DownLoadAndUploadRequestInfo downLoad = new DownLoadAndUploadRequestInfo();
            downLoad.Id = txtId;
            downLoad.folderIndex = 0;
            var txturl = $"{Constant.WxDownloadurl}/{httxinfo.Path}";
            //本地保存路径
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 0)
                            , httxinfo.FileName);
            RequestUtility.Download(txturl, pathf);//下载到wxapp
            var downInfo = FileStreamingHelper.Download(pathf);//下载到微信客户端

            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
    }
}
