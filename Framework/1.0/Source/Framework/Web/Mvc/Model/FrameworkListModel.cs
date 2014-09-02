using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework.Web.Mvc
{
    public class FrameworkListModel : FrameworkSearchModel
    {
        public FrameworkListModel()
            : base()
        {
            this.ModelList = new List<object>();
            this.SelectedIdList = new List<object>();
        }
        public List<object> ModelList { get; set; }
        public List<object> SelectedIdList { get; set; }
    }
}
