using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Collections.Specialized;
using System.Threading;

namespace Cdts.Web.Mvc
{
    [HandleError]
    public abstract class ControllerBase : Controller
    {
        private ModelBase modelBase = null;
        private Type modelType = null;
        private string viewName;

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        internal protected virtual string GetUserHostAddress()
        {
            return Request.UserHostAddress;

        }
        internal protected virtual HttpFileCollectionBase RequestFiles
        {
            get
            {
                return Request.Files;
            }
        }
        internal protected virtual string MapPath(string path)
        {
            return Server.MapPath(path);
        }
        internal protected virtual ModelBase CurrentModel
        {
            get
            {
                if (modelBase == null)
                {
                    if (modelType != null)
                        modelBase = Activator.CreateInstance(modelType) as ModelBase;
                    else
                        modelBase = NewModel;
                }
                return FillModel(modelBase);
            }
        }

        #region ViewData
        /// <summary>
        /// 获取ViewData内容
        /// </summary>
        internal protected virtual object GetViewData(string key)
        {
            return ViewData[key];
        }
        /// <summary>
        /// 设置ViewData内容
        /// </summary>
        internal protected virtual void SetViewData(string key, object value)
        {
            ViewData[key] = value;
        }
        #endregion

        #region RequestForm
        /// <summary>
        /// 获取RequestForm的内容
        /// </summary>
        /// <returns>返回内容</returns>
        internal protected virtual NameValueCollection GetRequestForm()
        {
            return Request.Form;
        }
        /// <summary>
        /// 获取RequestForm的内容
        /// </summary>
        /// <returns>返回内容</returns>
        internal protected virtual string GetRequestForm(string name)
        {
            return Request.Form[name];
        }
        #endregion

        #region QueryString
        /// <summary>
        /// 获取QueryString的内容
        /// </summary>
        /// <returns>返回内容</returns>
        internal protected virtual NameValueCollection GetQueryString()
        {
            return Request.QueryString;
        }
        /// <summary>
        /// 获取QueryString的内容
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回内容</returns>
        internal protected virtual string GetQueryString(string name)
        {
            return Request.QueryString[name];
        }
        #endregion

        #region Session
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="name">名称</param>
        internal protected virtual object GetSession(string name)
        {
            return Session[name];
        }
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>值</returns>
        internal protected virtual void SetSession(string name, object value)
        {
            Session[name] = value;
        }
        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="name">名称</param>
        internal protected virtual void RemoveSession(string name)
        {
            Session.Remove(name);
        }
        #endregion

        #region Cookies

        /// <summary>
        /// 获取Cookies
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>值</returns>
        internal protected virtual string GetCookies(string name)
        {
            if (Request.Cookies[name] == null)
                return null;
            string val = Request.Cookies[name].Value;
            val = UrlDecode(val);
            return val;
        }
        /// <summary>
        /// 设置Cookies
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="expireTime">过期时间</param>
        internal protected virtual void SetCookies(string name, DateTime expireTime)
        {
            if (Request.Cookies[name] == null)
            {
                return;
            }
            Response.Cookies[name].Expires = expireTime;
        }
        /// <summary>
        /// 设置Cookies
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        internal protected virtual void SetCookies(string name, string value)
        {
            value = UrlEncode(value);
            if (Request.Cookies[name] == null)
            {
                HttpCookie hc = new HttpCookie(name, value);
                Response.AppendCookie(hc);
            }
            else
            {
                Response.Cookies[name].Value = value;
            }
        }
        /// <summary>
        /// 设置Cookies
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="expireTime">过期时间</param>
        internal protected virtual void SetCookies(string name, string value, DateTime expireTime)
        {
            SetCookies(name, value);
            Response.Cookies[name].Expires = expireTime;
        }
        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="name"></param>
        internal protected void RemoveCookies(string name)
        {
            if (Request.Cookies[name] != null)
            {
                Response.Cookies[name].Expires = DateTime.Now.AddYears(-10);
            }
        }

        #endregion

        #region Url

