using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web;
using Cdts.Web;
using System.Net;
using Cdts.Utility.WinForm;

namespace Cdts.Utility.Mail.MailSets
{
    class Mail_QQcom : MailBase
    {
        public const string login_forward_url = @"https://mail.qq.com/cgi-bin/loginpage";
        public const string login_url = @"https://mail.qq.com/cgi-bin/login?sid=,2,zh_CN";
        public const string login_url_data = @"sid=%2C2%2Czh_CN&firstlogin=false&starttime={0}&redirecturl=&f=html&p={1}&ept=0&delegate_url=&s=&ts={2}&from=&ppp=&chg=0&target=&checkisWebLogin=9&uin={3}&aliastype=@qq.com&pp=0000000000&verifycode={4}";
        public const string login_checkcode_url = @"https://mail.qq.com/cgi-bin/getverifyimage?aid=23000101&f=html&ck=1&0.6069359359630926";
        public const string login_checkaccount_url = @"https://mail.qq.com/cgi-bin/getinvestigate?t=loginpage&s=json&stat=verifyimg&verifyuser={0}";


        public const string autoforward_url_sumit = @"{0}setting1?sid={1}";
        public const string cancelforward_url_submit_data = @"sid={0}&Fun=submit&signature=%3Cdiv%3E%26nbsp%3B%3C%2Fdiv%3E&autocontent__html=%3CDIV%3E%D5%E2%CA%C7%C0%B4%D7%D4QQ%D3%CA%CF%E4%B5%C4%BC%D9%C6%DA%D7%D4%B6%AF%BB%D8%B8%B4%D3%CA%BC%FE%A1%A3%3C%2FDIV%3E%0D%0A%3CDIV%3E%26nbsp%3B%3C%2FDIV%3E%0D%0A%3CDIV%3E%C4%FA%BA%C3%A3%AC%CE%D2%D7%EE%BD%FC%D5%FD%D4%DA%D0%DD%BC%D9%D6%D0%A3%AC%CE%DE%B7%A8%C7%D7%D7%D4%BB%D8%B8%B4%C4%FA%B5%C4%D3%CA%BC%FE%A1%A3%CE%D2%BD%AB%D4%DA%BC%D9%C6%DA%BD%E1%CA%F8%BA%F3%A3%AC%BE%A1%BF%EC%B8%F8%C4%FA%BB%D8%B8%B4%A1%A3%3C%2FDIV%3E&addhtml=yes&rtcheck=0&verifykey=&verifycode=&verifycode_cn=&showcount=1&defaultfontid=0&defaultsizeid=0&defaultcolor=default&listmode=1&delflag=0&selectSign=-1&wapsigncontent__txt=%B8%C3%D3%CA%BC%FE%B4%D3%D2%C6%B6%AF%C9%E8%B1%B8%B7%A2%CB%CD%0D%0A&noinclude=0&titlePrefix=0&autofwd=0&fwdaddress=%C7%EB%CC%EE%D0%B4%C4%FA%B5%C4%D3%CA%BC%FE%B5%D8%D6%B7&replytype=0&abstract=0&mailsize=0&weather=0&Birthday=1&todaynews=1&editor=0&addrhistory=1&txtformat=0&IsValid=0&IsWapValid=0&atcpsubject=0&autoaddaddress=0&savesendbox=0&sendmailunicode=0&bgsend=0&QQlight=0&qqplus=0&plpopen=1";
        public const string set_forward_url_submit_data = @"sid={0}&Fun=submit&signature=%3Cdiv%3E%26nbsp%3B%3C%2Fdiv%3E&autocontent__html=%3CDIV%3E%D5%E2%CA%C7%C0%B4%D7%D4QQ%D3%CA%CF%E4%B5%C4%BC%D9%C6%DA%D7%D4%B6%AF%BB%D8%B8%B4%D3%CA%BC%FE%A1%A3%3C%2FDIV%3E%0D%0A%3CDIV%3E%26nbsp%3B%3C%2FDIV%3E%0D%0A%3CDIV%3E%C4%FA%BA%C3%A3%AC%CE%D2%D7%EE%BD%FC%D5%FD%D4%DA%D0%DD%BC%D9%D6%D0%A3%AC%CE%DE%B7%A8%C7%D7%D7%D4%BB%D8%B8%B4%C4%FA%B5%C4%D3%CA%BC%FE%A1%A3%CE%D2%BD%AB%D4%DA%BC%D9%C6%DA%BD%E1%CA%F8%BA%F3%A3%AC%BE%A1%BF%EC%B8%F8%C4%FA%BB%D8%B8%B4%A1%A3%3C%2FDIV%3E&addhtml=yes&rtcheck=0&verifykey=&verifycode=&verifycode_cn=&showcount=1&defaultfontid=0&defaultsizeid=0&defaultcolor=default&listmode=1&delflag=0&selectSign=-1&wapsigncontent__txt=%B8%C3%D3%CA%BC%FE%B4%D3%D2%C6%B6%AF%C9%E8%B1%B8%B7%A2%CB%CD%0D%0A&noinclude=0&titlePrefix=0&autofwd=2&fwdaddress={1}&fwdbackup=1&replytype=0&abstract=0&mailsize=0&weather=0&Birthday=1&todaynews=1&editor=0&addrhistory=1&txtformat=0&IsValid=0&IsWapValid=0&atcpsubject=0&autoaddaddress=0&savesendbox=0&sendmailunicode=0&bgsend=0&QQlight=0&qqplus=0&plpopen=1";

