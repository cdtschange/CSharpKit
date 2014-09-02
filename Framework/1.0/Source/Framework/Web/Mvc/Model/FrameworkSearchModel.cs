using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework.Web.Mvc
{
    public class FrameworkSearchModel : FrameworkModel
    {
        public FrameworkSearchModel()
        {
            this.Page = 1;
            this.PageSize = 15;
        }
        public string Condition { get; set; }
        public string OrderBy { get; set; }
        public string Selector { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages
        {
            get
            {
                if (PageSize == 0)
                    return 0;
                return (int)Math.Ceiling(TotalRecords * 1.0 / PageSize);
            }
        }
    }
}
