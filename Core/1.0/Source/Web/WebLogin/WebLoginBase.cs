using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace Cdts.Web.WebLogin
{
    public class WebLoginBase
    {
        public string Name { get; set; }
        public HttpSendBase HttpSender { get; set; }
        private CookieContainer cookies;
        public CookieContainer Cookies
        {
            get { return cookies; }
            set { cookies = value; }
        }
        protected List<WebConfigElement> elements = new List<WebConfigElement>();
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public WebLoginBase(string name)
        {
            Name = name;
            foreach (WebConfigElement item in WebConfigSection.GetConfigSection().AllValues)
            {
                elements.Add(item);
            }
        }

        public virtual bool Login(string username, string password)
        {
            try
            {
                if (HttpSender == null)
                {
                    HttpSender = new HttpSendBase();
                }
                if (cookies == null)
                {
                    cookies = new CookieContainer();
                }
                WebConfigElement element = elements.FirstOrDefault(e => e.Name == Name);
                if (element == null)
                {
                    ErrorMessage = string.Format("Can not find the element of name:{0} in config." + Name);
                    return false;
                }
                string loginUrl = element.LoginUrl;
                string loginData = string.Format(element.LoginData, username, password);
                string loginReferer = element.LoginReferer;
                string loginSuccessRegex = element.LoginSuccessRegex;
                string loginErrorRegex = element.LoginErrorRegex;
                string html = HttpSender.SendPost(element.LoginUrl, Encoding.Default.GetBytes(loginData), Encoding.Default, loginReferer, ref cookies, out errorMessage);
                if (!string.IsNullOrEmpty(loginSuccessRegex))
                {
                    Match m = Regex.Match(html, loginSuccessRegex);
                    if (!m.Success)
                    {
                        ErrorMessage = string.Format("Login Failure：{0}，Detail：Username:{1} and Password:{2} incorrect！", Name, username, password);
                        return false;
                    }
                }
                else if (!string.IsNullOrEmpty(loginErrorRegex))
                {
                    Match m = Regex.Match(html, loginErrorRegex);
                    if (m.Success)
                    {
                        ErrorMessage = string.Format("Login Failure：{0}，Detail：Username:{1} and Password:{2} incorrect！", Name, username, password);
                        return false;
                    }
                }
                else
                {
                    ErrorMessage = string.Format("Login Failure：{0}，Detail：No matched regex config！", Name);

                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Login Failure：{0}. Detail：{1}！", Name, ex.Message);
                return false;
            }
        }
    }
}
