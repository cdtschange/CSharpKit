using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cdts.Core;

namespace Cdts.Framework.Web.Mvc
{
    public class BusinessModuleModel : FrameworkModel
    {
        public BusinessModuleModel()
        {
        }
        public BusinessModuleModel(BusinessModuleModel bs)
        {
            if (bs == null)
            {
                return;
            }
            Id = bs.Id;
            Name = bs.Name;
            Code = bs.Code;
            Description = bs.Description;
            Invalid = bs.Invalid;
            Path = bs.Path;

            if (bs.Parent == null)
            {
                Parent = null;
            }
            else
            {
                Parent = new BusinessModuleModel(bs.Parent);
            }
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public BusinessModuleModel Parent { get; set; }
        public string Description { get; set; }
        public bool Invalid { get; set; }
        public string Path { get; set; }
        public SelectList ParentList { get; set; }
        public IBusinessModule Fill(IGetObject getObject, IBusinessModule bs)
        {
            bs.Id = Id;
            bs.Code = Code;
            bs.Name = Name;
            bs.Description = Description;
            bs.Invalid = Invalid;

            if (Parent != null)
            {
                bs.Parent = (IBusinessModule)getObject.GetObject("Id", Parent.Id, typeof(IBusinessModule));
            }
            else
            {
                bs.Parent = null;
            }
            return bs;
        }
    }
}
