using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using WindowsLive;

namespace Cdts.Framework.ThdPartyAuth
{
    public class LiveIdAuth : AbstractThdPartyAuth
    {
        public LiveIdAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager)
            : base(userManager, thdPartAuthManager)
        {
        }

        private static string consumerKey = ConfigurationManager.AppSettings["wll_appid"];
        private static string consumerSecret = ConfigurationManager.AppSettings["wll_secret"];
        private static string loginUrl = ConfigurationManager.AppSettings["wll_loginUrl"];
        private static string securityalgorithm = ConfigurationManager.AppSettings["wll_securityalgorithm"];


        protected string ConsumerKey
        {
            get { return consumerKey; }
        }

        protected string ConsumerSecret
        {
            get { return consumerSecret; }
        }

        protected string LoginUrl
        {
            get { return loginUrl; }
        }

        protected string Securityalgorithm
        {
            get { return securityalgorithm; }
        }

        public override string ThdPartyName
        {
            get { return "live.com"; }
        }

        public override string GetLoginUrl()
        {
            return string.Format(LoginUrl, ConsumerKey, Securityalgorithm);
        }



        public override ThdPartyUserInfo GetThdPartyUserInfo(System.Collections.Specialized.NameValueCollection queryString)
        {
            ThdPartyUserInfo userInfo = null;
            WindowsLiveLogin wll = new WindowsLiveLogin(true);
            WindowsLiveLogin.User user = wll.ProcessLogin(queryString);
            if (user != null)
            {
                userInfo = new ThdPartyUserInfo();
                userInfo.Id = user.Id;
                userInfo.ThdPartyAuthName = this.ThdPartyName;
            }
            return userInfo;
        }
    }
}
