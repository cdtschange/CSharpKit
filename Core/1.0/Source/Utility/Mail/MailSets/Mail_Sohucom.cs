using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Web;
using System.Text.RegularExpressions;
using Cdts.Utility.WinForm;
using System.Windows.Forms;
using System.Web;

namespace Cdts.Utility.Mail.MailSets
{
    public class Mail_Sohucom : MailBase
    {
        public const string login_ssl = @"https://passport.sohu.com/sso/login.jsp?userid={0}%40sohu.com&password={1}&appid=1000&persistentcookie=0&s={2}&b=6&w=1440&pwdtype=1&v=26";

        public const string password_checkcode_img_url = @"http://passport.sohu.com/servlet/Captcha?time=0.3596601143191239";
        public const string password_url_submit = @"http://passport.sohu.com/web/ModifyPwdAccessAction.action";
        public const string password_url_data = @"oriPassword={0}&password={1}&passwordConfirm={1}&validateStr={2}&appid=9998&ru=&submit=%C8%B7%B6%A8%D0%DE%B8%C4%C3%DC%C2%EB";

        public const string autoforward_url_submit = @"http://mail.sohu.com/{0}/{1}/profile";
        public const string autoforward_url_data = @"autoreplyenable=false&startyear=2011&startmonth=7&startdate=11&endyear=2011&endmonth=10&enddate=9&AutoReply=&forwardenable=true&forward={0}&reserveforward=false&_method=put";
        public const string cancelforward_url_data = @"autoreplyenable=false&startyear=2011&startmonth=7&startdate=11&endyear=2011&endmonth=10&enddate=9&AutoReply=&forwardenable=false&forward=&reserveforward=false&_method=put";


        public const string reg_forward_url = @"https://passport.sohu.com/web/dispatchAction.action?appid=1000&ru=http://mail.sohu.com/reg/signup_success.jsp";
        public const string check_reg_account_url = @"https://passport.sohu.com/jsonajax/checkusername.action?shortname={0}&domain=sohu.com&appid=1000&_t={1}&mobileReg=false";
        public const string reg_account_url_submit = @"https://passport.sohu.com/web/Passportregister.action";
        public const string reg_account_url_submit_data = @"shortname={0}&user.password={1}&password2={1}&user.question=%CE%D2%BE%CD%B6%C1%B5%C4%D3%D7%B6%F9%D4%B0%C3%FB%B3%C6&questionbak=&answer=abcde&validate={2}&privacy=1&uuidCode={3}&app_para=appid%3D1000&ot_registerid=&registerid=&appid=1000&autologin=1&ru=http%3A%2F%2Fmail.sohu.com%2Freg%2Fsignup_success.jsp&domainName=sohu.com&registerType=Passport&showAllType=0";

        public string sub_1_domain = string.Empty;
        public string sub_2_domain = string.Empty;
        public Mail_Sohucom(MailModel model)
            : base(model)
        {

        }

        public override bool Login()
        {
            string html = null;
            string url = null;
            Match m = null;
            try
            {
                html = httpClient.SendGet(string.Format(login_ssl, model.Account, EncryptPwd(model.Password), Common.GetServerTime(13)), Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, "success");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }

                html = httpClient.SendGet("http://mail.sohu.com/", null, Encoding.UTF8, false, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"<a\s*href=""(?<url>.*?)"">here</a>");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }

                url = m.Groups["url"].Value;

                html = httpClient.SendGet(url, null, Encoding.UTF8, false, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"<a\s*href=""(?<url>.*?)"">here</a>");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }

                url = m.Groups["url"].Value;

