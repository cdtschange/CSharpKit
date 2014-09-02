using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Framework.ThdPartyAuth;
using System.Web.Mvc;

namespace Cdts.Framework.Web.Mvc.ThdPartyAuth
{
    /// <summary>
    /// 淘宝Controller
    /// </summary>
    public class TaobaoController : ThdPartyAuthController
    {
        public TaobaoController(IUserManager userManager, IThirdPartyAuthenticationManager thdPartyAuthManager)
            : base(userManager, thdPartyAuthManager)
        {

        }

        /// <summary>
        /// 获取OAuth
        /// </summary>
        /// <returns>返回OAuth</returns>
        public virtual TaobaoAuth GetOAuth()
        {
            return new TaobaoAuth(userManager, thdPartyAuthManager);
        }

        public override ActionResult Index()
        {
            if (GetQueryString("sign") != null) // 登录成功
            {
                TaobaoAuth oAuth = GetOAuth();
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
            TaobaoAuth oAuth = GetOAuth();
            string url = oAuth.GetLoginUrl();
            RedirectUrl(url);
            return null;
        }
    }
}
