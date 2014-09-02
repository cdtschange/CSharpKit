using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Web.Mvc;

namespace Cdts.Framework.Web.Mvc
{
    public class FrameworkMvcApplication : MvcApplication
    {
        public static List<string> OnLoginSuccessedActions { get; set; }
    }

    public class LoginSuccessedEventArgs : EventArgs
    {
        public LoginSuccessedEventArgs(IUser user)
        {
            User = user;
        }
        public IUser User
        {
            get;
            private set;
        }
    }
}