                m = Regex.Match(url, "com/(?<sub_1_domain>.*?)/(?<sub_2_domain>.*?)/main");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}登录失败，错误原因：解析匹配失败！", model.FullAccount);
                    return false;
                }

                this.sub_1_domain = m.Groups["sub_1_domain"].Value;
                this.sub_2_domain = m.Groups["sub_2_domain"].Value;

                html = httpClient.SendGet(url, Encoding.Default, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"<span>搜狐邮箱欢迎您！</span>");
                if (!m.Success)
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

        protected bool GetRegisterOrChangePasswordCheckCodeImage(out byte[] imgPic, out string syscheckcode)
        {
            imgPic = null;
            syscheckcode = null;
            try
            {
                imgPic = httpClient.SendGetStream(password_checkcode_img_url, null, Encoding.UTF8, ref cookie, out ErrorMessage);
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
            string data = string.Empty;
            Match m = null;
            try
            {
                html = httpClient.SendGet(reg_forward_url, Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);

                AuthCodeForm codeForm = new AuthCodeForm(GetRegisterOrChangePasswordCheckCodeImage);
                if (DialogResult.OK != codeForm.ShowDialog())
                {
                    ErrorMessage = string.Format("账号：{0}注册失败，错误原因：窗口点击错误！", model.FullAccount);
                    return false;
                }

                string validate = codeForm.GetCheckCode();
                string uuidcode = @"SCHLSLAPUJPLYLALELSPJLEPCPUJYPUJYLEJSLEPCLAPJPULYLALEJYPUJYLUPNJYLYLSLEPNJPJSLAPJLJLAPUJYLSJUPCPJLEJYLALELSLYLAPCJYLSPLPJLYJSPLJYLSPLPNLYJSLUJYPJPCLAJSLEPCPJLALSLEJSPLPCPJLALSPCPJLALEPCPJLAPJLEJPJYLEJSPLPCPJLALS";
                data = string.Format(reg_account_url_submit_data, model.Account, model.Password, validate, uuidcode);
                html = httpClient.SendPost(reg_account_url_submit, Encoding.GetEncoding("GB2312").GetBytes(data), Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"恭喜您，已经成功注册搜狐通行证");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("注册账号：{0}信息失败", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("注册账号：{0}信息失败，错误原因：{1}", model.FullAccount, e.Message);
                return false;
            }
        }

        public override bool CheckRegistAccount()
        {
            string html = string.Empty;
            Match m = null;
            try
            {
                html = httpClient.SendGet(string.Format(check_reg_account_url, model.Account, Common.GetServerTime(13)), Encoding.UTF8, ref cookie, out ErrorMessage);
                m = Regex.Match(html, @"""status"":""(?<status>\d+)""");
                if (m.Success)
                {
                    if (m.Groups["status"].Value == "0")
                    {
                        return true;
                    }
                }
                ErrorMessage = string.Format("账号：{0}无法注册！", model.FullAccount);
                return false;
            }
            catch (Exception e)
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

                AuthCodeForm codeForm = new AuthCodeForm(GetRegisterOrChangePasswordCheckCodeImage);
                if (DialogResult.OK != codeForm.ShowDialog())
                {
                    ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：窗口点击错误！", model.FullAccount);
                    return false;
                }

                string usercheckcode = codeForm.GetCheckCode();
                data = string.Format(password_url_data, model.Password, newPassword, usercheckcode);
                html = httpClient.SendPost(password_url_submit, Encoding.GetEncoding("GB2312").GetBytes(data), Encoding.GetEncoding("GB2312"), ref cookie, out ErrorMessage);
                m = Regex.Match(html, "修改密码成功！");
                if (!m.Success)
                {
                    ErrorMessage = string.Format("账号：{0}修改密码失败，错误原因：解析匹配失败！", model.FullAccount);
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
            try
            {
                data = string.Format(autoforward_url_data, HttpUtility.UrlEncode(email));
                html = httpClient.SendPost(string.Format(autoforward_url_submit, sub_1_domain, sub_2_domain), Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref cookie, out ErrorMessage);
                if (html.IndexOf("true") == -1)
                {
                    ErrorMessage = string.Format("账号：{0}自动转发设置失败{1}！", model.FullAccount);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("账号：{0}自动转发设置失败{1}！", model.FullAccount, e.Message);
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
            try
            {
                data = cancelforward_url_data;
                html = httpClient.SendPost(string.Format(autoforward_url_submit, sub_1_domain, sub_2_domain), Encoding.UTF8.GetBytes(data), Encoding.UTF8, ref cookie, out ErrorMessage);
                if (html.IndexOf("true") == -1)
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
            if (!model.Carrier.IsNeedActivation)
                return true;
            return false;
        }



        /// <summary>
        /// 加密登录密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string EncryptPwd(string pwd)
        {
            string result = null;
            ScriptEngine scriptEngine = new ScriptEngine(ScriptLanguage.JavaScript);
            scriptEngine.Reset();
            scriptEngine.AllowUI = false;
            scriptEngine.Timeout = 10000;
            string cmd = string.Format(@"hex_md5(""{0}"");", pwd);
            result = (string)scriptEngine.Eval(cmd, this.SohuJS);
            return result;
        }

        private readonly string SohuJS = @"var hexcase=0;var chrsz=8;function hex_md5(a){return binl2hex(core_md5(str2binl(a),a.length*chrsz))}function core_md5(e,k){e[k>>5]|=128<<((k)%32);e[(((k+64)>>>9)<<4)+14]=k;var f=1732584193;var g=-271733879;var h=-1732584194;" +
   @"var j=271733878;for(var b=0;b<e.length;b+=16){var l=f;var a=g;var c=h;var d=j;f=md5_ff(f,g,h,j,e[b+0],7,-680876936);j=md5_ff(j,f,g,h,e[b+1],12,-389564586);h=md5_ff(h,j,f,g,e[b+2],17,606105819);g=md5_ff(g,h,j,f,e[b+3],22,-1044525330);" +
   @"f=md5_ff(f,g,h,j,e[b+4],7,-176418897);j=md5_ff(j,f,g,h,e[b+5],12,1200080426);h=md5_ff(h,j,f,g,e[b+6],17,-1473231341);g=md5_ff(g,h,j,f,e[b+7],22,-45705983);f=md5_ff(f,g,h,j,e[b+8],7,1770035416);j=md5_ff(j,f,g,h,e[b+9],12,-1958414417);" +
   @"h=md5_ff(h,j,f,g,e[b+10],17,-42063);g=md5_ff(g,h,j,f,e[b+11],22,-1990404162);f=md5_ff(f,g,h,j,e[b+12],7,1804603682);j=md5_ff(j,f,g,h,e[b+13],12,-40341101);h=md5_ff(h,j,f,g,e[b+14],17,-1502002290);" +
   @"g=md5_ff(g,h,j,f,e[b+15],22,1236535329);f=md5_gg(f,g,h,j,e[b+1],5,-165796510);j=md5_gg(j,f,g,h,e[b+6],9,-1069501632);h=md5_gg(h,j,f,g,e[b+11],14,643717713);g=md5_gg(g,h,j,f,e[b+0],20,-373897302);" +
   @"f=md5_gg(f,g,h,j,e[b+5],5,-701558691);j=md5_gg(j,f,g,h,e[b+10],9,38016083);h=md5_gg(h,j,f,g,e[b+15],14,-660478335);g=md5_gg(g,h,j,f,e[b+4],20,-405537848);f=md5_gg(f,g,h,j,e[b+9],5,568446438);" +
   @"j=md5_gg(j,f,g,h,e[b+14],9,-1019803690);h=md5_gg(h,j,f,g,e[b+3],14,-187363961);g=md5_gg(g,h,j,f,e[b+8],20,1163531501);f=md5_gg(f,g,h,j,e[b+13],5,-1444681467);j=md5_gg(j,f,g,h,e[b+2],9,-51403784);h=md5_gg(h,j,f,g,e[b+7],14,1735328473);" +
   @"g=md5_gg(g,h,j,f,e[b+12],20,-1926607734);f=md5_hh(f,g,h,j,e[b+5],4,-378558);j=md5_hh(j,f,g,h,e[b+8],11,-2022574463);h=md5_hh(h,j,f,g,e[b+11],16,1839030562);g=md5_hh(g,h,j,f,e[b+14],23,-35309556);f=md5_hh(f,g,h,j,e[b+1],4,-1530992060);" +
   @"j=md5_hh(j,f,g,h,e[b+4],11,1272893353);h=md5_hh(h,j,f,g,e[b+7],16,-155497632);g=md5_hh(g,h,j,f,e[b+10],23,-1094730640);f=md5_hh(f,g,h,j,e[b+13],4,681279174);j=md5_hh(j,f,g,h,e[b+0],11,-358537222);h=md5_hh(h,j,f,g,e[b+3],16,-722521979);" +
   @"g=md5_hh(g,h,j,f,e[b+6],23,76029189);f=md5_hh(f,g,h,j,e[b+9],4,-640364487);j=md5_hh(j,f,g,h,e[b+12],11,-421815835);h=md5_hh(h,j,f,g,e[b+15],16,530742520);g=md5_hh(g,h,j,f,e[b+2],23,-995338651);f=md5_ii(f,g,h,j,e[b+0],6,-198630844);j=md5_ii(j,f,g,h,e[b+7],10,1126891415);" +
   @"h=md5_ii(h,j,f,g,e[b+14],15,-1416354905);g=md5_ii(g,h,j,f,e[b+5],21,-57434055);f=md5_ii(f,g,h,j,e[b+12],6,1700485571);j=md5_ii(j,f,g,h,e[b+3],10,-1894986606);h=md5_ii(h,j,f,g,e[b+10],15,-1051523);g=md5_ii(g,h,j,f,e[b+1],21,-2054922799);f=md5_ii(f,g,h,j,e[b+8],6,1873313359);j=md5_ii(j,f,g,h,e[b+15],10,-30611744);" +
   @"h=md5_ii(h,j,f,g,e[b+6],15,-1560198380);g=md5_ii(g,h,j,f,e[b+13],21,1309151649);f=md5_ii(f,g,h,j,e[b+4],6,-145523070);j=md5_ii(j,f,g,h,e[b+11],10,-1120210379);h=md5_ii(h,j,f,g,e[b+2],15,718787259);g=md5_ii(g,h,j,f,e[b+9],21,-343485551);f=safe_add(f,l);g=safe_add(g,a);h=safe_add(h,c);j=safe_add(j,d)}return Array(f,g,h,j)}" +
   @"function md5_cmn(b,e,f,a,c,d){return safe_add(bit_rol(safe_add(safe_add(e,b),safe_add(a,d)),c),f)}function md5_ff(f,g,b,c,a,d,e){return md5_cmn((g&b)|((~g)&c),f,g,a,d,e)}function md5_gg(f,g,b,c,a,d,e){return md5_cmn((g&c)|(b&(~c)),f,g,a,d,e)}function md5_hh(f,g,b,c,a,d,e){return md5_cmn(g^b^c,f,g,a,d,e)}function md5_ii(f,g,b,c,a,d,e){return md5_cmn(b^(g|(~c)),f,g,a,d,e)}" +
   @"function safe_add(a,b){var c=(a&65535)+(b&65535);var d=(a>>16)+(b>>16)+(c>>16);return(d<<16)|(c&65535)}function bit_rol(a,b){return(a<<b)|(a>>>(32-b))}function binl2hex(c){var d=hexcase?""0123456789ABCDEF"":""0123456789abcdef"";var b="""";for(var a=0;a<c.length*4;a++){b+=d.charAt((c[a>>2]>>((a%4)*8+4))&15)+d.charAt((c[a>>2]>>((a%4)*8))&15)}return b}function str2binl(b){var c=Array();" +
   @"var a=(1<<chrsz)-1;for(var d=0;d<b.length*chrsz;d+=chrsz){c[d>>5]|=(b.charCodeAt(d/chrsz)&a)<<(d%32)}return c}function b64_423(c){var d=new Array(""A"",""B"",""C"",""D"",""E"",""F"",""G"",""H"",""I"",""J"",""K"",""L"",""M"",""N"",""O"",""P"",""Q"",""R"",""S"",""T"",""U"",""V"",""W"",""X"",""Y"",""Z"",""a"",""b"",""c"",""d"",""e"",""f"",""g"",""h"",""i"",""j"",""k"",""l"",""m"",""n"",""o"",""p"",""q"",""r"",""s"",""t"",""u"",""v"",""w"",""x"",""y"",""z"",""0"",""1"",""2"",""3"",""4"",""5"",""6"",""7"",""8"",""9"",""-"",""_"");" +
   @"var b=new String();for(var e=0;e<c.length;e++){for(var a=0;a<64;a++){if(c.charAt(e)==d[a]){var f=a.toString(2);b+=(""000000""+f).substr(f.length);break}}if(a==64){if(e==2){return b.substr(0,8)}else{return b.substr(0,16)}}}return b}function b2i(b){var a=0;var d=128;for(var c=0;c<8;c++,d=d/2){if(b.charAt(c)==""1""){a+=d}}return String.fromCharCode(a)}function b64_decodex(b){var d=new Array();var c;var a="""";for(c=0;c<b.length;c+=4){a+=b64_423(b.substr(c,4))}" +
   @"for(c=0;c<a.length;c+=8){d+=b2i(a.substr(c,8))}return d}function utf8to16(f){var a,j,k,h,g,b,c,d,e;a=[];h=f.length;j=k=0;while(j<h){g=f.charCodeAt(j++);switch(g>>4){case 0:case 1:case 2:case 3:case 4:case 5:case 6:case 7:a[k++]=f.charAt(j-1);break;case 12:case 13:b=f.charCodeAt(j++);a[k++]=String.fromCharCode(((g&31)<<6)|(b&63));break;case 14:b=f.charCodeAt(j++);c=f.charCodeAt(j++);a[k++]=String.fromCharCode(((g&15)<<12)|((b&63)<<6)|(c&63));break;case 15:switch(g&15){case 0:case 1:case 2:case 3:case 4:case 5:case 6:case 7:b=f.charCodeAt(j++);c=f.charCodeAt(j++);d=f.charCodeAt(j++);e=((g&7)<<18)|((b&63)<<12)|((c&63)<<6)|(d&63)-65536;if(0<=e&&e<=1048575){a[k]=String.fromCharCode(((e>>>10)&1023)|55296,(e&1023)|56320)}else{a[k]=""?""}break;case 8:case 9:case 10:case 11:j+=4;a[k]=""?"";break;case 12:case 13:j+=5;a[k]=""?"";break}}k++}return a.join("""")}function getStringLen(b){var a=b.match(/[^\x00-\xff]/ig);return b.length+(a==null?0:a.length)}";


    }
}
