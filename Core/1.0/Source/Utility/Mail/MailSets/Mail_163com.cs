using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Cdts.Web;
using Cdts.Utility.WinForm;
using System.Windows.Forms;
using System.Net;

namespace Cdts.Utility.Mail.MailSets
{
    public class Mail_163com : MailBase
    {
        public const string login_ssl = @"https://ssl.mail.163.com/entry/coremail/fcg/ntesdoor2?lightweight=1&funcid=loginone&language=-1&passtype=1&verifycookie=1&iframe=1&from=web&product=mail163&style=-1&race=3915_358_880&net=t&df=index";
        public const string login_ssl_data = @"verifycookie=1&style=-1&product=mail163&savelogin=0&url2=http%3A%2F%2Fmail.163.com%2Ferrorpage%2Ferr_163.htm&username={0}&password={1}&selType=-1&secure=on";

        public const string password_url = @"http://reg.163.com/setinfo/ChangePasswd_1.jsp?noheader=1&username={0}@163.com&sid={1}&uid={0}@163.com&host={2}.mail.163.com&ver=js35&style=21&skin=163blue&color=005590";
        public const string password_url_submit = @"https://reg.163.com/setinfo/ChangePasswd_2.jsp";
        public const string password_url_data = @"noheader=1&username={0}&uname2={5}&cos=&oldpass={1}&password={2}&confirmPassword={2}&usercheckcode={3}&syscheckcode={4}&submit=%E4%B8%8B%E4%B8%80%E6%AD%A5";

        public const string password_checkcode_url = @"http://reg.163.com/services/getid";
        public const string password_checkcode_img_url = @"https://reg.163.com/services/getimg?id={0}";


        public const string openPOP3_url_submit = @"http://config.mail.163.com/pop3setting/aj.jsp?rnd=0.18457492738820735";
        public const string openPOP3_url_data = @"uid={0}@163.com&sid={1}&action=set&pop3config=on&imapconfig=on&smtpconfig=on";

        public const string autoforward_url_submit = @"http://config.mail.163.com/autofw/fwto.do?sid={0}&forwarddes={1}&keeplocal=0&callback=MM.autofwd.valCallback";
        public const string cancelforward_url = @"http://{0}.mail.163.com/js4/s?sid={1}&func=user:getAttrs&requestfunc=global:sequential&requesttime=239";
        public const string cancelforward_url_data = @"<?xml version=""1.0""?><object><array name=""attrIds""><string>forwarddes</string><string>forwardactive</string><string>keeplocal</string></array></object>";
        public const string cancelforward_url_submit = @"http://{0}.mail.163.com/js4/s?sid={1}&func=user:setAttrs&requestfunc=user:getAttrs&requesttime=176";
        public const string cancelforward_url__submit_data = @"<?xml version=""1.0""?><object><object name=""attrs""><string name=""forwarddes"">{0}</string><string name=""keeplocal"">1</string><int name=""forwardactive"">0</int></object></object>";

        public const string active_match_Subject = @"请验证自动转发的邮箱地址";
        public const string active_match_url = @"天内点击以下地址完成验证(?s).*?<A\s*href=""[^>]*>(?<url>.*?)</A>";

        public string sid = "";
        public string sub_domain = "";


        public const string check_reg_account_url = @"http://reg.email.163.com/mailregAll/checkuname.do?uname={0}";
        public const string reg_forward_url = @"http://reg.email.163.com/mailregAll/reg0.jsp";
        public const string reg_checkcode_url = @"http://reg.email.163.com/mailregAll/regv2/verifyCodeImg.jsp?t={0}";
        public const string reg_account_url_submit = @"http://reg.email.163.com/mailregAll/createmail2.do";
        public const string reg_account_url_submit_data = @"uname={0}&password={1}&passwordconf={1}&mobile=&verifycode={2}&domain=163.com&version=regvf1";
        public const string check_reg_test_url_1 = @"http://analytics.163.com/ntes?_nacc=163mail&_nvid={0}&_nvtm=0&_nvsf=0&_nvfi=1&_nlag=zh-cn&_nlmf={1}&_nres=1440x900&_nscd=32-bit&_nstm=0&_nurl=http%3A//reg.email.163.com/mailregAll/reg0.jsp&_ntit=%u6CE8%u518C%u7F51%u6613%u514D%u8D39%u90AE%u7BB1----%u4E2D%u56FD%u7B2C%u4E00%u5927%u7535%u5B50%u90AE%u4EF6%u670D%u52A1%u5546&_nref=&_nfla=10.0&_nssn=&_nxkey={2}{3}&_end1";

