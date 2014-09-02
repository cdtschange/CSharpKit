using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading;

namespace Cdts.Framework
{
    /// <summary>
    /// 上下文
    /// </summary>
    public static class Context
    {
        static IUserManager userManager = ManagerFactory.Create<IUserManager>();
        static Context()
        {
        }
        /// <summary>
        /// 当前用户
        /// </summary>
        public static IUser CurrentUser
        {
            get
            {
                IPrincipal curPrincipal = Thread.CurrentPrincipal;
                if (curPrincipal != null)
                {
                    Guid id;
                    try
                    {
                        id = new Guid(curPrincipal.Identity.Name);
                    }
                    catch
                    {
                        return null;
                    }
                    return userManager.LoadById(id);
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    Thread.CurrentPrincipal = null;
                    return;
                }
                IIdentity identity = new GenericIdentity(value.Id.ToString());
                IPrincipal principal = new GenericPrincipal(identity, new string[] { });//, value.PermissionString.ToArray());
                Thread.CurrentPrincipal = principal;
            }
        }
    }
}
