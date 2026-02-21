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
{/// <summary>
/// 合同模块控制器
/// </summary>
    public class ContractController : Controller
    {
        private IHttpContextAccessor _accessor;
        private IContTextService _IContTextService;
        public ContractController(IHttpContextAccessor accessor, IContTextService IContTextService)
        {
            _accessor = accessor;
            _IContTextService = IContTextService;
        }
        public IActionResult SIndex(string wxzh, int FinanceType)
        {
            ViewData["wxzh"] = wxzh;//"daiyekun"; 
            ViewData["HtTye"] =FinanceType;// 1; //
            return View();
        }

        public IActionResult SDetail(int id)
        {
            ViewData["Id"] = id;

            return View();
        }

        /// <summary>
        /// 合同查看页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Detail(int Id)
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
            var sr = HttpContext.Session.GetString("WxUserId");
            ViewData["contId"] = Id;
            ViewData["WxCurrUserId"] = HttpContext.Session.GetString("WxUserId");
            return View();
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="txtId">合同文本ID</param>
        /// <returns></returns>
        public IActionResult DownLoadTxt(int txtId)
        {

            var httxinfo = _IContTextService.Find(txtId);
            DownLoadAndUploadRequestInfo downLoad = new DownLoadAndUploadRequestInfo();
            downLoad.Id = txtId;
            downLoad.folderIndex = 6;
            var txturl = $"{Constant.WxDownloadurl}/{httxinfo.Path}";
            //本地保存路径
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6)
                            , httxinfo.FileName);
            RequestUtility.Download(txturl, pathf);//下载到wxapp
            var downInfo = FileStreamingHelper.Download(pathf);//下载到微信客户端

            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }

    }
}
