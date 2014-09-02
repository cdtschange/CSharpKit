using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Web.Mvc;

namespace Cdts.Framework.Web.Mvc
{
    public class FrameworkJsonModel : FrameworkModel, IJsonModel
    {
        public string Json { get; set; }
    }
}
