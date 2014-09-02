using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Framework.ThdPartyAuth;
using System.Web.Mvc;

namespace Cdts.Framework.Web.Mvc.ThdPartyAuth
{
    public class Kaixin001Controller : ThdPartyAuthController
    {
        public Kaixin001Controller(IUserManager userManager, IThirdPartyAuthenticationManager thdPartyAuthManager)
            : base(userManager, thdPartyAuthManager)
        {
        }

        /// <summary>
        /// 获取OAuth
        /// </summary>
        /// <returns>返回OAuth</returns>
        public virtual Kaixin001Auth GetOAuth()
        {
            return new Kaixin001Auth(userManager, thdPartyAuthManager);
        }

        public override ActionResult Index()
        {
            if (GetQueryString("hash") != null) // 登录成功
            {
                Kaixin001Auth oAuth = GetOAuth();
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
            Kaixin001Auth oAuth = GetOAuth();
            string url = oAuth.GetLoginUrl();
            RedirectUrl(url);
            return null;
        }

    }
}