        public string urlHead = string.Empty;//http://m80.mail.qq.com/cgi-bin/
        public string sid = string.Empty;//1CycRFt5NHspw0oE
        public string loginrandom = string.Empty; //&r=b0707ad21a32285fe27e89895ea4822e

        public const string active_match_Subject = @"QQMail自动转发验证邮件";
        public const string active_match_url = @"<a class=""domainButton"" href=""(?<url>[^""]*)""";


        public const string change_password_login_submit_url = "http://ptlogin2.qq.com/login?u={0}@qq.com&p={1}&verifycode={2}&aid=2001601&u1=http%3A%2F%2Faq.qq.com%2Fcn2%2Fchange_psw%2Fchange_psw_index&h=1&ptredirect=1&ptlang=2052&from_ui=1&dumy=&fp=loginerroralert&action=6-16-204982&mibao_css=";
        public const string change_password_checkcode_login_url = "http://captcha.qq.com/getimage?&uin=ddmailqq_2000@qq.com&aid=2001601&0.04559490678342093";
        public const string change_password_checkcode_submit_url = @"http://captcha.qq.com/getimage?aid=2000401&0.5704815219456718";
        public const string change_password_submit_forward_url = @"http://aq.qq.com/cn2/change_psw/change_psw_index";
        public const string change_password_submit_ajax_url = @"http://aq.qq.com/cn2/ajax/check_verifycode?verify_code={0}";
        public const string change_password_submit_url = @"http://aq.qq.com/cn2/change_psw/change_psw";//
        public const string change_password_submit_url_data = @"psw_old={0}&psw={1}&psw_ack={1}&verifycode={2}";

        public const string openPOP3_url_submit = @"{0}setting4?sid={1}";
        public const string openPOP3_url_data = @"sid={0}&Fun=submit&signature=&del_step=1&apply=&ccalias=&reload=0&recvaliasmail=0&verifykey={2}&verifycode=&verifycode_cn={3}&nickname={1}&setnick=1&datetype=0&birthMonth=10&birthDay=10&showdefaultemailfrom={4}@qq.com&poprecent=30&setBirthdayconfig=1&clitohttps=0&PopEsmtp=1&openimap=0&popmyFolder=0&popbookMail=0&popjunkMail=0&savesend_esmtp=0";
        public const string openPOP3_checkcode_url = @"{0}getverifyimage?aid=23000101&sid={1}&0.44828176545830406";

        public Mail_QQcom(MailModel model)
            : base(model)
        {

        }

