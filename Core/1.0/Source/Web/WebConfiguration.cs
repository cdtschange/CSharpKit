using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Cdts.Web
{
    public class WebConfigElement : System.Configuration.ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["Name"] as string;
            }
        }
        [ConfigurationProperty("Url")]
        public string Url
        {
            get
            {
                return this["Url"] as string;
            }
        }
        [ConfigurationProperty("LoginUrl")]
        public string LoginUrl
        {
            get
            {
                return this["LoginUrl"] as string;
            }
        }
        [ConfigurationProperty("LoginData")]
        public string LoginData
        {
            get
            {
                return this["LoginData"] as string;
            }
        }
        [ConfigurationProperty("LoginReferer")]
        public string LoginReferer
        {
            get
            {
                return this["LoginReferer"] as string;
            }
        }
        [ConfigurationProperty("LoginErrorRegex")]
        public string LoginErrorRegex
        {
            get
            {
                return this["LoginErrorRegex"] as string;
            }
        }
        [ConfigurationProperty("LoginSuccessRegex")]
        public string LoginSuccessRegex
        {
            get
            {
                return this["LoginSuccessRegex"] as string;
            }
        }
    }

    public class WebConfigElementCollection : ConfigurationElementCollection
    {
        public WebConfigElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as WebConfigElement;
            }

        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new WebConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WebConfigElement)(element)).Name;
        }
    }

    public class WebConfigSection : ConfigurationSection
    {

        public WebConfigSection()
        {

        }

        [ConfigurationProperty("WebCollection")]
        public WebConfigElementCollection AllValues
        {
            get
            {
                return this["WebCollection"] as WebConfigElementCollection;
            }
        }

        public static WebConfigSection GetConfigSection()
        {
            return ConfigurationManager.GetSection("WebSection") as WebConfigSection;
        }

    }
}
