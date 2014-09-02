using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Cdts.Utility.Mail
{
    public class SMTPMail
    {
        private MailModel model = null;
        private SmtpClient client = null;
        public MailPriority Priority { get; set; }
        public Encoding BodyEncoding { get; set; }
        public bool IsBodyHtml { get; set; }
        public string ErrorMessage { get; set; }

        public SMTPMail(MailModel model)
        {
            this.model = model;
            client = new SmtpClient();
            Priority = MailPriority.Normal;
            BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            IsBodyHtml = true;
        }


        public bool SendMail(MailModel model2, string subject, string body)
        {
            try
            {
                client.Host = model.Carrier.SMTPIP;
                client.Port = model.Carrier.SMTPPort;

                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential(model.Account, model.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                bool sm = false;
                switch (model.Carrier.SMTPSSL)
                {
                    case 2:
                        sm = true;
                        break;
                    case 0:
                    case 1:
                    default:
                        sm = false;
                        break;
                }
                client.EnableSsl = sm;
                client.Timeout = 120000;

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(model.FullAccount);
                mm.To.Add(new MailAddress(model2.FullAccount));
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = IsBodyHtml; //指定邮件格式,是否支持HTML格式        
                mm.BodyEncoding = BodyEncoding;//邮件采用的编码        
                mm.Priority = Priority;//设置邮件的优先级
                client.Credentials = new System.Net.NetworkCredential(mm.From.ToString(), model.Password);

                client.Send(mm);
                return true;
            }
            catch (System.Net.Mail.SmtpException e)
            {
                ErrorMessage = string.Format("SMTP账号：{0}发送邮件，错误原因：{1}", this.model.FullAccount, e.Message);
                return false;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("SMTP账号：{0}发送邮件，错误原因：{1}", this.model.FullAccount, e.Message);
                return false;
            }
        }


    }

}
