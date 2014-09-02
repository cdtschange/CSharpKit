using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp.Aspects;

namespace Cdts.Framework.Web.Mvc
{
    [Serializable]
    public abstract class FrameworkAuthorizationAttribute : OnMethodBoundaryAspect
    {
        string[] authorizationNames;
        public FrameworkAuthorizationAttribute()
            : this(new string[] { })
        {
        }
        public FrameworkAuthorizationAttribute(params string[] authorizationNames)
        {
            this.authorizationNames = authorizationNames;
        }
        protected abstract string LoginUrl { get; }
        protected IAuthorization Authorization
        {
            get
            {
                return AuthorizationFactory.Create();
            }
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            FrameworkController controller = args.Instance as FrameworkController;
            if (controller != null)
            {
                if (!controller.IsLogined)
                {
                    controller.NeedLogin(LoginUrl);
                    args.FlowBehavior = FlowBehavior.Return;
                }
                else
                {
                    if (authorizationNames.Length == 0)
                    {
                        return;
                    }
                    IUser currentUser = Framework.Context.CurrentUser;
                    var authorization = Authorization;
                    bool enable = false;
                    foreach (string name in authorizationNames)
                    {
                        if (authorization.Authorization(name, currentUser))
                        {
                            enable = true;
                            break;
                        }
                    }
                    if (!enable)
                    {
                        controller.NeedPermission();
                    }
                }
            }
        }
    }
}
