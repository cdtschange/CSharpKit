using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Framework.ThdPartyAuth;
using System.Web.Mvc;

namespace Cdts.Framework.Web.Mvc.ThdPartyAuth
{
    public class QQController : ThdPartyAuthController
    {

        public QQController(IUserManager userManager, IThirdPartyAuthenticationManager thdPartyAuthManager)
            : base(userManager, thdPartyAuthManager)
        {

        }

        /// <summary>
        /// 获取OAuth
        /// </summary>
        /// <param name="callbackUrl">回调Url</param>
        /// <param name="tokenSecret">密匙</param>
        /// <returns>返回OAuth</returns>
        public virtual QQAuth GetOAuth(string callbackUrl, string tokenSecret)
        {
            return new QQAuth(userManager, thdPartyAuthManager, callbackUrl, tokenSecret);
        }

        public override ActionResult Index()
        {
            if (GetQueryString("oauth_token") != null) // 登录成功
            {
                string tokenSecret = GetCookies("TokenSecret");
                QQAuth oAuth = GetOAuth("", tokenSecret);
                ThdPartyUserInfo userInfo = oAuth.GetThdPartyUserInfo(GetQueryString());
                if (userInfo != null)
                {
                    return BindUser(oAuth, userInfo);
                }
            }
            return RedirectToRoute("Default");
        }
        /// <summary>
        /// 登录
        /// </summary>
        public override ActionResult Login(string returnUrl)
        {
            base.Login(returnUrl);
            QQAuth oAuth = GetOAuth("http://www.cdts.com/ThdPartyAuth/Sina", "");
            string url = oAuth.GetLoginUrl();
            SetCookies("TokenSecret", oAuth.TokenSecret, DateTime.Now.AddHours(1));
            RedirectUrl(url);
            return null;
        }
    }
}
