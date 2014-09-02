using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Framework.ThdPartyAuth;
using System.Web.Mvc;

namespace Cdts.Framework.Web.Mvc.ThdPartyAuth
{
    /// <summary>
    /// LiveIdController
    /// </summary>
    public class LiveIdController : ThdPartyAuthController
    {
        public LiveIdController(IUserManager userManager, IThirdPartyAuthenticationManager thdPartyAuthManager)
            : base(userManager, thdPartyAuthManager)
        {
        }

        /// <summary>
        /// 获取OAuth
        /// </summary>
        /// <returns>返回OAuth</returns>
        public virtual LiveIdAuth GetOAuth()
        {
            return new LiveIdAuth(userManager, thdPartyAuthManager);
        }

        public override ActionResult Index()
        {
            LiveIdAuth oAuth = GetOAuth();
            ThdPartyUserInfo userInfo = oAuth.GetThdPartyUserInfo(GetRequestForm());
            if (userInfo != null) // 登录成功
            {
                return BindUser(oAuth, userInfo);
            }

            return RedirectToRoute("Default", new { controller = "Home", action = "Index" });
        }
        /// <summary>
        /// 登录
        /// </summary>
        public override ActionResult Login(string returnUrl)
        {
            base.Login(returnUrl);
            LiveIdAuth oAuth = GetOAuth();
            string url = oAuth.GetLoginUrl();
            RedirectUrl(url);
            return null;
        }
    }
}
