using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Cdts.Framework.Web.Mvc.ThdPartyAuth
{
    public class RenRenController : ThdPartyAuthController
    {
        //
        // GET: /RenRen/
        public RenRenController(IUserManager userManager, IThirdPartyAuthenticationManager thdPartyAuthManager)
            : base(userManager, thdPartyAuthManager)
        {
        }
        public override ActionResult Login(string returnUrl)
        {
            base.Login(returnUrl);
            throw new NotImplementedException();
        }

        public override ActionResult Index()
        {
            Response.Redirect("~/Member/Login");
            return null;
        }
    }
}
