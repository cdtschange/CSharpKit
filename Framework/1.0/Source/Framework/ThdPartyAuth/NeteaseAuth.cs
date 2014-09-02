using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Cdts.Framework.ThdPartyAuth
{
    public class NeteaseAuth : OAuthThdPartyAuth
    {
        private static string consumerKey = ConfigurationManager.AppSettings["NeteaseAppKey"];
        private static string consumerSecret = ConfigurationManager.AppSettings["NeteaseAppSecret"];
        private static string requestToken = ConfigurationManager.AppSettings["NeteaseRequestToken"];
        private static string authorize = ConfigurationManager.AppSettings["NeteaseAuthorize"];
        private static string accessToken = ConfigurationManager.AppSettings["NeteaseAccessToken"];
        private static string userInfoUrl = ConfigurationManager.AppSettings["NeteaseUserInfoUrl"];
        public NeteaseAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager, string callbackUrl)
            : this(userManager, thdPartAuthManager, callbackUrl, "")
        {
        }
        public NeteaseAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager, string callbackUrl, string tokenSecret)
            : base(userManager, thdPartAuthManager, callbackUrl, tokenSecret)
        {
        }
        public override string GetLoginUrl()
        {
            string url = oAuth.AuthorizationGet();
            TokenSecret = oAuth.TokenSecret;
            return url;
        }

        public override string ThdPartyName
        {
            get { return "163.com"; }
        }

        protected override string ConsumerKey
        {
            get { return consumerKey; }
        }

        protected override string ConsumerSecret
        {
            get { return consumerSecret; }
        }

        protected override string RequestToken
        {
            get { return requestToken; }
        }

        protected override string Authorize
        {
            get { return authorize; }
        }

        protected override string AccessToken
        {
            get { return accessToken; }
        }
        protected override string UserInfoUrl
        {
            get { return userInfoUrl; }
        }
        public override ThdPartyUserInfo GetThdPartyUserInfo(System.Collections.Specialized.NameValueCollection queryString)
        {
            ThdPartyUserInfo userInfo = null;
            oAuth.Verifier = queryString["oauth_verifier"];
            oAuth.TokenSecret = tokenSecret;
            oAuth.AccessTokenGet(queryString["oauth_token"]);
            if (oAuth.Token.Length > 0)
            {
                string xml = oAuth.oAuthWebRequest(OAuthHelper.Method.GET, UserInfoUrl, String.Empty);
                XmlDocument xdocment = new XmlDocument();
                xdocment.LoadXml(xml);
                userInfo = new ThdPartyUserInfo();
                userInfo.Id = xdocment.ChildNodes[1].ChildNodes[0].InnerText;
                userInfo.Nick = xdocment.ChildNodes[1].ChildNodes[1].InnerText;
                userInfo.Name = xdocment.ChildNodes[1].ChildNodes[2].InnerText;
                //string province = xdocment.ChildNodes[1].ChildNodes[3].InnerText;
                //string city = xdocment.ChildNodes[1].ChildNodes[4].InnerText;
                //string location = xdocment.ChildNodes[1].ChildNodes[5].InnerText;
                //string description = xdocment.ChildNodes[1].ChildNodes[6].InnerText;
                userInfo.Url = xdocment.ChildNodes[1].ChildNodes[7].InnerText;
                //string profile_image_url = xdocment.ChildNodes[1].ChildNodes[8].InnerText;
                //string domain = xdocment.ChildNodes[1].ChildNodes[9].InnerText;
                //string gender = xdocment.ChildNodes[1].ChildNodes[10].InnerText;
                //Response.Cookies["name"].Value = name;
                //Response.Cookies["name"].Expires = DateTime.Now.AddHours(1);
                userInfo.ThdPartyAuthName = this.ThdPartyName;
            }
            return userInfo;
        }
    }
}
