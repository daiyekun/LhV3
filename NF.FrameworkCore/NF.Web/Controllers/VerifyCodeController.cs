using System;
using System.Collections.Generic;
using System.DrawingCore.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.SessionExtend;
using NF.Common.Utility;

namespace NF.Web.Controllers
{
    public class VerifyCodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
                /// 数字验证码
                /// </summary>
                /// <returns></returns>
        public FileContentResult NumberVerifyCode()
        {
            string code = VerifyCode.GetSingleObj().CreateVerifyCode(VerifyCode.VerifyCodeType.NumberVerifyCode);
            byte[] codeImage = VerifyCode.GetSingleObj().CreateByteByImgVerifyCode(code, 127, 35);
          
            HttpContext.Session.SetSessionString(StaticData.NFVerifyCode, code.ToUpper());
            return File(codeImage, @"image/jpeg");
        }

        /// <summary>
        /// 字母验证码
        /// </summary>
        /// <returns></returns>
        public FileContentResult AbcVerifyCode()
        {
            string code = VerifyCode.GetSingleObj().CreateVerifyCode(VerifyCode.VerifyCodeType.AbcVerifyCode);
            var bitmap = VerifyCode.GetSingleObj().CreateBitmapByImgVerifyCode(code, 127, 35);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            //Session 存储
            HttpContext.Session.SetSessionString(StaticData.NFVerifyCode, code.ToUpper());
            return File(stream.ToArray(), "image/png");
        }

        /// <summary>
        /// 混合验证码
        /// </summary>
        /// <returns></returns>
        public FileContentResult MixVerifyCode()
        {
            string code = VerifyCode.GetSingleObj().CreateVerifyCode(VerifyCode.VerifyCodeType.MixVerifyCode);
            var bitmap = VerifyCode.GetSingleObj().CreateBitmapByImgVerifyCode(code, 127, 35);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            //Session 存储
            HttpContext.Session.SetSessionString(StaticData.NFVerifyCode, code.ToUpper());
            return File(stream.ToArray(), "image/gif");
        }
    }
}