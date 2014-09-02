using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Compilation;
using System.Threading;
using Newtonsoft.Json;
using System.Web.Routing;
using System.Linq.Expressions;

namespace System.Web.Mvc
{
    public static class Extensions
    {
        public static string Resource(this HtmlHelper htmlhelper, string expression, params object[] args)
        {
            string virtualPath = GetVirtualPath(htmlhelper);

            return GetResourceString(htmlhelper.ViewContext.HttpContext, expression, virtualPath, args);
        }

        public static string Resource(this Controller controller, string expression, params object[] args)
        {
            return GetResourceString(controller.HttpContext, expression, "~/", args);
        }

        private static string GetResourceString(HttpContextBase httpContext, string expression, string virtualPath, object[] args)
        {
            ExpressionBuilderContext context = new ExpressionBuilderContext(virtualPath);
            ResourceExpressionBuilder builder = new ResourceExpressionBuilder();
            string msg = expression;
            try
            {
                ResourceExpressionFields fields = (ResourceExpressionFields)builder.ParseExpression(expression, typeof(string), context);

                if (!string.IsNullOrEmpty(fields.ClassKey))
                    msg = string.Format((string)httpContext.GetGlobalResourceObject(fields.ClassKey, fields.ResourceKey, Thread.CurrentThread.CurrentUICulture), args);

                msg = string.Format((string)httpContext.GetLocalResourceObject(virtualPath, fields.ResourceKey, Thread.CurrentThread.CurrentUICulture), args);
            }
            catch
            {
            }
            return msg;
        }

        private static string GetVirtualPath(HtmlHelper htmlhelper)
        {
            WebFormView view = htmlhelper.ViewContext.View as WebFormView;

            if (view != null)
                return view.ViewPath;
            return null;
        }

        public static string ToJson(this FormCollection fc, params string[] setProperties)
        {
            List<string> keys = new List<string>(fc.AllKeys);
            Dictionary<string, object> jo = ToObject(fc, "", keys, setProperties);
            string json = JsonConvert.SerializeObject(jo);
            return json;
        }

        private static Dictionary<string, object> ToObject(FormCollection fc, string property, List<string> keys, params string[] setProperties)
        {
            if (keys == null || keys.Count < 1)
            {
                return null;
            }
            Dictionary<string, object> values = new Dictionary<string, object>();
            var list = keys.Where(k => !k.Contains(".")).OrderBy(k => k).ToList();
            foreach (string key in list)
            {
                string valueKey = key;
                if (!string.IsNullOrEmpty(property))
                {
                    valueKey = property + "." + key;
                }
                values.Add(key, fc[valueKey]);
            }
            var navs = keys.Where(k => k.Contains(".")).OrderBy(k => k).Select(k => k.Substring(0, k.IndexOf("."))).Distinct().ToList();
            foreach (string nav in navs)
            {
                if (setProperties.Contains(nav))
                {
                    if (!values.ContainsKey(nav))
                    {
                        values.Add(nav, new List<Dictionary<string, object>>());
                    }
                    List<Dictionary<string, object>> set = (List<Dictionary<string, object>>)values[nav];
                    var setPros = keys.Where(k => k.StartsWith(nav + ".")).Select(k => k.Substring((nav + ".").Length)).ToList();
                    foreach (string setPro in setPros)
                    {
                        string setProValue = fc[nav + "." + setPro];
                        string[] setProValues = setProValue.Split(",".ToCharArray());
                        for (int i = 0, l = setProValues.Length; i < l; i++)
                        {
                            if (set.Count < i + 1)
                            {
                                set.Add(new Dictionary<string, object>());
                            }
                            set[i].Add(setPro, setProValues[i]);
                        }
                    }
                }
                else
                {
                    string navKey = nav;
                    if (!string.IsNullOrEmpty(property))
                    {
                        navKey = property + "." + nav;
                    }
                    values.Add(nav, ToObject(fc, navKey, keys.Where(k => k.StartsWith(nav + ".")).Select(k => k.Substring((nav + ".").Length)).ToList()));
                }
            }
            return values;
        }

    }

    public static class NewLabelExtensions
    {
        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }
            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            //tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}
