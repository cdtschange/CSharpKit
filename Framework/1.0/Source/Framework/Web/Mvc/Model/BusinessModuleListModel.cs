using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework.Web.Mvc
{
    public class BusinessModuleListModel : FrameworkListModel
    {
        public BusinessModuleListModel()
            : base()
        {
            CurrentModel = new BusinessModuleModel();
            Selector = "Id,Name,Code,Parent,Description";
        }
        public BusinessModuleModel CurrentModel { get; set; }
    }
}
