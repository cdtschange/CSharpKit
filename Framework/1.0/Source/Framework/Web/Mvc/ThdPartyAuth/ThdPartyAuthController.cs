using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cdts.Framework.ThdPartyAuth;

namespace Cdts.Framework.Web.Mvc.ThdPartyAuth
{
    /// <summary>
    /// 第三方登录Controller
    /// </summary>
    public abstract class ThdPartyAuthController : FrameworkController
    {
        protected IUserManager userManager;
        protected IThirdPartyAuthenticationManager thdPartyAuthManager;

        public ThdPartyAuthController(IUserManager userManager, IThirdPartyAuthenticationManager thdPartyAuthManager)
        {
            this.userManager = userManager;
            this.thdPartyAuthManager = thdPartyAuthManager;
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public abstract ActionResult Index();
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Login(string returnUrl)
        {
            SetCookies("ReturnUrl", returnUrl);
            return null;
        }
        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="oAuth">oAuth</param>
        /// <param name="userInfo">从第三方获取的用户信息</param>
        /// <returns></returns>
        public virtual ActionResult BindUser(AbstractThdPartyAuth oAuth, ThdPartyUserInfo userInfo)
        {
            if (oAuth.IsFirstLogin(userInfo.Id)) // 第一次登录
            {
                SetCookies("ThdPartyUserInfo", userInfo.ToString());
                return RedirectToAction("BindUser", "Member");
            }
            IUser user = oAuth.GetAssociatedUser(userInfo.Id);
            if (user != null)
            {
                CreatUserCookies(user, false, false);
                return RedirectReturnUrl();
            }
            return RedirectToRoute("Default");
        }
    }
}