        /// <summary>
        /// 当前Url
        /// </summary>
        internal protected virtual string CurrentUrl
        {
            get { return Request.Path; }
        }
        /// <summary>
        /// Url加密
        /// </summary>
        internal protected virtual string UrlEncode(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = "/";
            }
            return Server.UrlEncode(url);
        }
        /// <summary>
        /// Url解码
        /// </summary>
        internal protected virtual string UrlDecode(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "/";
            }
            return Server.UrlDecode(url);
        }
        /// <summary>
        /// 转向Url
        /// </summary>
        /// <param name="url">Url</param>
        internal protected virtual void RedirectUrl(string url)
        {
            Response.Redirect(url);
        }
        /// <summary>
        /// 转向返回Url
        /// </summary>
        /// <returns></returns>
        internal protected virtual ActionResult RedirectReturnUrl()
        {
            string url = GetCookies("ReturnUrl");
            if (!string.IsNullOrEmpty(url) && url != "/Member/Login")
            {
                RemoveCookies("ReturnUrl");
                return Redirect(url);
            }
            return Redirect("/");
        }

        #endregion

        #region Model

        internal protected virtual ModelBase NewModel
        {
            get
            {
                return new ModelBase();
            }
        }
        /// <summary>
        /// 填充模型
        /// </summary>
        internal protected virtual ModelBase FillModel(ModelBase model)
        {
            if (model == null)
            {
                model = CurrentModel;
            }
            model.ClientIp = GetUserHostAddress();
            model.IsAuthenticated = false;
            model.Language = GetCookies("Language");
            model.RequestBeginTime = MvcApplication.RequestBeginTime;
            model.RequestEndTime = DateTime.Now;
            return model;
        }
        #endregion
        #region Json
        internal protected virtual JsonResult SuccessJson(string message)
        {
            return Json(new { Message = message, Success = true });
        }
        internal protected virtual JsonResult ErrorJson(string message)
        {
            return Json(new { Message = message, Success = false });
        }
        #endregion
        #region View
        protected override PartialViewResult PartialView(string viewName, object model)
        {
            FillModel(model as ModelBase);
            return base.PartialView(viewName, model);
        }
        protected override ViewResult View(IView view, object model)
        {
            FillModel(model as ModelBase);
            return base.View(view, model);
        }
        protected override ViewResult View(string viewName, string masterName, object model)
        {
            FillModel(model as ModelBase);
            return base.View(viewName, masterName, model);
        }
        #endregion
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string lang = GetCookies("Language");
            if (!string.IsNullOrEmpty(lang))
            {
                SetCultureInfo(lang);
            }
            foreach (KeyValuePair<string, object> kv in filterContext.ActionParameters)
            {
                ModelBase mb = kv.Value as ModelBase;
                if (mb != null)
                {
                    modelBase = mb;
                    FillModel(modelBase);
                    break;
                    //ToDo:如果一个Action接收2个Model以上的参数，需要把break去掉
                }
            }
            object[] attributes = null;
            if (modelBase == null)
            {
                attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ModelTypeAttribute), false);
                if (attributes.Length >= 1)
                {
                    ModelTypeAttribute attribute = attributes[0] as ModelTypeAttribute;
                    if (attribute != null)
                    {
                        modelType = attribute.Type;
                    }
                }
            }
            attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(MapViewAttribute), false);
            if (attributes.Length >= 1)
            {
                MapViewAttribute attribute = attributes[0] as MapViewAttribute;
                if (attribute != null)
                {
                    viewName = attribute.ViewName;
                }
            }
            //FillModel(CurrentModel);
        }
        List<Type> exceptionTypes = new List<Type>();
        /// <summary>
        /// 预期异常类型列表
        /// </summary>
        protected virtual List<Type> ExceptionTypes
        {
            get
            {
                if (exceptionTypes.Count < 1)
                {
                    exceptionTypes.Add(typeof(ArgumentException));
                    exceptionTypes.Add(typeof(InvalidOperationException));
                }
                return exceptionTypes;
            }
        }
        /// <summary>
        /// 是否是预期异常
        /// </summary>
        /// <param name="ex">异常</param>
        protected virtual bool ExceptionNeedProcess(Exception ex)
        {
            foreach (Type type in ExceptionTypes)
            {
                if (type.IsAssignableFrom(ex.GetType()))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            if (modelBase != null)
            {
                if (ExceptionNeedProcess(filterContext.Exception))
                {
                    modelBase.Message = filterContext.Exception.Message;
                    modelBase.HasErrors = true;
                    filterContext.Result = IsAjaxRequest ? (ActionResult)ErrorJson(modelBase.Message) : string.IsNullOrEmpty(viewName) ? View(modelBase) : View(viewName, modelBase);
                    filterContext.ExceptionHandled = true;
                    return;
                }
            }

        }

        internal protected virtual void SetCultureInfo(string lang)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        internal protected virtual bool IsAjaxRequest
        {
            get
            {
                return Request.IsAjaxRequest();
            }
        }

        internal protected virtual ActionResult GetResult(Func<ActionResult> ajaxResult, Func<ActionResult> nonAjaxResult)
        {
            return IsAjaxRequest ? ajaxResult.Invoke() : nonAjaxResult.Invoke();
        }
        internal protected virtual ActionResult GetResult(bool? isAjaxRequest, Func<ActionResult> ajaxResult, Func<ActionResult> nonAjaxResult)
        {
            return isAjaxRequest ?? false ? ajaxResult.Invoke() : nonAjaxResult.Invoke();
        }
    }
}
