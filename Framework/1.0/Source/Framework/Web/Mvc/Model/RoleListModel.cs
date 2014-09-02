using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework.Web.Mvc
{
    public class RoleListModel : FrameworkListModel
    {
        public RoleListModel()
            : base()
        {
            CurrentModel = new RoleModel();
            Selector = "Id,Name,Code,Parent(Id,Name,Code),Description";
        }
        public RoleModel CurrentModel { get; set; }
    }
}
