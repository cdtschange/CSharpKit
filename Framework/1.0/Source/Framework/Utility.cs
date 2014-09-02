using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace Cdts.Framework
{
    public static class Utility
    {
        private static string smtpHost = ConfigurationManager.AppSettings["SmtpServer"];
        private static int smtpPort;
        private static bool smtpSsl;
        private static string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
        private static string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
        static Utility()
        {
            if (!int.TryParse(ConfigurationManager.AppSettings["SmtpPort"], out smtpPort))
            {
                smtpPort = 25;
            }
            if (!bool.TryParse(ConfigurationManager.AppSettings["SmtpSsl"], out smtpSsl))
            {
                smtpSsl = false;
            }
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        public static void SendEMail(string to, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            string[] from = smtpUser.Split("@".ToCharArray());
            msg.From = new MailAddress(smtpUser);
            msg.To.Add(to);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.EnableSsl = smtpSsl;
            client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
            msg.Headers.Add("From", from[0] + "_" + Guid.NewGuid().ToString().Replace("-", "_") + "@" + from[1]);
            client.Send(msg); // 发送邮件
        }
    }
}
