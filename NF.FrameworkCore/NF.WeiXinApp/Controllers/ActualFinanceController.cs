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
    public class ActualFinanceController : Controller
    {
        private IHttpContextAccessor _accessor;
        private IActFinceFileService _IActFinceFileService;
        public ActualFinanceController(IHttpContextAccessor accessor, IActFinceFileService IActFinceFileService)
        {
            _accessor = accessor;
            _IActFinceFileService = IActFinceFileService;
        }


        /// <summary>
        /// 实际资金-收款
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(string wxzh, int Ftype)
        {
            ViewData["wxzh"] = wxzh;
            ViewData["Ftype"] = Ftype;
            return View();
        }

        public IActionResult Detail(int Id, int Htid, string wxzh, int Ftype)
        {
            if (wxzh == null)
            {
                var httpcontext = _accessor.HttpContext;
                var code = httpcontext.Request.Query["Code"];
                var accessToken = WeixinUtiliy.GetAccessTokenStr();
                var wxUser = WxQYHOAuth2Utility.SetSessionUser(accessToken, code);
                ViewData["wxzh"] = wxUser.UserId;// wxzh;
            }
            else
            {
                ViewData["wxzh"] = wxzh;
            }

            ViewData["contId"] = Id;
            ViewData["Htid"] = Htid;

            ViewData["Ftype"] = Ftype;
            ViewData["WxCurrUserId"] = HttpContext.Session.GetString("WxUserId");
            return View();
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="txtId">ID</param>
        /// <returns></returns>
        public IActionResult DownLoadTxt(int txtId)
        {
            var httxinfo = _IActFinceFileService.Find(txtId);
            DownLoadAndUploadRequestInfo downLoad = new DownLoadAndUploadRequestInfo();
            downLoad.Id = txtId;
            downLoad.folderIndex = 16;
            var txturl = $"{Constant.WxDownloadurl}/{httxinfo.Path}";
            //本地保存路径
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 16)
                            , httxinfo.FileName);
            RequestUtility.Download(txturl, pathf);//下载到wxapp
            var downInfo = FileStreamingHelper.Download(pathf);//下载到微信客户端

            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
    }
    
}
