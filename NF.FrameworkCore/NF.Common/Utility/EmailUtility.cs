using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
    public class EmailUtility
    {
        /// <summary>
        /// 放邮件
        /// </summary>
        /// <returns></returns>
        public static bool SendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("1111", "vipdaiyekun@qq.com"));
            message.To.Add(new MailboxAddress("111", "daiyekun@qq.com"));

            message.Subject = "发送邮箱标的";

            message.Body = new TextPart("plain") { Text = "测试邮箱" };
            //如果有Html
            //var bodyBuilder = new BodyBuilder();
            //bodyBuilder.HtmlBody = @"<b>This is bold and this is <i>italic</i></b>";
            //message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("SMTP.qq.com", 25, false);
                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //注册邮箱，授权妈
                    client.Authenticate("vipdaiyekun@qq.com", "emrskabfhinobbja");
                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }

        }


        /// <summary>
        /// 发送邮箱公共类
        /// </summary>
        /// <param name="mailFrom">发送者基本信息</param>
        /// <param name="content">发送内容</param>
        /// <param name="mailTos">接受者基本信息</param>
        /// <returns>true:成功，false</returns>
        public static bool SendEmail(MailFrom mailFrom, SendContent content,IList<MailTo> mailTos)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailFrom.NickName, mailFrom.Mailbox));
            foreach (var item in mailTos)
            {
                message.To.Add(new MailboxAddress(item.NickName, item.Mailbox));
            }
        
            message.Subject = content.Subject;
            if (content.IsHtml){
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = content.Body;
                message.Body = bodyBuilder.ToMessageBody();
            }
            else
            {
                message.Body = new TextPart("plain") { Text = content.Body };
            }
            using (var client = new SmtpClient())
            {
                   // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(mailFrom.MailService, mailFrom.Port, false);
                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //注册邮箱，授权妈
                    client.Authenticate(mailFrom.Mailbox, mailFrom.AuthCode);
                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                
               
            }

        }

    }

    public class MailFrom
    {
          /// <summary>
         /// 昵称名称
         /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 发送邮箱号码
        /// </summary>
        public string Mailbox { get; set; }
        /// <summary>
        /// 邮箱服务
        /// </summary>
        public string MailService { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
         /// <summary>
         /// 邮箱授权码（之前是密码）
         /// </summary>
        public string AuthCode { get; set; }

    }
    /// <summary>
    /// 发送内容
    /// </summary>
    public class SendContent
    {
        /// <summary>
        /// 发送内容是否是html
        /// </summary>
        public bool IsHtml { get; set; } = false;
        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 发送内容主体
        /// </summary>
        public string Body { get; set; }
    }
    /// <summary>
    /// 接受者邮箱-发送至--
    /// </summary>
    public class MailTo
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 接受者邮箱
        /// </summary>
        public string Mailbox { get; set; }


    }

}
