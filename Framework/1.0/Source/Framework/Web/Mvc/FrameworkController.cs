using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Web.Mvc;
using Cdts.Core;

namespace Cdts.Framework.Web.Mvc
{
    public abstract class FrameworkController : ControllerBase
    {
        internal protected virtual IAuthorization AuthorizationManager
        {
            get
            {
                return AuthorizationFactory.Create();
            }
        }
        /// <summary>
        /// 设置当前用户
        /// </summary>
        /// <param name="user">用户</param>
        internal protected virtual void SetCurrentUser(IUser user)
        {
            Cdts.Framework.Context.CurrentUser = user;
        }
        public virtual void NeedLogin(string loginUrl)
        {
            RedirectUrl(string.Format(loginUrl, UrlEncode(CurrentUrl)));
        }
        public virtual void NeedPermission()
        {
            RedirectUrl(NoPermissionUrl);
        }
        protected virtual string NoPermissionUrl
        {
            get
            {
                return "/Member/NoPermission";
            }
        }
        internal protected virtual void CheckLoginStatus()
        {
            IUser user = null;
            string id = GetCookies("UserId");
            if (!string.IsNullOrEmpty(id))
            {
                //isLogined = false;
                //}
                Guid userId = Guid.Empty;
                if (isLogined == null)
                {
                    try
                    {
                        userId = new Guid(id);
                    }
                    catch
                    {
                        isLogined = false;
                    }
                }
                bool validated = false;
                if (isLogined == null)
                {
                    if (!bool.TryParse(GetCookies("EmailValidated"), out validated))
                    {
                        ClearUserCookies();
                        isLogined = false;
                    }
                }
                if (isLogined == null)
                {
                    if (validated)
                    {
                        isLogined = true;
                    }
                }
                DateTime registerTime = DateTime.Now;
                if (isLogined == null)
                {
                    if (!DateTime.TryParse(GetCookies("RegisterTime"), out registerTime))
                    {
                        ClearUserCookies();
                        isLogined = false;
                    }
                }
                if (isLogined == null)
                {
                    if (registerTime.AddDays(30) < DateTime.Now)
                    {
                        ClearUserCookies();
                        isLogined = false;
                    }
                }
                if (isLogined == null)
                {
                    isLogined = true;
                }
                if ((bool)isLogined)
                {
                    user = ModelFactory.Create<IUser>();
                    user.Id = userId;
                }
                SetCurrentUser(user);
            }

        }
        private bool? isLogined = null;
        /// <summary>
        /// 是否登录
        /// </summary>
        internal protected virtual bool IsLogined
        {
            get
            {
                if (isLogined == null)
                {
                    CheckLoginStatus();
                }
                if (isLogined == null)
                {
                    return false;
                }
                return (bool)isLogined;
            }
        }
        /// <summary>
        /// 创建用户Cookies
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="autoLogin">是否自动登录</param>
        /// <param name="rememberAccount">是否记住用户名</param>
        internal protected virtual void CreatUserCookies(IUser user, bool autoLogin, bool rememberAccount)
        {
            SetCookies(string.Format("{0}:{1}:FailTimes", user.Email, user.CurrentLoginIp), DateTime.Now.AddDays(-10));
            SetCookies(string.Format("{0}:{1}:IsLock", user.Email, user.CurrentLoginIp), DateTime.Now.AddDays(-10));
            SetActionsCookies(FrameworkMvcApplication.OnLoginSuccessedActions);

            if (autoLogin)
            {
                SetCookies("UserId", user.Id.ToString(), DateTime.Now.AddYears(1));
                SetCookies("UserName", user.Name, DateTime.Now.AddYears(1));
                SetCookies("EmailValidated", user.EmailValidated.ToString(), DateTime.Now.AddYears(1));
                SetCookies("Email", user.Email, DateTime.Now.AddYears(1));
                SetCookies("RegisterTime", user.RegisterTime.ToString("yyyy-MM-dd hh:mm:ss"), DateTime.Now.AddYears(1));
            }
            else
            {
                SetCookies("UserId", user.Id.ToString());
                if (rememberAccount)
                {
                    SetCookies("UserName", user.Name, DateTime.Now.AddYears(1));
                    SetCookies("Email", user.Email, DateTime.Now.AddYears(1));
                }
                else
                {
                    SetCookies("UserName", user.Name);
                }
                SetCookies("EmailValidated", user.EmailValidated.ToString());
                SetCookies("RegisterTime", user.RegisterTime.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }
        /// <summary>
        /// 清除用户Cookies
        /// </summary>
        internal protected virtual void ClearUserCookies()
        {
            RemoveCookies("UserName");
            RemoveCookies("UserId");
            RemoveCookies("EmailValidated");
            RemoveCookies("Email");
            RemoveCookies("RegisterTime");
            RemoveCookies("OnLoginSuccessedActions");
        }
        internal protected virtual void SetActionsCookies(IList<string> actions)
        {
            string values = actions.Connect(",");
            if (string.IsNullOrEmpty(values))
            {
                RemoveCookies("OnLoginSuccessedActions");
            }
            else
            {
                SetCookies("OnLoginSuccessedActions", values);
            }
        }
        internal protected virtual IList<string> GetActionsFromCookies()
        {
            string actions = GetCookies("OnLoginSuccessedActions");
            return actions.Split<string>(",");
        }
        internal protected virtual void RemoveActionFromCookies(string action)
        {
            var actions = GetActionsFromCookies();
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
            SetActionsCookies(actions);
        }
        protected override ModelBase NewModel
        {
            get
            {
                return new FrameworkModel();
            }
        }
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            string u = filterContext.HttpContext.Request.Params["u"];
            if (!string.IsNullOrEmpty(u))
            {
                SetCookies("u", u, DateTime.Now.AddMonths(6));
            }
            CheckLoginStatus();
            if (IsLogined)
            {
                if (filterContext.ActionDescriptor.ActionName.ToLower() != "onloginsuccessed")
                {
                    var actions = GetActionsFromCookies();
                    if (actions.Count > 0)
                    {
                        string[] a = actions[0].Split(":".ToCharArray());
                        filterContext.Result = Redirect(string.Format("/{0}/{1}?url={2}", a[0], a[1], UrlEncode(CurrentUrl))); //RedirectToAction(a[1], a[0], new { url = UrlEncode(CurrentUrl) });
                        //actions.Remove(actions[0]);
                        //SetActionsCookies(actions);
                        return;
                    }
                }
            }
            base.OnActionExecuting(filterContext);

        }
        /// <summary>
        /// 填充模型
        /// </summary>
        protected override ModelBase FillModel(ModelBase model)
        {
            model = base.FillModel(model);
            Guid id = Guid.Empty;
            model.UserName = GetCookies("UserName");
            if (IsLogined)
            {
                try
                {
                    id = new Guid(GetCookies("UserId"));
                    bool validated = false;
                    bool.TryParse(GetCookies("EmailValidated"), out validated);
                    model.Validated = validated;

                    model.IsAuthenticated = true;
                    DateTime registerTime;
                    DateTime.TryParse(GetCookies("RegisterTime"), out registerTime);
                    FrameworkModel frameworkModel = model as FrameworkModel;
                    if (frameworkModel != null)
                    {
                        frameworkModel.RegisterTime = registerTime;
                        string email = GetCookies("Email");
                        if (!string.IsNullOrEmpty(email))
                        {
                            frameworkModel.Email = email;
                        }
                        frameworkModel.UserId = id;
                    }
                    model.AnonymousId = GetGuestAnonymousId();
                    //RemoveCookies("GuestAnonymousId");
                }
                catch
                {
                    ClearUserCookies();
                    return model;
                }
            }
            else
            {
                Guid? anonymousId = GetGuestAnonymousId();
                if (anonymousId == null)
                {
                    anonymousId = Guid.NewGuid();
                    SetCookies("GuestAnonymousId", anonymousId.ToString(), DateTime.Now.AddYears(1));

                }
                model.AnonymousId = anonymousId;
            }
            return model;
        }
        private Guid? GetGuestAnonymousId()
        {
            string guestId = GetCookies("GuestAnonymousId");
            Guid? anonymousId = null;
            if (!string.IsNullOrEmpty(guestId))
            {
                try
                {
                    anonymousId = new Guid(guestId);
                }
                catch
                {
                    anonymousId = null;
                }
            }
            return anonymousId;
        }
        protected void RemoveGuestAnonymousId()
        {
            RemoveCookies("GuestAnonymousId");
        }
    }
}
