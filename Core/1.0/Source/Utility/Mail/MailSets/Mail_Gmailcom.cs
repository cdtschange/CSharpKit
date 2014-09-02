using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using Cdts.Web;

namespace Cdts.Utility.Mail.MailSets
{
    public class Mail_Gmailcom : MailBase
    {
        public const string login_forward_url = @"https://www.google.com/accounts/ServiceLogin?service=mail&passive=true&rm=false&continue=http%3A%2F%2Fmail.google.com%2Fmail%2F%3Fui%3Dhtml%26zy%3Dl&bsv=zpwhtygjntrz&scc=1&ltmpl=default&ltmplcache=2";
        public const string login_url = @"https://www.google.com/accounts/ServiceLoginAuth";
        public const string login_url_data = @"ltmpl=default&ltmplcache=2&pstMsg=1&dnConn=&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F%3F&service=mail&rm=false&dsh=-6314543230483561164&ltmpl=default&hl=zh-CN&ltmpl=default&scc=1&ss=1&timeStmp=&secTok=&GALX={2}&Email={0}&Passwd={1}&rmShown=1&signIn=%E7%99%BB%E5%BD%95&asts=";

        public const string reg_check_account = @"https://www.google.com/accounts/CheckAvailability?service=mail&continue=http://www.google.com&Email={0}&FirstName=&LastName=&PrevEmail=&formId=createaccount&inputId=Email&dsh=7295608677008846200";

        public const string open_pop3_submit_url = "https://mail.google.com/mail/?ui=2&ik={0}&rid=mail%3Ass.7957.2.0&at={1}&view=up&act=prefs&_reqid=1166986&pcd=1&mb=0&rt=c";
        public const string open_pop3_submit_url_data = @"p_bx_pe=3&p_ix_pd=2&";

        public const string change_password_forward_url = @"https://www.google.com/accounts/EditPasswd?hl=zh-CN";
        public const string change_password_submit_url = @"https://www.google.com/accounts/UpdatePasswd";
        public const string change_password_submit_url_data = @"hl=zh-CN&timeStmp={0}&secTok={1}&group1=OldPasswd&OldPasswd={2}&Passwd={3}&PasswdAgain={3}&p=&save=%E4%BF%9D%E5%AD%98";

        public const string set_autoforward_account_url = @"https://mail.google.com/mail/?ui=2&ik={0}&rid=d39f..&at={1}&view=up&act=afw&em={2}&_reqid=1147977&pcd=1&mb=0&rt=c";
        public const string set_autoforward_submit_url = @"https://mail.google.com/mail/?ui=2&ik={0}&rid=mail%3Ass.4dc8.1.0&at={1}&view=up&act=prefs&_reqid=549144&pcd=1&mb=0&rt=c";
        public const string set_autoforward_submit_url_data = @"p_sx_at=trash&p_sx_em={0}&";
        public const string cancel_autoforward_submit_url_data = @"p_sx_at=&dp=sx_em&";
        public const string cancel_autoforward_account_url = @"https://mail.google.com/mail/?ui=2&ik={0}&rid=d5e9..&at={1}&view=up&act=rfw&em={2}&_reqid=553759&pcd=1&mb=0&rt=c";

        public const string active_match_Subject = @"Gmail 转发确认";
        public const string active_match_url = @"链接确认请求：\s*(?<url>.*?)\s*如";

        public string gmail_GALX = string.Empty;
        public string gmail_at = string.Empty;
        public string gmail_ik = string.Empty;
        public string gmail_secTok = string.Empty;
        public string gmail_timeStmp = string.Empty;
        public List<string> gmail_activemailList = null;

        public Mail_Gmailcom(MailModel model)
            : base(model)
        {
            gmail_activemailList = new List<string>();
        }

