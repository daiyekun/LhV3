using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.Web.Utility;

namespace NF.Web.Areas.System.Controllers
{
    /// <summary>
    /// 邮箱
    /// </summary>
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class EmailController : Controller
    {

        private ISysEmailService _ISysEmailService;
        public EmailController(ISysEmailService ISysEmailService)
        {
            _ISysEmailService = ISysEmailService;
        }

        /// <summary>
        /// 设置邮件
        /// </summary>
        /// <returns></returns>
        public IActionResult SetEmail()
        {
            return View();
        }
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            var emalinfo = _ISysEmailService.GetQueryable(a => true).FirstOrDefault();
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = emalinfo


            });

        }
        /// <summary>
        /// 保存或者修改
        /// </summary>
        /// <returns></returns>
        public IActionResult Save(SysEmail sysEmail)
        {
            if (sysEmail.Id > 0)
            {
                _ISysEmailService.Update(sysEmail);
            }
            else
            {
                _ISysEmailService.Add(sysEmail);
            }

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });


        }
        /// <summary>
        ///  测试邮箱
        /// </summary>
        /// <returns></returns>
        public IActionResult TestMail(string testMail)
        {
            var from = _ISysEmailService.GetQueryable(a => true).FirstOrDefault();
            if (from != null)
            {
                //MailFrom mailFrom, SendContent content,IList<MailTo> mailTos
                var mailform = new MailFrom
                {
                    MailService = from.ServiceName,
                    Mailbox = from.SenderMail,
                    NickName = from.SendNickname,
                    Port = from.ServicePort ?? 0,
                    AuthCode = from.MailPwd

                };
                var sendContent = new SendContent
                {
                    Body = "我是测试邮箱内容，收到了吧！",
                    IsHtml = false,
                    Subject = "我是一条测试邮箱"

                };
                var mailto = new MailTo
                {
                    Mailbox = testMail,
                    NickName = "邮箱昵称NF"


                };
                IList<MailTo> mailTos = new List<MailTo>();
                mailTos.Add(mailto);
                EmailUtility.SendEmail(mailform,sendContent,mailTos);

            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,


            });
        }
    }
}