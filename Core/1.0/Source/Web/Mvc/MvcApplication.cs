using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Cdts.Core.Unity;
using Cdts.Core;
using System.Web.Compilation;
using System.Configuration;
using System.Reflection;
using System.IO;

namespace Cdts.Web.Mvc
{
    public class MvcApplication : HttpApplication
    {
        [ThreadStatic]
        private static DateTime requestBeginTime;
        public static DateTime RequestBeginTime
        {
            get
            {
                return requestBeginTime;
            }
            set
            {
                requestBeginTime = value;
            }
        }
        [ThreadStatic]
        private static DateTime requestEndTime;
        public static DateTime RequestEndTime
        {
            get
            {
                return requestEndTime;
            }
            set
            {
                requestEndTime = value;
            }
        }

        public static event EventHandler OnLoginSuccessed;

        public override void Init()
        {
            base.Init();
            BeginRequest += HandleBeginRequest;
            EndRequest += HandleEndRequest;

        }

        void HandleBeginRequest(object sender, EventArgs e)
        {
            RequestBeginTime = DateTime.Now;
            OnBeginRequest(new HttpContextWrapper(Context));
        }
        protected virtual void OnBeginRequest(HttpContextBase context)
        {
        }
        void HandleEndRequest(object sender, EventArgs e)
        {
            PerRequestLifetimeManager.DisposeValues();
            OnEndRequest(new HttpContextWrapper(Context));
            //Context.Response.Write((DateTime.Now - RequestBeginTime).TotalMilliseconds);
            RequestEndTime = DateTime.Now;
        }
        protected virtual void OnEndRequest(HttpContextBase context)
        {
        }
        public void Application_Start()
        {
            KnownTypeList.KnownTypes.Clear();
            var list = BuildManager.GetReferencedAssemblies();
            List<string> assemblyNames = new List<string>();
            string searchAssemblies = ConfigurationManager.AppSettings["SearchAssemblies"];
            if (!string.IsNullOrEmpty(searchAssemblies))
            {
                assemblyNames.AddRange(searchAssemblies.Split(",".ToCharArray()));
            }
            foreach (Assembly item in list)
            {
                string file = Path.GetFileNameWithoutExtension(item.Location);
                bool searchIt = false;
                foreach (string name in assemblyNames)
                {
                    string name1 = name.Trim();
                    if (name1.EndsWith("*") && file.StartsWith(name1.Substring(0, name1.Length - 1)))
                    {
                        searchIt = true;
                        break;
                    }
                    else if (name1 == file)
                    {
                        searchIt = true;
                        break;
                    }
                }
                if (searchIt)
                {
                    Type[] types;
                    try
                    {
                        types = item.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        types = ex.Types;
                    }
                    KnownTypeList.AllTypes.AddRange(types.Where(t => !(t.IsInterface || t.IsAbstract || t.IsNested || t.IsEnum || t.IsCOMObject || t.IsImport) & t.IsPublic).ToList());
                }
            }
            OnStart();
        }
        protected virtual void OnStart()
        {
        }
        public void Application_End()
        {
            OnEnd();
        }
        protected virtual void OnEnd()
        {
        }
    }
}