        protected bool GetLoginCheckCodeImage(out byte[] imgPic, out string syscheckcode)
        {
            imgPic = null;
            syscheckcode = null;
            try
            {
                imgPic = httpClient.SendGetStream(login_checkcode_url, null, Encoding.UTF8, ref cookie, out ErrorMessage);
                if (imgPic.Length <= 0)
                {
                    ErrorMessage = string.Format("账号：{0}获取验证码图片失败！", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}获取验证码失败，错误原因：{1}！", model.FullAccount, e.Message);
                return false;
            }
        }
        protected bool GetChangePasswordCheckCodeImage(out byte[] imgPic, out string syscheckcode)
        {
            imgPic = null;
            syscheckcode = null;
            try
            {
                imgPic = httpClient.SendGetStream(change_password_checkcode_login_url, null, Encoding.UTF8, ref cookie, out ErrorMessage);
                if (imgPic.Length <= 0)
                {
                    ErrorMessage = string.Format("账号：{0}获取验证码图片失败！", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}获取验证码失败，错误原因：{1}！", model.FullAccount, e.Message);
                return false;
            }
        }
        protected bool GetChangePassword2CheckCodeImage(out byte[] imgPic, out string syscheckcode)
        {
            imgPic = null;
            syscheckcode = null;
            try
            {
                imgPic = httpClient.SendGetStream(change_password_checkcode_submit_url, null, Encoding.UTF8, ref cookie, out ErrorMessage);
                if (imgPic.Length <= 0)
                {
                    ErrorMessage = string.Format("账号：{0}获取验证码图片失败！", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}获取验证码失败，错误原因：{1}！", model.FullAccount, e.Message);
                return false;
            }
        }
        protected bool GetOpenPop3CheckCodeImage(out byte[] imgPic, out string syscheckcode)
        {
            imgPic = null;
            syscheckcode = null;
            try
            {
                imgPic = httpClient.SendGetStream(openPOP3_checkcode_url, null, Encoding.UTF8, ref cookie, out ErrorMessage);
                if (imgPic.Length <= 0)
                {
                    ErrorMessage = string.Format("账号：{0}获取验证码图片失败！", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}获取验证码失败，错误原因：{1}！", model.FullAccount, e.Message);
                return false;
            }
        }




        public override bool Login()
        {
            string html = string.Empty;
            string data = string.Empty;
            Match m = null;
            try
            {
                html = httpClient.SendGet(login_forward_url, Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);
                string verifycode = "";
                m = Regex.Match(model.Account, @"^[0-9]*$");
                //纯数字账号输入验证码
                //if (m.Success)
                {
                    html = httpClient.SendGet(login_checkaccount_url, Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);
                    AuthCodeForm codeForm = new AuthCodeForm(GetLoginCheckCodeImage);
                    if (DialogResult.OK != codeForm.ShowDialog())
                    {
                        ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：窗口点击错误！", model.FullAccount);
                        return false;
                    }

                    verifycode = codeForm.GetCheckCode();
                }
                data = string.Format(login_url_data, Common.GetServerTime(13), HttpUtility.UrlEncode(model.Password), Common.GetServerTime(10), model.Account, verifycode);
                html = httpClient.SendPost(login_url, Encoding.UTF8.GetBytes(data), Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"正在登录(?s).*?urlHead=""(?<urlHead>[^""]*)""(?s).*?sid=(?<sid>[^""]*)""(?s).*?&r=(?<loginrandom>[^""]*)"";");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败", model.FullAccount);
                    return false;
                }
                urlHead = m.Groups["urlHead"].Value;
                sid = m.Groups["sid"].Value;
                loginrandom = m.Groups["loginrandom"].Value;
                string url = urlHead + "frame_html?sid=" + sid + "&r=" + loginrandom;

                CookieContainer mycc = new CookieContainer();
                List<Cookie> ccList = Common.GetAllCookies(cookie);
                string s = urlHead.Substring(0, urlHead.IndexOf("/cgi-bin/"));
                foreach (Cookie ck in ccList)
                {
                    mycc.SetCookies(new Uri(string.Format(s)), string.Format("{0}={1}", ck.Name, ck.Value));
                }

                html = httpClient.SendGet(url, Encoding.GetEncoding("GB2312"), ref mycc, out ErrorMessage);
                m = Regex.Match(html, @"content=""no-cache""><title>QQ邮箱</title><script>");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败", model.FullAccount);
                    return false;
                }
                cookie = mycc;
                isLogin = true;
                return true;
            }
            catch (System.Exception e)
            {
                ErrorMessage = string.Format("账号：{0}登录失败，错误原因：{1}！", model.FullAccount, e.Message);
                return false;
            }
        }

        public override bool RegistAccount()
        {
            throw new Exception("QQMail无法注册！");
        }

        public override bool CheckRegistAccount()
        {
            throw new Exception("QQMail无法检测注册！");
        }

        public override bool ChangePassword(string newPassword)
        {
            string html = string.Empty;
            string data = string.Empty;
            Match m = null;
            try
            {
                string verifycode = "";
                AuthCodeForm codeForm = new AuthCodeForm(GetChangePasswordCheckCodeImage);
                if (DialogResult.OK != codeForm.ShowDialog())
                {
                    ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：窗口点击错误！", model.FullAccount);
                    return false;
                }

                verifycode = codeForm.GetCheckCode();
                string pwd = PwdEncrypt(model.Password, verifycode);

                html = httpClient.SendGet(string.Format(change_password_login_submit_url, model.Account, pwd, verifycode), Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"ptuiCB\('0','0','(?<url>[^']*)'.*?登录成功");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}修改密码时登录失败", model.FullAccount);
                    return false;
                }

                //修改密码：
                verifycode = "";
                codeForm = new AuthCodeForm(GetChangePassword2CheckCodeImage);
                if (DialogResult.OK != codeForm.ShowDialog())
                {
                    ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：窗口点击错误！", model.FullAccount);
                    return false;
                }
                verifycode = codeForm.GetCheckCode();
                html = httpClient.SendGet(string.Format(change_password_submit_ajax_url, verifycode), Encoding.UTF8, ref cookie, out ErrorMessage);
                if (html.IndexOf("0") == -1)
                {
                    ErrorMessage = string.Format("账号：{0}密码修改失败,验证码输入错误", model.FullAccount);
                    return false;
                }
                data = string.Format(change_password_submit_url_data, model.Password, newPassword, verifycode);
                CookieContainer mycc = new CookieContainer();
                List<Cookie> ccList = Common.GetAllCookies(cookie);
                foreach (Cookie ck in ccList)
                {
                    mycc.SetCookies(new Uri(string.Format("http://aq.qq.com")), string.Format("{0}={1}", ck.Name, ck.Value));
                }
                html = httpClient.SendPost(change_password_submit_url, Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref mycc, out ErrorMessage);
                m = Regex.Match(html, @"QQ密码修改成功");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}密码修改失败", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (System.Exception e)
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
            CookieContainer mycc = new CookieContainer();
            string s = urlHead.Substring(0, urlHead.IndexOf("/cgi-bin/"));
            List<Cookie> ccList = Common.GetAllCookies(cookie);
            foreach (Cookie ck in ccList)
            {
                mycc.SetCookies(new Uri(string.Format(s)), string.Format("{0}={1}", ck.Name, ck.Value));
            }
            string verifykey = "";
            string verify = "";
            data = string.Format(openPOP3_url_data, sid, model.Account, verifykey, verify, model.Account);
            html = httpClient.SendPost(string.Format(openPOP3_url_submit, urlHead, sid), Encoding.GetEncoding("GB2312").GetBytes(data), Encoding.GetEncoding("GB2312"), ref mycc, out ErrorMessage);
            m = Regex.Match(html, @"gsMsgSettingOk, ""success""");
            if (m.Success)
                return true;
            cookie = mycc;
            //是否需要输入验证码
            m = Regex.Match(html, @"请输入验证码以完成本次设置.*?\(""setting"", \s*""(?<verifykey>[^""]*)""\)");
            if (!m.Success)
            {
                ErrorMessage = string.Format("账号：{0}开启POP3功能失败！", model.FullAccount);
                return false;
            }

            verifykey = m.Groups["verifykey"].Value;

            //POP3验证码
            string verifycode = "";
            AuthCodeForm codeForm = new AuthCodeForm(GetOpenPop3CheckCodeImage);
            if (DialogResult.OK != codeForm.ShowDialog())
            {
                ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：窗口点击错误！", model.FullAccount);
                return false;
            }
            verifycode = codeForm.GetCheckCode();
            mycc = new CookieContainer();
            ccList = Common.GetAllCookies(cookie);
            foreach (Cookie ck in ccList)
            {
                mycc.SetCookies(new Uri(string.Format(s)), string.Format("{0}={1}", ck.Name, ck.Value));
            }


            data = string.Format(openPOP3_url_data, sid, "", verifykey, verifycode, model.Account);
            html = httpClient.SendPost(string.Format(openPOP3_url_submit, urlHead, sid), Encoding.GetEncoding("GB2312").GetBytes(data), Encoding.GetEncoding("GB2312"), ref mycc, out ErrorMessage);

            m = Regex.Match(html, @"gsMsgSettingOk, ""success""");
            cookie = mycc;
            if (!m.Success)
            {
                ErrorMessage = string.Format("账号：{0}开启POP3失败", model.FullAccount);
                return false;
            }

            cookie = mycc;
            return true;
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
            string data = string.Empty;
            Match m = null;
            try
            {
                data = string.Format(set_forward_url_submit_data, sid, email);
                CookieContainer mycc = new CookieContainer();
                string s = urlHead.Substring(0, urlHead.IndexOf("/cgi-bin/"));
                List<Cookie> ccList = Common.GetAllCookies(cookie);
                foreach (Cookie ck in ccList)
                {
                    mycc.SetCookies(new Uri(string.Format(s)), string.Format("{0}={1}", ck.Name, ck.Value));
                }
                html = httpClient.SendPost(string.Format(autoforward_url_sumit, urlHead, sid), Encoding.UTF8.GetBytes(data), Encoding.GetEncoding("GB2312"), login_url, ref mycc, out ErrorMessage);
                m = Regex.Match(html, @"gsMsgSettingOk, ""success""");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}设置自动转发功能失败！", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (Exception e)
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
            string data = string.Empty;
            Match m = null;
            try
            {
                data = string.Format(cancelforward_url_submit_data, sid);
                CookieContainer mycc = new CookieContainer();
                string s = urlHead.Substring(0, urlHead.IndexOf("/cgi-bin/"));
                List<Cookie> ccList = Common.GetAllCookies(cookie);
                foreach (Cookie ck in ccList)
                {
                    mycc.SetCookies(new Uri(string.Format(s)), string.Format("{0}={1}", ck.Name, ck.Value));
                }
                html = httpClient.SendPost(string.Format(autoforward_url_sumit, urlHead, sid), Encoding.UTF8.GetBytes(data), Encoding.GetEncoding("GB2312"), login_url, ref mycc, out ErrorMessage);
                m = Regex.Match(html, @"gsMsgSettingOk, ""success""");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}取消自动转发功能失败！", model.FullAccount);
                    return false;
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
                if (pop.MatchMail(active_match_Subject, active_match_url, ref url))
                {
                    html = httpClient.SendGet(url, Encoding.GetEncoding("gb2312"), ref cookie, out ErrorMessage);
                    if (Regex.Match(html, @"操作成功！").Success)
                        return true;
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

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="strPwd"></param>
        /// <param name="strValidationCode"></param>
        /// <returns></returns>
        private string PwdEncrypt(string strPwd, string strValidationCode)
        {

            string result = null;
            ScriptEngine scriptEngine = new ScriptEngine(ScriptLanguage.JavaScript);
            scriptEngine.Reset();
            scriptEngine.AllowUI = false;
            scriptEngine.Timeout = 10000;
            string cmd = string.Format(@"md5(md5_3('{0}') + '{1}')", strPwd, strValidationCode.ToUpper());
            result = (string)scriptEngine.Eval(cmd, this.strJs);
            return result;
        }

        /// <summary>
        /// 密码加密js
        /// </summary>
        public readonly string strJs = @"var hexcase = 1; var b64pad = """"; var chrsz = 8; var mode = 32;
        function md5_3(B) {
            var A = new Array; A = core_md5(str2binl(B), B.length * chrsz);
            A = core_md5(A, 16 * chrsz); A = core_md5(A, 16 * chrsz);
            return binl2hex(A)
        }
        function md5(A) { return hex_md5(A) }
        function hex_md5(A) { return binl2hex(core_md5(str2binl(A), A.length * chrsz)) }
        function str_md5(A) { return binl2str(core_md5(str2binl(A), A.length * chrsz)) }
        function core_md5(K, F) {
            K[F >> 5] |= 128 << ((F) % 32); K[(((F + 64) >>> 9) << 4) + 14] = F; var J = 1732584193;
            var I = -271733879; var H = -1732584194; var G = 271733878;
            for (var C = 0; C < K.length; C += 16) {
                var E = J; var D = I; var B = H; var A = G; J = md5_ff(J, I, H, G, K[C + 0], 7, -680876936); G = md5_ff(G, J, I, H, K[C + 1], 12, -389564586); H = md5_ff(H, G, J, I, K[C + 2], 17, 606105819); I = md5_ff(I, H, G, J, K[C + 3], 22, -1044525330); J = md5_ff(J, I, H, G, K[C + 4], 7, -176418897); G = md5_ff(G, J, I, H, K[C + 5], 12, 1200080426); H = md5_ff(H, G, J, I, K[C + 6], 17, -1473231341); I = md5_ff(I, H, G, J, K[C + 7], 22, -45705983); J = md5_ff(J, I, H, G, K[C + 8], 7, 1770035416); G = md5_ff(G, J, I, H, K[C + 9], 12, -1958414417); H = md5_ff(H, G, J, I, K[C + 10], 17, -42063); I = md5_ff(I, H, G, J, K[C + 11], 22, -1990404162); J = md5_ff(J, I, H, G, K[C + 12], 7, 1804603682); G = md5_ff(G, J, I, H, K[C + 13], 12, -40341101); H = md5_ff(H, G, J, I, K[C + 14], 17, -1502002290); I = md5_ff(I, H, G, J, K[C + 15], 22, 1236535329); J = md5_gg(J, I, H, G, K[C + 1], 5, -165796510); G = md5_gg(G, J, I, H, K[C + 6], 9, -1069501632); H = md5_gg(H, G, J, I, K[C + 11], 14, 643717713); I = md5_gg(I, H, G, J, K[C + 0], 20, -373897302); J = md5_gg(J, I, H, G, K[C + 5], 5, -701558691); G = md5_gg(G, J, I, H, K[C + 10], 9, 38016083); H = md5_gg(H, G, J, I, K[C + 15], 14, -660478335); I = md5_gg(I, H, G, J, K[C + 4], 20, -405537848); J = md5_gg(J, I, H, G, K[C + 9], 5, 568446438); G = md5_gg(G, J, I, H, K[C + 14], 9, -1019803690); H = md5_gg(H, G, J, I, K[C + 3], 14, -187363961); I = md5_gg(I, H, G, J, K[C + 8], 20, 1163531501); J = md5_gg(J, I, H, G, K[C + 13], 5, -1444681467); G = md5_gg(G, J, I, H, K[C + 2], 9, -51403784); H = md5_gg(H, G, J, I, K[C + 7], 14, 1735328473); I = md5_gg(I, H, G, J, K[C + 12], 20, -1926607734); J = md5_hh(J, I, H, G, K[C + 5], 4, -378558); G = md5_hh(G, J, I, H, K[C + 8], 11, -2022574463); H = md5_hh(H, G, J, I, K[C + 11], 16, 1839030562); I = md5_hh(I, H, G, J, K[C + 14], 23, -35309556); J = md5_hh(J, I, H, G, K[C + 1], 4, -1530992060); G = md5_hh(G, J, I, H, K[C + 4], 11, 1272893353); H = md5_hh(H, G, J, I, K[C + 7], 16, -155497632); I = md5_hh(I, H, G, J, K[C + 10], 23, -1094730640); J = md5_hh(J, I, H, G, K[C + 13], 4, 681279174); G = md5_hh(G, J, I, H, K[C + 0], 11, -358537222); H = md5_hh(H, G, J, I, K[C + 3], 16, -722521979); I = md5_hh(I, H, G, J, K[C + 6], 23, 76029189); J = md5_hh(J, I, H, G, K[C + 9], 4, -640364487); G = md5_hh(G, J, I, H, K[C + 12], 11, -421815835); H = md5_hh(H, G, J, I, K[C + 15], 16, 530742520); I = md5_hh(I, H, G, J, K[C + 2], 23, -995338651); J = md5_ii(J, I, H, G, K[C + 0], 6, -198630844); G = md5_ii(G, J, I, H, K[C + 7], 10, 1126891415); H = md5_ii(H, G, J, I, K[C + 14], 15, -1416354905); I = md5_ii(I, H, G, J, K[C + 5], 21, -57434055); J = md5_ii(J, I, H, G, K[C + 12], 6, 1700485571); G = md5_ii(G, J, I, H, K[C + 3], 10, -1894986606); H = md5_ii(H, G, J, I, K[C + 10], 15, -1051523); I = md5_ii(I, H, G, J, K[C + 1], 21, -2054922799); J = md5_ii(J, I, H, G, K[C + 8], 6, 1873313359); G = md5_ii(G, J, I, H, K[C + 15], 10, -30611744); H = md5_ii(H, G, J, I, K[C + 6], 15, -1560198380); I = md5_ii(I, H, G, J, K[C + 13], 21, 1309151649); J = md5_ii(J, I, H, G, K[C + 4], 6, -145523070); G = md5_ii(G, J, I, H, K[C + 11], 10, -1120210379); H = md5_ii(H, G, J, I, K[C + 2], 15, 718787259); I = md5_ii(I, H, G, J, K[C + 9], 21, -343485551); J = safe_add(J, E); I = safe_add(I, D); H = safe_add(H, B); G = safe_add(G, A)
            } if (mode == 16) { return Array(I, H) } else { return Array(J, I, H, G) }
        }
        function md5_cmn(F, C, B, A, E, D) { return safe_add(bit_rol(safe_add(safe_add(C, F), safe_add(A, D)), E), B) }
        function md5_ff(C, B, G, F, A, E, D) { return md5_cmn((B & G) | ((~B) & F), C, B, A, E, D) }
        function md5_gg(C, B, G, F, A, E, D) { return md5_cmn((B & F) | (G & (~F)), C, B, A, E, D) }
        function md5_hh(C, B, G, F, A, E, D) { return md5_cmn(B ^ G ^ F, C, B, A, E, D) }
        function md5_ii(C, B, G, F, A, E, D) { return md5_cmn(G ^ (B | (~F)), C, B, A, E, D) }
        function safe_add(A, D) { var C = (A & 65535) + (D & 65535); var B = (A >> 16) + (D >> 16) + (C >> 16); return (B << 16) | (C & 65535) }
        function bit_rol(A, B) { return (A << B) | (A >>> (32 - B)) }
        function str2binl(D) {
            var C = Array(); var A = (1 << chrsz) - 1;
            for (var B = 0; B < D.length * chrsz; B += chrsz) { C[B >> 5] |= (D.charCodeAt(B / chrsz) & A) << (B % 32) } return C
        }
        function binl2str(C) { var D = """"; var A = (1 << chrsz) - 1; for (var B = 0; B < C.length * 32; B += chrsz) { D += String.fromCharCode((C[B >> 5] >>> (B % 32)) & A) } return D }
        function binl2hex(C) {
            var B = hexcase ? ""0123456789ABCDEF"" : ""0123456789abcdef""; var D = """";
            for (var A = 0; A < C.length * 4; A++) { D += B.charAt((C[A >> 2] >> ((A % 4) * 8 + 4)) & 15) + B.charAt((C[A >> 2] >> ((A % 4) * 8)) & 15) } return D
        } ";

    }
}
