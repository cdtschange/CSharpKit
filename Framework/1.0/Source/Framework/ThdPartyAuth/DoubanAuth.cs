using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;

namespace Cdts.Framework.ThdPartyAuth
{
    public class DoubanAuth : OAuthThdPartyAuth
    {
        private static string consumerKey = ConfigurationManager.AppSettings["DoubanAppKey"];
        private static string consumerSecret = ConfigurationManager.AppSettings["DoubanAppSecret"];
        private static string requestToken = ConfigurationManager.AppSettings["DoubanRequestToken"];
        private static string authorize = ConfigurationManager.AppSettings["DoubanAuthorize"];
        private static string accessToken = ConfigurationManager.AppSettings["DoubanAccessToken"];
        private static string userInfoUrl = ConfigurationManager.AppSettings["DoubanUserInfoUrl"];
        public DoubanAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager, string callbackUrl)
            : this(userManager, thdPartAuthManager, callbackUrl, "")
        {
        }
        public DoubanAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager, string callbackUrl, string tokenSecret)
            : base(userManager, thdPartAuthManager, callbackUrl, tokenSecret)
        {
            oAuth = new OAuthHelper(ConsumerKey, ConsumerSecret, callbackUrl, RequestToken, Authorize, AccessToken, GetExtensionData);
            this.tokenSecret = tokenSecret;

        }

        private void GetExtensionData(NameValueCollection qs)
        {
            if (qs["douban_user_id"] != null)
            {
                UserId = qs["douban_user_id"];
            }
        }

        public override string GetLoginUrl()
        {
            string url = oAuth.AuthorizationGet();
            TokenSecret = oAuth.TokenSecret;
            return url;
        }

        public override string ThdPartyName
        {
            get { return "douban.com"; }
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
            get { return userInfoUrl + UserId; }
        }
        public string UserId { get; set; }



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

                userInfo.Id = this.UserId;
                userInfo.Nick = xdocment.ChildNodes[1].ChildNodes[1].InnerText;
                userInfo.Name = xdocment.ChildNodes[1].ChildNodes[7].InnerText;
                userInfo.ThdPartyAuthName = this.ThdPartyName;
            }
            return userInfo;
        }
    }
}
