using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Web.Mvc
{
    public class ModelTypeAttribute : Attribute
    {
        Type type;
        public ModelTypeAttribute(Type type)
        {
            this.type = type;
        }
        public Type Type
        {
            get
            {
                return type;
            }
        }
    }

    public class MapViewAttribute : Attribute
    {
        string viewName;
        public MapViewAttribute(string viewName)
        {
            this.viewName = viewName;
        }

        public string ViewName
        {
            get
            {
                return viewName;
            }
        }
    }
}