        public Mail_163com(MailModel model)
            : base(model)
        {

        }

        public override bool Login()
        {
            string data = string.Empty;
            string html = string.Empty;
            string url = string.Empty;
            try
            {
                data = string.Format(login_ssl_data, model.Account, model.Password);
                html = httpClient.SendPost(login_ssl, Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref cookie, out ErrorMessage);
                Match m = Regex.Match(html, @"href\s*=\s*""(?<url>.*?)""");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }
                url = m.Groups["url"].Value;
                this.sid = url.Substring(url.IndexOf("sid=") + 4);
                this.sub_domain = url.Substring(url.IndexOf("//") + 2, url.IndexOf(".") - url.IndexOf("//") - 2);
                html = httpClient.SendGet(url, Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"<iframe src=""(?<url>.*?)""");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }
                url = url.Substring(0, url.LastIndexOf('/') + 1) + m.Groups["url"].Value;
                html = httpClient.SendGet(url, Encoding.UTF8, ref cookie, out ErrorMessage);
                if (!Regex.Match(html, @"<title>极速4</title>").Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
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

        protected bool GetRegisterCheckCodeImage(out byte[] imgPic, out string syscheckcode)
        {
            imgPic = null;
            syscheckcode = null;
            try
            {
                imgPic = httpClient.SendGetStream(string.Format(reg_checkcode_url, Common.GetServerTime(13)), null, Encoding.UTF8, ref cookie, out ErrorMessage);
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
                syscheckcode = httpClient.SendGet(password_checkcode_url, Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);
                if (syscheckcode.Length != 40)
                {
                    ErrorMessage = string.Format("账号：{0}获取系统验证字符串失败！", model.FullAccount);
                    return false;
                }

                imgPic = httpClient.SendGetStream(string.Format(password_checkcode_img_url, syscheckcode), null, Encoding.UTF8, ref cookie, out ErrorMessage);
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

        public override bool RegistAccount()
        {
            string html = string.Empty;
            Match m = null;
            string data = null;
            string verifycode = null;
            try
            {
                html = httpClient.SendGet(reg_forward_url, Encoding.UTF8, ref cookie, out ErrorMessage);
                if (cookie.Count == 0)
                {
                    ErrorMessage = string.Format("账号：{0}获取Cookie失败！", model.FullAccount);
                    return false;
                }

                //////////////////////////////////////////////////////////////////////////
                string servertime = Common.GetServerTime(13);
                string randoms = "0.1773079660458608";
                string local = reg_forward_url;
                string referrer = reg_forward_url;
                string screenwidth = "1440";
                string screenheight = "900";
                string navigatoruserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0)";
                string documentcookie = "";
                string xy = "1440:717";

                string s = Encrypt_str_to_ent(servertime + randoms + local + referrer + screenwidth + screenheight + navigatoruserAgent + documentcookie + xy);
                s = Encrypt_ntes_hex_md5(s);
                cookie.SetCookies(new Uri(string.Format("http://reg.email.163.com")), string.Format("_ntes_nnid={0}", s + HttpUtility.UrlEncode(",") + "0"));

                string url = string.Format(check_reg_test_url_1, s, servertime.Substring(0, 10), servertime.Substring(6, 7), "0.30004");
                html = httpClient.SendGet(url, Encoding.UTF8, ref cookie, out ErrorMessage);
                //////////////////////////////////////////////////////////////////////////
                cookie.SetCookies(new Uri(string.Format("http://reg.email.163.com")), string.Format("__ntes__test__cookies={0}", Common.GetServerTime(13)));

                //////////////////////////////////////////////////////////////////////////

                AuthCodeForm codeForm = new AuthCodeForm(GetRegisterCheckCodeImage);
                if (DialogResult.OK != codeForm.ShowDialog())
                {
                    ErrorMessage = string.Format("账号：{0}注册失败，错误原因：窗口点击错误！", model.FullAccount);
                    return false;
                }
                verifycode = System.Web.HttpUtility.UrlEncode(codeForm.GetCheckCode()).ToUpper();
                data = string.Format(reg_account_url_submit_data, model.Account, model.Password, verifycode);

                CookieContainer mycc = new CookieContainer();
                List<Cookie> ccList = Common.GetAllCookies(cookie);
                foreach (Cookie ck in ccList)
                {
                    mycc.SetCookies(new Uri(string.Format("http://reg.email.163.com")), string.Format("{0}={1}", ck.Name, ck.Value));
                }

                html = httpClient.SendPost(reg_account_url_submit, Encoding.UTF8.GetBytes(data), Encoding.UTF8, reg_forward_url, ref mycc, out ErrorMessage);
                cookie = mycc;
                m = Regex.Match(html, @"true");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}注册失败，Fail/Greylist错误原因：{1}！", model.FullAccount, html);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}注册失败，错误原因：{1}！", model.FullAccount, e.Message);
                return false;
            }
        }