        public override bool Login()
        {
            string html = string.Empty;
            string data = string.Empty;
            Match m = null;
            try
            {
                html = httpClient.SendGet(login_forward_url, Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"<input\s+type=""hidden""\s+name=""GALX""[^>]*value=""(?<galx>.*?)""");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }
                gmail_GALX = m.Groups["galx"].Value;
                data = string.Format(login_url_data, model.Account, model.Password, gmail_GALX);
                html = httpClient.SendPost(login_url, Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"<title>Gmail</title>");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }

                m = Regex.Match(html, @"/mail"",\d+,""(?<ik>[^""]*)");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：html匹配失败！", model.FullAccount);
                    return false;
                }
                gmail_ik = m.Groups["ik"].Value;
                List<Cookie> ccList = Common.GetAllCookies(cookie);
                foreach (Cookie ck in ccList)
                {
                    if (ck.Name == "GMAIL_AT")
                        gmail_at = ck.Value;
                }
                if (string.IsNullOrEmpty(gmail_ik) || string.IsNullOrEmpty(gmail_at))
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：cookie解析失败！", model.FullAccount);
                    return false;
                }

                m = Regex.Match(html, @"\[""fwd"",\[(?<maillist>[^\]]*)\]");
                if (m.Success)
                {
                    string[] mailArray = m.Groups["maillist"].Value.Split(',');
                    foreach (string mail in mailArray)
                    {
                        gmail_activemailList.Add(mail.Trim('"'));
                    }
                }

                isLogin = true;
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}登录失败，错误原因：{1}！", model.FullAccount, e.Message);
                return false;
            }
        }

        public override bool RegistAccount()
        {
            throw new NotImplementedException();
        }

        public override bool CheckRegistAccount()
        {
            Match m = null;
            string html = string.Empty;
            try
            {
                html = httpClient.SendGet(string.Format(reg_check_account, model.Account), Encoding.UTF8, ref cookie, out ErrorMessage);
                //还可以使用
                m = Regex.Match(html, @"还可以使用");
                if (m.Success)
                {
                    return true;
                }
                ErrorMessage = string.Format("账号：{0}已存在！", model.FullAccount);
                return false;

            }
            catch (System.Exception e)
            {
                ErrorMessage = string.Format("检测账号：{0}验证信息失败，错误原因：{1}", model.FullAccount, e.Message);
                return false;
            }
        }

        public override bool ChangePassword(string newPassword)
        {
            if (!isLogin)
            {
                if (!this.Login())
                {
                    return false;
                }
            }
            string html = string.Empty;
            string data = string.Empty;
            Match m = null;
            try
            {
                string url = "https://mail.google.com/mail/?ui=2&ik=" + gmail_ik + "&rid=5065..&at=" + gmail_at + "&view=up&act=rap&_reqid=1162863&pcd=1&mb=0&rt=j";
                html = httpClient.SendGet(url, Encoding.UTF8, ref cookie, out ErrorMessage);
                html = httpClient.SendGet(change_password_forward_url, Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"timeStmp""\s*value='(?<timeStmp>\d+)'(?s).*?secTok""\s*value=\'(?<secTok>[^']*)");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}密码修改匹配失败", model.FullAccount);
                    return false;
                }
                gmail_secTok = m.Groups["secTok"].Value;
                gmail_timeStmp = m.Groups["timeStmp"].Value;
                CookieContainer mycc = new CookieContainer();
                List<Cookie> ccList = Common.GetAllCookies(cookie);
                foreach (Cookie ck in ccList)
                {
                    mycc.SetCookies(new Uri(string.Format("http://www.google.com")), string.Format("{0}={1}", ck.Name, ck.Value));
                }
                data = string.Format(change_password_submit_url_data, gmail_timeStmp, System.Web.HttpUtility.UrlEncode(gmail_secTok), model.Password, newPassword);
                html = httpClient.SendPost(change_password_submit_url, Encoding.UTF8.GetBytes(data), Encoding.UTF8, change_password_forward_url, ref mycc, out ErrorMessage);
                m = Regex.Match(html, "新密码已保存");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}密码修改提交失败", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}密码修改失败,错误原因：{1}", model.FullAccount, e.Message);
                return false;
            }
        }

        public override bool OpenPOP3()
        {
            if (!isLogin)
            {
                if (!this.Login())
                {
                    return false;
                }
            }
            string html = string.Empty;
            string data = string.Empty;
            Match m = null;
            try
            {
                data = open_pop3_submit_url_data;
                html = httpClient.SendPost(string.Format(open_pop3_submit_url, gmail_ik, gmail_at), Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, "while(1)");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}开启POP3失败，匹配错误", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (System.Exception e)
            {
                ErrorMessage = string.Format("账号：{0}开启POP3失败，错误原因:{1}", model.FullAccount, e.Message);
                return false;
            }
        }

        public override bool SetAutoForward(string email)
        {
            if (!isLogin)
            {
                if (!this.Login())
                {
                    return false;
                }
            }
            string html = string.Empty;
            Match m = null;
            try
            {
                html = httpClient.SendPost(string.Format(set_autoforward_account_url, gmail_ik, gmail_at, System.Web.HttpUtility.UrlEncode(email)), new byte[0], Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"while\(1\);");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}设置自动转发功能失败！", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (System.Exception e)
            {
                ErrorMessage = string.Format("账号：{0}设置自动转发功能失败，错误原因：{1}", model.FullAccount, e.Message);
                return false;
            }

        }

        public override bool CancelAutoForward()
        {
            if (!isLogin)
            {
                if (!this.Login())
                {
                    return false;
                }
            }
            string html = string.Empty;
            Match m = null;
            try
            {
                html = httpClient.SendPost(string.Format(set_autoforward_submit_url, gmail_ik, gmail_at), Encoding.UTF8.GetBytes(cancel_autoforward_submit_url_data), Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"while\(1\);");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}取消自动转发功能失败！", model.FullAccount);
                    return false;
                }

                //删除转发账号 
                foreach (string mail in gmail_activemailList)
                {
                    html = httpClient.SendPost(string.Format(cancel_autoforward_account_url, gmail_ik, gmail_at, mail), new byte[0], Encoding.UTF8, ref cookie, out ErrorMessage);
                    m = Regex.Match(html, @"while\(1\);");
                    if (!m.Success)
                    {
                        ErrorMessage = string.Format("账号：{0}删除自动转发的账号失败！", model.FullAccount);
                        return false;
                    }
                }


                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}取消自动转发功能失败，错误原因：{1}", model.FullAccount, e.Message);
                return false;
            }
        }

        public override bool ActiveCertification(MailModel nextLogin)
        {
            try
            {
                if (!model.Carrier.IsNeedActivation)
                    return true;

                POP3Mail pop = new POP3Mail(nextLogin);
                string url = string.Empty;
                string html = null;
                string data = string.Empty;
                Match m = null;

                if (pop.MatchMail(active_match_Subject, active_match_url, ref url))
                {
                    html = httpClient.SendGet(url, Encoding.UTF8, ref cookie, out ErrorMessage);
                    if (Regex.Match(html, @"确认成功！").Success)
                    {
                        //进入Gmail邮件设置转发的账号和删除副本
                        data = string.Format(set_autoforward_submit_url_data, System.Web.HttpUtility.UrlEncode(nextLogin.FullAccount));
                        html = httpClient.SendPost(string.Format(set_autoforward_submit_url, gmail_ik, gmail_at), Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref cookie, out ErrorMessage);
                        m = Regex.Match(html, @"while\(1\);");
                        if (!m.Success)
                        {
                            ErrorMessage = string.Format("账号：{0}确认自动转发功能失败！", model.FullAccount);
                            return false;
                        }

                        return true;
                    }
                }
                ErrorMessage = string.Format("账号：{0}自动激活验证还未获取到验证信息,POP3错误信息：{1}！", model.FullAccount, pop.ErrorMessage);
                return false;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}自动激活验证未获取到验证信息，错误原因：{1}", model.FullAccount, e.Message);
                return false;
            }
        }

    }
}
