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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Controllers
{
    public class ProjectController : Controller
    {
        private IHttpContextAccessor _accessor;
        private IProjAttachmentService _IProjAttachmentService;
       // private IContTextService _IContTextService;
        public ProjectController(IHttpContextAccessor accessor, IProjAttachmentService IProjAttachmentService)
        {
            _accessor = accessor;
            _IProjAttachmentService = IProjAttachmentService;
        }
        public IActionResult ProjectIndex(string Wxzh)
        {
            ViewData["wxzh"] = Wxzh;
            return View();
        }
        public IActionResult Detail(int Id, string Wxz)
        {
            //var httpcontext = _accessor.HttpContext;
            //var code = httpcontext.Request.Query["Code"];
            //var accessToken = WeixinUtiliy.GetAccessTokenStr();
            //var wxUser = WxQYHOAuth2Utility.SetSessionUser(accessToken, code);
            if (Wxz == null)
            {
                var httpcontext = _accessor.HttpContext;
                var code = httpcontext.Request.Query["Code"];
                var accessToken = WeixinUtiliy.GetAccessTokenStr();
                var wxUser = WxQYHOAuth2Utility.SetSessionUser(accessToken, code);
                ViewData["WxCurrUserId"] = wxUser.UserId;// wxzh;
            }
            else
            {
                ViewData["WxCurrUserId"] = Wxz;
            }

            ViewData["contId"] = Id;
            var d = HttpContext.Session.GetString("WxUserId");
            // ViewData["WxCurrUserId"] = wxUser.UserId;
            //  ViewData["WxCurrUserId"] = HttpContext.Session.GetString("WxUserId");
            return View();
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="txtId">合同文本ID</param>
        /// <returns></returns>
        public IActionResult DownLoadTxt(int txtId)
        {

            var httxinfo = _IProjAttachmentService.Find(txtId);
            DownLoadAndUploadRequestInfo downLoad = new DownLoadAndUploadRequestInfo();
            downLoad.Id = txtId;
            downLoad.folderIndex = 4;
            var txturl = $"{Constant.WxDownloadurl}/{httxinfo.Path}";
            //本地保存路径
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 4)
                            , httxinfo.FileName);
            RequestUtility.Download(txturl, pathf);//下载到wxapp
            var downInfo = FileStreamingHelper.Download(pathf);//下载到微信客户端

            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
    }
}
