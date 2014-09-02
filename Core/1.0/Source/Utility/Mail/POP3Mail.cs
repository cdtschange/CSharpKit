using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenPop.Pop3;
using System.Text.RegularExpressions;
using OpenPop.Mime.Header;

namespace Cdts.Utility.Mail
{
    public class POP3Mail
    {
        private MailModel model = null;
        private Pop3Client client = null;
        public string ErrorMessage { get; set; }

        public POP3Mail(MailModel model)
        {
            this.model = model;
            client = new Pop3Client();
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        private bool Connect()
        {
            try
            {
                if (client.Connected)
                    return true;

                bool sm = false;
                switch (model.Carrier.POP3SSL)
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
                client.Connect(model.Carrier.POP3IP, model.Carrier.POP3Port, sm);
                client.Authenticate(model.FullAccount, model.Password);

                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("POP3账号：{0}登录失败，错误原因：{1}", this.model.Account, e.Message);
                return false;
            }

        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        private bool Disconnect()
        {
            try
            {
                if (client.Connected)
                {
                    client.Disconnect();
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>
        /// 这里删除全部邮件，只删除POP3协议中的邮件，
        /// 根据邮件运营商各自的规范
        /// Sohu.com：只删除POP3协议邮件，无法同步删除Web中的邮件。
        /// 163/126/YEAH/21cn:全删除
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllMail()
        {
            try
            {
                if (!this.Connect())
                    return false;

                client.DeleteAllMessages();
                return this.Disconnect();
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("POP3账号：{0}删除邮件失败，错误原因：{1}", this.model.FullAccount, e.Message);
                return false;
            }
        }

        /// <summary>
        /// 递归邮件内容
        /// </summary>
        private void GetBodyHtml(OpenPop.Mime.MessagePart mp, ref string bodyhtml, ref string bodytext)
        {
            if (mp.IsMultiPart)
            {
                List<OpenPop.Mime.MessagePart> mp2List = mp.MessageParts;
                foreach (OpenPop.Mime.MessagePart mp2 in mp2List)
                {
                    GetBodyHtml(mp2, ref bodyhtml, ref bodytext);
                }
            }
            else
            {
                if (mp.ContentType.MediaType == "text/html")
                    bodyhtml += mp.GetBodyAsText();
                else if (mp.ContentType.MediaType == "text/plain")
                    bodytext += mp.GetBodyAsText();
            }
        }

        /// <summary>
        /// 匹配邮件内容
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="body">正文正则表达式</param>
        /// <returns>是否匹配</returns>
        public bool MatchMail(string subject, string body)
        {

            try
            {
                if (!this.Connect())
                    return false;

                Match m = null;
                for (int i = client.GetMessageCount() - 1; i >= 0; i--)
                {
                    MessageHeader mh = client.GetMessageHeaders(i);
                    m = Regex.Match(mh.Subject, subject);
                    if (m.Success)
                    {
                        OpenPop.Mime.Message mm = client.GetMessage(i);
                        string bodyHtml = string.Empty;
                        string bodyText = string.Empty;
                        GetBodyHtml(mm.MessagePart, ref bodyHtml, ref bodyText);
                        //如果发送的是text格式，没有html格式，则取text格式
                        bodyHtml = bodyHtml.Length > bodyText.Length ? bodyHtml : bodyText;
                        m = Regex.Match(bodyHtml, body);
                        if (m.Success)
                        {
                            return true;
                        }

                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("POP3账号：{0}匹配邮件失败，错误原因：{1}", this.model.FullAccount, e.Message);
                return false;
            }
            finally
            {
                this.Disconnect();
            }
        }
        /// <summary>
        /// 匹配邮件
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="body">正则：匹配的邮件内容，含一个url的正则捕获式</param>
        /// <param name="url">激活链接url</param>
        /// <returns></returns>
        public bool MatchMail(string subject, string body, ref string url)
        {
            try
            {
                if (!this.Connect())
                    return false;

                Match m = null;
                for (int i = client.GetMessageCount(); i > 0; i--)
                {
                    MessageHeader mh = client.GetMessageHeaders(i);
                    m = Regex.Match(mh.Subject, subject);

                    if (m.Success)
                    {
                        OpenPop.Mime.Message mm = client.GetMessage(i);
                        string bodyHtml = string.Empty;
                        string bodyText = string.Empty;
                        GetBodyHtml(mm.MessagePart, ref bodyHtml, ref bodyText);
                        //如果发送的是text格式，没有html格式，则取text格式
                        bodyHtml = bodyHtml.Length > bodyText.Length ? bodyHtml : bodyText;
                        m = Regex.Match(bodyHtml, body);
                        if (m.Success)
                        {
                            url = m.Groups["url"].Value;
                            return true;
                        }

                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("POP3账号：{0}查找激活邮件失败，错误原因：{1}", model.FullAccount, e.Message);
                return false;
            }
            finally
            {
                this.Disconnect();
            }
        }

    }
}
