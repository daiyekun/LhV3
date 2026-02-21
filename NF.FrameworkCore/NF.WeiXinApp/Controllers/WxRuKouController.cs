using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.WeiXin.Lib.Common;
using NF.WeiXin.Lib.Module;
using NF.WeiXin.Lib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Controllers
{
    /// <summary>
    /// 微信验证URL
    /// </summary>
    public class WxRuKouController : Controller
    {
        private IHttpContextAccessor _accessor;
        public WxRuKouController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///回调地址
        /// </summary>
        /// <returns></returns>
        public IActionResult InitWxPort(string echoStr, string msg_signature, string timestamp, string nonce)
        {
            var httpcontext = _accessor.HttpContext;
            if (httpcontext.Request.Method.ToLower().Equals("get"))
            {
                
                WeixinUtiliy weixin = new WeixinUtiliy();

                return Content(weixin.Auth2(echoStr, msg_signature, timestamp, nonce));
            }
            else
            {
                return Ok();

            }
           // return Ok();
        }
        /// <summary>
        /// 微信菜单跳转
        /// </summary>
        /// <returns></returns>
        public IActionResult WxHttpRedirect(string state)
        {
            var httpcontext = _accessor.HttpContext;
            var code = httpcontext.Request.Query["Code"];
            var currwxzh = HttpContext.Session.GetString("WxUserId");
            WxUser wxUser = null;
            if (string.IsNullOrWhiteSpace(currwxzh))
            {  
                var accessToken = WeixinUtiliy.GetAccessTokenStr();
                 wxUser=WxQYHOAuth2Utility.SetSessionUser(accessToken, code);
               
                HttpContext.Session.SetString("WxUserId", wxUser.UserId);
                currwxzh=wxUser != null ? wxUser.UserId : "";//微信账号
                //var userwx = HttpContext.Session.GetString("WxUserId");
            }
            //var wxUserId = HttpContext.Session.GetString("WxUserId");
            var baseurl = $"http://{Constant.WxAppBaseURL}/";
            var tempurl = $"{baseurl}Home/Index";
           
            switch (state)
            {
                //菜单
                case "NFCaiDan":
                    tempurl = $"/Home/Index";
                    break;
                //待处理
                case "NFDaiChuLi":
                    tempurl = $"/WorkFlow/DaiChuLi?wxzh={currwxzh}";
                    break;
                //我通过
                case "WoTongGuo":
                    tempurl = $"/WorkFlow/WooTongGuo?wxzh={currwxzh}&HtTye=1";
                    break;
                //我打回
                case "WoDaHui":
                    tempurl = $"/WorkFlow/WooDaHui?wxzh={currwxzh}&HtTye=0";
                    break;
                case "NFWorkflow"://审批
                    tempurl = $"/Home/ApproveIndex";
                    break;
                case "2020Contractdetail"://合同查看页面
                    var objId = httpcontext.Request.Query["OBJID"];
                    tempurl = $"/Contract/Detail?Id={objId}";
                    break;
                //客户
                case "2017kflist":
                    Response.Redirect("~/EBP/EBPCustomer.aspx");
                    break;
                //客户查看页面
                case "2017kfdetail":
                    //var objId = Request["OBJID"];
                    //Response.Redirect("~/EBP/EBPCustomerDetail.aspx?Id=" + objId);
                    break;
                //供应商
                case "2017superlislist":
                    Response.Redirect("~/EBP/EBPSupplier.aspx");
                    break;
                //供应商
                case "2017superlisdetail":
                    //var objId2 = Request["OBJID"];
                    //Response.Redirect("~/EBP/EBPSupplierDetail.aspx?Id=" + objId2);
                    break;
                //其他对方
                case "2017OtherPartieslist":
                    Response.Redirect("~/EBP/EBPOtherParties.aspx");
                    break;
                //其他对方
                case "2017OtherPartiesdetail":
                    //var objId3 = Request["OBJID"];
                    //Response.Redirect("~/EBP/EBPOtherPartiesDetail.aspx?Id=" + objId3);
                    break;
               

                    

            }
           // Log4netHelper.Info("进入："+ tempurl);
            return Redirect(tempurl);
        }
    }
}
