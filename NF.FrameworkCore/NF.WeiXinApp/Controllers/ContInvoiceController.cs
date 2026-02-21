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
    public class ContInvoiceController : Controller
    {
        //IInvoFileService
        private IHttpContextAccessor _accessor;
        private IInvoFileService _IInvoFileService;
        public ContInvoiceController(IHttpContextAccessor accessor, IInvoFileService IInvoFileService)
        {
            _accessor = accessor;
            _IInvoFileService = IInvoFileService;
        }

        public IActionResult Index(string wxzh, int Itype)
        {
            ViewData["wxzh"] = wxzh;
            ViewData["Itype"] = Itype;
            return View();
        }

        public IActionResult Detail(int id, int Htid, string wxzh, int Itype, int R)
        {
            //var httpcontext = _accessor.HttpContext;
            //var code = httpcontext.Request.Query["Code"];
            //var accessToken = WeixinUtiliy.GetAccessTokenStr();
            //var wxUser = WxQYHOAuth2Utility.SetSessionUser(accessToken, code);

            if (wxzh == null)
            {
                var httpcontext = _accessor.HttpContext;
                var code = httpcontext.Request.Query["Code"];
                var accessToken = WeixinUtiliy.GetAccessTokenStr();
                var wxUser = WxQYHOAuth2Utility.SetSessionUser(accessToken, code);
                ViewData["wxzh"] = wxUser.UserId;// wxzh;
                ViewData["WxCurrUserId"] = wxUser.UserId;// wxzh;

            }
            else
            {
                ViewData["wxzh"] = wxzh;
                ViewData["WxCurrUserId"] = wxzh;
            }
            ViewData["contId"] = id;
            ViewData["Htid"] = Htid;
            //  ViewData["wxzh"] = wxUser.UserId;// wxzh;
            ViewData["Itype"] = Itype;
            return View();
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="txtId">合同文本ID</param>
        /// <returns></returns>
        public IActionResult DownLoadTxt(int txtId)
        {

            var httxinfo = _IInvoFileService.Find(txtId);
            DownLoadAndUploadRequestInfo downLoad = new DownLoadAndUploadRequestInfo();
            downLoad.Id = txtId;
            downLoad.folderIndex = 17;
            var txturl = $"{Constant.WxDownloadurl}/{httxinfo.Path}";
            //本地保存路径
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 17)
                            , httxinfo.FileName);
            RequestUtility.Download(txturl, pathf);//下载到wxapp
            var downInfo = FileStreamingHelper.Download(pathf);//下载到微信客户端

            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
    }
}