        public override bool CheckRegistAccount()
        {
            string html = string.Empty;
            Match m = null;
            try
            {
                html = httpClient.SendGet(string.Format(check_reg_account_url, model.Account), Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"mail163:(?<account>[^,]*),");
                if (m.Success)
                {
                    if (m.Groups["account"].Value == "true")
                    {
                        return true;
                    }
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

        public override bool ChangePassword(string newpassword)
        {
            if (!isLogin)
            {
                if (!this.Login())
                {
                    return false;
                }
            }
            string html = string.Empty;
            try
            {
                html = httpClient.SendGet(string.Format(password_url, model.Account, this.sid, this.sub_domain), Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);
                Match m = Regex.Match(html, @"uname2 value=""(?<uname2>.*?)""");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }

                string uname2 = m.Groups["uname2"].Value;

                AuthCodeForm codeForm = new AuthCodeForm(GetChangePasswordCheckCodeImage);
                if (DialogResult.OK != codeForm.ShowDialog())
                {
                    ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：窗口点击错误！", model.FullAccount);
                    return false;
                }

                string usercheckcode = codeForm.GetCheckCode();
                string syscheckcode = codeForm.GetSysCode();

                string data = string.Format(password_url_data, model.Account, model.Password, newpassword, usercheckcode, syscheckcode, uname2);
                html = httpClient.SendPost(password_url_submit, Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref cookie, out ErrorMessage);
                if (!Regex.Match(html, @"success").Success)
                {
                    ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：解析匹配失败！{1}", model.FullAccount, ErrorMessage);
                    return false;
                }
                return true;
            }
            catch (System.Exception e)
            {
                ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：{1}", model.FullAccount, e.Message);
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
            data = string.Format(openPOP3_url_data, model.Account, sid);
            html = httpClient.SendPost(openPOP3_url_submit, Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref cookie, out ErrorMessage);
            if (!html.Equals("success"))
            {
                ErrorMessage = string.Format("账号：{0}开启POP3功能失败！", model.FullAccount);
                return false;
            }
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

            cookie.SetCookies(new Uri(string.Format("http://config.mail.163.com", sub_domain)), string.Format("Coremail={0}%{1}%{2}.mail.163.com", Common.GetServerTime(13), sid, sub_domain.Substring(1)));
            cookie.SetCookies(new Uri(string.Format("http://config.mail.163.com", sub_domain)), string.Format("mail_uid={0}@163.com", model.Account));

            html = httpClient.SendGet(string.Format(autoforward_url_submit, sid, email), Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);
            if (html.IndexOf("MM.autofwd.valCallback(1)") == -1)
            {
                ErrorMessage = string.Format("账号：{0}自动转发设置失败！", model.FullAccount);
                return false;
            }
            return true;
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
                data = cancelforward_url_data;
                html = httpClient.SendPost(string.Format(cancelforward_url, sub_domain, sid), Encoding.UTF8.GetBytes(data), Encoding.UTF8, "", true, "application/xml", ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"forwardactive"">(?<IsForward>\d+)<(?s).*?forwarddes"">(?<email>.*?)<");
                if (!m.Success || m.Groups["IsForward"].Value == "0")
                {
                    return true;
                }

                data = string.Format(cancelforward_url__submit_data, m.Groups["email"].Value);
                html = httpClient.SendPost(string.Format(cancelforward_url_submit, sub_domain, sid), Encoding.UTF8.GetBytes(data), Encoding.UTF8, "", true, "application/xml", ref cookie, out ErrorMessage);
                if (html.IndexOf("S_OK") == -1)
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
                    if (Regex.Match(html, @"验证成功！").Success)
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

        private string Encrypt_str_to_ent(string pwd)
        {
            string result = null;
            ScriptEngine scriptEngine = new ScriptEngine(ScriptLanguage.JavaScript);
            scriptEngine.Reset();
            scriptEngine.AllowUI = false;
            scriptEngine.Timeout = 10000;
            string cmd = string.Format(@"str_to_ent(""{0}"");", pwd);
            result = (string)scriptEngine.Eval(cmd, this.js_163_reg);
            return result;
        }

        private string Encrypt_ntes_hex_md5(string pwd)
        {
            string result = null;
            ScriptEngine scriptEngine = new ScriptEngine(ScriptLanguage.JavaScript);
            scriptEngine.Reset();
            scriptEngine.AllowUI = false;
            scriptEngine.Timeout = 10000;
            string cmd = string.Format(@"ntes_hex_md5(""{0}"");", pwd);
            result = (string)scriptEngine.Eval(cmd, this.js_163_reg);
            return result;
        }

        #region 加密JS

        private readonly string js_163_reg = @"var _ntes_hexcase = 0, _ntes_chrsz = 8;
function ntes_hex_md5(s) {
    return binl2hex(ntes_core_md5(str2binl(s), s.length * _ntes_chrsz))
}
function ntes_core_md5(x, len) {
    x[len >> 5] |= 0x80 << ((len) % 32);
    x[(((len + 64) >>> 9) << 4) + 14] = len;
    var a = 1732584193, b = -271733879, c = -1732584194, d = 271733878;
    for (var i = 0; i < x.length; i += 16) {
        var olda = a, oldb = b, oldc = c, oldd = d;
        a = md5_ff(a, b, c, d, x[i + 0], 7, -680876936);
        d = md5_ff(d, a, b, c, x[i + 1], 12, -389564586);
        c = md5_ff(c, d, a, b, x[i + 2], 17, 606105819);
        b = md5_ff(b, c, d, a, x[i + 3], 22, -1044525330);
        a = md5_ff(a, b, c, d, x[i + 4], 7, -176418897);
        d = md5_ff(d, a, b, c, x[i + 5], 12, 1200080426);
        c = md5_ff(c, d, a, b, x[i + 6], 17, -1473231341);
        b = md5_ff(b, c, d, a, x[i + 7], 22, -45705983);
        a = md5_ff(a, b, c, d, x[i + 8], 7, 1770035416);
        d = md5_ff(d, a, b, c, x[i + 9], 12, -1958414417);
        c = md5_ff(c, d, a, b, x[i + 10], 17, -42063);
        b = md5_ff(b, c, d, a, x[i + 11], 22, -1990404162);
        a = md5_ff(a, b, c, d, x[i + 12], 7, 1804603682);
        d = md5_ff(d, a, b, c, x[i + 13], 12, -40341101);
        c = md5_ff(c, d, a, b, x[i + 14], 17, -1502002290);
        b = md5_ff(b, c, d, a, x[i + 15], 22, 1236535329);
        a = md5_gg(a, b, c, d, x[i + 1], 5, -165796510);
        d = md5_gg(d, a, b, c, x[i + 6], 9, -1069501632);
        c = md5_gg(c, d, a, b, x[i + 11], 14, 643717713);
        b = md5_gg(b, c, d, a, x[i + 0], 20, -373897302);
        a = md5_gg(a, b, c, d, x[i + 5], 5, -701558691);
        d = md5_gg(d, a, b, c, x[i + 10], 9, 38016083);
        c = md5_gg(c, d, a, b, x[i + 15], 14, -660478335);
        b = md5_gg(b, c, d, a, x[i + 4], 20, -405537848);
        a = md5_gg(a, b, c, d, x[i + 9], 5, 568446438);
        d = md5_gg(d, a, b, c, x[i + 14], 9, -1019803690);
        c = md5_gg(c, d, a, b, x[i + 3], 14, -187363961);
        b = md5_gg(b, c, d, a, x[i + 8], 20, 1163531501);
        a = md5_gg(a, b, c, d, x[i + 13], 5, -1444681467);
        d = md5_gg(d, a, b, c, x[i + 2], 9, -51403784);
        c = md5_gg(c, d, a, b, x[i + 7], 14, 1735328473);
        b = md5_gg(b, c, d, a, x[i + 12], 20, -1926607734);
        a = md5_hh(a, b, c, d, x[i + 5], 4, -378558);
        d = md5_hh(d, a, b, c, x[i + 8], 11, -2022574463);
        c = md5_hh(c, d, a, b, x[i + 11], 16, 1839030562);
        b = md5_hh(b, c, d, a, x[i + 14], 23, -35309556);
        a = md5_hh(a, b, c, d, x[i + 1], 4, -1530992060);
        d = md5_hh(d, a, b, c, x[i + 4], 11, 1272893353);
        c = md5_hh(c, d, a, b, x[i + 7], 16, -155497632);
        b = md5_hh(b, c, d, a, x[i + 10], 23, -1094730640);
        a = md5_hh(a, b, c, d, x[i + 13], 4, 681279174);
        d = md5_hh(d, a, b, c, x[i + 0], 11, -358537222);
        c = md5_hh(c, d, a, b, x[i + 3], 16, -722521979);
        b = md5_hh(b, c, d, a, x[i + 6], 23, 76029189);
        a = md5_hh(a, b, c, d, x[i + 9], 4, -640364487);
        d = md5_hh(d, a, b, c, x[i + 12], 11, -421815835);
        c = md5_hh(c, d, a, b, x[i + 15], 16, 530742520);
        b = md5_hh(b, c, d, a, x[i + 2], 23, -995338651);
        a = md5_ii(a, b, c, d, x[i + 0], 6, -198630844);
        d = md5_ii(d, a, b, c, x[i + 7], 10, 1126891415);
        c = md5_ii(c, d, a, b, x[i + 14], 15, -1416354905);
        b = md5_ii(b, c, d, a, x[i + 5], 21, -57434055);
        a = md5_ii(a, b, c, d, x[i + 12], 6, 1700485571);
        d = md5_ii(d, a, b, c, x[i + 3], 10, -1894986606);
        c = md5_ii(c, d, a, b, x[i + 10], 15, -1051523);
        b = md5_ii(b, c, d, a, x[i + 1], 21, -2054922799);
        a = md5_ii(a, b, c, d, x[i + 8], 6, 1873313359);
        d = md5_ii(d, a, b, c, x[i + 15], 10, -30611744);
        c = md5_ii(c, d, a, b, x[i + 6], 15, -1560198380);
        b = md5_ii(b, c, d, a, x[i + 13], 21, 1309151649);
        a = md5_ii(a, b, c, d, x[i + 4], 6, -145523070);
        d = md5_ii(d, a, b, c, x[i + 11], 10, -1120210379);
        c = md5_ii(c, d, a, b, x[i + 2], 15, 718787259);
        b = md5_ii(b, c, d, a, x[i + 9], 21, -343485551);
        a = safe_add(a, olda);
        b = safe_add(b, oldb);
        c = safe_add(c, oldc);
        d = safe_add(d, oldd)
    }
    return Array(a, b, c, d)
}
function md5_cmn(q, a, b, x, s, t) {
    return safe_add(bit_rol(safe_add(safe_add(a, q), safe_add(x, t)), s), b)
}
function md5_ff(a, b, c, d, x, s, t) {
    return md5_cmn((b & c) | ((~b) & d), a, b, x, s, t)
}
function md5_gg(a, b, c, d, x, s, t) {
    return md5_cmn((b & d) | (c & (~d)), a, b, x, s, t)
}
function md5_hh(a, b, c, d, x, s, t) {
    return md5_cmn(b ^ c ^ d, a, b, x, s, t)
}
function md5_ii(a, b, c, d, x, s, t) {
    return md5_cmn(c ^ (b | (~d)), a, b, x, s, t)
}
function safe_add(x, y) {
    var lsw = (x & 0xFFFF) + (y & 0xFFFF), msw = (x >> 16) + (y >> 16) + (lsw >> 16);
    return (msw << 16) | (lsw & 0xFFFF)
}
function bit_rol(num, cnt) {
    return (num << cnt) | (num >>> (32 - cnt))
}
function str2binl(str) {
    var bin = new Array(), mask = (1 << _ntes_chrsz) - 1;
    for (var i = 0; i < str.length * _ntes_chrsz; i += _ntes_chrsz) bin[i >> 5] |= (str.charCodeAt(i / _ntes_chrsz) & mask) << (i % 32);
    return bin
}
function binl2hex(binarray) {
    var hex_tab = _ntes_hexcase ? ""0123456789ABCDEF"" : ""0123456789abcdef"", str = """";
    for (var i = 0; i < binarray.length * 4; i++) {
        str += hex_tab.charAt((binarray[i >> 2] >> ((i % 4) * 8 + 4)) & 0xF) + hex_tab.charAt((binarray[i >> 2] >> ((i % 4) * 8)) & 0xF)
    }
    return str
}

function str_to_ent(str) {
    var result = '', i;
    for (i = 0; i < str.length; i++) {
        var c = str.charCodeAt(i), tmp = '';
        if (c > 255) {
            while (c >= 1) {
                tmp = ""0123456789"".charAt(c % 10) + tmp;
                c = c / 10
            }
            if (tmp == '') {
                tmp = ""0""
            }
            tmp = ""#"" + tmp;
            tmp = ""&"" + tmp;
            tmp = tmp + "";"";
            result += tmp
        }
        else {
            result += str.charAt(i)
        }
    }
    return result
}";
        #endregion
    }
}
